namespace MusicApp
{
    public static class Prompt
    {
        public static string ShowDialog(string text, string caption)
        {
            using (Form prompt = new Form())
            {
                prompt.Width = 400;
                prompt.Height = 150;
                prompt.Text = caption;
                prompt.StartPosition = FormStartPosition.CenterScreen;

                Label lblText = new Label { Left = 20, Top = 20, Text = text, AutoSize = true };
                TextBox txtInput = new TextBox { Left = 20, Top = 50, Width = 340 };
                Button btnOk = new Button { Text = "OK", Left = 270, Top = 80, Width = 90 };

                btnOk.Click += (sender, e) => { prompt.DialogResult = DialogResult.OK; prompt.Close(); };

                prompt.Controls.Add(lblText);
                prompt.Controls.Add(txtInput);
                prompt.Controls.Add(btnOk);
                prompt.AcceptButton = btnOk;

                return prompt.ShowDialog() == DialogResult.OK ? txtInput.Text.Trim() : string.Empty;
            }
        }
    }
}
