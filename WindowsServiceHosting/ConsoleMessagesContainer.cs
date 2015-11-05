namespace WindowsServiceHosting
{
    using System;

    public class ConsoleMessagesContainer : IMessagesContainer
    {
        public void AddMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}