using Cafe.Models;
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
using System.Xml.Linq;

namespace Cafe.UAdmin
{
    /// <summary>
    /// Interaction logic for CoffeeManagement.xaml
    /// </summary>
    public partial class CoffeeManagement : Page
    {
        private Prn212CoffeeContext context = new Prn212CoffeeContext();
        public CoffeeManagement()
        {
            InitializeComponent();
            LoadList();
        }

        private void LoadList()
        {
            var CoffeeList = context.Coffees.ToList();
            lvCoffees.ItemsSource = CoffeeList;
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            vlCid.Text = string.Empty;
            vlCoffeeName.Text = string.Empty;
            vlPrice.Text = string.Empty;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            string CoffeeName = "";
            double Price = 0;
            string note = ",";
            if (vlCoffeeName.Text.IsNullOrEmpty() || !IsValidName(vlCoffeeName.Text))
            {
                note += "name,";
            }
            else
            {
                CoffeeName = vlCoffeeName.Text;
            }
            if (vlPrice.Text.IsNullOrEmpty() || !Double.TryParse(vlPrice.Text, out Price))
            {
                note += "price,";
            }
            if (context.Coffees.FirstOrDefault(cf => cf.CoffeeName == Name) != null)
            {
                note += ",dupe,";
            }
            if (!note.Equals(","))
            {
                string message = "";
                if (!note.Contains(",dupe,"))
                {
                    if (note.Contains(",name,price,")) { message = $"Tên cà phê và giá"; }
                    else if (note.Contains(",name,")) { message = $"Tên cà phê"; } 
                    else if (note.Contains(",price,")) { message = $"Giá"; }
                    message += $" không hợp lệ.";
                }
                else { message = "Tên cà phê đã tồn tại."; }
                lblWarning.Content = message;
            }
            else
            {
                Coffee c = new Coffee();
                c.CoffeeName = CoffeeName;
                c.Price = Price;
                context.Coffees.Add(c);
                context.SaveChanges();
                LoadList();
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (vlCid.Text.IsNullOrEmpty())
            {
                lblWarning.Content = "Chọn 1 loại cà phê bên dưới danh sách để cập nhập.";
                return;
            }
            int Cid = int.Parse(vlCid.Text);
            string CoffeeName = "";
            double Price = 0;
            string note = ",";
            if (vlCoffeeName.Text.IsNullOrEmpty() || !IsValidName(vlCoffeeName.Text))
            {
                note += "name,";
            }
            else
            {
                CoffeeName = vlCoffeeName.Text;
            }
            if (vlPrice.Text.IsNullOrEmpty() || !Double.TryParse(vlPrice.Text, out Price))
            {
                note += "price,";
            }
            if (!note.Equals(","))
            {
                string message = "Invalid ";
                if (note.Equals(",name,price,")) { message += "Name and Price."; }
                else if (note.Equals(",name,")) { message += "Name."; } else { message += "Price."; }
                lblWarning.Content = message;
            }
            else
            {
                Coffee? c = context.Coffees.FirstOrDefault(cf => cf.Cid == Cid);
                if (c == null)
                {
                    return;
                }
                c.CoffeeName = CoffeeName;
                c.Price = Price;
                context.SaveChanges();
                LoadList();
            }
        }

        private bool IsValidName(string name)
        {
            List<char> lc = name.ToLower().ToList();
            foreach (char c in lc)
            {
                if (c != ' ' && !char.IsLetter(c))
                {
                    return false;
                }
            }
            return true;
        }

        private void lvCoffees_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = lvCoffees.SelectedItem as Coffee;
            if (selected == null)
            {
                return;
            }
            vlCid.Text = "" + selected.Cid;
            vlCoffeeName.Text = selected.CoffeeName;
            vlPrice.Text = "" + selected.Price;
        }
    }
}
