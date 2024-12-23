using System;
using System.Data;
using System.Windows.Forms;

namespace MusicApp
{
    public partial class AdminForm : Form
    {
        public AdminForm()
        {
            InitializeComponent();
            LoadSongs();
            LoadAlbums();
            LoadPodcasts();
            LoadArtists();
            LoadListeners();
        }

        private void LoadSongs()
        {
            try
            {
                string query = "SELECT * FROM music.songs";
                dgvSongs.DataSource = DatabaseHelper.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading songs: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadAlbums()
        {
            try
            {
                string query = "SELECT * FROM music.albums";
                dgvAlbums.DataSource = DatabaseHelper.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading albums: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadPodcasts()
        {
            try
            {
                string query = "SELECT * FROM music.podcasts";
                dgvPodcasts.DataSource = DatabaseHelper.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading podcasts: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadArtists()
        {
            try
            {
                string query = "SELECT * FROM music.artists";
                dgvArtists.DataSource = DatabaseHelper.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading artists: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadListeners()
        {
            try
            {
                string query = @"
                    SELECT l.listenerid, u.username, u.email, u.registrationdate
                    FROM music.listener l
                    JOIN music.users u ON l.userid = u.userid
                    ORDER BY l.listenerid";
                dgvListeners.DataSource = DatabaseHelper.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading listeners: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddListener_Click(object sender, EventArgs e)
        {
            string userIdInput = Prompt.ShowDialog("Enter the User ID to Add as a Listener:", "Add Listener");
            if (!int.TryParse(userIdInput, out int userId))
            {
                MessageBox.Show("Invalid User ID entered.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string query = @"
                    INSERT INTO music.listener (userid)
                    SELECT userid FROM music.users WHERE userid = {userId}
                    ON CONFLICT DO NOTHING";

                DatabaseHelper.ExecuteNonQuery(query);
                MessageBox.Show("User added as a Listener successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadListeners();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding listener: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRemoveListener_Click(object sender, EventArgs e)
        {
            if (dgvListeners.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a listener to remove.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int listenerId = Convert.ToInt32(dgvListeners.SelectedRows[0].Cells["listenerid"].Value);

            DialogResult confirmResult = MessageBox.Show("Are you sure you want to remove this listener?", "Confirm Remove", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    string query = $@"
                        DELETE FROM music.listener
                        WHERE listenerid = {listenerId}";

                    DatabaseHelper.ExecuteNonQuery(query);
                    MessageBox.Show("Listener removed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadListeners();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error removing listener: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (tabControl.SelectedTab == tabSongs)
                {
                    new AddEditSongForm().ShowDialog();
                    LoadSongs();
                }
                else if (tabControl.SelectedTab == tabAlbums)
                {
                    new AddEditAlbumForm().ShowDialog();
                    LoadAlbums();
                }
                else if (tabControl.SelectedTab == tabPodcasts)
                {
                    new AddEditPodcastForm().ShowDialog();
                    LoadPodcasts();
                }
                else if (tabControl.SelectedTab == tabArtists)
                {
                    new AddEditArtistForm().ShowDialog();
                    LoadArtists();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (tabControl.SelectedTab == tabSongs)
                {
                    if (dgvSongs.SelectedRows.Count > 0)
                    {
                        int songId = Convert.ToInt32(dgvSongs.SelectedRows[0].Cells["SongID"].Value);
                        new AddEditSongForm(songId).ShowDialog();
                        LoadSongs();
                    }
                    else
                    {
                        MessageBox.Show("Please select a song to edit.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else if (tabControl.SelectedTab == tabAlbums)
                {
                    if (dgvAlbums.SelectedRows.Count > 0)
                    {
                        int albumId = Convert.ToInt32(dgvAlbums.SelectedRows[0].Cells["AlbumID"].Value);
                        new AddEditAlbumForm(albumId).ShowDialog();
                        LoadAlbums();
                    }
                    else
                    {
                        MessageBox.Show("Please select an album to edit.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else if (tabControl.SelectedTab == tabPodcasts)
                {
                    if (dgvPodcasts.SelectedRows.Count > 0)
                    {
                        int podcastId = Convert.ToInt32(dgvPodcasts.SelectedRows[0].Cells["PodcastID"].Value);
                        new AddEditPodcastForm(podcastId).ShowDialog();
                        LoadPodcasts();
                    }
                    else
                    {
                        MessageBox.Show("Please select a podcast to edit.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else if (tabControl.SelectedTab == tabArtists)
                {
                    if (dgvArtists.SelectedRows.Count > 0)
                    {
                        int artistId = Convert.ToInt32(dgvArtists.SelectedRows[0].Cells["ArtistID"].Value);
                        new AddEditArtistForm(artistId).ShowDialog();
                        LoadArtists();
                    }
                    else
                    {
                        MessageBox.Show("Please select an artist to edit.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error editing data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (tabControl.SelectedTab == tabSongs)
                {
                    if (dgvSongs.SelectedRows.Count > 0)
                    {
                        int songId = Convert.ToInt32(dgvSongs.SelectedRows[0].Cells["SongID"].Value);
                        string query = $"DELETE FROM music.songs WHERE songid = {songId}";
                        DatabaseHelper.ExecuteNonQuery(query);
                        LoadSongs();
                    }
                    else
                    {
                        MessageBox.Show("Please select a song to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else if (tabControl.SelectedTab == tabAlbums)
                {
                    if (dgvAlbums.SelectedRows.Count > 0)
                    {
                        int albumId = Convert.ToInt32(dgvAlbums.SelectedRows[0].Cells["AlbumID"].Value);
                        string query = $"DELETE FROM music.albums WHERE albumid = {albumId}";
                        DatabaseHelper.ExecuteNonQuery(query);
                        LoadAlbums();
                    }
                    else
                    {
                        MessageBox.Show("Please select an album to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else if (tabControl.SelectedTab == tabPodcasts)
                {
                    if (dgvPodcasts.SelectedRows.Count > 0)
                    {
                        int podcastId = Convert.ToInt32(dgvPodcasts.SelectedRows[0].Cells["PodcastID"].Value);
                        string query = $"DELETE FROM music.podcasts WHERE podcastid = {podcastId}";
                        DatabaseHelper.ExecuteNonQuery(query);
                        LoadPodcasts();
                    }
                    else
                    {
                        MessageBox.Show("Please select a podcast to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else if (tabControl.SelectedTab == tabArtists)
                {
                    if (dgvArtists.SelectedRows.Count > 0)
                    {
                        int artistId = Convert.ToInt32(dgvArtists.SelectedRows[0].Cells["ArtistID"].Value);
                        string query = $"DELETE FROM music.artists WHERE artistid = {artistId}";
                        DatabaseHelper.ExecuteNonQuery(query);
                        LoadArtists();
                    }
                    else
                    {
                        MessageBox.Show("Please select an artist to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else if (tabControl.SelectedTab == tabListeners)
                {
                    if (dgvListeners.SelectedRows.Count > 0)
                    {
                        int listenerId = Convert.ToInt32(dgvListeners.SelectedRows[0].Cells["listenerid"].Value);
                        string query = $"DELETE FROM music.listener WHERE listenerid = {listenerId}";
                        DatabaseHelper.ExecuteNonQuery(query);
                        LoadListeners();
                    }
                    else
                    {
                        MessageBox.Show("Please select a listener to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                if (tabControl.SelectedTab == tabSongs)
                {
                    LoadSongs();
                }
                else if (tabControl.SelectedTab == tabAlbums)
                {
                    LoadAlbums();
                }
                else if (tabControl.SelectedTab == tabPodcasts)
                {
                    LoadPodcasts();
                }
                else if (tabControl.SelectedTab == tabArtists)
                {
                    LoadArtists();
                }
                else if (tabControl.SelectedTab == tabListeners)
                {
                    LoadListeners();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error refreshing data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBackToLogin_Click(object sender, EventArgs e)
        {
            new LoginForm().Show();
            this.Close();
        }
    }
}
