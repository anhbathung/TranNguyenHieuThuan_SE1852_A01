using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using Services;
using BusinessObjects;

namespace TranNguyenHieuThuanWPF
{
    public partial class ProductManager : UserControl
    {
        private readonly ProductService _productService;
        private ObservableCollection<Product> _products;

        public ProductManager(ProductService productService)
        {
            InitializeComponent();
            _productService = productService;
            LoadProducts();
        }

        private void LoadProducts()
        {
            _products = new ObservableCollection<Product>(_productService.GetAllProducts());
            dgProducts.ItemsSource = _products;
        }

        private void dgProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgProducts.SelectedItem is Product prod)
            {
                txtId.Text = prod.ProductId.ToString();
                txtName.Text = prod.ProductName;
                txtQuantity.Text = prod.UnitsInStock.ToString();
                txtPrice.Text = prod.UnitPrice.ToString();
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            txtStatus.Text = "";
            int id = 0, quantity = 0;
            decimal price = 0;
            int.TryParse(txtId.Text, out id);
            int.TryParse(txtQuantity.Text, out quantity);
            decimal.TryParse(txtPrice.Text, out price);
            var prod = new Product
            {
                ProductId = id,
                ProductName = txtName.Text.Trim(),
                UnitsInStock = quantity,
                UnitPrice = price
            };
            if (string.IsNullOrWhiteSpace(prod.ProductName))
            {
                txtStatus.Text = "Tên SP không được để trống!";
                return;
            }
            if (!_productService.SaveProduct(prod))
            {
                txtStatus.Text = $"Mã SP đã tồn tại hoặc lỗi khi lưu!";
                return;
            }
            txtStatus.Text = "Lưu mới thành công!";
            ClearFields();
            LoadProducts();
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            txtStatus.Text = "";
            int id = 0, quantity = 0;
            decimal price = 0;
            int.TryParse(txtId.Text, out id);
            int.TryParse(txtQuantity.Text, out quantity);
            decimal.TryParse(txtPrice.Text, out price);
            var prod = new Product
            {
                ProductId = id,
                ProductName = txtName.Text.Trim(),
                UnitsInStock = quantity,
                UnitPrice = price
            };
            if (!_productService.UpdateProduct(prod))
            {
                txtStatus.Text = "Cập nhật thất bại!";
                return;
            }
            txtStatus.Text = "Cập nhật thành công!";
            ClearFields();
            LoadProducts();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            txtStatus.Text = "";
            int id = 0;
            int.TryParse(txtId.Text, out id);
            var result = MessageBox.Show("Bạn có chắc chắn muốn xóa sản phẩm này?", "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result != MessageBoxResult.Yes)
            {
                txtStatus.Text = "Đã hủy xóa sản phẩm.";
                return;
            }
            if (_productService.DeleteProduct(id))
            {
                txtStatus.Text = "Xóa thành công!";
                ClearFields();
                LoadProducts();
            }
            else
            {
                txtStatus.Text = "Xóa thất bại!";
            }
        }

        private void ClearFields()
        {
            txtId.Text = "";
            txtName.Text = "";
            txtQuantity.Text = "";
            txtPrice.Text = "";
        }
    }
} 