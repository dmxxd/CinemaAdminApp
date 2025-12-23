using CinemaAdminApp.Logic;
using CinemaAdminApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace CinemaAdminApp.Forms
{
    public partial class ReportViewerForm : Form
    {
        private readonly InventoryManager _inventoryManager = new InventoryManager();
        private readonly PersonalManager _personalManager = new PersonalManager();

        public ReportViewerForm()
        {
            InitializeComponent();
            ConfigureAppearance();
        }

        private void ReportViewerForm_Load(object sender, EventArgs e)
        {
            cmbReportType.Items.Add("📦 Запасы");
            cmbReportType.Items.Add("👥 Сотрудники");
            cmbReportType.SelectedIndex = 0;
            GenerateReport();
        }

        private void ConfigureAppearance()
        {
            this.BackColor = System.Drawing.Color.White;
            this.Font = new System.Drawing.Font("Segoe UI", 9F);

            panelHeader.BackColor = System.Drawing.Color.FromArgb(0, 51, 102);
            lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.ForeColor = System.Drawing.Color.White;

            cmbReportType.Font = new System.Drawing.Font("Segoe UI", 9F);
            cmbReportType.FlatStyle = FlatStyle.Flat;

            dgvReport.BackgroundColor = System.Drawing.Color.White;
            dgvReport.BorderStyle = BorderStyle.None;
            dgvReport.GridColor = System.Drawing.Color.FromArgb(240, 240, 240);
            dgvReport.RowHeadersVisible = false;
            dgvReport.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(248, 248, 248);
            dgvReport.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvReport.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(0, 123, 255);
            dgvReport.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;

            btnGenerate.BackColor = System.Drawing.Color.FromArgb(0, 123, 255);
            btnGenerate.ForeColor = System.Drawing.Color.White;
            btnGenerate.FlatStyle = FlatStyle.Flat;
            btnGenerate.FlatAppearance.BorderSize = 0;
            btnGenerate.Font = new System.Drawing.Font("Segoe UI", 9F, FontStyle.Bold);

            lblReportTitle.Font = new System.Drawing.Font("Segoe UI", 11F, FontStyle.Bold);
            lblReportTitle.ForeColor = System.Drawing.Color.FromArgb(0, 51, 102);
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            GenerateReport();
        }

        private void cmbReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            GenerateReport();
        }

        private void GenerateReport()
        {
            if (cmbReportType.SelectedItem == null) return;

            dgvReport.DataSource = null;
            dgvReport.Columns.Clear();

            try
            {
                string reportType = cmbReportType.SelectedItem.ToString();

                switch (reportType)
                {
                    case "📦 Запасы":
                        LoadInventoryReport();
                        break;

                    case "👥 Сотрудники":
                        LoadPersonalReport();
                        break;
                }

                if (dgvReport.Columns.Count > 0)
                {
                    dgvReport.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при формировании отчета: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadInventoryReport()
        {
            try
            {
                var inventoryData = _inventoryManager.GetAllItems();

                DataTable table = new DataTable();
                table.Columns.Add("ID", typeof(int));
                table.Columns.Add("Название", typeof(string));
                table.Columns.Add("Ед. изм.", typeof(string));
                table.Columns.Add("Себестоимость", typeof(decimal));
                table.Columns.Add("Цена продажи", typeof(decimal));
                table.Columns.Add("Остаток", typeof(int));
                table.Columns.Add("Общая стоимость", typeof(decimal));
                table.Columns.Add("Потенциальная выручка", typeof(decimal));

                foreach (var item in inventoryData)
                {
                    decimal totalCost = item.CostPrice * item.CurrentStock;
                    decimal totalSale = item.SalePrice * item.CurrentStock;

                    table.Rows.Add(
                        item.Id,
                        item.Name,
                        item.UnitOfMeasure,
                        item.CostPrice,
                        item.SalePrice,
                        item.CurrentStock,
                        totalCost,
                        totalSale
                    );
                }

                dgvReport.DataSource = table;
                lblReportTitle.Text = $"📦 Отчет: Запасы (всего: {inventoryData.Count} позиций)";

                dgvReport.Columns["Себестоимость"].DefaultCellStyle.Format = "C2";
                dgvReport.Columns["Цена продажи"].DefaultCellStyle.Format = "C2";
                dgvReport.Columns["Общая стоимость"].DefaultCellStyle.Format = "C2";
                dgvReport.Columns["Потенциальная выручка"].DefaultCellStyle.Format = "C2";
                dgvReport.Columns["Остаток"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                decimal totalCostSum = 0;
                decimal totalSaleSum = 0;
                int totalStock = 0;

                foreach (var item in inventoryData)
                {
                    totalCostSum += item.CostPrice * item.CurrentStock;
                    totalSaleSum += item.SalePrice * item.CurrentStock;
                    totalStock += item.CurrentStock;
                }

                lblReportTitle.Text += $" | Общий остаток: {totalStock} | Стоимость запасов: {totalCostSum:C}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки отчета по товарам: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadPersonalReport()
        {
            try
            {
                var personalData = _personalManager.GetAllPersonal();

                DataTable table = new DataTable();
                table.Columns.Add("ID", typeof(int));
                table.Columns.Add("ФИО", typeof(string));
                table.Columns.Add("Должность", typeof(string));
                table.Columns.Add("Ставка в час", typeof(decimal));
                table.Columns.Add("Дополнительная информация", typeof(string));

                foreach (var person in personalData)
                {
                    table.Rows.Add(
                        person.Id,
                        person.FullName,
                        person.Position,
                        person.HourlyRate,
                        $"ID: {person.Id}"
                    );
                }

                dgvReport.DataSource = table;
                lblReportTitle.Text = $"👥 Отчет: Сотрудники (всего: {personalData.Count} чел.)";

                dgvReport.Columns["Ставка в час"].DefaultCellStyle.Format = "C2";

                if (personalData.Count > 0)
                {
                    decimal avgRate = 0;
                    foreach (var person in personalData)
                    {
                        avgRate += (decimal)person.HourlyRate;
                    }
                    avgRate /= personalData.Count;
                    lblReportTitle.Text += $" | Средняя ставка: {avgRate:C}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки отчета по персоналу: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvReport.Rows.Count == 0)
                {
                    MessageBox.Show("Нет данных для печати", "Информация",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var result = MessageBox.Show("Печать отчета?", "Подтверждение",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    MessageBox.Show("Отчет подготовлен к печати", "Информация",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при печати: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvReport.Rows.Count == 0)
                {
                    MessageBox.Show("Нет данных для экспорта", "Информация",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "CSV файлы (*.csv)|*.csv|Все файлы (*.*)|*.*";
                saveFileDialog.Title = "Экспорт отчета";
                saveFileDialog.FileName = $"Отчет_{DateTime.Now:yyyyMMdd_HHmmss}.csv";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ExportToCsv(saveFileDialog.FileName);
                    MessageBox.Show($"Отчет успешно экспортирован в:\n{saveFileDialog.FileName}", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при экспорте: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExportToCsv(string fileName)
        {
            using (System.IO.StreamWriter writer = new System.IO.StreamWriter(fileName, false, System.Text.Encoding.UTF8))
            {
                writer.WriteLine($"Отчет: {lblReportTitle.Text}");
                writer.WriteLine($"Дата формирования: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                writer.WriteLine();

                for (int i = 0; i < dgvReport.Columns.Count; i++)
                {
                    writer.Write(dgvReport.Columns[i].HeaderText);
                    if (i < dgvReport.Columns.Count - 1)
                        writer.Write(";");
                }
                writer.WriteLine();

                foreach (DataGridViewRow row in dgvReport.Rows)
                {
                    for (int i = 0; i < dgvReport.Columns.Count; i++)
                    {
                        if (row.Cells[i].Value != null)
                            writer.Write(row.Cells[i].Value.ToString());
                        writer.Write(";");
                    }
                    writer.WriteLine();
                }
            }
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }
    }
}
