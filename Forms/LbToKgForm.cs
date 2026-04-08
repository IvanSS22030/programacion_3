using System;
using System.Drawing;
using System.Windows.Forms;
using ProyectoFinal.Services.Strategies;

namespace ProyectoFinal.Forms
{
    public class LbToKgForm : Form
    {
        private TextBox txtLb;
        private Label lblResult;
        private Button btnConvert;
        private Button btnReset;
        private IConversionStrategy _strategy;

        public LbToKgForm()
        {
            _strategy = new LbToKgStrategy();
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Conversor Lb a Kg";
            this.Size = new Size(300, 200);

            Label lblInput = new Label { Text = "Peso (lb):", Location = new Point(20, 20), AutoSize = true };
            txtLb = new TextBox { Location = new Point(100, 20), Width = 100 };
            
            btnConvert = new Button { Text = "Convertir", Location = new Point(20, 60), Width = 80 };
            btnConvert.Click += BtnConvert_Click;

            btnReset = new Button { Text = "Reiniciar", Location = new Point(110, 60), Width = 80 };
            btnReset.Click += BtnReset_Click;

            lblResult = new Label { Text = "Resultado: ", Location = new Point(20, 100), AutoSize = true };

            this.Controls.Add(lblInput);
            this.Controls.Add(txtLb);
            this.Controls.Add(btnConvert);
            this.Controls.Add(btnReset);
            this.Controls.Add(lblResult);
        }

        private void BtnConvert_Click(object sender, EventArgs e)
        {
            if (double.TryParse(txtLb.Text, out double val))
            {
                if (val < 0 || val > 10000)
                {
                    MessageBox.Show("El valor debe estar entre 0 y 10,000 libras.");
                    return;
                }
                double res = _strategy.Convert(val);
                lblResult.Text = $"Resultado: {res:F2} kg";
            }
            else
            {
                MessageBox.Show("Ingrese un número válido.");
            }
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            txtLb.Clear();
            lblResult.Text = "Resultado: ";
        }
    }
}

// Actualizaci�n de repositorio - 2026-04-08

// Optimize layout rendering
