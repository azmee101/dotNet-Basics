using System.Reflection;

public class ServiceCollections
{
    private readonly List<Type> _controllers = [];

    public void AddControllers()
    {
        var controllers = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => t.IsClass && t.Name.EndsWith("Controller"));
        _controllers.AddRange(controllers);
    }

    public List<Type> GetControllersTypes() => _controllers;
}