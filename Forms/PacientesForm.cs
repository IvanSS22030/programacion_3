using System;
using System.Drawing;
using System.Windows.Forms;
using ProyectoFinal.Models;
using ProyectoFinal.Services;

namespace ProyectoFinal.Forms
{
    public class PacientesForm : Form
    {
        private DataGridView dgvPacientes;
        private TextBox txtNombre, txtApellido, txtDireccion, txtTelefono;
        private ComboBox cmbEstatus;
        private Button btnAgregar, btnActualizar, btnEliminar, btnLimpiar;
        private Label lblCodigo;
        
        private PacientesManager _manager;
        private BindingSource _bindingSource;
        private int? _selectedCodigo = null;

        public PacientesForm()
        {
            _manager = new PacientesManager();
            _bindingSource = new BindingSource();
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Gestión de Pacientes";
            this.Size = new Size(850, 550);

            // Input Fields
            int y = 20;
            int labelWidth = 80;
            int inputWidth = 150;
            int gap = 20;

            Label lbl1 = new Label { Text = "Código:", Location = new Point(20, y), Width = labelWidth };
            lblCodigo = new Label { Text = "(Nuevo)", Location = new Point(20 + labelWidth, y), Width = 80, Font = new Font(this.Font, FontStyle.Bold) };

            y += 30;
            Label lbl2 = new Label { Text = "Nombre:", Location = new Point(20, y), Width = labelWidth };
            txtNombre = new TextBox { Location = new Point(20 + labelWidth, y), Width = inputWidth };

            Label lbl3 = new Label { Text = "Apellido:", Location = new Point(280, y), Width = labelWidth };
            txtApellido = new TextBox { Location = new Point(280 + labelWidth, y), Width = inputWidth };

            y += 30;
            Label lbl4 = new Label { Text = "Dirección:", Location = new Point(20, y), Width = labelWidth };
            txtDireccion = new TextBox { Location = new Point(20 + labelWidth, y), Width = 320 };

            y += 30;
            Label lbl5 = new Label { Text = "Teléfono:", Location = new Point(20, y), Width = labelWidth };
            txtTelefono = new TextBox { Location = new Point(20 + labelWidth, y), Width = inputWidth };

            Label lbl6 = new Label { Text = "Estatus:", Location = new Point(280, y), Width = labelWidth };
            cmbEstatus = new ComboBox { Location = new Point(280 + labelWidth, y), Width = 100 };
            cmbEstatus.Items.AddRange(new object[] { "Activo", "Inactivo" });
            cmbEstatus.SelectedIndex = 0;

            // Buttons
            y += 40;
            btnAgregar = new Button { Text = "Agregar", Location = new Point(20, y), Width = 80 };
            btnAgregar.Click += BtnAgregar_Click;

            btnActualizar = new Button { Text = "Actualizar", Location = new Point(110, y), Width = 80, Enabled = false };
            btnActualizar.Click += BtnActualizar_Click;

            btnEliminar = new Button { Text = "Eliminar", Location = new Point(200, y), Width = 80, Enabled = false };
            btnEliminar.Click += BtnEliminar_Click;

            btnLimpiar = new Button { Text = "Limpiar", Location = new Point(290, y), Width = 80 };
            btnLimpiar.Click += BtnLimpiar_Click;

            // Grid
            y += 40;
            dgvPacientes = new DataGridView { Location = new Point(20, y), Size = new Size(790, 300) };
            dgvPacientes.AutoGenerateColumns = true;
            dgvPacientes.DataSource = _bindingSource;
            dgvPacientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPacientes.CellDoubleClick += DgvPacientes_CellDoubleClick;

            // Add controls
            this.Controls.Add(lbl1);
            this.Controls.Add(lblCodigo);
            this.Controls.Add(lbl2);
            this.Controls.Add(txtNombre);
            this.Controls.Add(lbl3);
            this.Controls.Add(txtApellido);
            this.Controls.Add(lbl4);
            this.Controls.Add(txtDireccion);
            this.Controls.Add(lbl5);
            this.Controls.Add(txtTelefono);
            this.Controls.Add(lbl6);
            this.Controls.Add(cmbEstatus);
            this.Controls.Add(btnAgregar);
            this.Controls.Add(btnActualizar);
            this.Controls.Add(btnEliminar);
            this.Controls.Add(btnLimpiar);
            this.Controls.Add(dgvPacientes);

            RefreshGrid();
        }

