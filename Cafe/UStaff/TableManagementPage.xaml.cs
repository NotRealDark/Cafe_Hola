using Cafe.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
    /// Interaction logic for TableManagementPage.xaml
    /// </summary>
    public partial class TableManagementPage : Page
    {
        private Prn212CoffeeContext context = new Prn212CoffeeContext();
        private Frame FrStaff;
        private User account;
        public TableManagementPage(Frame frStaff, User acc)
        {
            InitializeComponent();
            LoadTables();
            FrStaff = frStaff;
            account = acc;
        }

        private void LoadTables()
        {
            List<Models.Table> Tables = context.Tables.ToList();
            List<Models.Table> FreeTables = new List<Models.Table>();
            foreach (Models.Table table in Tables)
            {
                if (table.Status == "free")
                {
                    FreeTables.Add(table);
                }
            }
            vlTid.ItemsSource = FreeTables.Select(t => t.Tid);
            List<TableOrder> to = context.TableOrders.ToList();
            List<TableOrder> ut = new List<TableOrder>();
            List<TableOrder> pt = new List<TableOrder>();
            foreach (TableOrder order in to)
            {
                if (order.Table.Status == "using")
                {
                    ut.Add(order);
                } else if (order.Table.Status == "pending")
                {
                    pt.Add(order);
                }
            }
            lvUsingTables.ItemsSource = ut.Select(x => new
            {
                Toid = x.Toid,
                TableId = x.TableId,
                FullName = (x.Booker == null ? $"Trực Tiếp" : x.Booker.FullName),
                BookTime = x.BookTime
            });
            lvPendingTables.ItemsSource = pt.Select(x => new
            {
                Toid = x.Toid,
                TableId = x.TableId,
                FullName = context.Users.FirstOrDefault(u => u.Uid == x.BookerId).FullName,
                BookTime = x.BookTime
            });
        }

        private void btnUse_Click(object sender, RoutedEventArgs e)
        {
            if (vlTid.Text.IsNullOrEmpty())
            {
                lblWarning.Content = $"Chọn 1 bàn để sử dụng.";
                return;
            }
            int tid = int.Parse(vlTid.Text);
            foreach (Models.Table table in context.Tables.ToList())
            {
                if (table.Tid == tid)
                {
                    table.Status = "using";
                    context.Tables.Update(table);
                    context.SaveChanges();
                    TableOrder to = new TableOrder();
                    to.TableId = tid;
                    DateTime current = DateTime.Now;
                    to.BookTime = current;
                    context.TableOrders.Add(to);
                    context.SaveChanges();
                    CoffeeOrder co = new CoffeeOrder();
                    co.TableOrderId = context.TableOrders.ToList().FirstOrDefault(to => to.TableId == tid && to.BookTime == current).Toid;
                    co.SellerId = account.Uid;
                    co.TotalPrice = 0;
                    context.CoffeeOrders.Add(co);
                    context.SaveChanges();
                    LoadTables();
                    break;
                }
            }
            
        }

        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            if (vlUsingToid.Text.IsNullOrEmpty())
            {
                lblWarning.Content = $"Chọn 1 bàn ở danh sách bàn đang sử dụng để xem chi tiết.";
                return;
            }
            FrStaff.Content = new SellingPage(FrStaff, account, int.Parse(vlUsingToid.Text));
        }

        private void lvUsingTables_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (vlPendingToid.Text.IsNullOrEmpty())
            {
                lblWarning.Content = $"Chọn 1 bàn ở danh sách đơn đặt bàn tương tác.";
                return;
            }
            TableOrder tableOrder = context.TableOrders.FirstOrDefault(to => to.Toid == int.Parse(vlPendingToid.Text));
            Models.Table table = context.Tables.FirstOrDefault(t => t.Tid == tableOrder.TableId);
            if (table.Status == "pending")
            {
                if (tableOrder.BookTime.CompareTo(DateTime.Now) > 0)
                {
                    lblWarning.Content = $"Chỉ có thể thêm đơn có thời gian đặt đã qua thời gian hiện tại.";
                    return;
                }
                table.Status = "using";
                context.Tables.Update(table);
                context.SaveChanges();
                CoffeeOrder co = new CoffeeOrder();
                co.TableOrderId = tableOrder.Toid;
                co.SellerId = account.Uid;
                co.TotalPrice = 0;
                context.CoffeeOrders.Add(co);
                context.SaveChanges();
                LoadTables();
            }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (vlPendingToid.Text.IsNullOrEmpty())
            {
                lblWarning.Content = $"Chọn 1 bàn ở danh sách đơn đặt bàn tương tác.";
                return;
            }
            TableOrder tableOrder = context.TableOrders.FirstOrDefault(to => to.Toid == int.Parse(vlPendingToid.Text));
            Models.Table table = context.Tables.FirstOrDefault(t => t.Tid == tableOrder.TableId);
            if (table.Status == "pending")
            {
                if (tableOrder.BookTime.CompareTo(DateTime.Now) > 0)
                {
                    lblWarning.Content = $"Chỉ có thể xóa đơn có thời gian đặt đã qua thời gian hiện tại.";
                    return;
                }
                table.Status = "free";
                context.Tables.Update(table);
                context.TableOrders.Remove(tableOrder);
                context.SaveChanges();
                LoadTables();
            }
        }
    }
}
