using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DriveMusicManager
{
    public class MusicManager
    {
        private readonly Dictionary<string, string> supportedFormats;

        private string targetDirectory;
        private string targetMusicDirectory;
        private string data1FilePath;
        private List<AlbumInfo> albums;

        public MusicManager(string targetDirectory)
        {
            supportedFormats = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { Constants.Extensions.Br3, Constants.Extensions.Mp4 },
                { Constants.Extensions.Br4, Constants.Extensions.Mp3 },
                { Constants.Extensions.Br5, Constants.Extensions.Wma },
                { Constants.Extensions.Mp4, Constants.Extensions.Br3 },
                { Constants.Extensions.Mp3, Constants.Extensions.Br4 },
                { Constants.Extensions.Wma, Constants.Extensions.Br5 },
            };

            InitDirectoryStructure(targetDirectory);

            albums = GetAlbumInfos();
        }

        public IEnumerable<AlbumInfo> Albums { get { return albums; } }

        public async Task AddToAlbum(string sourcePath, string albumId, CancellationToken cancellationToken, IProgress<float> progress)
        {
            var filePathsToProcess = new List<string>();
            string sourceDirectory;

            if (File.Exists(sourcePath))
            {
                filePathsToProcess.Add(sourcePath);

                sourceDirectory = Path.GetDirectoryName(sourcePath);

                UpdateDirectoryName(ref sourceDirectory);
            }
            else
            {
                UpdateDirectoryName(ref sourcePath);

                sourceDirectory = sourcePath;

                filePathsToProcess = GetFilesForProcessing(sourcePath);
            }

            string targetAlbumDirectory = GetTargetAlbumDirectory(albumId);

            int filesToProcessCount = filePathsToProcess.Count();
            int processedFilesCount = 0;

            foreach (var currentFilePath in filePathsToProcess)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }

                string newFilePath = GetOutputFilePath(sourceDirectory, targetAlbumDirectory, currentFilePath);

                await ConvertFile(currentFilePath, newFilePath);

                processedFilesCount++;

                if (progress != null)
                {
                    progress.Report((float)processedFilesCount / filesToProcessCount);
                }
            }
        }

        public void AddAlbum(string albumName)
        {
            UpdateAlbumInfo(albumName, null);
        }

        public void RenameAlbum(string albumId, string albumName)
        {
            UpdateAlbumInfo(albumName, albumId);
        }

        public void RemoveAlbum(string albumId)
        {
            int albumIndex = albums.FindIndex(x => x.AlbumId == albumId);

            albums.RemoveAt(albumIndex);

            string pathToBeDeleted = targetMusicDirectory + albumId;

            UpdateDirectoryName(ref pathToBeDeleted);

            Directory.Delete(pathToBeDeleted, true);

            for (int i = albumIndex; i < albums.Count; i++)
            {
                var currentAlbum = albums[i];

                string idAsString = currentAlbum.AlbumId.Substring(Constants.Ripped.Length);
                int actualAlbumId = int.Parse(idAsString);

                string pathToBeRenamedFrom = targetMusicDirectory + Constants.Ripped + actualAlbumId.ToString(CultureInfo.InvariantCulture);

                actualAlbumId--;

                currentAlbum.AlbumId = Constants.Ripped + actualAlbumId.ToString(CultureInfo.InvariantCulture);

                string pathToBeRenamedTo = targetMusicDirectory + currentAlbum.AlbumId;

                UpdateDirectoryName(ref pathToBeRenamedFrom);
                UpdateDirectoryName(ref pathToBeRenamedTo);

                Directory.Move(pathToBeRenamedFrom, pathToBeRenamedTo);
            }

            UpdateDataFile();
        }

        public long GetUsedStorage()
        {
            long usedStorage = CalculateFolderSize(targetDirectory);

            return usedStorage;
        }

        private void InitDirectoryStructure(string targetDirectory)
        {
            UpdateDirectoryName(ref targetDirectory);
            this.targetDirectory = targetDirectory;

            targetMusicDirectory = targetDirectory + Constants.MusicDirectory;

            Directory.CreateDirectory(targetMusicDirectory);

            data1FilePath = targetMusicDirectory + Constants.Data1File;

            CreateBackupVersionFile(targetDirectory);
        }

        private void CreateBackupVersionFile(string targetDirectory)
        {
            File.WriteAllText(targetDirectory + Constants.BMWDataDirectory + Constants.BackupFileName, Constants.BackupVersion);
        }

        private List<AlbumInfo> GetAlbumInfos()
        {
            var albums = new List<AlbumInfo>();

            if (!File.Exists(data1FilePath))
            {
                return albums;
            }

            var data1 = File.ReadAllLines(data1FilePath);            

            foreach (var dataRow in data1)
            {
                var rowItems = dataRow.Split(Constants.DataRowSeparator);
                string albumId = rowItems[0].Trim('/');

                string custom = rowItems.Skip(4).Aggregate((x, y) => x + Constants.DataRowSeparator + y);

                var albumInfo = new AlbumInfo()
                {
                    AlbumName = rowItems[1],
                    AlbumId = albumId,
                    Code1 = rowItems[2],
                    AlbumType = rowItems[3],
                    Custom = custom,
                };

                albums.Add(albumInfo);
            }

            return albums;
        }

        private string GetTargetAlbumDirectory(string albumId)
        {
            string targetAlbumDirectory = targetMusicDirectory + albumId;

            UpdateDirectoryName(ref targetAlbumDirectory);

            return targetAlbumDirectory;
        }

        private void UpdateAlbumInfo(string albumName, string albumId)
        {
            if (string.IsNullOrWhiteSpace(albumName))
            {
                return;
            }

            var foundAlbumInfo = albums.Where(x => x.AlbumId == albumId).SingleOrDefault();

            string targetAlbumDirectory = "";

            if (foundAlbumInfo != null)
            {
                foundAlbumInfo.AlbumName = albumName;

                targetAlbumDirectory = GetTargetAlbumDirectory(foundAlbumInfo.AlbumId);
            }
            else
            {
                string nextAlbumDirectory = GetNextAlbumDirectory(targetMusicDirectory);

                var albumInfo = new AlbumInfo()
                {
                    AlbumName = albumName,
                    AlbumId = nextAlbumDirectory,
                    Code1 = Constants.Code1,
                    AlbumType = Constants.AlbumType.MediaFolder,
                };

                albums.Add(albumInfo);

                targetAlbumDirectory = GetTargetAlbumDirectory(nextAlbumDirectory);
            }

            Directory.CreateDirectory(targetAlbumDirectory);

            UpdateDataFile();
        }

        private string GetNextAlbumDirectory(string filePath)
        {
            int i = 0;
            bool directoryExist;
            string directoryName;

            do
            {
                i++;

                directoryName = Constants.Ripped + i.ToString(CultureInfo.InvariantCulture);
                string directoryFullPath = filePath + directoryName;

                directoryExist = Directory.Exists(directoryFullPath);
            }
            while (directoryExist);

            return directoryName;
        }

        private void UpdateDataFile()
        {
            string data1Contents = "";

            foreach (var albumInfo in albums)
            {
                data1Contents += FormattableString.Invariant($"/{albumInfo.AlbumId}/\t{albumInfo.AlbumName}\t{albumInfo.Code1}\t{albumInfo.AlbumType}\t{albumInfo.Custom}\n");
            }

            File.WriteAllText(data1FilePath, data1Contents);
        }

        private List<string> GetFilesForProcessing(string sourceDirectory)
        {
            var fileNames = new List<string>();

            fileNames.AddRange(Directory.GetFiles(sourceDirectory, $"*{Constants.Extensions.Br3}", SearchOption.AllDirectories));
            fileNames.AddRange(Directory.GetFiles(sourceDirectory, $"*{Constants.Extensions.Br4}", SearchOption.AllDirectories));
            fileNames.AddRange(Directory.GetFiles(sourceDirectory, $"*{Constants.Extensions.Br5}", SearchOption.AllDirectories));
            fileNames.AddRange(Directory.GetFiles(sourceDirectory, $"*{Constants.Extensions.Mp4}", SearchOption.AllDirectories));
            fileNames.AddRange(Directory.GetFiles(sourceDirectory, $"*{Constants.Extensions.Mp3}", SearchOption.AllDirectories));
            fileNames.AddRange(Directory.GetFiles(sourceDirectory, $"*{Constants.Extensions.Wma}", SearchOption.AllDirectories));

            return fileNames;
        }

        private string GetOutputFilePath(string sourceDirectory, string targetAlbumDirectory, string originalFileName)
        {
            string filePath = Path.GetDirectoryName(originalFileName);
            string fileName = Path.GetFileNameWithoutExtension(originalFileName);
            string extension = Path.GetExtension(originalFileName);

            string newExtension = supportedFormats[extension];

            string newFileName = fileName + newExtension;

            UpdateDirectoryName(ref filePath);

            if (filePath.IndexOf(sourceDirectory) != 0)
            {
                throw new ArgumentException(originalFileName, nameof(originalFileName));
            }

            string innerFilePath = filePath.Substring(sourceDirectory.Length);

            string newFilePath = targetAlbumDirectory + innerFilePath;

            UpdateDirectoryName(ref newFilePath);

            Directory.CreateDirectory(newFilePath);

            newFilePath += newFileName;

            return newFilePath;
        }

        private void UpdateDirectoryName(ref string directoryName)
        {
            if (!directoryName.EndsWith("" + Path.DirectorySeparatorChar))
            {
                directoryName += Path.DirectorySeparatorChar;
            }
        }

        private async Task ConvertFile(string inputFileName, string outputFilePath)
        {
            var fileBytes = await ReadAllBytesAsync(inputFileName);

            for (int i = 0; i < fileBytes.Length; i++)
            {
                fileBytes[i] = (byte)~fileBytes[i];
            }

            await WriteAllBytesAsync(outputFilePath, fileBytes);
        }

        private async Task<byte[]> ReadAllBytesAsync(string path)
        {
            byte[] result;
            using (FileStream stream = File.Open(path, FileMode.Open))
            {
                result = new byte[stream.Length];
                await stream.ReadAsync(result, 0, (int)stream.Length);
                return result;
            }
        }

        private async Task WriteAllBytesAsync(string path, byte[] bytes)
        {
            using (var fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None, bytes.Length, true))
            {
                await fs.WriteAsync(bytes, 0, bytes.Length);
            }
        }

        private long CalculateFolderSize(string folderPath)
        {
            long folderSize = 0;

            if (!Directory.Exists(folderPath))
            {
                return folderSize;
            }
            else
            {
                foreach (string file in Directory.GetFiles(folderPath))
                {
                    if (File.Exists(file))
                    {
                        var finfo = new FileInfo(file);
                        folderSize += finfo.Length;
                    }
                }

                foreach (string dir in Directory.GetDirectories(folderPath))
                {
                    folderSize += CalculateFolderSize(dir);
                }
            }

            return folderSize;
        }
    }
}