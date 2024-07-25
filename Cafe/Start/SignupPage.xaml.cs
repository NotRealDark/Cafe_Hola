using Cafe.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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
    /// Interaction logic for SignupPage.xaml
    /// </summary>
    public partial class SignupPage : Page
    {
        Frame FrStart;
        Prn212CoffeeContext context = new Prn212CoffeeContext();
        public SignupPage(Frame frStart)
        {
            InitializeComponent();
            FrStart = frStart;
        }

        private void btnSignup_Click(object sender, RoutedEventArgs e)
        {
            if (vlUsername.Text.IsNullOrEmpty() || vlPassword.Password.IsNullOrEmpty() || vlRePassword.Password.IsNullOrEmpty() || vlFullName.Text.IsNullOrEmpty() || vlEmail.Text.IsNullOrEmpty() || vlPhone.Text.IsNullOrEmpty())
            {
                lblAlert.Content = $"Vui lòng điền đầu đủ thông tin";
                return;
            }
            Validation v = new Validation();
            String str = v.ValidateUsername(vlUsername.Text, context.Users.ToList());
            if (!str.Equals("Pass"))
            {
                if (str.Equals("OutOfLength"))
                {
                    lblAlert.Content = $"Tên đăng nhập phải có độ dài 8-16 kí tự.";
                } else if (str.Equals("ContainSpecial"))
                {
                    lblAlert.Content = $"Tên đăng nhập chỉ được chứa chữ và số.";
                } else
                {
                    lblAlert.Content = $"Tên đăng nhập này đã được sử dụng, vui lòng chọn cái khác.";
                }
                return;
            } else
            {
                str = v.ValidatePassword(vlPassword.Password);
                if (!str.Equals("Pass"))
                {
                    if (str.Equals("OutOfLength"))
                    {
                        lblAlert.Content = $"Mật khẩu phải có độ dài 8-16 kí tự.";
                    }
                    else
                    {
                        lblAlert.Content = $"Mật khẩu phải chữa chữ hoa, số và kĩ tự đặc biệt.";
                    }
                    return;
                } else if (!vlRePassword.Password.Equals(vlPassword.Password))
                {
                    lblAlert.Content = $"Mật khẩu nhập lại không khớp, vui lòng thử lại";
                    return;
                }
            }
            User user = new User();
            user.Username = vlUsername.Text;
            user.Password = vlPassword.Password;
            user.FullName = vlFullName.Text;
            user.Email = vlEmail.Text;
            user.PhoneNumber = vlPhone.Text;
            user.RoleId = 3;
            context.Users.Add(user);
            context.SaveChanges();
            lblAlert.Content = $"Đăng kí thành công, quay về đăng nhập để tiếp tục.";
        }

        private void btnLoginRedirect_Click(object sender, RoutedEventArgs e)
        {
            FrStart.Content = new LoginPage(FrStart);
        }
    }
}
