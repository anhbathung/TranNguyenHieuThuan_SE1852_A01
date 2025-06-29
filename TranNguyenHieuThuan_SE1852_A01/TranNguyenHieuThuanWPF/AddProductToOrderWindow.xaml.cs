using System.Collections.Generic;
using System.Windows;
using BusinessObjects;

namespace TranNguyenHieuThuanWPF
{
    public partial class AddProductToOrderWindow : Window
    {
        public Product? SelectedProduct { get; private set; }
        public int Quantity { get; private set; }

        public AddProductToOrderWindow(List<Product> products)
        {
            InitializeComponent();
            cbProduct.ItemsSource = products;
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            txtStatus.Text = "";
            if (cbProduct.SelectedItem is not Product product)
            {
                txtStatus.Text = "Vui lòng chọn sản phẩm!";
                return;
            }
            if (!int.TryParse(txtQuantity.Text, out int qty) || qty <= 0)
            {
                txtStatus.Text = "Số lượng phải là số nguyên dương!";
                return;
            }
            SelectedProduct = product;
            Quantity = qty;
            DialogResult = true;
            Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
} 