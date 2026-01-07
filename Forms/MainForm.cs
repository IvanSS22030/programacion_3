using System;
using System.Drawing;
using System.Windows.Forms;

namespace ProyectoFinal.Forms
{
    public class MainForm : Form
    {
        public MainForm()
        {
            var culture = new System.Globalization.CultureInfo("es-DO");
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = culture;

            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Proyecto Final - Menú Principal";
            this.Size = new Size(600, 400);
            this.IsMdiContainer = true; // Optional: Open forms inside or as dialogs. I'll open as dialogs or new windows for simplicity.

            MenuStrip menu = new MenuStrip();
            
            ToolStripMenuItem menuFile = new ToolStripMenuItem("Archivo");
            menuFile.DropDownItems.Add("Salir", null, (s, e) => Application.Exit());

            ToolStripMenuItem menuConverters = new ToolStripMenuItem("Conversores");
            menuConverters.DropDownItems.Add("Kilogramos a Libras", null, (s, e) => new KgToLbForm().Show());
            menuConverters.DropDownItems.Add("Libras a Kilogramos", null, (s, e) => new LbToKgForm().Show());
            menuConverters.DropDownItems.Add("Conversor Combinado", null, (s, e) => new CombinedConverterForm().Show());

            ToolStripMenuItem menuInventory = new ToolStripMenuItem("Inventario");
            menuInventory.DropDownItems.Add("Gestión de Inventario", null, (s, e) => new InventoryForm().Show());

            ToolStripMenuItem menuPacientes = new ToolStripMenuItem("Pacientes");
            menuPacientes.DropDownItems.Add("Gestión de Pacientes", null, (s, e) => new PacientesForm().Show());

            menu.Items.Add(menuFile);
            menu.Items.Add(menuConverters);
            menu.Items.Add(menuInventory);
            menu.Items.Add(menuPacientes);

            this.Controls.Add(menu);
            this.MainMenuStrip = menu;
        }
    }
}
