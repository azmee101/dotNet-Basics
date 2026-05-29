/*
    Event
    - An event is a delegate with restricted access
    - Only the class that declares it can invoke it (main difference with delegate)
    - Others can only subscribe (+=) or unsubscribe
*/

public class Event
{
    public static void Explanation()
    {
        Console.WriteLine("Event: ");
        PaymentService paymentService = new();

        paymentService.NotifyWithDelegate += () => Console.WriteLine("Email send from multicast delegate");
        paymentService.NotifyWithDelegate += () => Console.WriteLine("SMS send from multicast delegate");
        paymentService.NotifyWithDelegate?.Invoke();
        paymentService.ProcessWithDelegate();

        paymentService.NotifyWithEvent += () => Console.WriteLine("Email send from event");
        paymentService.NotifyWithEvent += () => Console.WriteLine("SMS send from event");
        paymentService.ProcessWithEvent();

    }
}