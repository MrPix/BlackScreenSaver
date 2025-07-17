namespace BlackScreenSaver.App
{

    internal static class Program
    {
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Run the tray application instead of the black form directly
            Application.Run(new TrayApplication());
        }
    }
}