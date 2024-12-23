using System;
using System.Data;
using System.Windows.Forms;

namespace MusicApp
{
    public partial class AddEditAlbumForm : Form
    {
        private int? albumId;

        public AddEditAlbumForm(int? id = null)
        {
            InitializeComponent();
            albumId = id;

            if (albumId.HasValue)
            {
                LoadAlbumDetails();
            }
        }

        private void LoadAlbumDetails()
        {
            string query = $"SELECT * FROM music.albums WHERE albumid = {albumId}";
            DataTable albumData = DatabaseHelper.ExecuteQuery(query);

            if (albumData.Rows.Count > 0)
            {
                DataRow row = albumData.Rows[0];
                txtTitle.Text = row["title"].ToString();
                dtpReleaseDate.Value = Convert.ToDateTime(row["releasedate"]);
                txtArtistID.Text = row["artistid"].ToString();
                txtGenreID.Text = row["genreid"].ToString();
                txtCategoryID.Text = row["categoryid"].ToString();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string title = txtTitle.Text.Trim();
            string releaseDate = dtpReleaseDate.Value.ToString("yyyy-MM-dd");

            if (!int.TryParse(txtArtistID.Text, out int artistId))
            {
                MessageBox.Show("Invalid Artist ID.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtGenreID.Text, out int genreId))
            {
                MessageBox.Show("Invalid Genre ID.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtCategoryID.Text, out int categoryId))
            {
                MessageBox.Show("Invalid Category ID.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query;
            if (albumId.HasValue)
            {
                query = $@"
                    UPDATE music.albums 
                    SET 
                        title = '{title}', 
                        releasedate = '{releaseDate}', 
                        artistid = {artistId}, 
                        genreid = {genreId}, 
                        categoryid = {categoryId}
                    WHERE albumid = {albumId}";
            }
            else
            {
                query = $@"
                    INSERT INTO music.albums (title, releasedate, artistid, genreid, categoryid) 
                    VALUES ('{title}', '{releaseDate}', {artistId}, {genreId}, {categoryId})";
            }

            try
            {
                DatabaseHelper.ExecuteNonQuery(query);
                MessageBox.Show("Album saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving album: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeComponent()
        {
            txtTitle = new TextBox();
            txtArtistID = new TextBox();
            txtGenreID = new TextBox();
            txtCategoryID = new TextBox();
            dtpReleaseDate = new DateTimePicker();
            btnSave = new Button();
            lblTitle = new Label();
            lblReleaseDate = new Label();
            lblArtistID = new Label();
            lblGenreID = new Label();
            lblCategoryID = new Label();
            SuspendLayout();
            // 
            // txtTitle
            // 
            txtTitle.Location = new System.Drawing.Point(164, 27);
            txtTitle.Name = "txtTitle";
            txtTitle.Size = new System.Drawing.Size(200, 27);
            txtTitle.TabIndex = 0;
            // 
            // txtArtistID
            // 
            txtArtistID.Location = new System.Drawing.Point(164, 107);
            txtArtistID.Name = "txtArtistID";
            txtArtistID.Size = new System.Drawing.Size(200, 27);
            txtArtistID.TabIndex = 1;
            // 
            // txtGenreID
            // 
            txtGenreID.Location = new System.Drawing.Point(164, 147);
            txtGenreID.Name = "txtGenreID";
            txtGenreID.Size = new System.Drawing.Size(200, 27);
            txtGenreID.TabIndex = 2;
            // 
            // txtCategoryID
            // 
            txtCategoryID.Location = new System.Drawing.Point(164, 187);
            txtCategoryID.Name = "txtCategoryID";
            txtCategoryID.Size = new System.Drawing.Size(200, 27);
            txtCategoryID.TabIndex = 3;
            // 
            // dtpReleaseDate
            // 
            dtpReleaseDate.Location = new System.Drawing.Point(164, 70);
            dtpReleaseDate.Name = "dtpReleaseDate";
            dtpReleaseDate.Size = new System.Drawing.Size(200, 27);
            dtpReleaseDate.TabIndex = 4;
            // 
            // btnSave
            // 
            btnSave.Location = new System.Drawing.Point(164, 244);
            btnSave.Name = "btnSave";
            btnSave.Size = new System.Drawing.Size(100, 30);
            btnSave.TabIndex = 5;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Location = new System.Drawing.Point(30, 30);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new System.Drawing.Size(39, 20);
            lblTitle.TabIndex = 6;
            lblTitle.Text = "Title:";
            // 
            // lblReleaseDate
            // 
            lblReleaseDate.AutoSize = true;
            lblReleaseDate.Location = new System.Drawing.Point(30, 70);
            lblReleaseDate.Name = "lblReleaseDate";
            lblReleaseDate.Size = new System.Drawing.Size(99, 20);
            lblReleaseDate.TabIndex = 7;
            lblReleaseDate.Text = "Release Date:";
            // 
            // lblArtistID
            // 
            lblArtistID.AutoSize = true;
            lblArtistID.Location = new System.Drawing.Point(30, 110);
            lblArtistID.Name = "lblArtistID";
            lblArtistID.Size = new System.Drawing.Size(61, 20);
            lblArtistID.TabIndex = 8;
            lblArtistID.Text = "Artist ID:";
            // 
            // lblGenreID
            // 
            lblGenreID.AutoSize = true;
            lblGenreID.Location = new System.Drawing.Point(30, 150);
            lblGenreID.Name = "lblGenreID";
            lblGenreID.Size = new System.Drawing.Size(67, 20);
            lblGenreID.TabIndex = 9;
            lblGenreID.Text = "Genre ID:";
            // 
            // lblCategoryID
            // 
            lblCategoryID.AutoSize = true;
            lblCategoryID.Location = new System.Drawing.Point(30, 190);
            lblCategoryID.Name = "lblCategoryID";
            lblCategoryID.Size = new System.Drawing.Size(87, 20);
            lblCategoryID.TabIndex = 10;
            lblCategoryID.Text = "Category ID:";
            // 
            // AddEditAlbumForm
            // 
            ClientSize = new System.Drawing.Size(400, 300);
            Controls.Add(lblCategoryID);
            Controls.Add(lblGenreID);
            Controls.Add(lblArtistID);
            Controls.Add(lblReleaseDate);
            Controls.Add(lblTitle);
            Controls.Add(btnSave);
            Controls.Add(dtpReleaseDate);
            Controls.Add(txtCategoryID);
            Controls.Add(txtGenreID);
            Controls.Add(txtArtistID);
            Controls.Add(txtTitle);
            Name = "AddEditAlbumForm";
            Text = "Add/Edit Album";
            ResumeLayout(false);
            PerformLayout();
        }

        private TextBox txtTitle;
        private TextBox txtArtistID;
        private TextBox txtGenreID;
        private TextBox txtCategoryID;
        private DateTimePicker dtpReleaseDate;
        private Button btnSave;
        private Label lblTitle;
        private Label lblReleaseDate;
        private Label lblArtistID;
        private Label lblGenreID;
        private Label lblCategoryID;
    }
}
