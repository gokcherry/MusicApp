using System;
using System.Data;
using System.Windows.Forms;

namespace MusicApp
{
    public partial class MainForm : Form
    {
        private int _listenerId; 

        public MainForm(int listenerId)
        {
            InitializeComponent();
            _listenerId = listenerId;
            LoadSongs();
            LoadPodcasts();
            LoadPlaylists();
            LoadLikes();
            LoadDislikes();
            LoadPlaylist();
        }
        private void LoadSongs()
        {
            string query = "SELECT songid, title, duration FROM music.songs";
            dgvSongs.DataSource = DatabaseHelper.ExecuteQuery(query);
        }
        private void LoadPodcasts()
        {
            string query = "SELECT podcastid, title, hostname, releasedate FROM music.podcasts";
            dgvPodcasts.DataSource = DatabaseHelper.ExecuteQuery(query);
        }
        private void LoadLikes()
        {
            try
            {
                string query = "SELECT * FROM music.likes WHERE isliked = true";
                DataTable likesTable = DatabaseHelper.ExecuteQuery(query);
                dgvLikes.DataSource = likesTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading likes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadDislikes()
        {
            try
            {
                string query = "SELECT * FROM music.likes WHERE isliked = false";
                DataTable dislikesTable = DatabaseHelper.ExecuteQuery(query);
                dgvDislikes.DataSource = dislikesTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading dislikes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadPlaylist()
        {
            try
            {
                string query = "SELECT * FROM music.playlists";
                DataTable playlistsTable = DatabaseHelper.ExecuteQuery(query);
                dgvPlaylist.DataSource = playlistsTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading playlists: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadPlaylists()
        {
            string query = $"SELECT playlistid, name, createdat FROM music.playlists WHERE listenerid = {_listenerId}";
            dgvPlaylists.DataSource = DatabaseHelper.ExecuteQuery(query);
        }

        private void btnNewPlaylist_Click(object sender, EventArgs e)
        {
            string playlistName = Prompt.ShowDialog("Enter Playlist Name:", "New Playlist");
            if (string.IsNullOrEmpty(playlistName))
            {
                MessageBox.Show("Playlist name cannot be empty.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = $"INSERT INTO music.playlists (name, listenerid, createdat) VALUES ('{playlistName}', {_listenerId}, NOW())";
            try
            {
                DatabaseHelper.ExecuteNonQuery(query);
                MessageBox.Show("Playlist created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadPlaylists();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnAddPlaylist_Click(object sender, EventArgs e)
        {
            if (dgvPlaylists.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a playlist.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int playlistId = Convert.ToInt32(dgvPlaylists.SelectedRows[0].Cells["playlistid"].Value);

            string songIdInput = Prompt.ShowDialog("Enter the Song ID to Add:", "Add Song");
            if (!int.TryParse(songIdInput, out int songId))
            {
                MessageBox.Show("Invalid Song ID entered.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = $"INSERT INTO music.playlist_items (playlistid, songid) VALUES ({playlistId}, {songId})";
            string logQuery = $"INSERT INTO music.playlist_modifications (playlistid, modification) " +
                              $"VALUES ({playlistId}, 'Added song with ID {songId} to playlist.')";

            try
            {
                DatabaseHelper.ExecuteNonQuery(query);
                DatabaseHelper.ExecuteNonQuery(logQuery);
                MessageBox.Show("Song added to playlist and modification logged!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnRemovePlaylist_Click(object sender, EventArgs e)
        {
            if (dgvPlaylists.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a playlist.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int playlistId = Convert.ToInt32(dgvPlaylists.SelectedRows[0].Cells["playlistid"].Value);

            string songIdInput = Prompt.ShowDialog("Enter the Song ID to Remove:", "Remove Song");
            if (!int.TryParse(songIdInput, out int songId))
            {
                MessageBox.Show("Invalid Song ID entered.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = $"DELETE FROM music.playlist_items WHERE playlistid = {playlistId} AND songid = {songId}";
            string logQuery = $"INSERT INTO music.playlist_modifications (playlistid, modification) " +
                              $"VALUES ({playlistId}, 'Removed song with ID {songId} from playlist.')";

            try
            {
                DatabaseHelper.ExecuteNonQuery(query);
                DatabaseHelper.ExecuteNonQuery(logQuery);
                MessageBox.Show("Song removed from playlist and modification logged!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void BtnLike_Click(object sender, EventArgs e)
        {
            HandleLikeDislike(true); // Pass `true` for "like"
        }
        private void BtnDislike_Click(object sender, EventArgs e)
        {
            HandleLikeDislike(false); // Pass `false` for "dislike"
        }
        private void HandleLikeDislike(bool isLiked)
        {
            string targetType = tabControl.SelectedTab == tabSongs ? "song" : "podcast";
            DataGridView dgv = tabControl.SelectedTab == tabSongs ? dgvSongs : dgvPodcasts;

            if (dgv.SelectedRows.Count > 0)
            {
                try
                {
                    int targetId = Convert.ToInt32(dgv.SelectedRows[0].Cells[0].Value); // Assuming the ID is in the first column
                    string query = $"SELECT music.like_or_dislike({_listenerId}, {targetId}, {isLiked}, '{targetType}')";
                    DatabaseHelper.ExecuteNonQuery(query);
                    MessageBox.Show($"{(isLiked ? "Liked" : "Disliked")} successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show($"Please select a {targetType} to {(isLiked ? "like" : "dislike")}.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void BtnLogout_Click(object sender, EventArgs e)
        {
            new LoginForm().Show();
            this.Close();
        }
        private void InitializeComponent()
        {
            btnLikeSong = new Button();
            btnDislikeSong = new Button();
            tabControl = new TabControl();
            tabSongs = new TabPage();
            btnRemovePlaylist = new Button();
            btnAddPlaylist = new Button();
            btnNewPlaylist = new Button();
            dgvPlaylists = new DataGridView();
            dgvSongs = new DataGridView();
            tabPodcasts = new TabPage();
            dgvPodcasts = new DataGridView();
            tabLog = new TabPage();
            tabMyPage = new TabControl();
            tabLikes = new TabPage();
            dgvLikes = new DataGridView();
            tabDislikes = new TabPage();
            dgvDislikes = new DataGridView();
            tabPlaylists = new TabPage();
            dgvPlaylist = new DataGridView();
            btnLogout = new Button();
            tabControl.SuspendLayout();
            tabSongs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPlaylists).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvSongs).BeginInit();
            tabPodcasts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPodcasts).BeginInit();
            tabLog.SuspendLayout();
            tabMyPage.SuspendLayout();
            tabLikes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLikes).BeginInit();
            tabDislikes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDislikes).BeginInit();
            tabPlaylists.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPlaylist).BeginInit();
            SuspendLayout();
            // 
            // btnLikeSong
            // 
            btnLikeSong.Location = new Point(22, 461);
            btnLikeSong.Name = "btnLikeSong";
            btnLikeSong.Size = new Size(94, 29);
            btnLikeSong.TabIndex = 2;
            btnLikeSong.Text = "Like";
            btnLikeSong.UseVisualStyleBackColor = true;
            btnLikeSong.Click += BtnLike_Click;
            // 
            // btnDislikeSong
            // 
            btnDislikeSong.Location = new Point(122, 461);
            btnDislikeSong.Name = "btnDislikeSong";
            btnDislikeSong.Size = new Size(94, 29);
            btnDislikeSong.TabIndex = 3;
            btnDislikeSong.Text = "Dislike";
            btnDislikeSong.UseVisualStyleBackColor = true;
            btnDislikeSong.Click += BtnDislike_Click;
            // 
            // tabControl
            // 
            tabControl.Controls.Add(tabSongs);
            tabControl.Controls.Add(tabPodcasts);
            tabControl.Controls.Add(tabLog);
            tabControl.Location = new Point(12, 12);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(731, 431);
            tabControl.TabIndex = 6;
            // 
            // tabSongs
            // 
            tabSongs.Controls.Add(btnRemovePlaylist);
            tabSongs.Controls.Add(btnAddPlaylist);
            tabSongs.Controls.Add(btnNewPlaylist);
            tabSongs.Controls.Add(dgvPlaylists);
            tabSongs.Controls.Add(dgvSongs);
            tabSongs.Location = new Point(4, 29);
            tabSongs.Name = "tabSongs";
            tabSongs.Padding = new Padding(3);
            tabSongs.Size = new Size(723, 398);
            tabSongs.TabIndex = 0;
            tabSongs.Text = "Songs";
            tabSongs.UseVisualStyleBackColor = true;
            // 
            // btnRemovePlaylist
            // 
            btnRemovePlaylist.Location = new Point(407, 363);
            btnRemovePlaylist.Name = "btnRemovePlaylist";
            btnRemovePlaylist.Size = new Size(94, 29);
            btnRemovePlaylist.TabIndex = 4;
            btnRemovePlaylist.Text = "Remove";
            btnRemovePlaylist.UseVisualStyleBackColor = true;
            btnRemovePlaylist.Click += btnRemovePlaylist_Click;
            // 
            // btnAddPlaylist
            // 
            btnAddPlaylist.Location = new Point(307, 363);
            btnAddPlaylist.Name = "btnAddPlaylist";
            btnAddPlaylist.Size = new Size(94, 29);
            btnAddPlaylist.TabIndex = 3;
            btnAddPlaylist.Text = "Add";
            btnAddPlaylist.UseVisualStyleBackColor = true;
            btnAddPlaylist.Click += btnAddPlaylist_Click;
            // 
            // btnNewPlaylist
            // 
            btnNewPlaylist.Location = new Point(207, 363);
            btnNewPlaylist.Name = "btnNewPlaylist";
            btnNewPlaylist.Size = new Size(94, 29);
            btnNewPlaylist.TabIndex = 2;
            btnNewPlaylist.Text = "New";
            btnNewPlaylist.UseVisualStyleBackColor = true;
            btnNewPlaylist.Click += btnNewPlaylist_Click;
            // 
            // dgvPlaylists
            // 
            dgvPlaylists.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPlaylists.Location = new Point(6, 270);
            dgvPlaylists.Name = "dgvPlaylists";
            dgvPlaylists.RowHeadersWidth = 51;
            dgvPlaylists.Size = new Size(711, 87);
            dgvPlaylists.TabIndex = 1;
            // 
            // dgvSongs
            // 
            dgvSongs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSongs.Location = new Point(6, 6);
            dgvSongs.Name = "dgvSongs";
            dgvSongs.RowHeadersWidth = 51;
            dgvSongs.Size = new Size(711, 258);
            dgvSongs.TabIndex = 0;
            // 
            // tabPodcasts
            // 
            tabPodcasts.Controls.Add(dgvPodcasts);
            tabPodcasts.Location = new Point(4, 29);
            tabPodcasts.Name = "tabPodcasts";
            tabPodcasts.Padding = new Padding(3);
            tabPodcasts.Size = new Size(723, 398);
            tabPodcasts.TabIndex = 1;
            tabPodcasts.Text = "Podcasts";
            tabPodcasts.UseVisualStyleBackColor = true;
            // 
            // dgvPodcasts
            // 
            dgvPodcasts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPodcasts.Location = new Point(6, 6);
            dgvPodcasts.Name = "dgvPodcasts";
            dgvPodcasts.RowHeadersWidth = 51;
            dgvPodcasts.Size = new Size(711, 386);
            dgvPodcasts.TabIndex = 1;
            // 
            // tabLog
            // 
            tabLog.Controls.Add(tabMyPage);
            tabLog.Location = new Point(4, 29);
            tabLog.Name = "tabLog";
            tabLog.Padding = new Padding(3);
            tabLog.Size = new Size(723, 398);
            tabLog.TabIndex = 2;
            tabLog.Text = "My Page";
            tabLog.UseVisualStyleBackColor = true;
            // 
            // tabMyPage
            // 
            tabMyPage.Controls.Add(tabLikes);
            tabMyPage.Controls.Add(tabDislikes);
            tabMyPage.Controls.Add(tabPlaylists);
            tabMyPage.Location = new Point(6, 6);
            tabMyPage.Name = "tabMyPage";
            tabMyPage.SelectedIndex = 0;
            tabMyPage.Size = new Size(711, 386);
            tabMyPage.TabIndex = 0;
            // 
            // tabLikes
            // 
            tabLikes.Controls.Add(dgvLikes);
            tabLikes.Location = new Point(4, 29);
            tabLikes.Name = "tabLikes";
            tabLikes.Padding = new Padding(3);
            tabLikes.Size = new Size(703, 353);
            tabLikes.TabIndex = 0;
            tabLikes.Text = "Likes";
            tabLikes.UseVisualStyleBackColor = true;
            // 
            // dgvLikes
            // 
            dgvLikes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLikes.Location = new Point(6, 6);
            dgvLikes.Name = "dgvLikes";
            dgvLikes.RowHeadersWidth = 51;
            dgvLikes.Size = new Size(691, 341);
            dgvLikes.TabIndex = 1;
            // 
            // tabDislikes
            // 
            tabDislikes.Controls.Add(dgvDislikes);
            tabDislikes.Location = new Point(4, 29);
            tabDislikes.Name = "tabDislikes";
            tabDislikes.Padding = new Padding(3);
            tabDislikes.Size = new Size(703, 353);
            tabDislikes.TabIndex = 1;
            tabDislikes.Text = "Dislikes";
            tabDislikes.UseVisualStyleBackColor = true;
            // 
            // dgvDislikes
            // 
            dgvDislikes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDislikes.Location = new Point(6, 6);
            dgvDislikes.Name = "dgvDislikes";
            dgvDislikes.RowHeadersWidth = 51;
            dgvDislikes.Size = new Size(691, 341);
            dgvDislikes.TabIndex = 2;
            dgvDislikes.CellContentClick += dataGridView3_CellContentClick;
            // 
            // tabPlaylists
            // 
            tabPlaylists.Controls.Add(dgvPlaylist);
            tabPlaylists.Location = new Point(4, 29);
            tabPlaylists.Name = "tabPlaylists";
            tabPlaylists.Padding = new Padding(3);
            tabPlaylists.Size = new Size(703, 353);
            tabPlaylists.TabIndex = 2;
            tabPlaylists.Text = "Playlists";
            tabPlaylists.UseVisualStyleBackColor = true;
            // 
            // dgvPlaylist
            // 
            dgvPlaylist.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPlaylist.Location = new Point(6, 6);
            dgvPlaylist.Name = "dgvPlaylist";
            dgvPlaylist.RowHeadersWidth = 51;
            dgvPlaylist.Size = new Size(691, 341);
            dgvPlaylist.TabIndex = 0;
            // 
            // btnLogout
            // 
            btnLogout.Location = new Point(622, 461);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(94, 29);
            btnLogout.TabIndex = 7;
            btnLogout.Text = "Logout";
            btnLogout.UseVisualStyleBackColor = true;
            btnLogout.Click += BtnLogout_Click;
            // 
            // MainForm
            // 
            ClientSize = new Size(755, 502);
            Controls.Add(btnLogout);
            Controls.Add(tabControl);
            Controls.Add(btnDislikeSong);
            Controls.Add(btnLikeSong);
            Name = "MainForm";
            Load += MainForm_Load;
            tabControl.ResumeLayout(false);
            tabSongs.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvPlaylists).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvSongs).EndInit();
            tabPodcasts.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvPodcasts).EndInit();
            tabLog.ResumeLayout(false);
            tabMyPage.ResumeLayout(false);
            tabLikes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvLikes).EndInit();
            tabDislikes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvDislikes).EndInit();
            tabPlaylists.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvPlaylist).EndInit();
            ResumeLayout(false);
        }

        private Button btnLikeSong;
        private TabControl tabControl;
        private TabPage tabSongs;
        private DataGridView dgvSongs;
        private TabPage tabPodcasts;
        private DataGridView dgvPodcasts;
        private Button btnDislikeSong;
        private DataGridView dgvPlaylists;
        private Button btnRemovePlaylist;
        private Button btnAddPlaylist;
        private Button btnNewPlaylist;
        private TabPage tabLog;
        private TabControl tabMyPage;
        private TabPage tabLikes;
        private TabPage tabDislikes;
        private TabPage tabPlaylists;
        private DataGridView dgvLikes;
        private DataGridView dgvDislikes;
        private DataGridView dgvPlaylist;
        private Button btnLogout;

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
