namespace WindowsServiceHosting
{
    using System;

    public class ConsoleLogger : ILogger
    {
        public void AddMessage(string message)
        {
            Console.WriteLine(message);
        }

        public void AddError(string errorMessage)
        {
            var definedColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(errorMessage);
            Console.ForegroundColor = definedColor;
        }
    }
}