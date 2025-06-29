using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Repositories;

namespace Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepositories _customerRepositories;

        public CustomerService()
        {
            _customerRepositories = new CustomerRepositories();
        }

        public List<Customer> GetAllCustomers()
        {
            return _customerRepositories.GetAllCustomers();
        }

        public void InitializeDataset()
        {
            _customerRepositories.InitializeDataset();
        }

        public Customer? GetCustomerById(int id)
        {
            return _customerRepositories.GetCustomerById(id);
        }

        public Customer? GetCustomerByPhone(string phone)
        {
            return _customerRepositories.GetCustomerByPhone(phone);
        }

        public bool SaveCustomer(Customer customer)
        {
            if (string.IsNullOrWhiteSpace(customer.CompanyName) || string.IsNullOrWhiteSpace(customer.Phone))
            {
                return false;
            }

            if (_customerRepositories.GetCustomerByPhone(customer.Phone) != null)
            {
                return false;
            }

            return _customerRepositories.SaveCustomer(customer);
        }

        public bool UpdateCustomer(Customer customer)
        {
            if (string.IsNullOrWhiteSpace(customer.CompanyName) || string.IsNullOrWhiteSpace(customer.Phone))
            {
                return false;
            }

            return _customerRepositories.UpdateCustomer(customer);
        }

        public bool DeleteCustomer(int id)
        {
            return _customerRepositories.DeleteCustomer(id);
        }

        public List<Customer> SearchCustomers(string keyword)
        {
            return _customerRepositories.SearchCustomers(keyword);
        }
    }
} 