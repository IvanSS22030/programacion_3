using System;
using System.Drawing;
using System.Windows.Forms;
using ProyectoFinal.Services.Strategies;
using System.Collections.Generic;

namespace ProyectoFinal.Forms
{
    public class CombinedConverterForm : Form
    {
        private ComboBox cmbType;
        private TextBox txtValue;
        private Label lblResult;
        private Button btnConvert;
        private ListBox lstHistory;
        
        private List<string> history = new List<string>();

        public CombinedConverterForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Conversor Combinado";
            this.Size = new Size(400, 350);

            Label lblType = new Label { Text = "Tipo:", Location = new Point(20, 20), AutoSize = true };
            cmbType = new ComboBox { Location = new Point(100, 20), Width = 150 };
            cmbType.Items.Add("Kilogramos a Libras");
            cmbType.Items.Add("Libras a Kilogramos");
            cmbType.SelectedIndex = 0;

            Label lblValue = new Label { Text = "Valor:", Location = new Point(20, 60), AutoSize = true };
            txtValue = new TextBox { Location = new Point(100, 60), Width = 100 };

            btnConvert = new Button { Text = "Convertir", Location = new Point(20, 100), Width = 80 };
            btnConvert.Click += BtnConvert_Click;

            lblResult = new Label { Text = "Resultado: ", Location = new Point(120, 105), AutoSize = true };

            Label lblHistory = new Label { Text = "Historial (Últimos 5):", Location = new Point(20, 140), AutoSize = true };
            lstHistory = new ListBox { Location = new Point(20, 160), Width = 340, Height = 120 };

            this.Controls.Add(lblType);
            this.Controls.Add(cmbType);
            this.Controls.Add(lblValue);
            this.Controls.Add(txtValue);
            this.Controls.Add(btnConvert);
            this.Controls.Add(lblResult);
            this.Controls.Add(lblHistory);
            this.Controls.Add(lstHistory);
        }

        private void BtnConvert_Click(object sender, EventArgs e)
        {
            if (double.TryParse(txtValue.Text, out double val))
            {
                IConversionStrategy strategy = null;
                if (cmbType.SelectedIndex == 0) // Kg to Lb
                    strategy = new KgToLbStrategy();
                else
                    strategy = new LbToKgStrategy();

                // Simple validation for robustness
                if (val < 0)
                {
                    MessageBox.Show("El valor no puede ser negativo.");
                    return;
                }

                double res = strategy.Convert(val);
                lblResult.Text = $"Resultado: {res:F2} {strategy.ToUnit}";

                AddToHistory($"{DateTime.Now}: {val} {strategy.FromUnit} -> {res:F2} {strategy.ToUnit}");
            }
            else
            {
                MessageBox.Show("Ingrese un número válido.");
            }
        }

        private void AddToHistory(string entry)
        {
            history.Insert(0, entry);
            if (history.Count > 5) history.RemoveAt(5);
            
            lstHistory.Items.Clear();
            foreach (var item in history)
            {
                lstHistory.Items.Add(item);
            }
        }
    }
}

// Actualizaci�n de repositorio - 2026-04-08

// Optimize layout rendering
