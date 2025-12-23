using CinemaAdminApp.Logic;
using CinemaAdminApp.Models;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CinemaAdminApp.Forms
{
    public partial class PersonalManagerForm : Form
    {
        private readonly PersonalManager _manager = new PersonalManager();
        private Personal _selectedPersonal;

        public PersonalManagerForm()
        {
            InitializeComponent();
            ConfigureAppearance();
            SetupDataGridView();
            SetupEventHandlers();
        }

        private void PersonalManagerForm_Load(object sender, EventArgs e)
        {
            LoadPersonalData();
            ClearInputFields();
            UpdateStatistics();
        }

        private void ConfigureAppearance()
        {
            this.BackColor = System.Drawing.Color.White;
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MinimumSize = new Size(900, 600);

            panelHeader.BackColor = System.Drawing.Color.FromArgb(0, 51, 102);
            lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.ForeColor = System.Drawing.Color.White;

            dgvPersonnel.BackgroundColor = System.Drawing.Color.White;
            dgvPersonnel.BorderStyle = BorderStyle.FixedSingle;
            dgvPersonnel.GridColor = System.Drawing.Color.FromArgb(240, 240, 240);
            dgvPersonnel.RowHeadersVisible = false;
            dgvPersonnel.AllowUserToAddRows = false;
            dgvPersonnel.AllowUserToDeleteRows = false;
            dgvPersonnel.AllowUserToResizeRows = false;
            dgvPersonnel.ReadOnly = true;
            dgvPersonnel.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPersonnel.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            dgvPersonnel.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            dgvPersonnel.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            dgvPersonnel.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(248, 248, 248);
            dgvPersonnel.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(0, 123, 255);
            dgvPersonnel.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;

            dgvPersonnel.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            dgvPersonnel.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(33, 37, 41);
            dgvPersonnel.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvPersonnel.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvPersonnel.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgvPersonnel.ColumnHeadersHeight = 35;
            dgvPersonnel.EnableHeadersVisualStyles = false;

            groupBoxInput.Font = new System.Drawing.Font("Segoe UI", 9F, FontStyle.Bold);
            groupBoxInput.ForeColor = System.Drawing.Color.FromArgb(52, 58, 64);

            groupBoxStats.Font = new System.Drawing.Font("Segoe UI", 9F, FontStyle.Bold);
            groupBoxStats.ForeColor = System.Drawing.Color.FromArgb(52, 58, 64);

            foreach (Control control in groupBoxInput.Controls)
            {
                if (control is TextBox textBox)
                {
                    textBox.BackColor = System.Drawing.Color.White;
                    textBox.ForeColor = System.Drawing.Color.Black;
                    textBox.BorderStyle = BorderStyle.FixedSingle;
                    textBox.Font = new System.Drawing.Font("Segoe UI", 9F);
                }
            }

            txtId.BackColor = System.Drawing.Color.FromArgb(248, 248, 248);
            txtId.ForeColor = System.Drawing.Color.FromArgb(108, 117, 125);
            txtId.ReadOnly = true;

            nudHourlyRate.BackColor = System.Drawing.Color.White;
            nudHourlyRate.ForeColor = System.Drawing.Color.Black;
            nudHourlyRate.BorderStyle = BorderStyle.FixedSingle;
            nudHourlyRate.Font = new System.Drawing.Font("Segoe UI", 9F);
            nudHourlyRate.DecimalPlaces = 2;
            nudHourlyRate.Minimum = 0;
            nudHourlyRate.Maximum = 1000000;
            nudHourlyRate.Increment = 10;

            foreach (Control control in groupBoxInput.Controls)
            {
                if (control is Label label)
                {
                    label.ForeColor = System.Drawing.Color.FromArgb(73, 80, 87);
                    label.Font = new System.Drawing.Font("Segoe UI", 9F);
                }
            }
            ConfigureButton(btnCreate, System.Drawing.Color.FromArgb(40, 167, 69), "Создать", 9);
            ConfigureButton(btnUpdate, System.Drawing.Color.FromArgb(0, 123, 255), "Обновить", 9);
            ConfigureButton(btnDelete, System.Drawing.Color.FromArgb(220, 53, 69), "Удалить", 9);
            ConfigureButton(btnClear, System.Drawing.Color.FromArgb(108, 117, 125), "Очистить", 9);
            ConfigureButton(btnRefresh, System.Drawing.Color.FromArgb(23, 162, 184), "Обновить список", 9);

            lblTotalCount.Font = new System.Drawing.Font("Segoe UI", 9F, FontStyle.Bold);
            lblTotalCount.ForeColor = System.Drawing.Color.FromArgb(40, 167, 69);

            lblAvgRate.Font = new System.Drawing.Font("Segoe UI", 9F, FontStyle.Bold);
            lblAvgRate.ForeColor = System.Drawing.Color.FromArgb(0, 123, 255);
        }

        private void ConfigureButton(Button button, Color backColor, string text, int fontSize)
        {
            button.BackColor = backColor;
            button.ForeColor = System.Drawing.Color.White;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.Font = new System.Drawing.Font("Segoe UI", fontSize, FontStyle.Bold);
            button.Cursor = Cursors.Hand;
            button.Padding = new Padding(15, 8, 15, 8);
            button.Text = text;
        }

        private void SetupEventHandlers()
        {
            btnCreate.MouseEnter += Button_MouseEnter;
            btnCreate.MouseLeave += Button_MouseLeave;

            btnUpdate.MouseEnter += Button_MouseEnter;
            btnUpdate.MouseLeave += Button_MouseLeave;

            btnDelete.MouseEnter += Button_MouseEnter;
            btnDelete.MouseLeave += Button_MouseLeave;

            btnClear.MouseEnter += Button_MouseEnter;
            btnClear.MouseLeave += Button_MouseLeave;

            btnRefresh.MouseEnter += Button_MouseEnter;
            btnRefresh.MouseLeave += Button_MouseLeave;

            txtFullName.Enter += TextBox_Enter;
            txtFullName.Leave += TextBox_Leave;

            txtPosition.Enter += TextBox_Enter;
            txtPosition.Leave += TextBox_Leave;

            nudHourlyRate.Enter += Numeric_Enter;
            nudHourlyRate.Leave += Numeric_Leave;
        }

        private void SetupDataGridView()
        {
            dgvPersonnel.AutoGenerateColumns = false;
            dgvPersonnel.Columns.Clear();

            DataGridViewTextBoxColumn idColumn = new DataGridViewTextBoxColumn
            {
                Name = "Id",
                HeaderText = "ID",
                DataPropertyName = "Id",
                Width = 60,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                    ForeColor = System.Drawing.Color.Black,
                    BackColor = System.Drawing.Color.White
                }
            };
            DataGridViewTextBoxColumn nameColumn = new DataGridViewTextBoxColumn
            {
                Name = "FullName",
                HeaderText = "ФИО сотрудника",
                DataPropertyName = "FullName",
                Width = 250,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                    ForeColor = System.Drawing.Color.Black,
                    BackColor = System.Drawing.Color.White
                }
            };
            DataGridViewTextBoxColumn positionColumn = new DataGridViewTextBoxColumn
            {
                Name = "Position",
                HeaderText = "Должность",
                DataPropertyName = "Position",
                Width = 200,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                    ForeColor = System.Drawing.Color.Black,
                    BackColor = System.Drawing.Color.White
                }
            };
            DataGridViewTextBoxColumn rateColumn = new DataGridViewTextBoxColumn
            {
                Name = "HourlyRate",
                HeaderText = "Ставка в час",
                DataPropertyName = "HourlyRate",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "C2",
                    Alignment = DataGridViewContentAlignment.MiddleRight,
                    Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                    ForeColor = System.Drawing.Color.Black,
                    BackColor = System.Drawing.Color.White
                }
            };
            dgvPersonnel.Columns.AddRange(new DataGridViewColumn[]
            {
                idColumn, nameColumn, positionColumn, rateColumn
            });
            dgvPersonnel.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPersonnel.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvPersonnel.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        private void LoadPersonalData()
        {
            try
            {
                var personalList = _manager.GetAllPersonal();
                dgvPersonnel.DataSource = personalList?.ToList();
                UpdateStatistics();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateStatistics()
        {
            try
            {
                var personalList = _manager.GetAllPersonal()?.ToList();

                if (personalList == null || personalList.Count == 0)
                {
                    lblTotalCount.Text = "Всего сотрудников: 0";
                    lblAvgRate.Text = "Средняя ставка: 0 ₽";
                    return;
                }
                lblTotalCount.Text = $"Всего сотрудников: {personalList.Count}";

                decimal totalRate = 0;
                foreach (var person in personalList)
                {
                    totalRate += (decimal)person.HourlyRate;
                }
                decimal avgRate = totalRate / personalList.Count;
                lblAvgRate.Text = $"Средняя ставка: {avgRate:C}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка обновления статистики: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ClearInputFields()
        {
            txtId.Text = "";
            txtFullName.Text = "";
            txtPosition.Text = "";
            nudHourlyRate.Value = 500;
            _selectedPersonal = null;
            UpdateButtonStates();

            dgvPersonnel.ClearSelection();

            txtFullName.Focus();
        }

        private void UpdateButtonStates()
        {
            bool hasSelection = _selectedPersonal != null;

            btnCreate.Enabled = true;
            btnUpdate.Enabled = hasSelection;
            btnDelete.Enabled = hasSelection;

            btnUpdate.BackColor = hasSelection ? System.Drawing.Color.FromArgb(0, 123, 255) : System.Drawing.Color.FromArgb(180, 180, 180);
            btnDelete.BackColor = hasSelection ? System.Drawing.Color.FromArgb(220, 53, 69) : System.Drawing.Color.FromArgb(180, 180, 180);
        }

        private void dgvPersonnel_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvPersonnel.RowCount) return;

            try
            {
                var row = dgvPersonnel.Rows[e.RowIndex];
                int id = Convert.ToInt32(row.Cells["Id"].Value);

                var personalList = _manager.GetAllPersonal();
                _selectedPersonal = personalList?.FirstOrDefault(p => p.Id == id);

                if (_selectedPersonal == null) return;

                txtId.Text = _selectedPersonal.Id.ToString();
                txtFullName.Text = _selectedPersonal.FullName;
                txtPosition.Text = _selectedPersonal.Position;
                nudHourlyRate.Value = (decimal)_selectedPersonal.HourlyRate;

                UpdateButtonStates();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка выбора строки: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvPersonnel_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dgvPersonnel_CellClick(sender, e);
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            try
            {
                var newPersonal = new Personal
                {
                    FullName = txtFullName.Text.Trim(),
                    Position = txtPosition.Text.Trim(),
                    HourlyRate = (double)nudHourlyRate.Value
                };

                if (_manager.CreatePersonal(newPersonal))
                {
                    MessageBox.Show("✅ Сотрудник успешно добавлен", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadPersonalData();
                    ClearInputFields();
                }
                else
                {
                    MessageBox.Show("❌ Не удалось добавить сотрудника", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка создания: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_selectedPersonal == null || !ValidateInput()) return;

            try
            {
                var originalName = _selectedPersonal.FullName;

                _selectedPersonal.FullName = txtFullName.Text.Trim();
                _selectedPersonal.Position = txtPosition.Text.Trim();
                _selectedPersonal.HourlyRate = (double)nudHourlyRate.Value;

                if (_manager.UpdatePersonal(_selectedPersonal))
                {
                    MessageBox.Show($"✅ Данные сотрудника '{originalName}' обновлены", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadPersonalData();
                    ClearInputFields();
                }
                else
                {
                    MessageBox.Show("❌ Не удалось обновить данные", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка обновления: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedPersonal == null) return;

            try
            {
                var result = MessageBox.Show(
                    $"Вы действительно хотите удалить сотрудника:\n\n" +
                    $"ФИО: {_selectedPersonal.FullName}\n" +
                    $"Должность: {_selectedPersonal.Position}\n\n" +
                    "Это действие нельзя отменить!",
                    "Подтверждение удаления",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2);

                if (result == DialogResult.Yes)
                {
                    if (_manager.DeletePersonal(_selectedPersonal.Id))
                    {
                        MessageBox.Show("✅ Сотрудник успешно удален", "Успех",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadPersonalData();
                        ClearInputFields();
                    }
                    else
                    {
                        MessageBox.Show("❌ Не удалось удалить сотрудника", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка удаления: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearInputFields();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadPersonalData();
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("⚠️ Поле 'ФИО' не может быть пустым", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFullName.Focus();
                return false;
            }

            if (txtFullName.Text.Trim().Length < 5)
            {
                MessageBox.Show("⚠️ ФИО должно содержать не менее 5 символов", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFullName.Focus();
                return false;
            }

            // Проверка должности
            if (string.IsNullOrWhiteSpace(txtPosition.Text))
            {
                MessageBox.Show("⚠️ Поле 'Должность' не может быть пустым", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPosition.Focus();
                return false;
            }

            // Проверка ставки
            if (nudHourlyRate.Value <= 0)
            {
                MessageBox.Show("⚠️ Ставка должна быть больше 0", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nudHourlyRate.Focus();
                return false;
            }

            return true;
        }

        // Обработчики для светлой темы
        private void Button_MouseEnter(object sender, EventArgs e)
        {
            if (sender is Button button && button.Enabled)
            {
                // Сделать цвет немного светлее
                var originalColor = button.BackColor;
                button.BackColor = ControlPaint.Light(originalColor, 0.2f);
            }
        }

        private void Button_MouseLeave(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                // Восстанавливаем оригинальный цвет
                if (button == btnCreate)
                    button.BackColor = System.Drawing.Color.FromArgb(40, 167, 69);
                else if (button == btnUpdate)
                    button.BackColor = _selectedPersonal != null ? System.Drawing.Color.FromArgb(0, 123, 255) : System.Drawing.Color.FromArgb(180, 180, 180);
                else if (button == btnDelete)
                    button.BackColor = _selectedPersonal != null ? System.Drawing.Color.FromArgb(220, 53, 69) : System.Drawing.Color.FromArgb(180, 180, 180);
                else if (button == btnClear)
                    button.BackColor = System.Drawing.Color.FromArgb(108, 117, 125);
                else if (button == btnRefresh)
                    button.BackColor = System.Drawing.Color.FromArgb(23, 162, 184);
            }
        }

        private void TextBox_Enter(object sender, EventArgs e)
        {
            if (sender is TextBox textBox)
            {
                textBox.BackColor = System.Drawing.Color.FromArgb(230, 240, 255);
                textBox.BorderStyle = BorderStyle.FixedSingle;
            }
        }

        private void TextBox_Leave(object sender, EventArgs e)
        {
            if (sender is TextBox textBox)
            {
                textBox.BackColor = System.Drawing.Color.White;
                textBox.BorderStyle = BorderStyle.FixedSingle;
            }
        }

        private void Numeric_Enter(object sender, EventArgs e)
        {
            nudHourlyRate.BackColor = System.Drawing.Color.FromArgb(230, 240, 255);
        }

        private void Numeric_Leave(object sender, EventArgs e)
        {
            nudHourlyRate.BackColor = System.Drawing.Color.White;
        }

        // Обработчики для клавиши Enter в текстовых полях
        private void txtFullName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                txtPosition.Focus();
            }
        }

        private void txtPosition_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                nudHourlyRate.Focus();
            }
        }

        private void nudHourlyRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                if (_selectedPersonal == null)
                    btnCreate.PerformClick();
                else
                    btnUpdate.PerformClick();
            }
        }

        // Обработчик для горячих клавиш
        private void PersonalManagerForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && _selectedPersonal != null)
            {
                btnDelete.PerformClick();
            }
            else if (e.KeyCode == Keys.F5)
            {
                btnRefresh.PerformClick();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                btnClear.PerformClick();
            }
        }

        // Обработчик для очистки при изменении текста
        private void txtFullName_TextChanged(object sender, EventArgs e)
        {
            // Если пользователь начал вводить ФИО, сбрасываем выделение
            if (!string.IsNullOrEmpty(txtFullName.Text) &&
                (string.IsNullOrEmpty(txtId.Text) || _selectedPersonal?.FullName != txtFullName.Text))
            {
                _selectedPersonal = null;
                txtId.Text = "";
                UpdateButtonStates();
            }
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }
    }
}