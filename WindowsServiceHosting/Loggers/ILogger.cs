namespace WindowsServiceHosting.Loggers
{
    public interface ILogger
    {
        void AddMessage(string message);

        void AddError(string errorMessage);
    }
}