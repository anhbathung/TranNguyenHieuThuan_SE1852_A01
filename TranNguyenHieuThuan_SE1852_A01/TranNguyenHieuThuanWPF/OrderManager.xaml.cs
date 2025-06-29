using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BusinessObjects;
using Services;

namespace TranNguyenHieuThuanWPF
{
    public partial class OrderManager : UserControl
    {
        private readonly OrderService _orderService = new OrderService();
        private readonly CustomerService _customerService = new CustomerService();
        private readonly ProductService _productService = new ProductService();

        private ObservableCollection<Order> _orders;
        private ObservableCollection<OrderDetail> _orderDetails = new ObservableCollection<OrderDetail>();
        private ObservableCollection<Product> _products;
        private int? _selectedOrderId = null;

        public OrderManager()
        {
            InitializeComponent();
            LoadCombos();
            LoadOrders();
        }

        private void LoadCombos()
        {
            cbCustomer.ItemsSource = _customerService.GetAllCustomers();
            cbCustomer.DisplayMemberPath = "CompanyName";
            cbCustomer.SelectedValuePath = "CustomerId";


            _products = new ObservableCollection<Product>(_productService.GetAllProducts());
        }

        private void LoadOrders()
        {
            var orders = _orderService.GetAllOrders();

            // Gán lại navigation property cho từng order
            var customers = _customerService.GetAllCustomers();
            var products = _productService.GetAllProducts();

            foreach (var order in orders)
            {
                order.Customer = customers.FirstOrDefault(c => c.CustomerId == order.CustomerId);
                foreach (var detail in order.OrderDetails)
                {
                    detail.Product = products.FirstOrDefault(p => p.ProductId == detail.ProductId);
                }
            }

            _orders = new ObservableCollection<Order>(orders);
            dgOrders.ItemsSource = _orders.Select(o => new
            {
                o.OrderId,
                CustomerName = o.Customer?.CompanyName ?? "",
                EmployeeName = o.Employee?.Name ?? "",
                o.OrderDate,
                TotalAmount = o.OrderDetails?.Sum(d => d.UnitPrice * d.Quantity) ?? 0
            }).ToList();
        }

