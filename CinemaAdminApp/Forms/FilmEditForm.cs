using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CinemaAdminApp.Logic;
using CinemaAdminApp.Models;

namespace CinemaAdminApp.Forms
{
    public partial class FilmEditForm : Form
    {
        private FilmManager _filmManager = new FilmManager();
        private List<Film> films = new List<Film>();
        private Film selectedFilm = null;

        public FilmEditForm()
        {
            InitializeComponent();
            SetupForm();
        }

        private void SetupForm()
        {
            this.Text = "Управление фильмами";
            LoadData();
            SetupDataGridView();
            UpdateFilmsList();
        }

        private void LoadData()
        {
            films = _filmManager.GetAllFilms();
        }

        private void SetupDataGridView()
        {
            dgvFilms.AutoGenerateColumns = false;
            dgvFilms.Columns.Clear();

            DataGridViewTextBoxColumn colId = new DataGridViewTextBoxColumn();
            colId.Name = "Id";
            colId.HeaderText = "ID";
            colId.DataPropertyName = "Id";
            colId.Width = 50;

            DataGridViewTextBoxColumn colTitle = new DataGridViewTextBoxColumn();
            colTitle.Name = "Title";
            colTitle.HeaderText = "Название";
            colTitle.DataPropertyName = "Title";
            colTitle.Width = 200;

            DataGridViewTextBoxColumn colGenre = new DataGridViewTextBoxColumn();
            colGenre.Name = "Genre";
            colGenre.HeaderText = "Жанр";
            colGenre.DataPropertyName = "Genre";
            colGenre.Width = 120;

            DataGridViewTextBoxColumn colDuration = new DataGridViewTextBoxColumn();
            colDuration.Name = "Duration";
            colDuration.HeaderText = "Длительность (мин)";
            colDuration.DataPropertyName = "Duration";
            colDuration.Width = 120;

            DataGridViewTextBoxColumn colDescription = new DataGridViewTextBoxColumn();
            colDescription.Name = "Description";
            colDescription.HeaderText = "Описание";
            colDescription.DataPropertyName = "Description";
            colDescription.Width = 300;

            dgvFilms.Columns.AddRange(new DataGridViewColumn[]
            {
                colId,
                colTitle,
                colGenre,
                colDuration,
                colDescription
            });
        }

        private void UpdateFilmsList()
        {
            LoadData();
            dgvFilms.DataSource = null;
            dgvFilms.DataSource = films;
            lblFilmCount.Text = $"Всего фильмов: {films.Count}";
        }

        private void ClearForm()
        {
            txtTitle.Clear();
            txtGenre.Clear();
            txtDuration.Value = 60;
            txtDescription.Clear();
            selectedFilm = null;
            btnDelete.Enabled = false;
            btnSave.Text = "Добавить";
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show("Введите название фильма", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTitle.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtGenre.Text))
            {
                MessageBox.Show("Введите жанр фильма", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGenre.Focus();
                return false;
            }

            if (txtDuration.Value <= 0)
            {
                MessageBox.Show("Длительность должна быть больше 0 минут", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDuration.Focus();
                return false;
            }

            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateForm()) return;

            Film film = new Film
            {
                Title = txtTitle.Text.Trim(),
                Genre = txtGenre.Text.Trim(),
                Duration = (int)txtDuration.Value,
                Description = txtDescription.Text.Trim()
            };

            bool success;

            if (selectedFilm == null)
            {
                success = _filmManager.AddFilm(film);
            }
            else
            {
                film.Id = selectedFilm.Id;
                success = _filmManager.UpdateFilm(film);
            }

            if (success)
            {
                UpdateFilmsList();
                ClearForm();
                MessageBox.Show("Фильм сохранен", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Ошибка при сохранении фильма. Возможно, фильм с таким названием уже существует.",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedFilm == null) return;

            var result = MessageBox.Show($"Удалить фильм '{selectedFilm.Title}'?",
                "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                bool success = _filmManager.DeleteFilm(selectedFilm.Id);
                if (success)
                {
                    UpdateFilmsList();
                    ClearForm();
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void dgvFilms_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < films.Count)
            {
                selectedFilm = films[e.RowIndex];
                txtTitle.Text = selectedFilm.Title;
                txtGenre.Text = selectedFilm.Genre;
                txtDuration.Value = selectedFilm.Duration;
                txtDescription.Text = selectedFilm.Description;
                btnDelete.Enabled = true;
                btnSave.Text = "Обновить";
            }
        }

        private void txtDuration_ValueChanged(object sender, EventArgs e)
        {
            int hours = (int)txtDuration.Value / 60;
            int minutes = (int)txtDuration.Value % 60;
            label3.Text = $"Длительность: {hours}ч {minutes}м";
        }
    }
}
