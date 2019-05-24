using System;
using System.Text;
using System.Threading.Tasks;
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

        public static void AsyncExceptionMessage(Task task, string defaultMessage = "One or more excpetions occured.")
        {
            var stringBuilder = new StringBuilder();

            foreach (var exception in task.Exception.InnerExceptions)
            {
                stringBuilder.AppendLine(exception.Message);
            }

            if (stringBuilder.Length == 0)
            {
                stringBuilder.Append(defaultMessage);
            }

            MessageBox.Show(stringBuilder.ToString(), "LeafSQL", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
    }
}
