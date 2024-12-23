using System;
using System.Windows.Forms;

namespace MusicApp
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
            btnBackToLogin.Click += btnBackToLogin_Click;
            btnRegister.Click += btnRegister_Click;
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            string email = txtEmail.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = $"INSERT INTO music.users (username, password, email) VALUES ('{username}', '{password}', '{email}')";
            try
            {
                DatabaseHelper.ExecuteNonQuery(query);
                MessageBox.Show("Registration Successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                new LoginForm().Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBackToLogin_Click(object sender, EventArgs e)
        {
            new LoginForm().Show();
            this.Hide();
        }

        private void InitializeComponent()
        {
            this.Username = new Label();
            this.Email = new Label();
            this.Password = new Label();
            this.txtUsername = new TextBox();
            this.txtEmail = new TextBox();
            this.txtPassword = new TextBox();
            this.btnBackToLogin = new Button();
            this.btnRegister = new Button();
            this.SuspendLayout();

            this.Username.AutoSize = true;
            this.Username.Location = new System.Drawing.Point(12, 50);
            this.Username.Name = "Username";
            this.Username.Size = new System.Drawing.Size(82, 20);
            this.Username.Text = "Username:";

            this.Email.AutoSize = true;
            this.Email.Location = new System.Drawing.Point(12, 100);
            this.Email.Name = "Email";
            this.Email.Size = new System.Drawing.Size(53, 20);
            this.Email.Text = "Email:";

            this.Password.AutoSize = true;
            this.Password.Location = new System.Drawing.Point(12, 147);
            this.Password.Name = "Password";
            this.Password.Size = new System.Drawing.Size(77, 20);
            this.Password.Text = "Password:";

            this.txtUsername.Location = new System.Drawing.Point(145, 47);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(125, 27);

            this.txtEmail.Location = new System.Drawing.Point(145, 97);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(125, 27);

            this.txtPassword.Location = new System.Drawing.Point(145, 144);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(125, 27);
            this.txtPassword.PasswordChar = '*';

            this.btnBackToLogin.Location = new System.Drawing.Point(12, 194);
            this.btnBackToLogin.Name = "btnBackToLogin";
            this.btnBackToLogin.Size = new System.Drawing.Size(94, 29);
            this.btnBackToLogin.Text = "Login";

            this.btnRegister.Location = new System.Drawing.Point(176, 194);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(94, 29);
            this.btnRegister.Text = "Register";

            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(282, 253);
            this.Controls.Add(this.btnRegister);
            this.Controls.Add(this.btnBackToLogin);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.Password);
            this.Controls.Add(this.Email);
            this.Controls.Add(this.Username);
            this.Name = "RegisterForm";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Register";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private Label Username;
        private Label Email;
        private Label Password;
        private TextBox txtUsername;
        private TextBox txtEmail;
        private TextBox txtPassword;
        private Button btnBackToLogin;
        private Button btnRegister;
    }
}
