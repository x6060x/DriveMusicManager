namespace DriveMusicManager
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.browseSourcePath = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.sourcePathTextBox = new System.Windows.Forms.TextBox();
            this.destinationPathTextBox = new System.Windows.Forms.TextBox();
            this.browseDestinationPath = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.startStopProcessing = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.availableSpaceStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.albumsGroupBox = new System.Windows.Forms.GroupBox();
            this.albumItemsListView = new System.Windows.Forms.ListView();
            this.addNewAlbum = new System.Windows.Forms.Button();
            this.removeAlbum = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.albumNameComboBox = new System.Windows.Forms.ComboBox();
            this.loadAlbums = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            this.albumsGroupBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // browseSourcePath
            // 
            this.browseSourcePath.Location = new System.Drawing.Point(433, 19);
            this.browseSourcePath.Name = "browseSourcePath";
            this.browseSourcePath.Size = new System.Drawing.Size(85, 23);
            this.browseSourcePath.TabIndex = 7;
            this.browseSourcePath.Text = "Browse";
            this.browseSourcePath.UseVisualStyleBackColor = true;
            this.browseSourcePath.Click += new System.EventHandler(this.BrowseSourcePath_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Source:";
            // 
            // sourcePathTextBox
            // 
            this.sourcePathTextBox.Location = new System.Drawing.Point(80, 21);
            this.sourcePathTextBox.Name = "sourcePathTextBox";
            this.sourcePathTextBox.Size = new System.Drawing.Size(347, 20);
            this.sourcePathTextBox.TabIndex = 6;
            // 
            // destinationPathTextBox
            // 
            this.destinationPathTextBox.Location = new System.Drawing.Point(81, 14);
            this.destinationPathTextBox.Name = "destinationPathTextBox";
            this.destinationPathTextBox.Size = new System.Drawing.Size(305, 20);
            this.destinationPathTextBox.TabIndex = 1;
            // 
            // browseDestinationPath
            // 
            this.browseDestinationPath.Location = new System.Drawing.Point(392, 12);
            this.browseDestinationPath.Name = "browseDestinationPath";
            this.browseDestinationPath.Size = new System.Drawing.Size(75, 23);
            this.browseDestinationPath.TabIndex = 2;
            this.browseDestinationPath.Text = "Browse";
            this.browseDestinationPath.UseVisualStyleBackColor = true;
            this.browseDestinationPath.Click += new System.EventHandler(this.BrowseDestinationPath_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Destination:";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(6, 77);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(512, 23);
            this.progressBar.Step = 1;
            this.progressBar.TabIndex = 6;
            // 
            // startStopProcessing
            // 
            this.startStopProcessing.Location = new System.Drawing.Point(433, 48);
            this.startStopProcessing.Name = "startStopProcessing";
            this.startStopProcessing.Size = new System.Drawing.Size(85, 23);
            this.startStopProcessing.TabIndex = 9;
            this.startStopProcessing.Text = "Add to album";
            this.startStopProcessing.UseVisualStyleBackColor = true;
            this.startStopProcessing.Click += new System.EventHandler(this.StartStopProcessing_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Album name:";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.availableSpaceStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 314);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(560, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 10;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // availableSpaceStatusLabel
            // 
            this.availableSpaceStatusLabel.Name = "availableSpaceStatusLabel";
            this.availableSpaceStatusLabel.Size = new System.Drawing.Size(144, 17);
            this.availableSpaceStatusLabel.Text = "availableSpaceStatusLabel";
            // 
            // albumsGroupBox
            // 
            this.albumsGroupBox.Controls.Add(this.albumItemsListView);
            this.albumsGroupBox.Controls.Add(this.addNewAlbum);
            this.albumsGroupBox.Controls.Add(this.removeAlbum);
            this.albumsGroupBox.Controls.Add(this.groupBox1);
            this.albumsGroupBox.Location = new System.Drawing.Point(12, 41);
            this.albumsGroupBox.Name = "albumsGroupBox";
            this.albumsGroupBox.Size = new System.Drawing.Size(536, 261);
            this.albumsGroupBox.TabIndex = 11;
            this.albumsGroupBox.TabStop = false;
            this.albumsGroupBox.Text = "Albums";
            // 
            // albumItemsListView
            // 
            this.albumItemsListView.LabelEdit = true;
            this.albumItemsListView.Location = new System.Drawing.Point(6, 19);
            this.albumItemsListView.Name = "albumItemsListView";
            this.albumItemsListView.Size = new System.Drawing.Size(443, 121);
            this.albumItemsListView.TabIndex = 10;
            this.albumItemsListView.UseCompatibleStateImageBehavior = false;
            this.albumItemsListView.View = System.Windows.Forms.View.List;
            this.albumItemsListView.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.AlbumItemsListView_AfterLabelEdit);
            this.albumItemsListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AlbumItemsListView_KeyDown);
            this.albumItemsListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.AlbumItemsListView_MouseDoubleClick);
            // 
            // addNewAlbum
            // 
            this.addNewAlbum.Location = new System.Drawing.Point(455, 19);
            this.addNewAlbum.Name = "addNewAlbum";
            this.addNewAlbum.Size = new System.Drawing.Size(75, 23);
            this.addNewAlbum.TabIndex = 4;
            this.addNewAlbum.Text = "Add";
            this.addNewAlbum.UseVisualStyleBackColor = true;
            this.addNewAlbum.Click += new System.EventHandler(this.AddNewAlbum_Click);
            // 
            // removeAlbum
            // 
            this.removeAlbum.Location = new System.Drawing.Point(455, 48);
            this.removeAlbum.Name = "removeAlbum";
            this.removeAlbum.Size = new System.Drawing.Size(75, 23);
            this.removeAlbum.TabIndex = 5;
            this.removeAlbum.Text = "Remove";
            this.removeAlbum.UseVisualStyleBackColor = true;
            this.removeAlbum.Click += new System.EventHandler(this.RemoveAlbum_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.albumNameComboBox);
            this.groupBox1.Controls.Add(this.browseSourcePath);
            this.groupBox1.Controls.Add(this.sourcePathTextBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.startStopProcessing);
            this.groupBox1.Controls.Add(this.progressBar);
            this.groupBox1.Location = new System.Drawing.Point(6, 146);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(524, 109);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Add to album";
            // 
            // albumNameComboBox
            // 
            this.albumNameComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.albumNameComboBox.FormattingEnabled = true;
            this.albumNameComboBox.Location = new System.Drawing.Point(80, 49);
            this.albumNameComboBox.Name = "albumNameComboBox";
            this.albumNameComboBox.Size = new System.Drawing.Size(347, 21);
            this.albumNameComboBox.TabIndex = 8;
            // 
            // loadAlbums
            // 
            this.loadAlbums.Location = new System.Drawing.Point(473, 12);
            this.loadAlbums.Name = "loadAlbums";
            this.loadAlbums.Size = new System.Drawing.Size(75, 23);
            this.loadAlbums.TabIndex = 3;
            this.loadAlbums.Text = "Load albums";
            this.loadAlbums.UseVisualStyleBackColor = true;
            this.loadAlbums.Click += new System.EventHandler(this.LoadAlbums_Click);
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 336);
            this.Controls.Add(this.loadAlbums);
            this.Controls.Add(this.albumsGroupBox);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.browseDestinationPath);
            this.Controls.Add(this.destinationPathTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Music Manager";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.albumsGroupBox.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button browseSourcePath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox sourcePathTextBox;
        private System.Windows.Forms.TextBox destinationPathTextBox;
        private System.Windows.Forms.Button browseDestinationPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button startStopProcessing;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel availableSpaceStatusLabel;
        private System.Windows.Forms.GroupBox albumsGroupBox;
        private System.Windows.Forms.Button loadAlbums;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button removeAlbum;
        private System.Windows.Forms.ComboBox albumNameComboBox;
        private System.Windows.Forms.Button addNewAlbum;
        private System.Windows.Forms.ListView albumItemsListView;
    }
}

