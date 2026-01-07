using System;
using System.Drawing;
using System.Windows.Forms;
using ProyectoFinal.Services.Strategies;

namespace ProyectoFinal.Forms
{
    public class KgToLbForm : Form
    {
        private TextBox txtKg;
        private Label lblResult;
        private Button btnConvert;
        private Button btnReset;
        private IConversionStrategy _strategy;

        public KgToLbForm()
        {
            _strategy = new KgToLbStrategy();
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Conversor Kg a Lb";
            this.Size = new Size(300, 200);

            Label lblInput = new Label { Text = "Peso (kg):", Location = new Point(20, 20), AutoSize = true };
            txtKg = new TextBox { Location = new Point(100, 20), Width = 100 };
            
            btnConvert = new Button { Text = "Convertir", Location = new Point(20, 60), Width = 80 };
            btnConvert.Click += BtnConvert_Click;

            btnReset = new Button { Text = "Reiniciar", Location = new Point(110, 60), Width = 80 };
            btnReset.Click += BtnReset_Click;

            lblResult = new Label { Text = "Resultado: ", Location = new Point(20, 100), AutoSize = true };

            this.Controls.Add(lblInput);
            this.Controls.Add(txtKg);
            this.Controls.Add(btnConvert);
            this.Controls.Add(btnReset);
            this.Controls.Add(lblResult);
        }

        private void BtnConvert_Click(object sender, EventArgs e)
        {
            if (double.TryParse(txtKg.Text, out double val))
            {
                if (val < 0)
                {
                    MessageBox.Show("El valor debe ser positivo.");
                    return;
                }
                double res = _strategy.Convert(val);
                lblResult.Text = $"Resultado: {res:F2} lb";
            }
            else
            {
                MessageBox.Show("Ingrese un número válido.");
            }
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            txtKg.Clear();
            lblResult.Text = "Resultado: ";
        }
    }
}
