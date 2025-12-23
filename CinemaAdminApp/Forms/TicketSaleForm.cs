using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CinemaAdminApp.Logic;
using CinemaAdminApp.Models;

namespace CinemaAdminApp.Forms
{
    public partial class TicketSaleForm : Form
    {
        private SessionManager _sessionManager = new SessionManager();
        private List<Session> sessions = new List<Session>();
        private Session selectedSession = null;
        private int selectedSeat = -1;
        private decimal ticketPrice = 0;
        private List<int> soldSeats = new List<int>();
        private Dictionary<int, List<int>> _soldSeatsBySession = new Dictionary<int, List<int>>();

        public TicketSaleForm()
        {
            InitializeComponent();
            SetupForm();
        }

        private void SetupForm()
        {
            this.Text = "Продажа билетов";
            LoadSessionsToComboBox();
        }

        private void LoadSessionsToComboBox()
        {
            sessions = _sessionManager.GetAvailableSessions();

            cmbSessions.DataSource = null;
            cmbSessions.DisplayMember = "DisplayInfo";
            cmbSessions.ValueMember = "Id";

            if (sessions.Count > 0)
            {
                cmbSessions.DataSource = sessions;
                cmbSessions.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("Нет доступных сеансов для продажи", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbSessions.Enabled = false;
                btnSell.Enabled = false;
                btnClear.Enabled = false;
            }
        }

        private void cmbSessions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSessions.SelectedItem is Session session)
            {
                selectedSession = session;
                ticketPrice = session.Price;
                lblPrice.Text = $"{ticketPrice} ₽";
                lblHallInfo.Text = $"Зал: {session.HallName} | Свободных мест: {session.AvailableSeats}";

                LoadSoldSeatsForSession(session.Id);
                GenerateSeatLayout();
            }
            else
            {
                ClearSelection();
            }
        }

        private void LoadSoldSeatsForSession(int sessionId)
        {
            soldSeats.Clear();

            if (_soldSeatsBySession.ContainsKey(sessionId))
            {
                soldSeats = new List<int>(_soldSeatsBySession[sessionId]);
            }
            else
            {
                soldSeats = new List<int>();
                _soldSeatsBySession[sessionId] = soldSeats;
            }
        }

        private int GetHallCapacity(string hallName)
        {
            var halls = _sessionManager.GetAllHalls();
            var hall = halls.Find(h => h.Name == hallName);
            return hall?.Capacity ?? 100; 
        }

        private void ClearSelection()
        {
            selectedSeat = -1;
            lblSelectedSeat.Text = "Не выбрано";
            btnSell.Enabled = false;
        }

        private void GenerateSeatLayout()
        {
            pnlSeats.Controls.Clear();
            ClearSelection();

            if (selectedSession == null) return;

            int totalCapacity = GetHallCapacity(selectedSession.HallName);
            int seatsPerRow = 10;
            int buttonSize = 35;
            int padding = 5;

            for (int seat = 1; seat <= totalCapacity; seat++)
            {
                Button btnSeat = new Button();
                btnSeat.Text = seat.ToString();
                btnSeat.Tag = seat;
                btnSeat.Size = new Size(buttonSize, buttonSize);
                btnSeat.Font = new Font("Segoe UI", 8, FontStyle.Regular);

                int row = (seat - 1) / seatsPerRow;
                int col = (seat - 1) % seatsPerRow;

                btnSeat.Location = new Point(col * (buttonSize + padding), row * (buttonSize + padding));

                bool isSold = soldSeats.Contains(seat);

                if (isSold)
                {
                    btnSeat.BackColor = Color.FromArgb(220, 53, 69); 
                    btnSeat.ForeColor = Color.White;
                    btnSeat.Enabled = false;
                    btnSeat.Cursor = Cursors.No;
                }
                else
                {
                    btnSeat.BackColor = Color.FromArgb(0, 123, 255); 
                    btnSeat.ForeColor = Color.White;
                    btnSeat.Cursor = Cursors.Hand;
                    btnSeat.Click += BtnSeat_Click;
                }

                pnlSeats.Controls.Add(btnSeat);
            }
        }

        private void BtnSeat_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;

            foreach (Control control in pnlSeats.Controls)
            {
                if (control is Button btn && btn.Enabled)
                {
                    int seatNum = (int)btn.Tag;
                    if (!soldSeats.Contains(seatNum))
                    {
                        btn.BackColor = Color.FromArgb(0, 123, 255); 
                    }
                }
            }

            selectedSeat = (int)clickedButton.Tag;
            clickedButton.BackColor = Color.FromArgb(40, 167, 69); 

            lblSelectedSeat.Text = selectedSeat.ToString();
            btnSell.Enabled = true;
        }

        private void btnSell_Click(object sender, EventArgs e)
        {
            if (selectedSession != null && selectedSeat != -1)
            {
                if (soldSeats.Contains(selectedSeat))
                {
                    MessageBox.Show("Это место уже продано!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                int totalCapacity = GetHallCapacity(selectedSession.HallName);
                if (selectedSeat > totalCapacity || selectedSeat < 1)
                {
                    MessageBox.Show("Неверный номер места", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                bool success = _sessionManager.SellTicket(selectedSession.Id, selectedSeat);

                if (success)
                {
                    string message = $"Билет продан успешно!\n\n" +
                                   $"Фильм: {selectedSession.FilmTitle}\n" +
                                   $"Дата и время: {selectedSession.Date} {selectedSession.Time}\n" +
                                   $"Зал: {selectedSession.HallName}\n" +
                                   $"Место: {selectedSeat}\n" +
                                   $"Цена: {ticketPrice} ₽";

                    MessageBox.Show(message, "Продажа билета",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    soldSeats.Add(selectedSeat);
                    _soldSeatsBySession[selectedSession.Id] = soldSeats;

                    selectedSession.AvailableSeats--;

                    lblHallInfo.Text = $"Зал: {selectedSession.HallName} | Свободных мест: {selectedSession.AvailableSeats}";

                    UpdateSeatColorAfterSale(selectedSeat);

                    ClearSelection();
                }
                else
                {
                    MessageBox.Show("Ошибка при продаже билета", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Выберите сеанс и место", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void UpdateSeatColorAfterSale(int seatNumber)
        {
            foreach (Control control in pnlSeats.Controls)
            {
                if (control is Button btn && btn.Tag != null && (int)btn.Tag == seatNumber)
                {
                    btn.BackColor = Color.FromArgb(220, 53, 69); 
                    btn.ForeColor = Color.White;
                    btn.Enabled = false;
                    btn.Cursor = Cursors.No;

                    btn.Click -= BtnSeat_Click;
                    break;
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearSelection();

            foreach (Control control in pnlSeats.Controls)
            {
                if (control is Button btn && btn.Enabled)
                {
                    int seatNum = (int)btn.Tag;
                    if (!soldSeats.Contains(seatNum))
                    {
                        btn.BackColor = Color.FromArgb(0, 123, 255); 
                    }
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadSessionsToComboBox();
            if (cmbSessions.Enabled && cmbSessions.Items.Count > 0)
            {
                cmbSessions_SelectedIndexChanged(null, EventArgs.Empty);
                MessageBox.Show("Список сеансов обновлен", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
