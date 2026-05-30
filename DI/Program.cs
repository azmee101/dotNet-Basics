using DI;

public static class Program
{
    public static void Main()
    {
        var container = new ServiceCollection();

        container.AddTransient<OrderService>();
        container.AddTransient<IMessageService, EmailService>();
        container.AddSingleton<IMyService, MyService>();

        var provider = container.BuildServiceProvider();

        // ── DEMO 1: Constructor Injection ────────────────────────
        Console.WriteLine("DEMO 1: Constructor Injection");

        var order = provider.GetRequiredService<OrderService>();
        order.PlaceOrder("Laptop");

        // ── DEMO 2: Singleton ─────────────────────────────────────
        Console.WriteLine("\nDEMO 2: Singleton — always the same instance");

        var s1 = provider.GetRequiredService<IMyService>();
        var s2 = provider.GetRequiredService<IMyService>();
        var s3 = provider.GetRequiredService<IMyService>();

        Console.WriteLine($"  Request 1: {s1.Id}");
        Console.WriteLine($"  Request 2: {s2.Id}");
        Console.WriteLine($"  Request 3: {s3.Id}");
        Console.WriteLine($"  All same? -> {s1.Id == s2.Id && s2.Id == s3.Id}");

        // ── DEMO 3: Transient ─────────────────────────────────────
        Console.WriteLine("\nDEMO 3: Transient — new instance every time");

        var transientContainer = new ServiceCollection();
        transientContainer.AddTransient<IMyService, MyService>();
        var transientProvider = transientContainer.BuildServiceProvider();

        var t1 = transientProvider.GetRequiredService<IMyService>();
        var t2 = transientProvider.GetRequiredService<IMyService>();
        var t3 = transientProvider.GetRequiredService<IMyService>();

        Console.WriteLine($"  Request 1: {t1.Id}");
        Console.WriteLine($"  Request 2: {t2.Id}");
        Console.WriteLine($"  Request 3: {t3.Id}");
        Console.WriteLine($"  All same? -> {t1.Id == t2.Id && t2.Id == t3.Id}");

        // ── DEMO 4: Scoped ────────────────────────────────────────
        Console.WriteLine("\nDEMO 4: Scoped — same within a scope, different across scopes");

        var scopedContainer = new ServiceCollection();
        scopedContainer.AddScoped<IMyService, MyService>();
        var scopedProvider = scopedContainer.BuildServiceProvider();

        using (var scopeA = scopedProvider.CreateScope())
        {
            var a1 = scopeA.ServiceProvider.GetRequiredService<IMyService>();
            var a2 = scopeA.ServiceProvider.GetRequiredService<IMyService>();
            Console.WriteLine($"  [Scope A] Request 1: {a1.Id}");
            Console.WriteLine($"  [Scope A] Request 2: {a2.Id}");
            Console.WriteLine($"  [Scope A] Same? -> {a1.Id == a2.Id}");
        }

        using (var scopeB = scopedProvider.CreateScope())
        {
            var b1 = scopeB.ServiceProvider.GetRequiredService<IMyService>();
            var b2 = scopeB.ServiceProvider.GetRequiredService<IMyService>();
            Console.WriteLine($"  [Scope B] Request 1: {b1.Id}");
            Console.WriteLine($"  [Scope B] Request 2: {b2.Id}");
            Console.WriteLine($"  [Scope B] Same? -> {b1.Id == b2.Id}");
        }
    }
}