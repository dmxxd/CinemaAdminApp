using CinemaAdminApp.Logic;
using CinemaAdminApp.Models;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace CinemaAdminApp.Forms
{
    public partial class InventoryManagerForm : Form
    {
        private InventoryManager _manager = new InventoryManager();
        private int _selectedItemId = -1;

        public InventoryManagerForm()
        {
            InitializeComponent();
            ConfigureAppearance();
            SetupForm();
        }

        private void ConfigureAppearance()
        {
            this.BackColor = System.Drawing.Color.White;
            this.Font = new System.Drawing.Font("Segoe UI", 9F);

            dgvInventory.BackgroundColor = System.Drawing.Color.White;
            dgvInventory.BorderStyle = BorderStyle.None;
            dgvInventory.GridColor = System.Drawing.Color.FromArgb(240, 240, 240);
            dgvInventory.RowHeadersVisible = false;
            dgvInventory.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(248, 248, 248);
            dgvInventory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvInventory.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(0, 123, 255);
            dgvInventory.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;

            txtName.BorderStyle = BorderStyle.FixedSingle;
            txtUnit.BorderStyle = BorderStyle.FixedSingle;
            txtId.BorderStyle = BorderStyle.None;
            txtId.BackColor = System.Drawing.Color.FromArgb(248, 248, 248);

            btnCreate.BackColor = System.Drawing.Color.FromArgb(40, 167, 69);
            btnCreate.ForeColor = System.Drawing.Color.White;
            btnCreate.FlatStyle = FlatStyle.Flat;
            btnCreate.FlatAppearance.BorderSize = 0;

            btnUpdate.BackColor = System.Drawing.Color.FromArgb(0, 123, 255);
            btnUpdate.ForeColor = System.Drawing.Color.White;
            btnUpdate.FlatStyle = FlatStyle.Flat;
            btnUpdate.FlatAppearance.BorderSize = 0;

            btnDelete.BackColor = System.Drawing.Color.FromArgb(220, 53, 69);
            btnDelete.ForeColor = System.Drawing.Color.White;
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.FlatAppearance.BorderSize = 0;

            btnBuy.BackColor = System.Drawing.Color.FromArgb(40, 167, 69);
            btnBuy.ForeColor = System.Drawing.Color.White;
            btnBuy.FlatStyle = FlatStyle.Flat;
            btnBuy.FlatAppearance.BorderSize = 0;

            btnSell.BackColor = System.Drawing.Color.FromArgb(0, 123, 255);
            btnSell.ForeColor = System.Drawing.Color.White;
            btnSell.FlatStyle = FlatStyle.Flat;
            btnSell.FlatAppearance.BorderSize = 0;

            groupBox1.Font = new System.Drawing.Font("Segoe UI", 9F, FontStyle.Bold);
            groupBox2.Font = new System.Drawing.Font("Segoe UI", 9F, FontStyle.Bold);

            lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.ForeColor = System.Drawing.Color.White;
            panelHeader.BackColor = System.Drawing.Color.FromArgb(0, 51, 102);
        }

        private void SetupForm()
        {
            this.Text = "Управление запасами";
            ClearInputFields();
            LoadInventoryData();
        }

        private void InventoryManagerForm_Load(object sender, EventArgs e)
        {
            LoadInventoryData();
        }

        private void LoadInventoryData()
        {
            try
            {
                var items = _manager.GetAllItems();
                dgvInventory.DataSource = null;

                var bindingList = new System.ComponentModel.BindingList<InventoryItem>(items);
                var bindingSource = new BindingSource();
                bindingSource.DataSource = bindingList;
                dgvInventory.DataSource = bindingSource;

                SetupDataGridViewColumns();
                dgvInventory.ClearSelection();
                ClearInputFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupDataGridViewColumns()
        {
            if (dgvInventory.Columns.Count == 0) return;

            dgvInventory.Columns.Clear();

            DataGridViewTextBoxColumn colId = new DataGridViewTextBoxColumn();
            colId.Name = "Id";
            colId.DataPropertyName = "Id";
            colId.HeaderText = "ID";
            colId.Width = 50;
            colId.Visible = false;

            DataGridViewTextBoxColumn colName = new DataGridViewTextBoxColumn();
            colName.Name = "Name";
            colName.DataPropertyName = "Name";
            colName.HeaderText = "Название";
            colName.Width = 150;

            DataGridViewTextBoxColumn colUnit = new DataGridViewTextBoxColumn();
            colUnit.Name = "UnitOfMeasure";
            colUnit.DataPropertyName = "UnitOfMeasure";
            colUnit.HeaderText = "Ед. изм.";
            colUnit.Width = 70;

            DataGridViewTextBoxColumn colCostPrice = new DataGridViewTextBoxColumn();
            colCostPrice.Name = "CostPrice";
            colCostPrice.DataPropertyName = "CostPrice";
            colCostPrice.HeaderText = "Себестоимость";
            colCostPrice.Width = 100;
            colCostPrice.DefaultCellStyle.Format = "C2";

            DataGridViewTextBoxColumn colSalePrice = new DataGridViewTextBoxColumn();
            colSalePrice.Name = "SalePrice";
            colSalePrice.DataPropertyName = "SalePrice";
            colSalePrice.HeaderText = "Цена продажи";
            colSalePrice.Width = 100;
            colSalePrice.DefaultCellStyle.Format = "C2";

            DataGridViewTextBoxColumn colCurrentStock = new DataGridViewTextBoxColumn();
            colCurrentStock.Name = "CurrentStock";
            colCurrentStock.DataPropertyName = "CurrentStock";
            colCurrentStock.HeaderText = "Остаток";
            colCurrentStock.Width = 80;
            colCurrentStock.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvInventory.Columns.AddRange(new DataGridViewColumn[] {
                colId, colName, colUnit, colCostPrice, colSalePrice, colCurrentStock
            });
        }

        private void ClearInputFields()
        {
            _selectedItemId = -1;
            txtId.Clear();
            txtName.Clear();
            txtUnit.Clear();
            nudCostPrice.Value = 0;
            nudSalePrice.Value = 0;
            lblCurrentStock.Text = "---";
            nudQuantityChange.Value = 1;
            UpdateButtonStates();
        }

        private void UpdateButtonStates()
        {
            bool hasSelection = _selectedItemId != -1;

            btnCreate.Enabled = true;
            btnUpdate.Enabled = hasSelection;
            btnDelete.Enabled = hasSelection;
            btnBuy.Enabled = hasSelection;
            btnSell.Enabled = hasSelection;
        }

        private void dgvInventory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvInventory.RowCount)
            {
                try
                {
                    DataGridViewRow row = dgvInventory.Rows[e.RowIndex];

                    if (row.Cells["Id"].Value != null)
                    {
                        _selectedItemId = Convert.ToInt32(row.Cells["Id"].Value);
                        txtId.Text = _selectedItemId.ToString();
                        txtName.Text = row.Cells["Name"].Value?.ToString() ?? "";
                        txtUnit.Text = row.Cells["UnitOfMeasure"].Value?.ToString() ?? "";

                        if (row.Cells["CostPrice"].Value != null)
                            nudCostPrice.Value = Convert.ToDecimal(row.Cells["CostPrice"].Value);

                        if (row.Cells["SalePrice"].Value != null)
                            nudSalePrice.Value = Convert.ToDecimal(row.Cells["SalePrice"].Value);

                        if (row.Cells["CurrentStock"].Value != null)
                        {
                            int stock = Convert.ToInt32(row.Cells["CurrentStock"].Value);
                            lblCurrentStock.Text = stock.ToString();
                            lblCurrentStock.ForeColor = stock > 10 ?
                                System.Drawing.Color.Green :
                                (stock > 0 ? System.Drawing.Color.Orange : System.Drawing.Color.Red);
                        }

                        UpdateButtonStates();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка выбора товара: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private InventoryItem GetItemFromInput()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Введите название товара", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return null;
            }

            if (string.IsNullOrWhiteSpace(txtUnit.Text))
            {
                MessageBox.Show("Введите единицу измерения", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUnit.Focus();
                return null;
            }

            if (nudCostPrice.Value <= 0)
            {
                MessageBox.Show("Себестоимость должна быть больше 0", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nudCostPrice.Focus();
                return null;
            }

            if (nudSalePrice.Value <= 0)
            {
                MessageBox.Show("Цена продажи должна быть больше 0", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nudSalePrice.Focus();
                return null;
            }

            return new InventoryItem
            {
                Id = _selectedItemId,
                Name = txtName.Text.Trim(),
                UnitOfMeasure = txtUnit.Text.Trim(),
                CostPrice = nudCostPrice.Value,
                SalePrice = nudSalePrice.Value,
                CurrentStock = 0
            };
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                var newItem = GetItemFromInput();
                if (newItem == null) return;

                newItem.Id = 0;

                if (_manager.CreateItem(newItem))
                {
                    MessageBox.Show("Товар успешно создан", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadInventoryData();
                }
                else
                {
                    MessageBox.Show("Ошибка при создании товара. Возможно, товар с таким названием уже существует.",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка создания товара: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_selectedItemId == -1) return;

            try
            {
                var updatedItem = GetItemFromInput();
                if (updatedItem == null) return;

                updatedItem.Id = _selectedItemId;

                if (_manager.UpdateItem(updatedItem))
                {
                    MessageBox.Show("Товар успешно обновлен", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadInventoryData();
                }
                else
                {
                    MessageBox.Show("Ошибка при обновлении товара", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка обновления товара: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedItemId == -1) return;

            try
            {
                var result = MessageBox.Show("Удалить выбранный товар?", "Подтверждение удаления",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                if (result == DialogResult.Yes)
                {
                    if (_manager.DeleteItem(_selectedItemId))
                    {
                        MessageBox.Show("Товар успешно удален", "Успех",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadInventoryData();
                    }
                    else
                    {
                        MessageBox.Show("Ошибка при удалении товара", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка удаления товара: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBuy_Click(object sender, EventArgs e)
        {
            if (_selectedItemId == -1)
            {
                MessageBox.Show("Выберите товар", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int quantity = (int)nudQuantityChange.Value;
                if (quantity <= 0)
                {
                    MessageBox.Show("Количество должно быть больше 0", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (_manager.AddStock(_selectedItemId, quantity))
                {
                    MessageBox.Show($"Успешно закуплено {quantity} ед. товара", "Закупка",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadInventoryData();
                }
                else
                {
                    MessageBox.Show("Ошибка при закупке товара", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка закупки: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSell_Click(object sender, EventArgs e)
        {
            if (_selectedItemId == -1)
            {
                MessageBox.Show("Выберите товар", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int quantity = (int)nudQuantityChange.Value;
                if (quantity <= 0)
                {
                    MessageBox.Show("Количество должно быть больше 0", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var item = _manager.GetItemById(_selectedItemId);
                if (item != null && item.CurrentStock < quantity)
                {
                    MessageBox.Show($"Недостаточно товара на складе. Доступно: {item.CurrentStock} ед.",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (_manager.SellStock(_selectedItemId, quantity))
                {
                    decimal totalAmount = quantity * item?.SalePrice ?? 0;
                    MessageBox.Show($"Успешно продано {quantity} ед. товара\nОбщая сумма: {totalAmount:C}",
                        "Продажа", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadInventoryData();
                }
                else
                {
                    MessageBox.Show("Ошибка при продаже товара", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка продажи: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void nudSalePrice_ValueChanged(object sender, EventArgs e)
        {
            if (nudSalePrice.Value < nudCostPrice.Value)
            {
                nudSalePrice.Value = nudCostPrice.Value;
                MessageBox.Show("Цена продажи не может быть меньше себестоимости", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void nudCostPrice_ValueChanged(object sender, EventArgs e)
        {
            if (nudSalePrice.Value < nudCostPrice.Value)
            {
                nudSalePrice.Value = nudCostPrice.Value;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearInputFields();
            dgvInventory.ClearSelection();
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }
    }
}
