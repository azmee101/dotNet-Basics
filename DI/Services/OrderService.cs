using System;

namespace DI
{
    public class OrderService
    {
        private readonly IMessageService _messageService;

        // Constructor Injection: DI container এই constructor call করে
        // এবং IMessageService এর registered implementation inject করে
        public OrderService(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public void PlaceOrder(string item)
        {
            Console.WriteLine($"Order placed: {item}");
            _messageService.Send($"Your order for '{item}' is confirmed!");
        }
    }
}
