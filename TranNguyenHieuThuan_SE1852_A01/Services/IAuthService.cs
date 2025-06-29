using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace Services
{
    public interface IAuthService
    {
        public Employee? EmployeeLogin(string username, string password);
        public Customer? CustomerLogin(string phone);
        public bool IsEmployeeLoggedIn();
        public bool IsCustomerLoggedIn();
        public Employee? GetCurrentEmployee();
        public Customer? GetCurrentCustomer();
        public void Logout();
    }
} 