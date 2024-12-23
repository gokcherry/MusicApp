using System;
using System.Data;
using System.Windows.Forms;
using MusicApp;

namespace MusicApp
{
    public partial class AddEditArtistForm : Form
    {
        private int? artistId;

        public AddEditArtistForm(int? id = null)
        {
            InitializeComponent();
            artistId = id;

            if (artistId.HasValue)
            {
                LoadArtistDetails();
            }
        }
        private void LoadArtistDetails()
        {
            string query = $"SELECT * FROM music.artists WHERE artistid = {artistId}";
            DataTable artistData = DatabaseHelper.ExecuteQuery(query);

            if (artistData.Rows.Count > 0)
            {
                DataRow row = artistData.Rows[0];
                txtName.Text = row["Name"].ToString();
                txtBio.Text = row["Bio"].ToString();
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            string bio = txtBio.Text.Trim();

            string query;
            if (artistId.HasValue)
            {
                query = $"UPDATE music.artists SET name = '{name}', bio = '{bio}' WHERE artistid = {artistId}";
            }
            else
            {
                query = $"INSERT INTO music.artists (name, bio) VALUES ('{name}', '{bio}')";
            }

            try
            {
                DatabaseHelper.ExecuteNonQuery(query);
                MessageBox.Show("Artist saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void InitializeComponent()
        {
            this.txtName = new TextBox();
            this.txtBio = new TextBox();
            this.btnSave = new Button();
            this.lblName = new Label();
            this.lblBio = new Label();
            this.SuspendLayout();
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(120, 30);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(200, 27);
            this.txtName.TabIndex = 0;
            // 
            // txtBio
            // 
            this.txtBio.Location = new System.Drawing.Point(120, 70);
            this.txtBio.Multiline = true;
            this.txtBio.Name = "txtBio";
            this.txtBio.Size = new System.Drawing.Size(200, 100);
            this.txtBio.TabIndex = 1;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(120, 190);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 30);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(30, 30);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(49, 20);
            this.lblName.TabIndex = 3;
            this.lblName.Text = "Name:";
            // 
            // lblBio
            // 
            this.lblBio.AutoSize = true;
            this.lblBio.Location = new System.Drawing.Point(30, 70);
            this.lblBio.Name = "lblBio";
            this.lblBio.Size = new System.Drawing.Size(32, 20);
            this.lblBio.TabIndex = 4;
            this.lblBio.Text = "Bio:";
            // 
            // AddEditArtistForm
            // 
            this.ClientSize = new System.Drawing.Size(400, 300);
            this.Controls.Add(this.lblBio);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtBio);
            this.Controls.Add(this.txtName);
            this.Name = "AddEditArtistForm";
            this.Text = "Add/Edit Artist";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private TextBox txtName;
        private TextBox txtBio;
        private Button btnSave;
        private Label lblName;
        private Label lblBio;
    }
}
