using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoFinal
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Set culture to Spanish
            var culture = new System.Globalization.CultureInfo("es-ES");
            System.Globalization.CultureInfo.DefaultThreadCurrentCulture = culture;
            System.Globalization.CultureInfo.DefaultThreadCurrentUICulture = culture;
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = culture;

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Forms.MainForm());
        }
    }
}

// Actualización de repositorio - 2026-04-08
