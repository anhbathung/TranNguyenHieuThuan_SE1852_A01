using System.Windows;
using Services;
using BusinessObjects;

namespace TranNguyenHieuThuanWPF
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly AuthService _authService;
        private readonly CustomerService _customerService;

       public LoginWindow()
        {
            InitializeComponent();
            
            _authService = new AuthService();
            _customerService = new CustomerService();

            // Initialize datasets
            _customerService.InitializeDataset();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Password;
            string phone = txtPhone.Text.Trim();

            if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password))
            {
                var employee = _authService.EmployeeLogin(username, password);
                if (employee != null)
                {
                    txtStatus.Text = "Đăng nhập nhân viên thành công!";
                    var mainWindow = new MainWindow(_authService);
                    mainWindow.Show();
                    this.Close();
                    return;
                }
                else
                {
                    txtStatus.Text = "Sai tài khoản hoặc mật khẩu nhân viên!";
                    return;
                }
            }

            if (!string.IsNullOrWhiteSpace(phone))
            {
                var customer = _authService.CustomerLogin(phone);
                if (customer != null)
                {
                    txtStatus.Text = "Đăng nhập khách hàng thành công!";
                    var customerWindow = new CustomerWindow(_authService);
                    customerWindow.Show();
                    this.Close();
                    return;
                }
                else
                {
                    txtStatus.Text = "Số điện thoại không tồn tại!";
                    return;
                }
            }

            txtStatus.Text = "Vui lòng nhập thông tin đăng nhập!";
        }
    }
} 