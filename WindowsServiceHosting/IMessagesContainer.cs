namespace WindowsServiceHosting
{
    public interface IMessagesContainer
    {
        void AddMessage(string message);

        void AddError(string errorMessage);
    }
}