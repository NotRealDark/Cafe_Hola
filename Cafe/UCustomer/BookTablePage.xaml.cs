using Cafe.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Drawing;
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

namespace Cafe.UCustomer
{
    /// <summary>
    /// Interaction logic for BookTablePage.xaml
    /// </summary>
    public partial class BookTablePage : Page
    {
        private Prn212CoffeeContext context = new Prn212CoffeeContext();
        private User account;
        public BookTablePage(Models.User acc)
        {
            InitializeComponent();
            this.account = acc;
            List<string> SizeList = new List<string> { $"Nhỏ: 2 người", $"Vừa: 4 người", $"Lón: 6 người"};
            vlSizeList.ItemsSource = SizeList;
            List<int> nums = new List<int>();
            for (int i = 7; i <= 22; i++)
            {
                nums.Add(i);
            }
            vlHour.ItemsSource = nums;
            vlHour.SelectedIndex = 0;
            nums = new List<int>();
            while (nums.Count < 60)
            {
                nums.Add(nums.Count);
            }
            vlMinute.ItemsSource = nums;
            vlMinute.SelectedIndex = 0;
            LoadHistory();
        }

        private void LoadCount()
        {
            int size = 0;
            switch (vlSizeList.SelectedValue)
            {
                case $"Nhỏ: 2 người":
                    size = 2;
                    break;
                case $"Vừa: 4 người":
                    size = 4;
                    break;
                case $"Lón: 6 người":
                    size = 6;
                    break;
            }
            int count = 0;
            foreach (Models.Table table in context.Tables.ToList())
            {
                if (table.Size == size && table.Status == "free")
                {
                    count++;
                }
            }
            vlAmount.Text = count.ToString();
        }

        private void LoadHistory()
        {
            
            List<TableOrder> history = GetHistory();
            lvBookHistory.ItemsSource = history.Select(h => new
            {
                TableId = h.TableId,
                BookDateTime = $"Ngày {h.BookTime.Day} tháng {h.BookTime.Month} năm {h.BookTime.Year}, {h.BookTime.Hour} giờ {h.BookTime.Minute} phút"
            });
        }

        private List<TableOrder> GetHistory()
        {
            List<TableOrder> history = new List<TableOrder>();
            foreach (TableOrder to in context.TableOrders.ToList())
            {
                if (to.BookerId == account.Uid)
                {
                    history.Add(to);
                }
            }
            return history;
        }

        private void vlSizeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadCount();
        }

        private void btnBook_Click(object sender, RoutedEventArgs e)
        {
            List<TableOrder> history = GetHistory();
            foreach (TableOrder to in history)
            {
                if (to.BookTime.CompareTo(DateTime.Now) > 0 && to.Table.Status != "free")
                {
                    lblWarning.Content = $"Bạn chỉ được đặt và giữ 1 bàn duy nhất.";
                    return;
                }
            }
            if (vlSizeList.Text.IsNullOrEmpty() || vlBookDate.Text.IsNullOrEmpty() || vlHour.Text.IsNullOrEmpty() || vlMinute.Text.IsNullOrEmpty())
            {
                lblWarning.Content = $"Vui lòng điền đầy đủ thông tin để đặt.";
                return;
            }
            if (vlAmount.Text == "0")
            {
                lblWarning.Content = $"Bàn bạn muốn đặt đã hết, vui lòng chọn bàn khác.";
                return;
            }
            DateTime bookDate = new DateTime();
            if (!DateTime.TryParse(vlBookDate.Text, out bookDate))
            {
                lblWarning.Content = $"Ngày tháng không hợp lệ.";
                return;
            }
            int size = 0;
            switch (vlSizeList.SelectedValue)
            {
                case $"Nhỏ: 2 người":
                    size = 2;
                    break;
                case $"Vừa: 4 người":
                    size = 4;
                    break;
                case $"Lón: 6 người":
                    size = 6;
                    break;
            }
            TimeSpan ts = new TimeSpan(int.Parse(vlHour.Text), int.Parse(vlMinute.Text), 0);
            bookDate = bookDate.Date + ts;
            if (bookDate.CompareTo(DateTime.Now) <= 0)
            {
                lblWarning.Content = $"Bạn không thể đặt cho thời gian trong quá khứ.";
                return;
            }
            TableOrder tableOrder = new TableOrder();
            Models.Table table = context.Tables.FirstOrDefault(t => t.Size == size && t.Status == "free");
            tableOrder.BookerId = account.Uid;
            tableOrder.TableId = table.Tid;
            tableOrder.BookTime = bookDate;
            context.TableOrders.Add(tableOrder);
            table.Status = "pending";
            context.Tables.Update(table);
            context.SaveChanges();
            LoadCount();
            LoadHistory();
            lblWarning.Content = $"Đặt bàn thành công.";
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            List<TableOrder> history = GetHistory();
            foreach (TableOrder to in history)
            {
                if (to.BookTime.CompareTo(DateTime.Now) > 0)
                {
                    Models.Table table = context.Tables.FirstOrDefault(t => t.Tid == to.TableId);
                    int size = table.Size;
                    table.Status = "free";
                    context.Tables.Update(table);
                    context.TableOrders.Remove(to);
                    context.SaveChanges();
                    lblWarning.Content = $"Xóa bàn đặt trước thành công.";
                    LoadCount();
                    LoadHistory();
                    return;
                }
            }
            lblWarning.Content = $"Bạn không có bàn đặt trước nào sắp tới.";
        }
    }
}
