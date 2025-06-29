using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace DataAccessLayer
{
    public class CustomerDAO
    {
        static List<Customer> customers = new List<Customer>();

        public void InitializeDataset()
        {
            if (customers.Count > 0) return;
            customers.Add(new Customer
            {
                CustomerId = 1,
                CompanyName = "ABC ",
                ContactName = "Jane",
                ContactTitle = "Manager",
                Address = "123 ",
                City = "Ho Chi Minh",
                Country = "Vietnam",
                Phone = "0357924680"
            });
            customers.Add(new Customer
            {
                CustomerId = 2,
                CompanyName = "DEF",
                ContactName = "John",
                ContactTitle = "Director",
                Address = "456 ",
                City = "Hanoi",
                Country = "Vietnam",
                Phone = "0987654321"
            });
            customers.Add(new Customer
            {
                CustomerId = 3,
                CompanyName = "XYZ",
                ContactName = "Mike",
                ContactTitle = "CEO",
                Address = "789 ",
                City = "Da Nang",
                Country = "Vietnam",
                Phone = "0123456789"
            });
        }

        public List<Customer> GetAllCustomers()
        {
            return customers;
        }

        public Customer? GetCustomerById(int id)
        {
            return customers.FirstOrDefault(c => c.CustomerId == id);
        }

        public Customer? GetCustomerByPhone(string phone)
        {
            return customers.FirstOrDefault(c => c.Phone == phone);
        }

        public bool SaveCustomer(Customer customer)
        {
            Customer old = customers.FirstOrDefault(x => x.CustomerId == customer.CustomerId);
            if (old != null)
                return false;
            customers.Add(customer);
            return true;
        }

        public bool UpdateCustomer(Customer customer)
        {
            Customer old = customers.FirstOrDefault(x => x.CustomerId == customer.CustomerId);
            if (old == null)
                return false;
            old.CompanyName = customer.CompanyName;
            old.ContactName = customer.ContactName;
            old.ContactTitle = customer.ContactTitle;
            old.Address = customer.Address;
            old.City = customer.City;
            old.Country = customer.Country;
            old.Phone = customer.Phone;
            return true;
        }

        public bool DeleteCustomer(int id)
        {
            Customer old = customers.FirstOrDefault(x => x.CustomerId == id);
            if (old == null)
                return false;
            customers.Remove(old);
            return true;
        }

        public List<Customer> SearchCustomers(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return GetAllCustomers();
            return customers.Where(c => c.CompanyName.Contains(keyword, StringComparison.OrdinalIgnoreCase)
                || c.ContactName?.Contains(keyword, StringComparison.OrdinalIgnoreCase) == true
                || c.Phone?.Contains(keyword, StringComparison.OrdinalIgnoreCase) == true)
                .ToList();
        }
    }
} 