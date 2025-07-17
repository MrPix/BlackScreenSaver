namespace BlackScreenSaver.App
{
    public class BlackForm : Form
    {
        public BlackForm()
        {
            BackColor = Color.Black;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            TopMost = true;

            // Hide the cursor
            Cursor.Hide();

            KeyDown += BlackForm_KeyDown;
            MouseClick += (s, e) =>
            {
                Cursor.Show();
                Close();
            };
            FormClosing += BlackForm_FormClosing;

            // Make sure the form can receive key events
            KeyPreview = true;
        }

        private void BlackForm_FormClosing(object? sender, FormClosingEventArgs e)
        {
            Cursor.Show();
        }

        private void BlackForm_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape || (e.Alt && e.KeyCode == Keys.F4))
            {
                Cursor.Show();
                Close();
            }
        }
    }
}