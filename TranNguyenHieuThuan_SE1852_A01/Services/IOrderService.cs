using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace Services
{
    public interface IOrderService
    {
        public List<Order> GetAllOrders();
        public void InitializeDataset();
        public Order? GetOrderById(int id);
        public List<Order> GetOrdersByCustomerId(int customerId);
        public List<Order> GetOrdersByEmployeeId(int employeeId);
        public bool SaveOrder(Order order);
        public bool UpdateOrder(Order order);
        public bool DeleteOrder(int id);
        public List<OrderDetail> GetOrderDetailsByOrderId(int orderId);
        public bool SaveOrderDetail(OrderDetail orderDetail);
        public bool DeleteOrderDetail(int orderId, int productId);
        public List<Order> SearchOrders(string keyword);
        public List<Order> GetOrdersByDateRange(DateTime startDate, DateTime endDate);
        public decimal CalculateOrderTotal(int orderId);
    }
} 