namespace DriveMusicManager
{
    internal class Constants
    {
        public const string BMWDataDirectory = @"BMWData\";
        public const string MusicDirectory = @"BMWData\Music\";
        public const string BackupFileName = "BMWBackup.ver";
        public const string BackupVersion = "V1\n";
        public const string Ripped = "Ripped";
        public const string Data1File = "data_1";
        public const string Code1 = "1258287780";
        public const char DataRowSeparator = '\t';

        public class AlbumType
        {
            public const string AudioCD = "8";
            public const string MediaFolder = "2";
        }

        public class Extensions
        {
            public const string Br3 = ".BR3";
            public const string Br4 = ".BR4";
            public const string Br5 = ".BR5";

            public const string Mp4 = ".mp4";
            public const string Mp3 = ".mp3";
            public const string Wma = ".wma";
        }
    }
}
