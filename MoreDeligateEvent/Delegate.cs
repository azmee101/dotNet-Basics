/*
    Delegate:
    - A delegate is a like a **pointer to a method**
    - Can store **one or more methods**
    - can be invoked directly from anywhere that has access.
*/

public class Delegate
{
    public static void Explanation()
    {
        
        Console.WriteLine("Delegate: ");
        Func<int, int, int> OperationFunc = Calculator.Multiply;
        Action<string> printMessage = message => Console.WriteLine(message);
        Predicate<int> isEven = (int value) => value % 2 == 0;

        // Multicast Delegate
        // - A delegate can hold multiple methods using +=
        // - Invoking it calls all methods in order
        // - If return type exists, the return of the last method is returned

        Operation operation = Calculator.Sum;
        operation += Calculator.Substraction;
        operation.Invoke(20, 10);
        Mathmetical operation2 = Calculator.Multiply;
        int res = operation2.Invoke(10, 20);
        Console.WriteLine($"Result of multiplication is : {res}");
        Console.WriteLine();
    }
}