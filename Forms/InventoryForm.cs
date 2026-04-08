using System;
using System.Drawing;
using System.Windows.Forms;
using ProyectoFinal.Models;
using ProyectoFinal.Services;
using System.Collections.Generic;
using System.Globalization;

namespace ProyectoFinal.Forms
{
    public class InventoryForm : Form
    {
        private DataGridView dgvInventory;
        private TextBox txtCode, txtName, txtQty;
        private ComboBox cmbType;
        private TextBox txtDate;
        private Button btnCalendar;
        private Panel pnlCalendar;
        private DateTime _selectedDate;
        private DateTime _displayMonth;
        private Button btnAdd, btnRemove;
        private Label lblTotalEntries, lblTotalExits, lblNetStock;
        
        private InventoryManager _manager;
        private BindingSource _bindingSource;
        private CultureInfo _culture;

        // Spanish month and day names
        private readonly string[] _meses = { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", 
                                              "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
        private readonly string[] _diasCortos = { "Dom", "Lun", "Mar", "Mié", "Jue", "Vie", "Sáb" };
        private readonly string[] _diasLargos = { "domingo", "lunes", "martes", "miércoles", "jueves", "viernes", "sábado" };

        public InventoryForm()
        {
            _culture = new CultureInfo("es-DO");
            System.Threading.Thread.CurrentThread.CurrentCulture = _culture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = _culture;

            _manager = new InventoryManager();
            _bindingSource = new BindingSource();
            _selectedDate = DateTime.Now;
            _displayMonth = new DateTime(_selectedDate.Year, _selectedDate.Month, 1);
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Gestión de Inventario";
            this.Size = new Size(800, 600);

            // Input Fields
            Label lblCode = new Label { Text = "Código:", Location = new Point(20, 20), AutoSize = true };
            txtCode = new TextBox { Location = new Point(80, 20), Width = 100 };

            Label lblName = new Label { Text = "Nombre:", Location = new Point(200, 20), AutoSize = true };
            txtName = new TextBox { Location = new Point(260, 20), Width = 150 };

            Label lblType = new Label { Text = "Tipo:", Location = new Point(430, 20), AutoSize = true };
            cmbType = new ComboBox { Location = new Point(470, 20), Width = 100 };
            cmbType.Items.AddRange(new object[] { "Entrada", "Salida" });
            cmbType.SelectedIndex = 0;

            Label lblQty = new Label { Text = "Cantidad:", Location = new Point(20, 50), AutoSize = true };
            txtQty = new TextBox { Location = new Point(80, 50), Width = 100 };

            Label lblDate = new Label { Text = "Fecha:", Location = new Point(200, 50), AutoSize = true };
            
            // Custom Date Picker - WIDER
            txtDate = new TextBox { Location = new Point(260, 50), Width = 230, ReadOnly = true, BackColor = Color.White };
            btnCalendar = new Button { Text = "▼", Location = new Point(490, 49), Width = 25, Height = 23 };
            btnCalendar.Click += BtnCalendar_Click;
            
            // Calendar Panel (fully custom)
            pnlCalendar = new Panel { Location = new Point(260, 75), Size = new Size(260, 200), Visible = false, BorderStyle = BorderStyle.FixedSingle, BackColor = Color.White };
            BuildCalendar();
            
            UpdateDateText();
            
            btnAdd = new Button { Text = "Agregar", Location = new Point(530, 50), Width = 80 };
            btnAdd.Click += BtnAdd_Click;

            // Grid
            dgvInventory = new DataGridView { Location = new Point(20, 100), Size = new Size(740, 350) };
            dgvInventory.AutoGenerateColumns = true;
            dgvInventory.DataSource = _bindingSource;
            dgvInventory.CellDoubleClick += DgvInventory_CellDoubleClick;

            btnRemove = new Button { Text = "Eliminar Seleccionado", Location = new Point(20, 460), Width = 150 };
            btnRemove.Click += BtnRemove_Click;

            // Totals
            lblTotalEntries = new Label { Text = "Total Entradas: 0", Location = new Point(200, 465), AutoSize = true };
            lblTotalExits = new Label { Text = "Total Salidas: 0", Location = new Point(400, 465), AutoSize = true };
            lblNetStock = new Label { Text = "Stock Neto: 0", Location = new Point(600, 465), AutoSize = true, Font = new Font(this.Font, FontStyle.Bold) };

            this.Controls.Add(lblCode);
            this.Controls.Add(txtCode);
            this.Controls.Add(lblName);
            this.Controls.Add(txtName);
            this.Controls.Add(lblType);
            this.Controls.Add(cmbType);
            this.Controls.Add(lblQty);
            this.Controls.Add(txtQty);
            this.Controls.Add(lblDate);
            this.Controls.Add(txtDate);
            this.Controls.Add(btnCalendar);
            this.Controls.Add(pnlCalendar);
            this.Controls.Add(btnAdd);
            this.Controls.Add(dgvInventory);
            this.Controls.Add(btnRemove);
            this.Controls.Add(lblTotalEntries);
            this.Controls.Add(lblTotalExits);
            this.Controls.Add(lblNetStock);

            this.Click += (s, e) => pnlCalendar.Visible = false;
            dgvInventory.Click += (s, e) => pnlCalendar.Visible = false;

            RefreshGrid();
        }

        private void BuildCalendar()
        {
            pnlCalendar.Controls.Clear();
            
            // Header with month navigation
            Button btnPrev = new Button { Text = "<", Location = new Point(5, 5), Width = 30, Height = 25 };
            btnPrev.Click += (s, e) => { _displayMonth = _displayMonth.AddMonths(-1); BuildCalendar(); };
            
            Label lblMonth = new Label { 
                Text = $"{_meses[_displayMonth.Month - 1]} {_displayMonth.Year}", 
                Location = new Point(40, 8), 
                Width = 180, 
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font(this.Font, FontStyle.Bold)
            };
            
            Button btnNext = new Button { Text = ">", Location = new Point(225, 5), Width = 30, Height = 25 };
            btnNext.Click += (s, e) => { _displayMonth = _displayMonth.AddMonths(1); BuildCalendar(); };
            
            pnlCalendar.Controls.Add(btnPrev);
            pnlCalendar.Controls.Add(lblMonth);
            pnlCalendar.Controls.Add(btnNext);
            
            // Day headers
            int startX = 5;
            int startY = 35;
            int cellW = 36;
            int cellH = 22;
            
            for (int i = 0; i < 7; i++)
            {
                Label lbl = new Label { 
                    Text = _diasCortos[i], 
                    Location = new Point(startX + i * cellW, startY), 
                    Width = cellW, 
                    Height = cellH,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font(this.Font.FontFamily, 8, FontStyle.Bold)
                };
                pnlCalendar.Controls.Add(lbl);
            }
            
            // Days
            DateTime firstDay = new DateTime(_displayMonth.Year, _displayMonth.Month, 1);
            int startDayOfWeek = (int)firstDay.DayOfWeek;
            int daysInMonth = DateTime.DaysInMonth(_displayMonth.Year, _displayMonth.Month);
            
            int row = 0;
            int col = startDayOfWeek;
            
            for (int day = 1; day <= daysInMonth; day++)
            {
                int x = startX + col * cellW;
                int y = startY + cellH + row * cellH;
                
                Button btnDay = new Button { 
                    Text = day.ToString(), 
                    Location = new Point(x, y), 
                    Width = cellW - 2, 
                    Height = cellH - 2,
                    FlatStyle = FlatStyle.Flat,
                    Tag = day
                };
                btnDay.FlatAppearance.BorderSize = 0;
                
                // Highlight selected date
                if (_displayMonth.Year == _selectedDate.Year && 
                    _displayMonth.Month == _selectedDate.Month && 
                    day == _selectedDate.Day)
                {
                    btnDay.BackColor = Color.LightBlue;
                }
                
                // Highlight today
                if (_displayMonth.Year == DateTime.Now.Year && 
                    _displayMonth.Month == DateTime.Now.Month && 
                    day == DateTime.Now.Day)
                {
                    btnDay.FlatAppearance.BorderSize = 1;
                    btnDay.FlatAppearance.BorderColor = Color.Blue;
                }
                
                btnDay.Click += (s, e) => {
                    int selectedDay = (int)((Button)s).Tag;
                    _selectedDate = new DateTime(_displayMonth.Year, _displayMonth.Month, selectedDay);
                    UpdateDateText();
                    pnlCalendar.Visible = false;
                };
                
                pnlCalendar.Controls.Add(btnDay);
                
                col++;
                if (col > 6)
                {
                    col = 0;
                    row++;
                }
            }
        }

        private void UpdateDateText()
        {
            string dia = _diasLargos[(int)_selectedDate.DayOfWeek];
            string mes = _meses[_selectedDate.Month - 1].ToLower();
            txtDate.Text = $"{dia}, {_selectedDate.Day:00} de {mes} de {_selectedDate.Year}";
        }

        private void BtnCalendar_Click(object sender, EventArgs e)
        {
            _displayMonth = new DateTime(_selectedDate.Year, _selectedDate.Month, 1);
            BuildCalendar();
            pnlCalendar.Visible = !pnlCalendar.Visible;
            pnlCalendar.BringToFront();
        }

        private void DgvInventory_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var item = dgvInventory.Rows[e.RowIndex].DataBoundItem as InventoryItem;
                if (item != null)
                {
                    txtCode.Text = item.Code;
                    txtName.Text = item.Name;
                    txtQty.Text = item.Quantity.ToString();
                    cmbType.SelectedItem = item.Type;
                    _selectedDate = item.Date;
                    UpdateDateText();
                }
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtQty.Text, out int qty) || qty <= 0)
            {
                MessageBox.Show("La cantidad debe ser un entero positivo.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtCode.Text) || string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Código y Nombre son obligatorios.");
                return;
            }

