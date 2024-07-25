using Cafe.Models;
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

namespace Cafe.UAdmin
{
    /// <summary>
    /// Interaction logic for CoffeeOrderManagement.xaml
    /// </summary>
    public partial class CoffeeOrderManagement : Page
    {
        Prn212CoffeeContext context = new Prn212CoffeeContext();
        List<CoffeeOrder> coffeeOrders;
        public CoffeeOrderManagement()
        {
            InitializeComponent();
            coffeeOrders = context.CoffeeOrders.ToList();
            lvCofeeOrders.ItemsSource = coffeeOrders.Select(co => new
            {
                CustomerName = (context.TableOrders.FirstOrDefault(to => to.Toid == co.TableOrderId).BookerId == null ? $"Trực Tiếp" : context.Users.FirstOrDefault(u => u.Uid == context.TableOrders.FirstOrDefault(to => to.Toid == co.TableOrderId).BookerId).FullName),
                SellerName = context.Users.FirstOrDefault(u => u.Uid == co.SellerId).FullName,
                TotalPrice = co.TotalPrice,
                SellDate = context.TableOrders.FirstOrDefault(to => to.Toid == co.TableOrderId).BookTime,
                TableId = context.TableOrders.FirstOrDefault(to => to.Toid == co.TableOrderId).TableId,
                Coid = co.Coid
            });
        }

        private void lvCofeeOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int coid = int.Parse(vlHiddenCoid.Text);
            List<OrderItem> orderItems = new List<OrderItem>();
            foreach (OrderItem ot in context.OrderItems.ToList())
            {
                if (ot.CoffeeOrderId == coid)
                {
                    orderItems.Add(ot);
                }
            }
            lvOrderCoffees.ItemsSource = orderItems.Select(ot => new
            {
                CoffeeName = context.Coffees.FirstOrDefault(c => c.Cid == ot.CoffeeId).CoffeeName,
                Quantity = ot.Quantity,
                PriceEach = context.Coffees.FirstOrDefault(c => c.Cid == ot.CoffeeId).Price,
                TotalEach = context.Coffees.FirstOrDefault(c => c.Cid == ot.CoffeeId).Price * ot.Quantity,
            });
        }
    }
}
