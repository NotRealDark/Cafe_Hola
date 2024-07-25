using Cafe.Models;
using Cafe.Start;
using Cafe.UStaff;
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

namespace Cafe.UCustomer
{
    /// <summary>
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        private Prn212CoffeeContext context = new Prn212CoffeeContext();
        private User account;

        public CustomerWindow(User acc)
        {
            InitializeComponent();
            account = acc;
            lblCustomerName.Content = account.Username;
            frCustomer.Content = new BookTablePage(account);
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            StartWindow w = new StartWindow();
            w.Show();
            this.Close();
        }
    }
}
