using System;
using System.Data;
using System.Windows.Forms;

namespace MusicApp
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            btnGoToRegister.Click += new EventHandler(btnGoToRegister_Click);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string adminQuery = $"SELECT * FROM music.admin WHERE username = '{username}' AND password = '{password}'";
            DataTable adminResult = DatabaseHelper.ExecuteQuery(adminQuery);

            if (adminResult.Rows.Count > 0)
            {
                MessageBox.Show("Admin Login Successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                new AdminForm().Show();
                this.Hide();
                return;
            }
            string userQuery = $"SELECT listenerid FROM music.listener WHERE username = '{username}' AND password = '{password}'";
            DataTable userResult = DatabaseHelper.ExecuteQuery(userQuery);

            if (userResult.Rows.Count > 0)
            {
                int listenerId = Convert.ToInt32(userResult.Rows[0]["listenerid"]);
                MessageBox.Show("Login Successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                new MainForm(listenerId).Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGoToRegister_Click(object sender, EventArgs e)
        {
            new RegisterForm().Show();
            this.Hide();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void InitializeComponent()
        {
            Username = new Label();
            Password = new Label();
            txtUsername = new TextBox();
            txtPassword = new TextBox();
            btnGoToRegister = new Button();
            btnLogin = new Button();
            btnExit = new Button();
            SuspendLayout();
            // 
            // Username
            // 
            Username.AutoSize = true;
            Username.Location = new Point(12, 52);
            Username.Name = "Username";
            Username.Size = new Size(86, 20);
            Username.TabIndex = 0;
            Username.Text = "Username : ";
            Username.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Password
            // 
            Password.AutoSize = true;
            Password.Location = new Point(12, 116);
            Password.Name = "Password";
            Password.Size = new Size(81, 20);
            Password.TabIndex = 1;
            Password.Text = "Password : ";
            // 
            // txtUsername
            // 
            txtUsername.Location = new Point(145, 49);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(125, 27);
            txtUsername.TabIndex = 2;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(145, 113);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new Size(125, 27);
            txtPassword.TabIndex = 3;
            // 
            // btnGoToRegister
            // 
            btnGoToRegister.Location = new Point(12, 170);
            btnGoToRegister.Name = "btnGoToRegister";
            btnGoToRegister.Size = new Size(94, 29);
            btnGoToRegister.TabIndex = 4;
            btnGoToRegister.Text = "Register";
            btnGoToRegister.UseVisualStyleBackColor = true;
            // 
            // btnLogin
            // 
            btnLogin.Location = new Point(176, 170);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(94, 29);
            btnLogin.TabIndex = 5;
            btnLogin.Text = "Login";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += btnLogin_Click;
            // 
            // btnExit
            // 
            btnExit.Location = new Point(92, 212);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(94, 29);
            btnExit.TabIndex = 6;
            btnExit.Text = "Exit";
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += btnExit_Click;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(282, 253);
            Controls.Add(btnExit);
            Controls.Add(btnLogin);
            Controls.Add(btnGoToRegister);
            Controls.Add(txtPassword);
            Controls.Add(txtUsername);
            Controls.Add(Password);
            Controls.Add(Username);
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Login";
            ResumeLayout(false);
            PerformLayout();
        }

        private Label Username;
        private Label Password;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button btnGoToRegister;
        private Button btnExit;
        private Button btnLogin;
    }
}
