using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CinemaAdminApp.Logic;
using CinemaAdminApp.Models;

namespace CinemaAdminApp.Forms
{
    public partial class SessionManagerForm : Form
    {
        private SessionManager _sessionManager = new SessionManager();
        private List<Session> sessions = new List<Session>();
        private Session selectedSession = null;
        private List<Film> _films = new List<Film>();

        public SessionManagerForm()
        {
            InitializeComponent();
            SetupForm();
        }

        private void SetupForm()
        {
            Text = "Управление сеансами";
            LoadData();
            SetupDataGridView();
            UpdateSessionsList();
            LoadFilmsToComboBox();
            LoadHallsToComboBox();
        }

        private void LoadData()
        {
            sessions = _sessionManager.GetAllSessions();
            _films = _sessionManager.GetAllFilms();
        }

        private void SetupDataGridView()
        {
            dgvSessions.AutoGenerateColumns = false;
            dgvSessions.Columns.Clear();

            DataGridViewTextBoxColumn colId = new DataGridViewTextBoxColumn();
            colId.Name = "Id";
            colId.HeaderText = "ID";
            colId.DataPropertyName = "Id";
            colId.Width = 50;

            DataGridViewTextBoxColumn colFilm = new DataGridViewTextBoxColumn();
            colFilm.Name = "FilmTitle";
            colFilm.HeaderText = "Фильм";
            colFilm.DataPropertyName = "FilmTitle";
            colFilm.Width = 180;

            DataGridViewTextBoxColumn colHall = new DataGridViewTextBoxColumn();
            colHall.Name = "HallName";
            colHall.HeaderText = "Зал";
            colHall.DataPropertyName = "HallName";
            colHall.Width = 80;

            DataGridViewTextBoxColumn colDate = new DataGridViewTextBoxColumn();
            colDate.Name = "Date";
            colDate.HeaderText = "Дата";
            colDate.DataPropertyName = "Date";
            colDate.Width = 100;

            DataGridViewTextBoxColumn colTime = new DataGridViewTextBoxColumn();
            colTime.Name = "Time";
            colTime.HeaderText = "Время";
            colTime.DataPropertyName = "Time";
            colTime.Width = 80;

            DataGridViewTextBoxColumn colPrice = new DataGridViewTextBoxColumn();
            colPrice.Name = "Price";
            colPrice.HeaderText = "Цена (руб)";
            colPrice.DataPropertyName = "Price";
            colPrice.Width = 90;

            DataGridViewTextBoxColumn colSeats = new DataGridViewTextBoxColumn();
            colSeats.Name = "AvailableSeats";
            colSeats.HeaderText = "Свободных мест";
            colSeats.DataPropertyName = "AvailableSeats";
            colSeats.Width = 100;

            dgvSessions.Columns.AddRange(new DataGridViewColumn[]
            {
                colId,
                colFilm,
                colHall,
                colDate,
                colTime,
                colPrice,
                colSeats
            });
        }

        private void UpdateSessionsList()
        {
            LoadData();
            dgvSessions.DataSource = null;
            dgvSessions.DataSource = sessions;
            lblSessionCount.Text = $"Всего сеансов: {sessions.Count}";
        }

