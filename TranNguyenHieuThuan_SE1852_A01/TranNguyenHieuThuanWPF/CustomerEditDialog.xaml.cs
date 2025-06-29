using System.Windows;
using BusinessObjects;

namespace TranNguyenHieuThuanWPF
{
    public partial class CustomerEditDialog : Window
    {
        public Customer EditedCustomer { get; private set; }

        public CustomerEditDialog(Customer customer)
        {
            InitializeComponent();
            txtCompanyName.Text = customer.CompanyName;
            txtContactName.Text = customer.ContactName;
            txtContactTitle.Text = customer.ContactTitle;
            txtAddress.Text = customer.Address;
            txtPhone.Text = customer.Phone;
            EditedCustomer = new Customer
            {
                CustomerId = customer.CustomerId
            };
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCompanyName.Text) ||
                string.IsNullOrWhiteSpace(txtContactName.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin bắt buộc!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            EditedCustomer.CompanyName = txtCompanyName.Text.Trim();
            EditedCustomer.ContactName = txtContactName.Text.Trim();
            EditedCustomer.ContactTitle = txtContactTitle.Text.Trim();
            EditedCustomer.Address = txtAddress.Text.Trim();
            EditedCustomer.Phone = txtPhone.Text.Trim();
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