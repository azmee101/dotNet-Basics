public delegate void Operation(int num1, int num2);
public delegate int MathOperation(int num1, int num2);

public class Calculator
{
    public static void Sum(int a, int b)
    {
        Console.WriteLine($"Summation is : {a + b}");
    }

    public static void Substraction(int a, int b)
    {
        Console.WriteLine($"Substraction is {a - b}");
    }

    public static int Multiply(int a, int b)
    {
        return a * b;
    }
}

public delegate void Notify();

public class PaymentService
{
    public Notify? NotifyWithDelegate;
    public event Notify? NotifyWithEvent;

    public void ProcessWithDelegate()
    {
        Console.WriteLine("ProcessWithDelegate:");
        Console.WriteLine("Process completed");

        Console.WriteLine();
    }

    public void ProcessWithEvent()
    {
        Console.WriteLine("ProcessWithEvent:");
        Console.WriteLine("Process Completed");

        NotifyWithEvent?.Invoke();
        Console.WriteLine();
    }
}

public class Program
{
    public static void SendEmail() => Console.WriteLine("Email Sent");
    public static void SendSMS() => Console.WriteLine("SMS Sent");

    public static void Main(string[] args)
    {
        Operation op1 = Calculator.Sum;

        // op1 += Calculator.Substraction;
        // In delegate , multiple method can be stored.

        Operation op2 = Calculator.Substraction;
        MathOperation op3 = Calculator.Multiply;

        op1(10, 20);
        op2(30, 20);

        int res1 = op3(10, 20);

        Console.WriteLine($"Multiplication is: {res1}");
        Console.WriteLine();

        PaymentService paymentService = new();

        // delegate
        paymentService.NotifyWithDelegate += SendEmail;
        paymentService.NotifyWithDelegate += paymentService.ProcessWithDelegate;

        paymentService.NotifyWithDelegate?.Invoke();

        // event
        paymentService.NotifyWithEvent += SendSMS;
        paymentService.ProcessWithEvent();      


        // ✅ delegate — বাইরে থেকে overwrite করা যায়
        // paymentService.NotifyWithDelegate = SendSMS; // আগের SendEmail, ProcessWithDelegate মুছে গেলো!

        // ❌ event — বাইরে থেকে overwrite করা যায় না
        // paymentService.NotifyWithEvent = SendSMS; // compile error!

        // ✅ delegate — বাইরে থেকে invoke করা যায়
        // paymentService.NotifyWithDelegate?.Invoke(); [line 76]

        // ❌ event — বাইরে থেকে invoke করা যায় না
        // paymentService.NotifyWithEvent?.Invoke(); // compile error!
    }
    
}

/*
    += এর কাজ: মেথডকে লাইনে দাঁড় করানো (Subscription)।

    .Invoke() এর কাজ: লাইন শুরু করার সংকেত দেওয়া (Execution)।

    ইনভোক না করলে মেথডগুলো আজীবন লাইনেই দাঁড়িয়ে থাকবে, কখনোই রান হবে না!
*/