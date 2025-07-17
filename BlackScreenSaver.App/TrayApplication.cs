using Microsoft.Win32;
using System.Runtime.InteropServices;

namespace BlackScreenSaver.App
{
    public class TrayApplication : Form
    {
        private NotifyIcon? trayIcon;
        private ContextMenuStrip? trayMenu;
        private const int WM_HOTKEY = 0x0312;
        private const int HOTKEY_ID = 1;

        // P/Invoke declarations for hotkey registration
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        // Hotkey modifiers
        private const int MOD_CONTROL = 0x0002;

        public TrayApplication()
        {
            InitializeTrayIcon();
            RegisterHotkeys();

            // Hide the main form
            WindowState = FormWindowState.Minimized;
            ShowInTaskbar = false;
            Visible = false;
        }

        private void InitializeTrayIcon()
        {
            // Create tray menu
            trayMenu = new ContextMenuStrip();
            trayMenu.Items.Add("Run Black Screen", null, OnRunBlackScreen);
            trayMenu.Items.Add("-"); // Separator

            // Add dynamic startup menu item
            var startupItem = new ToolStripMenuItem("Add to Startup", null, OnAddToStartup);
            UpdateStartupMenuText(startupItem);
            trayMenu.Items.Add(startupItem);

            trayMenu.Items.Add("-"); // Separator
            trayMenu.Items.Add("Exit", null, OnExit);

            // Create tray icon using a simple black square icon
            var iconBitmap = CreateBlackIcon();

            trayIcon = new NotifyIcon()
            {
                Text = "Black Screen Saver (Ctrl+F12 to activate)",
                Icon = Icon.FromHandle(iconBitmap.GetHicon()),
                ContextMenuStrip = trayMenu,
                Visible = true
            };

            trayIcon.DoubleClick += (s, e) => OnRunBlackScreen(s, e);
        }

        private Bitmap CreateBlackIcon()
        {
            // Create a simple 16x16 black square icon
            var bitmap = new Bitmap(16, 16);
            using (var g = Graphics.FromImage(bitmap))
            {
                g.FillRectangle(Brushes.Black, 0, 0, 16, 16);
                g.DrawRectangle(Pens.Gray, 0, 0, 15, 15);
            }
            return bitmap;
        }

        private void UpdateStartupMenuText(ToolStripMenuItem menuItem)
        {
            bool isInStartup = IsInStartup();
            menuItem.Text = isInStartup ? "Remove from Startup" : "Add to Startup";
        }

        private bool IsInStartup()
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", false)!)
                {
                    return key?.GetValue("BlackScreenSaver") != null;
                }
            }
            catch
            {
                return false;
            }
        }

        private void RegisterHotkeys()
        {
            // Register Ctrl+F12 hotkey
            RegisterHotKey(this.Handle, HOTKEY_ID, MOD_CONTROL, (int)Keys.F12);
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == WM_HOTKEY && m.WParam.ToInt32() == HOTKEY_ID)
            {
                OnRunBlackScreen(this, EventArgs.Empty);
            }
        }

        private void OnRunBlackScreen(object? sender, EventArgs e)
        {
            var blackForm = new BlackForm();
            Cursor.Hide();
            blackForm.ShowDialog();
        }

        private void OnAddToStartup(object? sender, EventArgs e)
        {
            try
            {
                string appName = "BlackScreenSaver";
                string appPath = Application.ExecutablePath;

                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true)!)
                {
                    if (key.GetValue(appName) == null)
                    {
                        key.SetValue(appName, appPath);
                        MessageBox.Show("Application added to startup successfully!\nIt will start automatically when Windows starts.", "Startup",
                                      MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        key.DeleteValue(appName);
                        MessageBox.Show("Application removed from startup successfully!", "Startup",
                                      MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    // Update menu text
                    if (sender is ToolStripMenuItem menuItem)
                    {
                        UpdateStartupMenuText(menuItem);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error modifying startup: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnExit(object? sender, EventArgs e)
        {
            // Cleanup
            UnregisterHotKey(this.Handle, HOTKEY_ID);
            trayIcon?.Dispose();
            Cursor.Show();
            Application.Exit();
        }

        protected override void SetVisibleCore(bool value)
        {
            base.SetVisibleCore(false);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                UnregisterHotKey(this.Handle, HOTKEY_ID);
                trayIcon?.Dispose();
                trayMenu?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}