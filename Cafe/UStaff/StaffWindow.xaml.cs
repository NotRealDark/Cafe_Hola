using Cafe.Models;
using Cafe.Start;
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
using System.Windows.Shapes;

namespace Cafe.UStaff
{
    /// <summary>
    /// Interaction logic for StaffWindow.xaml
    /// </summary>
    public partial class StaffWindow : Window
    {
        private Prn212CoffeeContext context = new Prn212CoffeeContext();
        private User account;

        public StaffWindow(User acc)
        {
            InitializeComponent();
            account = acc;
            lblStaffName.Content = account.Username;
            frStaff.Content = new TableManagementPage(frStaff, account);
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            StartWindow w = new StartWindow();
            w.Show();
            this.Close();
        }
    }

}
