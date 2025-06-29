using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using DataAccessLayer;

namespace Repositories
{
    public class CustomerRepositories : ICustomerRepositories
    {
        CustomerDAO customerDAO = new CustomerDAO();

        public List<Customer> GetAllCustomers()
        {
            return customerDAO.GetAllCustomers();
        }

        public void InitializeDataset()
        {
            customerDAO.InitializeDataset();
        }

        public Customer? GetCustomerById(int id)
        {
            return customerDAO.GetCustomerById(id);
        }

        public Customer? GetCustomerByPhone(string phone)
        {
            return customerDAO.GetCustomerByPhone(phone);
        }

        public bool SaveCustomer(Customer customer)
        {
            return customerDAO.SaveCustomer(customer);
        }

        public bool UpdateCustomer(Customer customer)
        {
            return customerDAO.UpdateCustomer(customer);
        }

        public bool DeleteCustomer(int id)
        {
            return customerDAO.DeleteCustomer(id);
        }

        public List<Customer> SearchCustomers(string keyword)
        {
            return customerDAO.SearchCustomers(keyword);
        }
    }
} 