namespace DAL.DataServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DAL.Entities;
    using DAL.Infrastructure;
    using DAL.QueryObjects;

    public class OrdersDataService : BaseDataService
    {
        public OrdersDataService(IConnectionFactory connectionFactory)
            : base(connectionFactory)
        {
        }

        public IEnumerable<Order> GetAll()
        {
            using (var connection = this.GetConnection())
            {
                var orderQueryObject = new OrderQueryObject();
                return connection.Query<Order>(orderQueryObject.GetAll());
            }
        }

        public Order GetById(int orderId)
        {
            using (var connection = this.GetConnection())
            {
                var orderQueryObject = new OrderQueryObject();
                var order = connection.Query<Order>(orderQueryObject.GetById(orderId)).SingleOrDefault();

                if (order == null)
                {
                    throw new EntityNotFoundException("Order by defined id is not found.", orderId.ToString());
                }

                var orderDetailQueryObject = new OrderDetailQueryObject();
                var orderDetails = connection.Query<OrderDetail, Product, OrderDetail>(
                    orderDetailQueryObject.GetByOrderId(orderId),
                    (o, p) => 
                    {
                        o.Product = p;
                        return o;
                    },
                    "ProductID").ToList();

                order.OrderDetails = orderDetails;

                return order;
            }
        }

        public int DeleteOrder(int orderId)
        {
            using (var connection = this.GetConnection())
            {
                var orderQueryObject = new OrderQueryObject();

                using (var scope = TransactionScopeHelper.GetDefaultTransactionScope())
                {
                    var affectedRows = connection.Query<int>(orderQueryObject.DeleteOrder(orderId)).Single();

                    scope.Complete();

                    return affectedRows;
                }
            }
        }

        public int InsertOrder(Order order)
        {
            using (var connection = this.GetConnection())
            {
                var orderQueryObject = new OrderQueryObject();

                var orderId = connection.Query<int>(orderQueryObject.InsertOrder(order)).First();

                return orderId;
            }
        }

        public int UpdateOrder(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException("order");
            }

            if (order.OrderID <= 0)
            {
                throw new ArgumentException("OrderID is not valid.");
            }

            using (var connection = this.GetConnection())
            {
                var orderQueryObject = new OrderQueryObject();

                var affectedRows = connection.Query<int>(orderQueryObject.UpdateOrder(order)).First();

                return affectedRows;
            }
        }
    }
}