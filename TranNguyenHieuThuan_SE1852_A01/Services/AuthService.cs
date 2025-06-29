using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Repositories;

namespace Services
{
    public class AuthService : IAuthService
    {
        private readonly ICustomerRepositories _customerRepositories;
        private Employee? _currentEmployee;
        private Customer? _currentCustomer;
        
        private static readonly List<Employee> _employees = new List<Employee>
        {
            new Employee
            {
                EmployeeId = 1,
                Name = "admin",
                UserName = "thuanne",
                Password = "123456",
                JobTitle = "Manager"
            },
            new Employee
            {
                EmployeeId = 2,
                Name = "staff",
                UserName = "nhanvien",
                Password = "654321",
                JobTitle = "Staff"
            },
            new Employee
            {
                EmployeeId = 3,
                Name = "sales",
                UserName = "banhang",
                Password = "456789",
                JobTitle = "Sales Representative"
            }
        };

        public AuthService()
        {
            _customerRepositories = new CustomerRepositories();
            _customerRepositories.InitializeDataset(); 
        }

        public Employee? EmployeeLogin(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return null;
            }

            var employee = _employees.FirstOrDefault(e => e.UserName == username && e.Password == password);
            if (employee != null)
            {
                _currentEmployee = employee;
                _currentCustomer = null; 
            }
            return employee;
        }

        public Customer? CustomerLogin(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
            {
                return null;
            }

            var customer = _customerRepositories.GetCustomerByPhone(phone);
            if (customer != null)
            {
                _currentCustomer = customer;
                _currentEmployee = null; 
            }
            return customer;
        }

        public bool IsEmployeeLoggedIn()
        {
            return _currentEmployee != null;
        }

        public bool IsCustomerLoggedIn()
        {
            return _currentCustomer != null;
        }

        public Employee? GetCurrentEmployee()
        {
            return _currentEmployee;
        }

        public Customer? GetCurrentCustomer()
        {
            return _currentCustomer;
        }

        public void Logout()
        {
            _currentEmployee = null;
            _currentCustomer = null;
        }
    }
} 