using System;

namespace DI
{
    public class MyService : IMyService
    {
        public Guid Id { get; } = Guid.NewGuid();
    }
}
