using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeBreak
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
            Application.Run(new Form1());
        }

        private static readonly object obj = new object();
        private static readonly Random rnd = new Random();

        public static int RandomNumber(int num)
        {
            lock (obj)
                return rnd.Next(num);
        }

        public static int RandomNumber(int min, int max)
        {
            lock (obj)
                return rnd.Next(min, max);
        }
    }
}
