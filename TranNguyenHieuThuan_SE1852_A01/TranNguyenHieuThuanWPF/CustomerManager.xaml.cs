using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using Services;
using BusinessObjects;
using System.Linq;

namespace TranNguyenHieuThuanWPF
{
    public partial class CustomerManager : UserControl
    {
        private readonly CustomerService _customerService;
        private ObservableCollection<Customer> _customers;

        public CustomerManager(CustomerService customerService)
        {
            InitializeComponent();
            _customerService = customerService;
            LoadCustomers();
        }

        private void LoadCustomers()
        {
            _customers = new ObservableCollection<Customer>(_customerService.GetAllCustomers());
            dgCustomers.ItemsSource = _customers;
        }

        private void dgCustomers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgCustomers.SelectedItem is Customer cust)
            {
                txtId.Text = cust.CustomerId.ToString();
                txtCompanyName.Text = cust.CompanyName;
                txtContactName.Text = cust.ContactName;
                txtContactTitle.Text = cust.ContactTitle;
                txtAddress.Text = cust.Address;
                txtPhone.Text = cust.Phone;
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            txtStatus.Text = "";
            int id = 0;
            int.TryParse(txtId.Text, out id); 
            var cust = new Customer
            {
                CustomerId = id,
                CompanyName = txtCompanyName.Text.Trim(),
                ContactName = txtContactName.Text.Trim(),
                ContactTitle = txtContactTitle.Text.Trim(),
                Address = txtAddress.Text.Trim(),
                Phone = txtPhone.Text.Trim()
            };
            if (string.IsNullOrWhiteSpace(cust.CompanyName) || string.IsNullOrWhiteSpace(cust.Phone))
            {
                txtStatus.Text = "Tên công ty và SĐT không được để trống!";
                return;
            }
            if (!_customerService.SaveCustomer(cust))
            {
                txtStatus.Text = "Mã KH hoặc SĐT đã tồn tại";
                return;
            }
            txtStatus.Text = "Lưu mới thành công!";
            ClearFields();
            LoadCustomers();
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            txtStatus.Text = "";
            if (!int.TryParse(txtId.Text, out int id))
            {
                txtStatus.Text = "Mã KH không hợp lệ!";
                return;
            }
            var cust = new Customer
            {
                CustomerId = id,
                CompanyName = txtCompanyName.Text.Trim(),
                ContactName = txtContactName.Text.Trim(),
                ContactTitle = txtContactTitle.Text.Trim(),
                Address = txtAddress.Text.Trim(),
                Phone = txtPhone.Text.Trim()
            };
            if (!_customerService.UpdateCustomer(cust))
            {
                txtStatus.Text = "Cập nhật thất bại!";
                return;
            }
            txtStatus.Text = "Cập nhật thành công!";
            ClearFields();
            LoadCustomers();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            txtStatus.Text = "";
            if (!int.TryParse(txtId.Text, out int id))
            {
                txtStatus.Text = "Mã KH không hợp lệ!";
                return;
            }
            var result = MessageBox.Show("Bạn có chắc chắn muốn xóa khách hàng này?", "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result != MessageBoxResult.Yes)
            {
                txtStatus.Text = "Đã hủy xóa khách hàng.";
                return;
            }
            if (_customerService.DeleteCustomer(id))
            {
                txtStatus.Text = "Xóa thành công!";
                ClearFields();
                LoadCustomers();
            }
            else
            {
                txtStatus.Text = "Xóa thất bại!";
            }
        }

        private void BtnShowOrderHistory_Click(object sender, RoutedEventArgs e)
        {
            OrderHistoryPanel.Visibility = Visibility.Visible;
            LoadOrderHistory();
        }

        private void BtnBackToCustomerMenu_Click(object sender, RoutedEventArgs e)
        {
            OrderHistoryPanel.Visibility = Visibility.Collapsed;
        }

        private void LoadOrderHistory()
        {
            int customerId = GetCurrentCustomerId();
            var orders = new Services.OrderService().GetOrdersByCustomerId(customerId);
            dgOrderHistory.ItemsSource = orders.Select(o => new
            {
                o.OrderId,
                o.OrderDate,
                TotalAmount = o.OrderDetails?.Sum(d => d.UnitPrice * d.Quantity) ?? 0
            }).ToList();
        }

        private int GetCurrentCustomerId()
        {
            return 1;
        }

        private void ClearFields()
        {
            txtId.Text = "";
            txtCompanyName.Text = "";
            txtContactName.Text = "";
            txtContactTitle.Text = "";
            txtAddress.Text = "";
            txtPhone.Text = "";
        }
    }
} 