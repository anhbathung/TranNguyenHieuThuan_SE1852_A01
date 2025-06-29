using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace DataAccessLayer
{
    public class OrderDAO
    {
        static List<Order> orders = new List<Order>();
        static List<OrderDetail> orderDetails = new List<OrderDetail>();
        
       
        private static readonly List<Employee> _employees = new List<Employee>
        {
            new Employee
            {
                EmployeeId = 1,
                Name = "thuanne",
                UserName = "admin",
                Password = "123456",
                JobTitle = "Manager"
            },
            new Employee
            {
                EmployeeId = 2,
                Name = "nhan vien",
                UserName = "staff",
                Password = "654321",
                JobTitle = "Staff"
            },
            new Employee
            {
                EmployeeId = 3,
                Name = "ban hang",
                UserName = "sales",
                Password = "456789",
                JobTitle = "Sales Representative"
            }
        };

        public void InitializeDataset()
        {
            if (orders.Count > 0) return;
            orders.Add(new Order
            {
                OrderId = 1,
                CustomerId = 1,
                EmployeeId = 1,
                OrderDate = DateTime.Now.AddDays(-10),
                RequiredDate = DateTime.Now.AddDays(-5),
                ShippedDate = DateTime.Now.AddDays(-7),
                ShipVia = 1,
                Freight = 15.50m
            });
            orders.Add(new Order
            {
                OrderId = 2,
                CustomerId = 2,
                EmployeeId = 2,
                OrderDate = DateTime.Now.AddDays(-5),
                RequiredDate = DateTime.Now.AddDays(-2),
                ShippedDate = DateTime.Now.AddDays(-3),
                ShipVia = 2,
                Freight = 25.00m
            });

           
            orderDetails.Add(new OrderDetail
            {
                OrderId = 1,
                ProductId = 1,
                UnitPrice = 1299.99m,
                Quantity = 2,
                Discount = 0.05f
            });
            orderDetails.Add(new OrderDetail
            {
                OrderId = 1,
                ProductId = 2,
                UnitPrice = 999.99m,
                Quantity = 1,
                Discount = 0.00f
            });
            orderDetails.Add(new OrderDetail
            {
                OrderId = 2,
                ProductId = 3,
                UnitPrice = 899.99m,
                Quantity = 3,
                Discount = 0.10f
            });
        }

        public List<Order> GetAllOrders()
        {
            
            var customers = new CustomerDAO().GetAllCustomers();
            var products = new ProductDAO().GetAllProducts();

            foreach (var order in orders)
            {
               
                order.OrderDetails = orderDetails.Where(od => od.OrderId == order.OrderId).ToList();

                
                order.Customer = customers.FirstOrDefault(c => c.CustomerId == order.CustomerId);
                order.Employee = _employees.FirstOrDefault(e => e.EmployeeId == order.EmployeeId);

                foreach (var detail in order.OrderDetails)
                {
                    detail.Product = products.FirstOrDefault(p => p.ProductId == detail.ProductId);
                }
            }
            return orders;
        }

        public Order? GetOrderById(int id)
        {
            return orders.FirstOrDefault(o => o.OrderId == id);
        }

        public List<Order> GetOrdersByCustomerId(int customerId)
        {
            return orders.Where(o => o.CustomerId == customerId).ToList();
        }

        public List<Order> GetOrdersByEmployeeId(int employeeId)
        {
            return orders.Where(o => o.EmployeeId == employeeId).ToList();
        }

        public bool SaveOrder(Order order)
        {
            if (order.OrderId == 0)
            {
                int maxId = orders.Any() ? orders.Max(x => x.OrderId) : 0;
                order.OrderId = maxId + 1;
            }
            Order old = orders.FirstOrDefault(x => x.OrderId == order.OrderId);
            if (old != null)
                return false;
            orders.Add(order);
            return true;
        }

        public bool UpdateOrder(Order order)
        {
            Order old = orders.FirstOrDefault(x => x.OrderId == order.OrderId);
            if (old == null)
                return false;
            old.CustomerId = order.CustomerId;
            old.EmployeeId = order.EmployeeId;
            old.OrderDate = order.OrderDate;
            old.RequiredDate = order.RequiredDate;
            old.ShippedDate = order.ShippedDate;
            old.ShipVia = order.ShipVia;
            old.Freight = order.Freight;
            orderDetails.RemoveAll(od => od.OrderId == order.OrderId);
            if (order.OrderDetails != null)
            {
                foreach (var detail in order.OrderDetails)
                {
                    var newDetail = new OrderDetail
                    {
                        OrderId = order.OrderId,
                        ProductId = detail.ProductId,
                        Quantity = detail.Quantity,
                        UnitPrice = detail.UnitPrice,
                        Discount = detail.Discount,
                        Product = detail.Product
                    };
                    orderDetails.Add(newDetail);
                }
            }
            return true;
        }

        public bool DeleteOrder(int id)
        {
            Order old = orders.FirstOrDefault(x => x.OrderId == id);
            if (old == null)
                return false;
            orders.Remove(old);
            orderDetails.RemoveAll(od => od.OrderId == id);
            return true;
        }

        public List<OrderDetail> GetOrderDetailsByOrderId(int orderId)
        {
            return orderDetails.Where(od => od.OrderId == orderId).ToList();
        }

        public bool SaveOrderDetail(OrderDetail orderDetail)
        {
            orderDetails.Add(orderDetail);
            return true;
        }

        public bool DeleteOrderDetail(int orderId, int productId)
        {
            var detail = orderDetails.FirstOrDefault(od => od.OrderId == orderId && od.ProductId == productId);
            if (detail == null)
                return false;
            orderDetails.Remove(detail);
            return true;
        }

        public List<Order> SearchOrders(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return GetAllOrders();
            return orders.Where(o => o.OrderId.ToString().Contains(keyword, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
    }
} 