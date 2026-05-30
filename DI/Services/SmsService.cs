using System;

namespace DI
{
    public class SmsService : IMessageService
    {
        public void Send(string message) => Console.WriteLine($"SMS: {message}");
    }
}
