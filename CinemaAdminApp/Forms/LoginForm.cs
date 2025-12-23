using CinemaAdminApp.Logic;
using CinemaAdminApp.Models;
using System;
using System.Linq;
using System.Windows.Forms;

namespace CinemaAdminApp.Forms
{
    public partial class LoginForm : Form
    {
        private UserManager _userManager = new UserManager();

        public LoginForm()
        {
            InitializeComponent();
            InitializeDefaultAdmin();
        }

        private void InitializeDefaultAdmin()
        {
            if (!_userManager.GetAllUsers().Any())
            {
                _userManager.AddUser("admin", "123", "Администратор");
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string login = txtLogin.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Введите логин и пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            User authenticatedUser = _userManager.Authenticate(login, password);

            if (authenticatedUser != null)
            {
                this.Hide();
                MainForm mainForm = new MainForm(authenticatedUser);
                mainForm.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Clear();
                txtPassword.Focus();
            }
        }

        private void linkForgotPassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Обратитесь к администратору для восстановления пароля", "Восстановление пароля", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            txtLogin.Focus();
        }

        private void txtLogin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtPassword.Focus();
                e.Handled = true;
            }
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnLogin.PerformClick();
                e.Handled = true;
            }
        }
    }
}