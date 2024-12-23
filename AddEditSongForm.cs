using System;
using System.Data;
using System.Windows.Forms;

namespace MusicApp
{
    public partial class AddEditSongForm : Form
    {
        private int? songId;

        public AddEditSongForm(int? id = null)
        {
            InitializeComponent();
            songId = id;

            if (songId.HasValue)
            {
                LoadSongDetails();
            }
        }
        private void LoadSongDetails()
        {
            try
            {
                string query = $"SELECT * FROM music.songs WHERE songid = {songId}";
                DataTable songData = DatabaseHelper.ExecuteQuery(query);

                if (songData.Rows.Count > 0)
                {
                    DataRow row = songData.Rows[0];
                    txtTitle.Text = row["title"].ToString();
                    txtDuration.Text = row["duration"].ToString();
                    txtArtistID.Text = row["artistid"].ToString();
                    txtAlbumID.Text = row["albumid"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading song details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show("Title cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtDuration.Text, out int duration) || duration <= 0)
            {
                MessageBox.Show("Invalid duration. Please enter a positive number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtArtistID.Text, out int artistId))
            {
                MessageBox.Show("Invalid Artist ID.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtAlbumID.Text, out int albumId))
            {
                MessageBox.Show("Invalid Album ID.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string title = txtTitle.Text.Trim();

            try
            {
                string query;

                if (songId.HasValue)
                {
                    query = $@"
                        UPDATE music.songs 
                        SET 
                            title = '{title}', 
                            duration = {duration}, 
                            artistid = {artistId}, 
                            albumid = {albumId}
                        WHERE songid = {songId}";
                }
                else
                {
                    query = $@"
                        INSERT INTO music.songs (title, duration, artistid, albumid) 
                        VALUES ('{title}', {duration}, {artistId}, {albumId})";

                    string triggerFunctionQuery = $"SELECT music.get_album_details({albumId})";
                    DataTable albumDetails = DatabaseHelper.ExecuteQuery(triggerFunctionQuery);

                    if (albumDetails.Rows.Count > 0)
                    {
                        MessageBox.Show("Album details updated successfully via function trigger!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                DatabaseHelper.ExecuteNonQuery(query);
                MessageBox.Show("Song saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving song: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeComponent()
        {
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.txtDuration = new System.Windows.Forms.TextBox();
            this.txtArtistID = new System.Windows.Forms.TextBox();
            this.txtAlbumID = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblDuration = new System.Windows.Forms.Label();
            this.lblArtistID = new System.Windows.Forms.Label();
            this.lblAlbumID = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(120, 30);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(200, 27);
            this.txtTitle.TabIndex = 0;
            // 
            // txtDuration
            // 
            this.txtDuration.Location = new System.Drawing.Point(120, 70);
            this.txtDuration.Name = "txtDuration";
            this.txtDuration.Size = new System.Drawing.Size(200, 27);
            this.txtDuration.TabIndex = 1;
            // 
            // txtArtistID
            // 
            this.txtArtistID.Location = new System.Drawing.Point(120, 110);
            this.txtArtistID.Name = "txtArtistID";
            this.txtArtistID.Size = new System.Drawing.Size(200, 27);
            this.txtArtistID.TabIndex = 2;
            // 
            // txtAlbumID
            // 
            this.txtAlbumID.Location = new System.Drawing.Point(120, 150);
            this.txtAlbumID.Name = "txtAlbumID";
            this.txtAlbumID.Size = new System.Drawing.Size(200, 27);
            this.txtAlbumID.TabIndex = 3;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(120, 200);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 30);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(30, 30);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(39, 20);
            this.lblTitle.TabIndex = 5;
            this.lblTitle.Text = "Title:";
            // 
            // lblDuration
            // 
            this.lblDuration.AutoSize = true;
            this.lblDuration.Location = new System.Drawing.Point(30, 70);
            this.lblDuration.Name = "lblDuration";
            this.lblDuration.Size = new System.Drawing.Size(67, 20);
            this.lblDuration.TabIndex = 6;
            this.lblDuration.Text = "Duration:";
            // 
            // lblArtistID
            // 
            this.lblArtistID.AutoSize = true;
            this.lblArtistID.Location = new System.Drawing.Point(30, 110);
            this.lblArtistID.Name = "lblArtistID";
            this.lblArtistID.Size = new System.Drawing.Size(61, 20);
            this.lblArtistID.TabIndex = 7;
            this.lblArtistID.Text = "Artist ID:";
            // 
            // lblAlbumID
            // 
            this.lblAlbumID.AutoSize = true;
            this.lblAlbumID.Location = new System.Drawing.Point(30, 150);
            this.lblAlbumID.Name = "lblAlbumID";
            this.lblAlbumID.Size = new System.Drawing.Size(69, 20);
            this.lblAlbumID.TabIndex = 8;
            this.lblAlbumID.Text = "Album ID:";
            // 
            // AddEditSongForm
            // 
            this.ClientSize = new System.Drawing.Size(400, 300);
            this.Controls.Add(this.lblAlbumID);
            this.Controls.Add(this.lblArtistID);
            this.Controls.Add(this.lblDuration);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtAlbumID);
            this.Controls.Add(this.txtArtistID);
            this.Controls.Add(this.txtDuration);
            this.Controls.Add(this.txtTitle);
            this.Name = "AddEditSongForm";
            this.Text = "Add/Edit Song";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.TextBox txtDuration;
        private System.Windows.Forms.TextBox txtArtistID;
        private System.Windows.Forms.TextBox txtAlbumID;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblDuration;
        private System.Windows.Forms.Label lblArtistID;
        private System.Windows.Forms.Label lblAlbumID;
    }
}
