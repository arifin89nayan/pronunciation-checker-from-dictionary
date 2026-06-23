using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.UIDesign;

namespace WindowsFormsApp1
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Application.Run(new MainForm());
            //Application.Run(new TestMain());
            //AppConfig config = AppConfig.Load();
            //AppState state = new AppState(config);
            Application.Run(new Inputtext());
            //Application.Run(new NewStartingForm());

        }
    }
}
