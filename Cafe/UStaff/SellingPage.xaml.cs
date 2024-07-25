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

namespace Cafe.UStaff
{
    /// <summary>
    /// Interaction logic for SellingPage.xaml
    /// </summary>
    public partial class SellingPage : Page
    {
        private Prn212CoffeeContext context = new Prn212CoffeeContext();
        private Frame FrStaff;
        private CoffeeOrder? co;
        private User account;

        public SellingPage(Frame frStaff,User acc, int toid)
        {
            InitializeComponent();
            this.FrStaff = frStaff;
            this.account = acc;
            this.co = context.CoffeeOrders.FirstOrDefault(co => co.TableOrderId == toid);
            lblTitle.Content = this.co.TableOrder == null;
            var CoffeeNameList = context.Coffees.Select(c => c.CoffeeName).ToList();
            vlName.ItemsSource = CoffeeNameList;
            if (!CoffeeNameList.IsNullOrEmpty())
            {
                vlName.Text = context.Coffees.ElementAt(0).CoffeeName;
                vlPrice.Text = "" + context.Coffees.ElementAt(0).Price;
            }
            lblTitle.Content = $"Đơn của bàn số {context.TableOrders.FirstOrDefault(to => to.Toid == co.TableOrderId).TableId}";
            LoadList();
        }

        private void LoadList()
        {
            List<OrderItem> orderCoffees = new List<OrderItem>();
            foreach (OrderItem item in context.OrderItems.ToList())
            {
                if (item.CoffeeOrderId == co.Coid)
                {
                    orderCoffees.Add(item);
                }
            }
            double? sum = 0;
            foreach (var orderCoffee in orderCoffees)
            {
                sum += context.Coffees.FirstOrDefault(cf => cf.Cid == orderCoffee.CoffeeId).Price * orderCoffee.Quantity;
            }
            co.TotalPrice = sum;
            context.CoffeeOrders.Update(co);
            context.SaveChanges();
            lvOrderCoffees.ItemsSource = orderCoffees.Select(oc => new
            {
                CoffeeName = context.Coffees.FirstOrDefault(cf => cf.Cid == oc.CoffeeId).CoffeeName,
                Quantity = oc.Quantity,
                PriceEach = context.Coffees.FirstOrDefault(cf => cf.Cid == oc.CoffeeId).Price,
                TotalEach = context.Coffees.FirstOrDefault(cf => cf.Cid == oc.CoffeeId).Price * oc.Quantity
            }).ToList();
            lblTotal.Content = $"Tổng : {sum}";

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            FrStaff.Content = new TableManagementPage(FrStaff, account);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            string note = "";
            int Quantity = 0;
            if (vlQuantity.Text.IsNullOrEmpty())
            {
                note = "miss";
            }
            else if (!int.TryParse(vlQuantity.Text, out Quantity) || Quantity < 1)
            {
                note = "invalid";
            }
            if (note.Equals(""))
            {
                bool exist = false;
                foreach (OrderItem orderItem in context.OrderItems.ToList())
                {
                    if (context.Coffees.FirstOrDefault(cf => cf.Cid == orderItem.CoffeeId).CoffeeName == vlName.Text)
                    {
                        orderItem.Quantity += Quantity;
                        context.OrderItems.Update(orderItem);
                        context.SaveChanges();
                        exist = true;
                        break;
                    }
                }
                if (!exist)
                {
                    OrderItem ot = new OrderItem();
                    ot.CoffeeOrderId = this.co.Coid;
                    ot.CoffeeId = context.Coffees.FirstOrDefault(cf => cf.CoffeeName == vlName.Text).Cid;
                    ot.Quantity = Quantity;
                    context.OrderItems.Add(ot);
                }
                context.SaveChanges();
                LoadList();
            }
            else
            {
                if (note.Equals("miss")) { lblWarning.Content = $"Thiếu số lượng."; }
                else if (note.Equals("invalid")) { lblWarning.Content = "Số lượng không hợp lệ, vui lòng nhập vào số nguyên dương."; }
            }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            string note = "";
            int Quantity = 0;
            if (context.OrderItems.ToList().FirstOrDefault(oc => context.Coffees.FirstOrDefault(cf => cf.Cid == oc.CoffeeId).CoffeeName.Equals(vlName.Text)) == null)
            {
                note = "name";
            }
            else if (!int.TryParse(vlQuantity.Text, out Quantity) || Quantity < 1)
            {
                note = "quantity";
            }
            if (note.Equals(""))
            {
                foreach (OrderItem ot in context.OrderItems.ToList())
                {
                    if (context.Coffees.FirstOrDefault(cf => cf.Cid == ot.CoffeeId).CoffeeName == vlName.Text)
                    {
                        if (ot.Quantity <= Quantity)
                        {
                            context.OrderItems.Remove(ot);
                        }
                        else
                        {
                            ot.Quantity -= Quantity;
                            context.OrderItems.Update(ot);
                        }
                        context.SaveChanges();
                        LoadList();
                        break;
                    }
                }
            }
            else
            {
                if (note.Equals("name")) { lblWarning.Content = "Loại cà phê đang chọn không có trong danh sách đơn hàng"; }
                else { lblWarning.Content = "Số lượng loại bỏ không hợp lệ, vui lòng thử lại."; }
            }
        }

        private void btnCheckout_Click(object sender, RoutedEventArgs e)
        {
            Models.Table table = context.Tables.FirstOrDefault(t => t.Tid == context.TableOrders.FirstOrDefault(to => to.Toid == co.TableOrderId).TableId);
            table.Status = "free";
            context.Tables.Update(table);
            context.SaveChanges();
            FrStaff.Content = new TableManagementPage(FrStaff, account);
        }

    }
}
