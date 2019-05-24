using LeafSQL.Library.Payloads.Responses;
using System;
using System.Windows.Forms;

namespace LeafSQL.UI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Forms.FormMain());
        }

        public static void AsyncResultMessage(IActionResponse result, string defaultMessage)
        {
            string message = result == null || (result.Message == null || result.Message == string.Empty) ? defaultMessage : result.Message;
            MessageBox.Show(message, "LeafSQL", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
    }
}
