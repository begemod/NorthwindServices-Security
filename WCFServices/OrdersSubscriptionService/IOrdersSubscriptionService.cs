namespace WCFServices.OrdersSubscriptionService
{
    using System.ServiceModel;

    public interface IBroadcastCallback
    {
        [OperationContract(IsOneWay = true)]
        void OrderStatusIsChanged(int orderId);
    }

    [ServiceContract(Namespace = "http://epam.com/NorthwindService", CallbackContract = typeof(IBroadcastCallback))]
    public interface IOrdersSubscriptionService
    {
        [OperationContract]
        bool Subscribe(string clientIdentifier);

        [OperationContract]
        bool Unsubscribe(string clientIdentifier);
    }
}