        private void dgOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgOrders.SelectedIndex < 0) return;
            var orders = _orderService.GetAllOrders();
            var order = orders.ElementAt(dgOrders.SelectedIndex);
            _selectedOrderId = order.OrderId;
            cbCustomer.SelectedValue = order.CustomerId;
            cbEmployee.SelectedValue = order.EmployeeId;
            dpOrderDate.SelectedDate = order.OrderDate;
            _orderDetails = new ObservableCollection<OrderDetail>(order.OrderDetails ?? new List<OrderDetail>());
            dgOrderDetails.ItemsSource = _orderDetails.Select(d => new
            {
                ProductName = d.Product?.ProductName ?? "",
                d.Quantity,
                d.UnitPrice,
                Total = (d.UnitPrice) * (d.Quantity)
            }).ToList();
        }

        private void BtnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            var win = new AddProductToOrderWindow(_products.ToList());
            if (win.ShowDialog() == true)
            {
                var selectedProduct = win.SelectedProduct;
                int quantity = win.Quantity;
                if (selectedProduct != null && quantity > 0)
                {
                    _orderDetails.Add(new OrderDetail
                    {
                        ProductId = selectedProduct.ProductId,
                        Product = selectedProduct,
                        Quantity = (short)quantity,
                        UnitPrice = (decimal)selectedProduct.UnitPrice
                    });
                    dgOrderDetails.ItemsSource = _orderDetails.Select(d => new
                    {
                        ProductName = d.Product?.ProductName ?? "",
                        d.Quantity,
                        d.UnitPrice,
                        Total = (d.UnitPrice) * (d.Quantity)
                    }).ToList();
                }
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            txtStatus.Text = "";
            if (cbCustomer.SelectedValue == null || cbEmployee.SelectedValue == null || dpOrderDate.SelectedDate == null || !_orderDetails.Any())
            {
                txtStatus.Text = "Vui lòng nhập đủ thông tin và thêm ít nhất 1 sản phẩm!";
                return;
            }
            var order = new Order
            {
                CustomerId = (int)cbCustomer.SelectedValue,
                EmployeeId = (int)cbEmployee.SelectedValue,
                OrderDate = dpOrderDate.SelectedDate.Value,
                OrderDetails = _orderDetails.ToList()
            };
            if (!_orderService.SaveOrder(order))
            {
                txtStatus.Text = "Lưu mới thất bại!";
                return;
            }
            txtStatus.Text = "Lưu mới thành công!";
            ClearFields();
            LoadOrders();
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            txtStatus.Text = "";
            if (_selectedOrderId == null) return;
            var order = _orderService.GetOrderById(_selectedOrderId.Value);
            if (order == null) return;
            order.CustomerId = (int)cbCustomer.SelectedValue;
            order.EmployeeId = (int)cbEmployee.SelectedValue;
            order.OrderDate = dpOrderDate.SelectedDate.Value;
            order.OrderDetails = _orderDetails.ToList();
            if (!_orderService.UpdateOrder(order))
            {
                txtStatus.Text = "Cập nhật thất bại!";
                return;
            }
            txtStatus.Text = "Cập nhật thành công!";
            ClearFields();
            LoadOrders();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            txtStatus.Text = "";
            if (dgOrders.SelectedIndex < 0) return;
            var order = _orderService.GetAllOrders().ElementAt(dgOrders.SelectedIndex);
            var result = MessageBox.Show("Bạn có chắc chắn muốn xóa đơn hàng này?", "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result != MessageBoxResult.Yes)
            {
                txtStatus.Text = "Đã hủy xóa đơn hàng.";
                return;
            }
            if (_orderService.DeleteOrder(order.OrderId))
            {
                txtStatus.Text = "Xóa thành công!";
                ClearFields();
                LoadOrders();
            }
            else
            {
                txtStatus.Text = "Xóa thất bại!";
            }
        }

        private void BtnEditProduct_Click(object sender, RoutedEventArgs e)
        {
            if (dgOrderDetails.SelectedIndex < 0) return;
            var detail = _orderDetails[dgOrderDetails.SelectedIndex];
            var win = new AddProductToOrderWindow(_products.ToList());
            win.cbProduct.SelectedItem = detail.Product;
            win.txtQuantity.Text = detail.Quantity.ToString();
            if (win.ShowDialog() == true)
            {
                detail.Product = win.SelectedProduct;
                detail.ProductId = win.SelectedProduct.ProductId;
                detail.Quantity = (short)win.Quantity;
                detail.UnitPrice = (decimal)win.SelectedProduct.UnitPrice;
                dgOrderDetails.ItemsSource = _orderDetails.Select(d => new
                {
                    ProductName = d.Product?.ProductName ?? "",
                    d.Quantity,
                    d.UnitPrice,
                    Total = (d.UnitPrice) * (d.Quantity)
                }).ToList();
            }
        }

        private void BtnRemoveProduct_Click(object sender, RoutedEventArgs e)
        {
            if (dgOrderDetails.SelectedIndex < 0) return;
            var result = MessageBox.Show("Bạn có chắc chắn muốn xóa sản phẩm này khỏi đơn hàng?", "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result != MessageBoxResult.Yes)
            {
                txtStatus.Text = "Đã hủy xóa sản phẩm khỏi đơn hàng.";
                return;
            }
            _orderDetails.RemoveAt(dgOrderDetails.SelectedIndex);
            dgOrderDetails.ItemsSource = _orderDetails.Select(d => new
            {
                ProductName = d.Product?.ProductName ?? "",
                d.Quantity,
                d.UnitPrice,
                Total = (d.UnitPrice) * (d.Quantity)
            }).ToList();
        }

        private void ClearFields()
        {
            cbCustomer.SelectedIndex = -1;
            cbEmployee.SelectedIndex = -1;
            dpOrderDate.SelectedDate = null;
            _orderDetails.Clear();
            dgOrderDetails.ItemsSource = null;
            _selectedOrderId = null;
        }
    }
} 