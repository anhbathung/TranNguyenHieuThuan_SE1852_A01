using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace Services
{
    public interface ICustomerService
    {
        public List<Customer> GetAllCustomers();
        public void InitializeDataset();
        public Customer? GetCustomerById(int id);
        public Customer? GetCustomerByPhone(string phone);
        public bool SaveCustomer(Customer customer);
        public bool UpdateCustomer(Customer customer);
        public bool DeleteCustomer(int id);
        public List<Customer> SearchCustomers(string keyword);
    }
} 