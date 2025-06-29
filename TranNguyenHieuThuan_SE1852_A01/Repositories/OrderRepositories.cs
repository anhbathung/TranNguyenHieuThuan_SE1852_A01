using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using DataAccessLayer;

namespace Repositories
{
    public class OrderRepositories : IOrderRepositories
    {
        OrderDAO orderDAO = new OrderDAO();

        public List<Order> GetAllOrders()
        {
            return orderDAO.GetAllOrders();
        }

        public void InitializeDataset()
        {
            orderDAO.InitializeDataset();
        }

        public Order? GetOrderById(int id)
        {
            return orderDAO.GetOrderById(id);
        }

        public List<Order> GetOrdersByCustomerId(int customerId)
        {
            return orderDAO.GetOrdersByCustomerId(customerId);
        }

        public List<Order> GetOrdersByEmployeeId(int employeeId)
        {
            return orderDAO.GetOrdersByEmployeeId(employeeId);
        }

        public bool SaveOrder(Order order)
        {
            return orderDAO.SaveOrder(order);
        }

        public bool UpdateOrder(Order order)
        {
            return orderDAO.UpdateOrder(order);
        }

        public bool DeleteOrder(int id)
        {
            return orderDAO.DeleteOrder(id);
        }

        public List<OrderDetail> GetOrderDetailsByOrderId(int orderId)
        {
            return orderDAO.GetOrderDetailsByOrderId(orderId);
        }

        public bool SaveOrderDetail(OrderDetail orderDetail)
        {
            return orderDAO.SaveOrderDetail(orderDetail);
        }

        public bool DeleteOrderDetail(int orderId, int productId)
        {
            return orderDAO.DeleteOrderDetail(orderId, productId);
        }

        public List<Order> SearchOrders(string keyword)
        {
            return orderDAO.SearchOrders(keyword);
        }
    }
} 