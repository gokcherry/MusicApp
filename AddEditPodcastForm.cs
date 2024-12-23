using System;
using System.Data;
using System.Windows.Forms;

namespace MusicApp
{
    public partial class AddEditPodcastForm : Form
    {
        private int? podcastId;

        public AddEditPodcastForm(int? id = null)
        {
            InitializeComponent();
            podcastId = id;

            if (podcastId.HasValue)
            {
                LoadPodcastDetails();
            }
        }
        private void LoadPodcastDetails()
        {
            string query = $"SELECT * FROM music.podcasts WHERE podcastid = {podcastId}";
            DataTable podcastData = DatabaseHelper.ExecuteQuery(query);

            if (podcastData.Rows.Count > 0)
            {
                DataRow row = podcastData.Rows[0];
                txtTitle.Text = row["title"].ToString(); 
                txtDescription.Text = row["description"].ToString(); 
                txtHostName.Text = row["hostname"].ToString(); 
                txtGenreID.Text = row["genreid"].ToString(); 
                dtpReleaseDate.Value = Convert.ToDateTime(row["releasedate"]); 
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            string title = txtTitle.Text.Trim();
            string description = txtDescription.Text.Trim();
            string hostName = txtHostName.Text.Trim();
            string releaseDate = dtpReleaseDate.Value.ToString("yyyy-MM-dd");

            if (!int.TryParse(txtGenreID.Text, out int genreId))
            {
                MessageBox.Show("Invalid Genre ID.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query;
            if (podcastId.HasValue)
            {
                query = $@"
                    UPDATE music.podcasts
                    SET title = '{title}', 
                        description = '{description}', 
                        hostname = '{hostName}', 
                        genreid = {genreId}, 
                        releasedate = '{releaseDate}'
                    WHERE podcastid = {podcastId}";
            }
            else
            {
                query = $@"
                    INSERT INTO music.podcasts (title, description, hostname, genreid, releasedate)
                    VALUES ('{title}', '{description}', '{hostName}', {genreId}, '{releaseDate}')";
            }

            try
            {
                DatabaseHelper.ExecuteNonQuery(query);
                MessageBox.Show("Podcast saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving podcast: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeComponent()
        {
            txtTitle = new TextBox();
            txtDescription = new TextBox();
            txtHostName = new TextBox();
            txtGenreID = new TextBox();
            dtpReleaseDate = new DateTimePicker();
            btnSave = new Button();
            lblTitle = new Label();
            lblDescription = new Label();
            lblHostName = new Label();
            lblGenreID = new Label();
            lblReleaseDate = new Label();
            SuspendLayout();
            // 
            // txtTitle
            // 
            txtTitle.Location = new System.Drawing.Point(169, 27);
            txtTitle.Name = "txtTitle";
            txtTitle.Size = new System.Drawing.Size(200, 27);
            txtTitle.TabIndex = 0;
            // 
            // txtDescription
            // 
            txtDescription.Location = new System.Drawing.Point(169, 67);
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new System.Drawing.Size(200, 27);
            txtDescription.TabIndex = 1;
            // 
            // txtHostName
            // 
            txtHostName.Location = new System.Drawing.Point(169, 107);
            txtHostName.Name = "txtHostName";
            txtHostName.Size = new System.Drawing.Size(200, 27);
            txtHostName.TabIndex = 2;
            // 
            // txtGenreID
            // 
            txtGenreID.Location = new System.Drawing.Point(169, 147);
            txtGenreID.Name = "txtGenreID";
            txtGenreID.Size = new System.Drawing.Size(200, 27);
            txtGenreID.TabIndex = 3;
            // 
            // dtpReleaseDate
            // 
            dtpReleaseDate.Location = new System.Drawing.Point(169, 185);
            dtpReleaseDate.Name = "dtpReleaseDate";
            dtpReleaseDate.Size = new System.Drawing.Size(200, 27);
            dtpReleaseDate.TabIndex = 4;
            // 
            // btnSave
            // 
            btnSave.Location = new System.Drawing.Point(120, 240);
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
            // lblDescription
            // 
            lblDescription.AutoSize = true;
            lblDescription.Location = new System.Drawing.Point(30, 70);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new System.Drawing.Size(88, 20);
            lblDescription.TabIndex = 7;
            lblDescription.Text = "Description:";
            // 
            // lblHostName
            // 
            lblHostName.AutoSize = true;
            lblHostName.Location = new System.Drawing.Point(30, 110);
            lblHostName.Name = "lblHostName";
            lblHostName.Size = new System.Drawing.Size(87, 20);
            lblHostName.TabIndex = 8;
            lblHostName.Text = "Host Name:";
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
            // lblReleaseDate
            // 
            lblReleaseDate.AutoSize = true;
            lblReleaseDate.Location = new System.Drawing.Point(30, 190);
            lblReleaseDate.Name = "lblReleaseDate";
            lblReleaseDate.Size = new System.Drawing.Size(99, 20);
            lblReleaseDate.TabIndex = 10;
            lblReleaseDate.Text = "Release Date:";
            // 
            // AddEditPodcastForm
            // 
            ClientSize = new System.Drawing.Size(400, 300);
            Controls.Add(lblReleaseDate);
            Controls.Add(lblGenreID);
            Controls.Add(lblHostName);
            Controls.Add(lblDescription);
            Controls.Add(lblTitle);
            Controls.Add(btnSave);
            Controls.Add(dtpReleaseDate);
            Controls.Add(txtGenreID);
            Controls.Add(txtHostName);
            Controls.Add(txtDescription);
            Controls.Add(txtTitle);
            Name = "AddEditPodcastForm";
            Text = "Add/Edit Podcast";
            ResumeLayout(false);
            PerformLayout();
        }

        private TextBox txtTitle;
        private TextBox txtDescription;
        private TextBox txtHostName;
        private TextBox txtGenreID;
        private DateTimePicker dtpReleaseDate;
        private Button btnSave;
        private Label lblTitle;
        private Label lblDescription;
        private Label lblHostName;
        private Label lblGenreID;
        private Label lblReleaseDate;
    }
}
