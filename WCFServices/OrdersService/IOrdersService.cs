namespace WCFServices.OrdersService
{
    using System.Collections.Generic;
    using System.ServiceModel;
    using WCFServices.DataContracts;

    [ServiceContract(Namespace = "http://epam.com/NorthwindService")]
    public interface IOrdersService
    {
        [OperationContract]
        IEnumerable<OrderDTO> GetAll();

        [OperationContract]
        OrderDTO GetById(int orderId);

        [OperationContract]
        int DeleteOrder(int orderId);

        [OperationContract]
        int CreateNewOrder(OrderDTO order);

        [OperationContract]
        void UpdateOrder(OrderDTO order);

        [OperationContract]
        void ProcessOrder(int orderId);

        [OperationContract]
        void CloseOrder(int orderId);
    }
}