        private void DgvPacientes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var p = dgvPacientes.Rows[e.RowIndex].DataBoundItem as Paciente;
                if (p != null)
                {
                    _selectedCodigo = p.CodigoPac;
                    lblCodigo.Text = p.CodigoPac.ToString();
                    txtNombre.Text = p.NombrePac;
                    txtApellido.Text = p.ApellidoPac;
                    txtDireccion.Text = p.DireccionPac;
                    txtTelefono.Text = p.TelefonoPac;
                    cmbEstatus.SelectedItem = p.EstatusPac;

                    btnAgregar.Enabled = false;
                    btnActualizar.Enabled = true;
                    btnEliminar.Enabled = true;
                }
            }
        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtApellido.Text))
            {
                MessageBox.Show("Nombre y Apellido son obligatorios.");
                return;
            }

            var p = new Paciente
            {
                NombrePac = txtNombre.Text,
                ApellidoPac = txtApellido.Text,
                DireccionPac = txtDireccion.Text,
                TelefonoPac = txtTelefono.Text,
                EstatusPac = cmbEstatus.SelectedItem?.ToString() ?? "Activo"
            };

            try
            {
                _manager.AddPaciente(p);
                RefreshGrid();
                ClearInputs();
                MessageBox.Show("Paciente agregado correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void BtnActualizar_Click(object sender, EventArgs e)
        {
            if (_selectedCodigo == null)
            {
                MessageBox.Show("Seleccione un paciente para actualizar.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtApellido.Text))
            {
                MessageBox.Show("Nombre y Apellido son obligatorios.");
                return;
            }

            var p = new Paciente
            {
                CodigoPac = _selectedCodigo.Value,
                NombrePac = txtNombre.Text,
                ApellidoPac = txtApellido.Text,
                DireccionPac = txtDireccion.Text,
                TelefonoPac = txtTelefono.Text,
                EstatusPac = cmbEstatus.SelectedItem?.ToString() ?? "Activo"
            };

            try
            {
                _manager.UpdatePaciente(p);
                RefreshGrid();
                ClearInputs();
                MessageBox.Show("Paciente actualizado correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (_selectedCodigo == null)
            {
                MessageBox.Show("Seleccione un paciente para eliminar.");
                return;
            }

            var result = MessageBox.Show("¿Está seguro de eliminar este paciente?", "Confirmar", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                try
                {
                    _manager.DeletePaciente(_selectedCodigo.Value);
                    RefreshGrid();
                    ClearInputs();
                    MessageBox.Show("Paciente eliminado correctamente.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        private void BtnLimpiar_Click(object sender, EventArgs e)
        {
            ClearInputs();
        }

        private void RefreshGrid()
        {
            try
            {
                _bindingSource.DataSource = null;
                _bindingSource.DataSource = _manager.GetAllPacientes();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar pacientes: {ex.Message}");
            }
        }

        private void ClearInputs()
        {
            _selectedCodigo = null;
            lblCodigo.Text = "(Nuevo)";
            txtNombre.Clear();
            txtApellido.Clear();
            txtDireccion.Clear();
            txtTelefono.Clear();
            cmbEstatus.SelectedIndex = 0;

            btnAgregar.Enabled = true;
            btnActualizar.Enabled = false;
            btnEliminar.Enabled = false;
        }
    }
}

// Actualizaci�n de repositorio - 2026-04-08
