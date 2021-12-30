using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace fileName
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //String[] args = { @"E:\USER\videos\IDM\Forza Horizon 5 2021-12-15 22-56-40-1.m4v" };
            if (args.Length > 0)
            {
                Form1 f = new Form1();
                f.Args = args;
                Application.Run(f);
            }
            else
            {
                SecureFile.Form1 f = new SecureFile.Form1();
                Application.Run(f);
            }
            //Form1 f = new Form1();
            //f.Args = args;
            //Application.Run(f);



        }
    }
}