            var item = new InventoryItem
            {
                Code = txtCode.Text,
                Name = txtName.Text,
                Type = cmbType.SelectedItem.ToString(),
                Quantity = qty,
                Date = _selectedDate
            };

            try
            {
                _manager.AddItem(item);
                RefreshGrid();
                ClearInputs();
                MessageBox.Show("Guardado correctamente en Base de Datos.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar en BD: {ex.Message}\n\nAsegúrese de que la base de datos 'pacientesdb' existe y la tabla 'Inventario' está creada.");
            }
        }

        private void BtnRemove_Click(object sender, EventArgs e)
        {
            if (dgvInventory.SelectedRows.Count > 0)
            {
                var item = dgvInventory.SelectedRows[0].DataBoundItem as InventoryItem;
                if (item != null)
                {
                    _manager.RemoveItem(item);
                    RefreshGrid();
                }
            }
            else if (dgvInventory.CurrentRow != null)
            {
                 var item = dgvInventory.CurrentRow.DataBoundItem as InventoryItem;
                 if (item != null)
                {
                    _manager.RemoveItem(item);
                    RefreshGrid();
                }
            }
        }

        private void RefreshGrid()
        {
            _bindingSource.DataSource = null;
            _bindingSource.DataSource = _manager.GetItems();
            
            lblTotalEntries.Text = $"Total Entradas: {_manager.GetTotalEntries()}";
            lblTotalExits.Text = $"Total Salidas: {_manager.GetTotalExits()}";
            lblNetStock.Text = $"Stock Neto: {_manager.GetNetStock()}";
        }

        private void ClearInputs()
        {
            txtCode.Clear();
            txtName.Clear();
            txtQty.Clear();
            cmbType.SelectedIndex = 0;
            _selectedDate = DateTime.Now;
            UpdateDateText();
        }
    }
}

// Actualizaci�n de repositorio - 2026-04-08
