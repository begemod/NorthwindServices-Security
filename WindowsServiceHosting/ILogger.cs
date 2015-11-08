namespace WindowsServiceHosting
{
    public interface ILogger
    {
        void AddMessage(string message);

        void AddError(string errorMessage);
    }
}