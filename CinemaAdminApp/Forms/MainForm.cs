using CinemaAdminApp.Models;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace CinemaAdminApp.Forms
{
    public partial class MainForm : Form
    {
        private User _currentUser;
        private Panel sidebarPanel;
        private Panel contentPanel;
        private Label lblUserInfo;
        private Button btnFilms;
        private Button btnSessions;
        private Button btnTicketSales;
        private Button btnInventory;
        private Button btnReports;
        private Button btnPersonal;
        private Button btnExit;

        public MainForm(User user)
        {
            InitializeComponent();
            _currentUser = user;
            SetupUI();
            ConfigureAccessRights();
        }

        private void SetupUI()
        {
            this.Text = $"АРМ Администратора кинотеатра | {_currentUser.Username} ({_currentUser.Role})";
            this.WindowState = FormWindowState.Maximized;
            CreateSidebar();
            CreateContent();
        }

        private void CreateSidebar()
        {
            sidebarPanel = new Panel();
            sidebarPanel.BackColor = Color.FromArgb(0, 51, 102);
            sidebarPanel.Dock = DockStyle.Left;
            sidebarPanel.Width = 220;

            lblUserInfo = new Label();
            lblUserInfo.Text = $"👤 {_currentUser.Username}\n[{_currentUser.Role}]";
            lblUserInfo.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
            lblUserInfo.ForeColor = Color.White;
            lblUserInfo.TextAlign = ContentAlignment.MiddleCenter;
            lblUserInfo.Dock = DockStyle.Top;
            lblUserInfo.Height = 70;
            lblUserInfo.BackColor = Color.FromArgb(0, 71, 142);
            lblUserInfo.Padding = new Padding(0, 10, 0, 0);

            btnFilms = CreateSidebarButton("🎬 ФИЛЬМЫ", "Films");
            btnSessions = CreateSidebarButton("⏰ СЕАНСЫ", "Sessions");
            btnTicketSales = CreateSidebarButton("🎫 ПРОДАЖА БИЛЕТОВ", "TicketSales");
            btnInventory = CreateSidebarButton("📦 УЧЕТ ЗАПАСОВ", "Inventory");
            btnReports = CreateSidebarButton("📈 ОТЧЕТЫ", "Reports");
            btnPersonal = CreateSidebarButton("👥 ПЕРСОНАЛ", "Personal");
            btnExit = CreateSidebarButton("🚪 ВЫХОД", "Exit");

            btnExit.BackColor = Color.FromArgb(220, 53, 69);
            btnExit.ForeColor = Color.White;

            int top = lblUserInfo.Height + 20;
            Button[] buttons = { btnFilms, btnSessions, btnTicketSales,
                                btnInventory, btnReports, btnPersonal, btnExit };

            foreach (var btn in buttons)
            {
                btn.Top = top;
                top += btn.Height + 10;
                sidebarPanel.Controls.Add(btn);
            }

            sidebarPanel.Controls.Add(lblUserInfo);
            this.Controls.Add(sidebarPanel);
        }

        private Button CreateSidebarButton(string text, string tag)
        {
            Button btn = new Button();
            btn.Text = text;
            btn.Tag = tag;
            btn.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
            btn.ForeColor = Color.White;
            btn.BackColor = Color.FromArgb(0, 71, 142);
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Width = sidebarPanel.Width - 20;
            btn.Height = 45;
            btn.Left = 10;
            btn.TextAlign = ContentAlignment.MiddleLeft;
            btn.Cursor = Cursors.Hand;
            btn.Click += SidebarButton_Click;

            btn.MouseEnter += (s, e) =>
            {
                btn.BackColor = Color.FromArgb(0, 91, 172);
            };

            btn.MouseLeave += (s, e) =>
            {
                if (btn != btnExit)
                    btn.BackColor = Color.FromArgb(0, 71, 142);
            };

            return btn;
        }

        private void CreateContent()
        {
            contentPanel = new Panel();
            contentPanel.Dock = DockStyle.Fill;
            contentPanel.BackColor = Color.White;
            this.Controls.Add(contentPanel);
            contentPanel.BringToFront();
        }

        private void ConfigureAccessRights()
        {
            string role = _currentUser.Role;

            if (role == "Кассир")
            {
                btnInventory.Visible = false;
                btnReports.Visible = false;
                btnPersonal.Visible = false;
                btnFilms.Visible = false;
                btnSessions.Visible = false;
            }
            else if (role == "Менеджер")
            {
                btnPersonal.Visible = false;
            }
        }

        private void SidebarButton_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                contentPanel.Controls.Clear();

                switch (btn.Tag.ToString())
                {
                    case "Films":
                        if (btnFilms.Visible)
                            ShowFilms();
                        break;
                    case "Sessions":
                        if (btnSessions.Visible)
                            ShowSessions();
                        break;
                    case "TicketSales":
                        ShowTicketSales();
                        break;
                    case "Inventory":
                        if (btnInventory.Visible)
                            ShowInventory();
                        break;
                    case "Reports":
                        if (btnReports.Visible)
                            ShowReports();
                        break;
                    case "Personal":
                        if (btnPersonal.Visible)
                            ShowPersonal();
                        break;
                    case "Exit":
                        Application.Exit();
                        break;
                }
            }
        }
        private void ShowFilms()
        {
            FilmEditForm filmsForm = new FilmEditForm();
            filmsForm.TopLevel = false;
            filmsForm.FormBorderStyle = FormBorderStyle.None;
            filmsForm.Dock = DockStyle.Fill;
            contentPanel.Controls.Add(filmsForm);
            filmsForm.Show();
        }

        private void ShowSessions()
        {
            SessionManagerForm sessionsForm = new SessionManagerForm();
            sessionsForm.TopLevel = false;
            sessionsForm.FormBorderStyle = FormBorderStyle.None;
            sessionsForm.Dock = DockStyle.Fill;
            contentPanel.Controls.Add(sessionsForm);
            sessionsForm.Show();
        }

        private void ShowTicketSales()
        {
            TicketSaleForm ticketForm = new TicketSaleForm();
            ticketForm.TopLevel = false;
            ticketForm.FormBorderStyle = FormBorderStyle.None;
            ticketForm.Dock = DockStyle.Fill;
            contentPanel.Controls.Add(ticketForm);
            ticketForm.Show();
        }

        private void ShowInventory()
        {
            InventoryManagerForm inventoryForm = new InventoryManagerForm();
            inventoryForm.TopLevel = false;
            inventoryForm.FormBorderStyle = FormBorderStyle.None;
            inventoryForm.Dock = DockStyle.Fill;
            contentPanel.Controls.Add(inventoryForm);
            inventoryForm.Show();
        }

        private void ShowReports()
        {
            ReportViewerForm reportsForm = new ReportViewerForm();
            reportsForm.TopLevel = false;
            reportsForm.FormBorderStyle = FormBorderStyle.None;
            reportsForm.Dock = DockStyle.Fill;
            contentPanel.Controls.Add(reportsForm);
            reportsForm.Show();
        }

        private void ShowPersonal()
        {
            PersonalManagerForm personalForm = new PersonalManagerForm();
            personalForm.TopLevel = false;
            personalForm.FormBorderStyle = FormBorderStyle.None;
            personalForm.Dock = DockStyle.Fill;
            contentPanel.Controls.Add(personalForm);
            personalForm.Show();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
        }
    }
}