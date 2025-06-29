using System.Windows;
using Services;
using TranNguyenHieuThuanWPF;

namespace TranNguyenHieuThuanWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly AuthService _authService;
        private readonly ProductService _productService;
        private readonly CustomerService _customerService;
        private readonly CategoryService _categoryService;
        private readonly OrderService _orderService;

        public MainWindow(AuthService authService)
        {
            InitializeComponent();
            
            _authService = authService;
            _productService = new ProductService();
            _customerService = new CustomerService();
            _categoryService = new CategoryService();
            _orderService = new OrderService();

            _productService.InitializeDataset();
            _customerService.InitializeDataset();
            _categoryService.InitializeDataset();
            _orderService.InitializeDataset();

            var currentEmployee = _authService.GetCurrentEmployee();
            if (currentEmployee != null)
            {
                this.Title = $"Ha Viet Thanh - Sales Management System - {currentEmployee.Name} ({currentEmployee.JobTitle})";
            }
        }


        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            _authService.Logout();
            var loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

        private void btnCustomer_Click(object sender, RoutedEventArgs e)
        {
            var customerService = new CustomerService();
            MainContent.Content = new CustomerManager(customerService);
        }

        private void btnProduct_Click(object sender, RoutedEventArgs e)
        {
            var productService = new ProductService();
            MainContent.Content = new ProductManager(productService);
        }

        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new OrderManager();
        }

        private void ReportButton_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new ReportManager();
        }
    }
} 