using Cafe.Models;
using Cafe.UAdmin;
using Cafe.UCustomer;
using Cafe.UStaff;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Cafe.Start
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        Frame FrStart;
        Prn212CoffeeContext context = new Prn212CoffeeContext();
        public LoginPage(Frame frStart)
        {
            InitializeComponent();
            FrStart = frStart;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (vlUsername.Text.IsNullOrEmpty() || vlPassword.Password.IsNullOrEmpty())
            {
                lblAlert.Content = $"Vui lòng điền đầy đủ thông tin";
                return;
            }
            string username = vlUsername.Text;
            string password = vlPassword.Password;
            List<User> users = context.Users.ToList();
            User? acc = null;
            foreach (User user in users)
            {
                if (user.Username == username)
                {
                    if (user.Password == password)
                    {
                        acc = user;
                    } else
                    {
                        break;
                    }
                }
            }
            if (acc != null)
            {
                switch (acc.RoleId)
                {
                    case 1:
                        AdminWindow aw = new AdminWindow(acc);
                        aw.Show();
                        Window.GetWindow(this).Close();
                        break;
                    case 2:
                        StaffWindow sw = new StaffWindow(acc);
                        sw.Show();
                        Window.GetWindow(this).Close();
                        break;
                    case 3:
                        CustomerWindow cw = new CustomerWindow(acc);
                        cw.Show();
                        Window.GetWindow(this).Close();
                        break;
                }
            } else
            {
                lblAlert.Content = $"Tên đăng nhập/Mật khẩu không hợp lệ.";
            }
        }

        private void btnSignupRedirect_Click(object sender, RoutedEventArgs e)
        {
            FrStart.Content = new SignupPage(FrStart);
        }
    }
}
