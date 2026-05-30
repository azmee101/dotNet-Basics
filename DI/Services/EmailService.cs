using System;

namespace DI
{
    public class EmailService : IMessageService
    {
        public void Send(string message) => Console.WriteLine($"Email: {message}");
    }
}
