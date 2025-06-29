using System.Linq;
using System.Windows;
using Services;

namespace TranNguyenHieuThuanWPF
{
    /// <summary>
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        private readonly AuthService _authService;
        private readonly OrderService _orderService;
        private readonly CustomerService _customerService;

        public CustomerWindow(AuthService authService)
        {
            InitializeComponent();
            
            _authService = authService;
            _orderService = new OrderService();
            _customerService = new CustomerService();

            _orderService.InitializeDataset();
            _customerService.InitializeDataset();

            var currentCustomer = _authService.GetCurrentCustomer();
            if (currentCustomer != null)
            {
                this.Title = $"Customer Portal - {currentCustomer.CompanyName} ({currentCustomer.ContactName})";
            }
        }

        private void OrderHistoryButton_Click(object sender, RoutedEventArgs e)
        {
            MainPanel.Visibility = Visibility.Collapsed;
            OrderHistoryPanel.Visibility = Visibility.Visible;
            LoadOrderHistory();
        }

        private void BtnBackToMenu_Click(object sender, RoutedEventArgs e)
        {
            OrderHistoryPanel.Visibility = Visibility.Collapsed;
            MainPanel.Visibility = Visibility.Visible;
        }

        private void LoadOrderHistory()
        {
            var currentCustomer = _authService.GetCurrentCustomer();
            if (currentCustomer != null)
            {
                var orders = _orderService.GetOrdersByCustomerId(currentCustomer.CustomerId);
                dgOrderHistory.ItemsSource = orders.Select(order => new
                {
                    order.OrderId,
                    order.OrderDate,
                    TotalAmount = _orderService.CalculateOrderTotal(order.OrderId)
                }).ToList();
            }
        }

        private void EditProfileButton_Click(object sender, RoutedEventArgs e)
        {
            MainPanel.Visibility = Visibility.Collapsed;
            EditProfilePanel.Visibility = Visibility.Visible;
            var currentCustomer = _authService.GetCurrentCustomer();
            if (currentCustomer != null)
            {
                txtCompanyName.Text = currentCustomer.CompanyName;
                txtContactName.Text = currentCustomer.ContactName;
                txtContactTitle.Text = currentCustomer.ContactTitle;
                txtAddress.Text = currentCustomer.Address;
                txtPhone.Text = currentCustomer.Phone;
            }
        }

        private void BtnSaveProfile_Click(object sender, RoutedEventArgs e)
        {
            var currentCustomer = _authService.GetCurrentCustomer();
            if (currentCustomer == null)
            {
                MessageBox.Show("Không tìm thấy thông tin khách hàng!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtCompanyName.Text) || string.IsNullOrWhiteSpace(txtContactName.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin bắt buộc!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            currentCustomer.CompanyName = txtCompanyName.Text.Trim();
            currentCustomer.ContactName = txtContactName.Text.Trim();
            currentCustomer.ContactTitle = txtContactTitle.Text.Trim();
            currentCustomer.Address = txtAddress.Text.Trim();
            currentCustomer.Phone = txtPhone.Text.Trim();
            if (_customerService.UpdateCustomer(currentCustomer))
            {
                MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Title = $"Customer Portal - {currentCustomer.CompanyName} ({currentCustomer.ContactName})";
                EditProfilePanel.Visibility = Visibility.Collapsed;
                MainPanel.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Cập nhật thất bại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnBackToMenuFromEdit_Click(object sender, RoutedEventArgs e)
        {
            EditProfilePanel.Visibility = Visibility.Collapsed;
            MainPanel.Visibility = Visibility.Visible;
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            _authService.Logout();
            var loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
    }
} 