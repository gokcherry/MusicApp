namespace MusicApp
{
    partial class AdminForm
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
        private void InitializeComponent()
        {
            tabArtists = new TabPage();
            dgvArtists = new DataGridView();
            tabPodcasts = new TabPage();
            dgvPodcasts = new DataGridView();
            tabAlbums = new TabPage();
            dgvAlbums = new DataGridView();
            tabSongs = new TabPage();
            dgvSongs = new DataGridView();
            btnRefresh = new Button();
            btnDelete = new Button();
            btnAdd = new Button();
            tabControl = new TabControl();
            tabListeners = new TabPage();
            btnRemoveListener = new Button();
            btnAddListener = new Button();
            dgvListeners = new DataGridView();
            btnEdit = new Button();
            btnGoBackToLogin = new Button();
            tabArtists.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvArtists).BeginInit();
            tabPodcasts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPodcasts).BeginInit();
            tabAlbums.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAlbums).BeginInit();
            tabSongs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSongs).BeginInit();
            tabControl.SuspendLayout();
            tabListeners.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvListeners).BeginInit();
            SuspendLayout();
            // 
            // tabArtists
            // 
            tabArtists.Controls.Add(dgvArtists);
            tabArtists.Location = new Point(4, 29);
            tabArtists.Name = "tabArtists";
            tabArtists.Padding = new Padding(3);
            tabArtists.Size = new Size(768, 393);
            tabArtists.TabIndex = 3;
            tabArtists.Text = "Artists";
            tabArtists.UseVisualStyleBackColor = true;
            // 
            // dgvArtists
            // 
            dgvArtists.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvArtists.GridColor = SystemColors.InactiveCaption;
            dgvArtists.Location = new Point(6, 6);
            dgvArtists.Name = "dgvArtists";
            dgvArtists.RowHeadersWidth = 51;
            dgvArtists.Size = new Size(756, 381);
            dgvArtists.TabIndex = 3;
            // 
            // tabPodcasts
            // 
            tabPodcasts.Controls.Add(dgvPodcasts);
            tabPodcasts.Location = new Point(4, 29);
            tabPodcasts.Name = "tabPodcasts";
            tabPodcasts.Padding = new Padding(3);
            tabPodcasts.Size = new Size(768, 393);
            tabPodcasts.TabIndex = 2;
            tabPodcasts.Text = "Podcasts";
            tabPodcasts.UseVisualStyleBackColor = true;
            // 
            // dgvPodcasts
            // 
            dgvPodcasts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPodcasts.Location = new Point(6, 6);
            dgvPodcasts.Name = "dgvPodcasts";
            dgvPodcasts.RowHeadersWidth = 51;
            dgvPodcasts.Size = new Size(756, 381);
            dgvPodcasts.TabIndex = 3;
            // 
            // tabAlbums
            // 
            tabAlbums.Controls.Add(dgvAlbums);
            tabAlbums.Location = new Point(4, 29);
            tabAlbums.Name = "tabAlbums";
            tabAlbums.Padding = new Padding(3);
            tabAlbums.Size = new Size(768, 393);
            tabAlbums.TabIndex = 1;
            tabAlbums.Text = "Albums";
            tabAlbums.UseVisualStyleBackColor = true;
            // 
            // dgvAlbums
            // 
            dgvAlbums.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAlbums.Location = new Point(6, 6);
            dgvAlbums.Name = "dgvAlbums";
            dgvAlbums.RowHeadersWidth = 51;
            dgvAlbums.Size = new Size(756, 381);
            dgvAlbums.TabIndex = 3;
            // 
            // tabSongs
            // 
            tabSongs.Controls.Add(dgvSongs);
            tabSongs.Location = new Point(4, 29);
            tabSongs.Name = "tabSongs";
            tabSongs.Padding = new Padding(3);
            tabSongs.Size = new Size(768, 393);
            tabSongs.TabIndex = 0;
            tabSongs.Text = "Songs";
            tabSongs.UseVisualStyleBackColor = true;
            // 
            // dgvSongs
            // 
            dgvSongs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSongs.Location = new Point(6, 6);
            dgvSongs.Name = "dgvSongs";
            dgvSongs.RowHeadersWidth = 51;
            dgvSongs.Size = new Size(756, 381);
            dgvSongs.TabIndex = 2;
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new Point(222, 444);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(94, 29);
            btnRefresh.TabIndex = 5;
            btnRefresh.Text = "Refresh";
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(122, 444);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(94, 29);
            btnDelete.TabIndex = 4;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(22, 444);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(94, 29);
            btnAdd.TabIndex = 3;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // tabControl
            // 
            tabControl.Controls.Add(tabSongs);
            tabControl.Controls.Add(tabAlbums);
            tabControl.Controls.Add(tabPodcasts);
            tabControl.Controls.Add(tabArtists);
            tabControl.Controls.Add(tabListeners);
            tabControl.Location = new Point(12, 12);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(776, 426);
            tabControl.TabIndex = 0;
            // 
            // tabListeners
            // 
            tabListeners.Controls.Add(btnRemoveListener);
            tabListeners.Controls.Add(btnAddListener);
            tabListeners.Controls.Add(dgvListeners);
            tabListeners.Location = new Point(4, 29);
            tabListeners.Name = "tabListeners";
            tabListeners.Padding = new Padding(3);
            tabListeners.Size = new Size(768, 393);
            tabListeners.TabIndex = 4;
            tabListeners.Text = "Listeners";
            tabListeners.UseVisualStyleBackColor = true;
            // 
            // btnRemoveListener
            // 
            btnRemoveListener.Location = new Point(537, 355);
            btnRemoveListener.Name = "btnRemoveListener";
            btnRemoveListener.Size = new Size(94, 29);
            btnRemoveListener.TabIndex = 2;
            btnRemoveListener.Text = "Remove";
            btnRemoveListener.UseVisualStyleBackColor = true;
            btnRemoveListener.Click += btnRemoveListener_Click;
            // 
            // btnAddListener
            // 
            btnAddListener.Location = new Point(437, 355);
            btnAddListener.Name = "btnAddListener";
            btnAddListener.Size = new Size(94, 29);
            btnAddListener.TabIndex = 1;
            btnAddListener.Text = "Add";
            btnAddListener.UseVisualStyleBackColor = true;
            btnAddListener.Click += btnAddListener_Click;
            // 
            // dgvListeners
            // 
            dgvListeners.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvListeners.Location = new Point(6, 4);
            dgvListeners.Name = "dgvListeners";
            dgvListeners.RowHeadersWidth = 51;
            dgvListeners.Size = new Size(759, 345);
            dgvListeners.TabIndex = 0;
            // 
            // btnEdit
            // 
            btnEdit.Location = new Point(322, 444);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(94, 29);
            btnEdit.TabIndex = 6;
            btnEdit.Text = "Edit";
            btnEdit.UseVisualStyleBackColor = true;
            btnEdit.Click += btnEdit_Click;
            // 
            // btnGoBackToLogin
            // 
            btnGoBackToLogin.Location = new Point(690, 444);
            btnGoBackToLogin.Name = "btnGoBackToLogin";
            btnGoBackToLogin.Size = new Size(94, 29);
            btnGoBackToLogin.TabIndex = 7;
            btnGoBackToLogin.Text = "Log Out";
            btnGoBackToLogin.UseVisualStyleBackColor = true;
            btnGoBackToLogin.Click += btnBackToLogin_Click;
            // 
            // AdminForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 484);
            Controls.Add(btnGoBackToLogin);
            Controls.Add(btnEdit);
            Controls.Add(btnRefresh);
            Controls.Add(tabControl);
            Controls.Add(btnDelete);
            Controls.Add(btnAdd);
            Name = "AdminForm";
            Text = "AdminForm";
            tabArtists.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvArtists).EndInit();
            tabPodcasts.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvPodcasts).EndInit();
            tabAlbums.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvAlbums).EndInit();
            tabSongs.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvSongs).EndInit();
            tabControl.ResumeLayout(false);
            tabListeners.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvListeners).EndInit();
            ResumeLayout(false);
        }

        private TabPage tabArtists;
        private DataGridView dgvArtists;
        private TabPage tabPodcasts;
        private DataGridView dgvPodcasts;
        private TabPage tabAlbums;
        private DataGridView dgvAlbums;
        private TabPage tabSongs;
        private Button btnRefresh;
        private Button btnDelete;
        private Button btnAdd;
        private DataGridView dgvSongs;
        private TabControl tabControl;
        private Button btnEdit;
        private Button btnGoBackToLogin;
        private TabPage tabListeners;
        private DataGridView dgvListeners;
        private Button btnRemoveListener;
        private Button btnAddListener;
    }
}