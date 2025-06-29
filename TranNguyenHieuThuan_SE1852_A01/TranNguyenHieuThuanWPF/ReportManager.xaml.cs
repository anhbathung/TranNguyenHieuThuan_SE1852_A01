using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BusinessObjects;
using Services;

namespace TranNguyenHieuThuanWPF
{
    public partial class ReportManager : UserControl
    {
        private readonly OrderService _orderService = new OrderService();
        private ObservableCollection<Order> _orders;

        public ReportManager()
        {
            InitializeComponent();
            LoadAllOrders();
        }

        private void LoadAllOrders()
        {
            var orders = _orderService.GetAllOrders();
            _orders = new ObservableCollection<Order>(orders);
            ShowOrders(_orders);
        }

        private void BtnFilter_Click(object sender, RoutedEventArgs e)
        {
            DateTime? from = dpFrom.SelectedDate;
            DateTime? to = dpTo.SelectedDate;
            var orders = _orderService.GetAllOrders();
            if (from != null && to != null)
            {
                orders = orders.Where(o => o.OrderDate >= from && o.OrderDate <= to).ToList();
            }
            var sorted = orders.OrderByDescending(o => o.OrderDate).ToList();
            ShowOrders(new ObservableCollection<Order>(sorted));
        }

        private void ShowOrders(ObservableCollection<Order> orders)
        {
            dgOrders.ItemsSource = orders.Select(o => new
            {
                o.OrderId,
                CustomerName = o.Customer?.CompanyName ?? "",
                EmployeeName = o.Employee?.Name ?? "",
                o.OrderDate,
                TotalAmount = o.OrderDetails?.Sum(d => d.UnitPrice * d.Quantity) ?? 0
            }).ToList();
            txtTotalOrders.Text = orders.Count.ToString();
            txtTotalRevenue.Text = orders.Sum(o => o.OrderDetails?.Sum(d => d.UnitPrice * d.Quantity) ?? 0).ToString("N0");
        }
    }
} 