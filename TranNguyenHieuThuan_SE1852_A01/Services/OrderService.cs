using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Repositories;

namespace Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepositories _orderRepositories;
        private readonly IProductRepositories _productRepositories;

        public OrderService()
        {
            _orderRepositories = new OrderRepositories();
            _productRepositories = new ProductRepositories();
        }

        public List<Order> GetAllOrders()
        {
            return _orderRepositories.GetAllOrders();
        }

        public void InitializeDataset()
        {
            _orderRepositories.InitializeDataset();
        }

        public Order? GetOrderById(int id)
        {
            return _orderRepositories.GetOrderById(id);
        }

        public List<Order> GetOrdersByCustomerId(int customerId)
        {
            return _orderRepositories.GetOrdersByCustomerId(customerId);
        }

        public List<Order> GetOrdersByEmployeeId(int employeeId)
        {
            return _orderRepositories.GetOrdersByEmployeeId(employeeId);
        }

        public bool SaveOrder(Order order)
        {
            if (order.CustomerId == null || order.EmployeeId == null || order.OrderDate == null)
            {
                return false;
            }

            return _orderRepositories.SaveOrder(order);
        }

        public bool UpdateOrder(Order order)
        {
            if (order.CustomerId == null || order.EmployeeId == null || order.OrderDate == null)
            {
                return false;
            }

            return _orderRepositories.UpdateOrder(order);
        }

        public bool DeleteOrder(int id)
        {
            return _orderRepositories.DeleteOrder(id);
        }

        public List<OrderDetail> GetOrderDetailsByOrderId(int orderId)
        {
            return _orderRepositories.GetOrderDetailsByOrderId(orderId);
        }

        public bool SaveOrderDetail(OrderDetail orderDetail)
        {
            if (orderDetail.Quantity <= 0 || orderDetail.UnitPrice <= 0)
            {
                return false;
            }

            return _orderRepositories.SaveOrderDetail(orderDetail);
        }

        public bool DeleteOrderDetail(int orderId, int productId)
        {
            return _orderRepositories.DeleteOrderDetail(orderId, productId);
        }

        public List<Order> SearchOrders(string keyword)
        {
            return _orderRepositories.SearchOrders(keyword);
        }

        public List<Order> GetOrdersByDateRange(DateTime startDate, DateTime endDate)
        {
            return _orderRepositories.GetAllOrders()
                .Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate)
                .OrderByDescending(o => o.OrderDate)
                .ToList();
        }

        public decimal CalculateOrderTotal(int orderId)
        {
            var orderDetails = GetOrderDetailsByOrderId(orderId);
            decimal total = 0;

            foreach (var detail in orderDetails)
            {
                decimal subtotal = detail.UnitPrice * detail.Quantity;
                decimal discount = subtotal * (decimal)detail.Discount;
                total += subtotal - discount;
            }

            var order = GetOrderById(orderId);
            if (order?.Freight != null)
            {
                total += order.Freight.Value;
            }

            return total;
        }
    }
} 