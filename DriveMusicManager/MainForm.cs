using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DriveMusicManager
{
    public partial class MainForm : Form
    {
        private const long TotalAvailableStorageInMegaBytes = 12500;

        private MusicManager musicManager;

        private CancellationTokenSource cancellationTokenSource;
        private Progress<float> progress;
        private bool processingStarted = false;

        public MainForm()
        {
            InitializeComponent();

            availableSpaceStatusLabel.Text = "";
            albumsGroupBox.Enabled = false;

            progress = new Progress<float>(value => progressBar.Value = (int)(value * 100));
        }

        private void BrowseDestinationPath_Click(object sender, EventArgs e)
        {
            string path = BrowseDirectory();
            if (path != null)
            {
                destinationPathTextBox.Text = path;
            }
        }

        private void LoadAlbums_Click(object sender, EventArgs e)
        {
            string destinationPath = destinationPathTextBox.Text;
            musicManager = new MusicManager(destinationPath);

            RefreshAlbums();

            albumsGroupBox.Enabled = true;

            UpdateStorageInfo();
        }

        private void BrowseSourcePath_Click(object sender, EventArgs e)
        {
            string path = BrowseDirectory();
            if (path != null)
            {
                sourcePathTextBox.Text = path;
            }
        }

        private void AddNewAlbum_Click(object sender, EventArgs e)
        {
            musicManager.AddAlbum("New album");

            RefreshAlbums();
        }

        private void RemoveAlbum_Click(object sender, EventArgs e)
        {
            RemoveAlbum();
        }

        private async void StartStopProcessing_Click(object sender, EventArgs e)
        {
            if (processingStarted)
            {
                CancelAddingAlbum();
            }
            else
            {
                processingStarted = true;
                startStopProcessing.Text = "Cancel";

                await AddToAlbum();

                startStopProcessing.Text = "Add to album";
                processingStarted = false;
            }
        }

        private void AlbumItemsListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (albumItemsListView.SelectedItems.Count == 1)
            {
                albumItemsListView.SelectedItems[0].BeginEdit();
            }
        }

        private void AlbumItemsListView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.F2 && albumItemsListView.SelectedItems.Count == 1)
            {
                albumItemsListView.SelectedItems[0].BeginEdit();
            }
            else if (e.KeyData == Keys.Delete && albumItemsListView.SelectedItems.Count == 1)
            {
                RemoveAlbum();
            }
        }

        private void AlbumItemsListView_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e.Label))
            {
                e.CancelEdit = true;
                return;
            }

            var albumId = (string)albumItemsListView.Items[e.Item].Tag;

            musicManager.RenameAlbum(albumId, e.Label);

            RefreshAlbums();
        }

        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            sourcePathTextBox.Text = ((string[])e.Data.GetData(DataFormats.FileDrop)).FirstOrDefault();
        }

        private void RemoveAlbum()
        {
            AlbumInfo albumInfo = GetSelectedAlbumInfo();

            if (string.IsNullOrEmpty(albumInfo?.AlbumId))
            {
                MessageBox.Show("Please select an album to remove.", "Album not selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show($"Are you sure you want to remove album \"{albumInfo.AlbumName}\"?{Environment.NewLine}All files in that album will be deleted.", "Remove album", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (result != DialogResult.Yes)
            {
                return;
            }

            musicManager.RemoveAlbum(albumInfo.AlbumId);

            RefreshAlbums();
        }

        private async Task AddToAlbum()
        {
            string sourcePath = sourcePathTextBox.Text;
            string albumId = GetSelectedAlbumId();

            if (string.IsNullOrWhiteSpace(albumId))
            {
                MessageBox.Show("Please select album to import songs to.", "Album not selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            cancellationTokenSource = new CancellationTokenSource();

            await musicManager.AddToAlbum(sourcePath, albumId, cancellationTokenSource.Token, progress).ConfigureAwait(false);

            UpdateStorageInfo();

            MessageBox.Show("The selected folder was successfully imported.", "Folder imported", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void CancelAddingAlbum()
        {
            cancellationTokenSource.Cancel();
        }

        private void RefreshAlbums()
        {
            albumItemsListView.Items.Clear();
            albumNameComboBox.Items.Clear();

            foreach (var albumInfo in musicManager.Albums)
            {
                albumItemsListView.Items.Add(new ListViewItem()
                {
                    Text = albumInfo.AlbumName,
                    Tag = albumInfo.AlbumId,
                });

                if (albumInfo.AlbumType == Constants.AlbumType.MediaFolder)
                {
                    albumNameComboBox.Items.Add(new ComboBoxItem()
                    {
                        AlbumName = albumInfo.AlbumName,
                        AlbumId = albumInfo.AlbumId,
                    });
                }
            }

            albumNameComboBox.SelectedIndex = 0;
        }

        private AlbumInfo GetSelectedAlbumInfo()
        {
            ListViewItem selectedItem = null;

            if (albumItemsListView.SelectedItems.Count == 1)
            {
                selectedItem = albumItemsListView.SelectedItems[0];
            } 

            var selectedAlbumInfo = musicManager.Albums.Where(x => x.AlbumId == (string)selectedItem?.Tag).SingleOrDefault();

            return selectedAlbumInfo;
        }

        private string GetSelectedAlbumId()
        {
            return ((ComboBoxItem)albumNameComboBox.SelectedItem)?.AlbumId;
        }

        private void UpdateStorageInfo()
        {
            long usedStorage = musicManager.GetUsedStorage();
            float usedStorageInMegaBytes = usedStorage / (1024 * 1024);

            float usedStoragePercentage = usedStorageInMegaBytes / TotalAvailableStorageInMegaBytes * 100.0f;

            availableSpaceStatusLabel.Text = $@"{usedStorageInMegaBytes} MB / {TotalAvailableStorageInMegaBytes} MB ({usedStoragePercentage}%)";
        }

        private string BrowseDirectory()
        {
            using (var folderBrowserDialog = new FolderBrowserDialog())
            {
                DialogResult result = folderBrowserDialog.ShowDialog();

                return result == DialogResult.OK ? folderBrowserDialog.SelectedPath : null;
            }
        }

        private class ComboBoxItem
        {
            public string AlbumName { get; set; }

            public string AlbumId { get; set; }

            public override string ToString()
            {
                return AlbumName;
            }
        }
    }
}