        private void LoadFilmsToComboBox()
        {
            if (_films == null || _films.Count == 0)
            {
                MessageBox.Show("Нет доступных фильмов. Сначала добавьте фильмы.",
                    "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbFilm.Enabled = false;
                return;
            }

            cmbFilm.DataSource = null;
            cmbFilm.DisplayMember = "Title";
            cmbFilm.ValueMember = "Id";
            cmbFilm.DataSource = _films;
            cmbFilm.Enabled = true;
        }

        private void LoadHallsToComboBox()
        {
            var halls = _sessionManager.GetAllHalls();
            if (halls == null || halls.Count == 0)
            {
                MessageBox.Show("Нет доступных залов. Сначала добавьте залы.",
                    "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbHall.Enabled = false;
                return;
            }

            cmbHall.DataSource = null;
            cmbHall.DisplayMember = "Name";
            cmbHall.ValueMember = "Id";
            cmbHall.DataSource = halls;
            cmbHall.Enabled = true;
        }

        private bool ValidateForm()
        {
            if (cmbFilm.Items.Count == 0)
            {
                MessageBox.Show("Сначала добавьте фильмы", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (cmbFilm.SelectedIndex < 0)
            {
                MessageBox.Show("Выберите фильм", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbFilm.Focus();
                return false;
            }

            if (cmbHall.Items.Count == 0)
            {
                MessageBox.Show("Сначала добавьте залы", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (cmbHall.SelectedIndex < 0)
            {
                MessageBox.Show("Выберите зал", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbHall.Focus();
                return false;
            }

            if (nudPrice.Value <= 0)
            {
                MessageBox.Show("Цена должна быть больше 0", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nudPrice.Focus();
                return false;
            }

            return true;
        }

        private void btnCreateSession_Click(object sender, EventArgs e)
        {
            if (!ValidateForm()) return;

            Film selectedFilm = cmbFilm.SelectedItem as Film;
            Hall selectedHall = cmbHall.SelectedItem as Hall;

            Session newSession = new Session
            {
                FilmId = selectedFilm.Id,
                FilmTitle = selectedFilm.Title,
                HallId = selectedHall.Id,
                HallName = selectedHall.Name,
                Date = dtpDate.Value.ToString("dd.MM.yyyy"),
                Time = dtpTime.Value.ToString("HH:mm"),
                Price = nudPrice.Value,
                AvailableSeats = selectedHall.Capacity
            };

            bool success;

            if (selectedSession == null)
            {
                success = _sessionManager.CreateSession(newSession);
                if (success)
                {
                    UpdateSessionsList();
                    ClearForm();
                    MessageBox.Show("Сеанс создан успешно", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Ошибка при создании сеанса", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                newSession.Id = selectedSession.Id;
                success = _sessionManager.UpdateSession(newSession);
                if (success)
                {
                    UpdateSessionsList();
                    ClearForm();
                    MessageBox.Show("Сеанс обновлен успешно", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Ошибка при обновлении сеанса", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ClearForm()
        {
            if (cmbFilm.Items.Count > 0) cmbFilm.SelectedIndex = 0;
            if (cmbHall.Items.Count > 0) cmbHall.SelectedIndex = 0;
            dtpDate.Value = DateTime.Today;
            dtpTime.Value = DateTime.Now;
            nudPrice.Value = 300;
            selectedSession = null;
            btnDelete.Enabled = false;
            btnCreateSession.Text = "Создать сеанс";
        }

        private void dgvSessions_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < sessions.Count)
            {
                selectedSession = sessions[e.RowIndex];

                var film = _films.Find(f => f.Id == selectedSession.FilmId);
                if (film != null)
                {
                    cmbFilm.SelectedValue = film.Id;
                }

                cmbHall.SelectedValue = selectedSession.HallId;

                DateTime date;
                if (DateTime.TryParse(selectedSession.Date, out date))
                {
                    dtpDate.Value = date;
                }

                DateTime time;
                if (DateTime.TryParse(selectedSession.Time, out time))
                {
                    dtpTime.Value = time;
                }

                nudPrice.Value = selectedSession.Price;
                btnDelete.Enabled = true;
                btnCreateSession.Text = "Обновить сеанс";
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedSession == null) return;

            var result = MessageBox.Show($"Удалить сеанс '{selectedSession.FilmTitle}'?",
                "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                bool success = _sessionManager.DeleteSession(selectedSession.Id);
                if (success)
                {
                    UpdateSessionsList();
                    ClearForm();
                    MessageBox.Show("Сеанс удален успешно", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Ошибка при удалении сеанса", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void SessionManagerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }
    }
}