using System.Reflection;
using Microsoft.AspNetCore.StaticFiles;

public class WebApplicationBuilder
{
    public ServiceCollections Services { get; } = new ServiceCollections();

    public WebApplication Build()
    {
        return new WebApplication(Services);
    }
}

public class WebApplication
{
    private readonly ServiceCollections _services;
    private readonly Router _router = new();

    public WebApplication(ServiceCollections services)
    {
        _services = services;
    }

    public WebApplication MapGet(string path, Func<RequestContext, string> handler)
    {
        _router.MapGet(path, handler);
        return this;
    }

    public WebApplication MapPost(string path, Func<RequestContext, string> handler)
    {
        _router.MapPost(path, handler);
        return this;
    }

    public WebApplication MapControllers()
    {
        var controllerTypes = _services.GetControllersTypes(); // List<Type>
        // controllerTypes is refering list of controllers

        foreach(var controller in controllerTypes)
        {
            // controller refering a single controller (here is UserController)
            var methods = controller.GetMethods(BindingFlags.Instance | BindingFlags.Public);

            foreach(var method in methods)
            {
                var attributes = method.GetCustomAttributes<HttpMethodAttribute>();

                foreach(var attribute in attributes)
                {
                    if(attribute.Method == "GET")
                    {
                        _router.MapGet(attribute.Path, (requestContext) =>
                        {
                            var instance = Activator.CreateInstance(controller); // creating an instance of "UserController" during Run-Time
                            var result = method.Invoke(instance, null);
                            return result?.ToString() ?? "";
                        });
                    }
                    else if(attribute.Method == "POST")
                    {
                        _router.MapPost(attribute.Path, (requestContext) =>
                        {
                            var instance = Activator.CreateInstance(controller); // creating an instance of "UserController" during Run-Time
                            var result = method.Invoke(instance, null);
                            return result?.ToString() ?? "";
                        });                        
                    }
                }
            }
        }
        return this;
    }

    public async Task RunAsync(int port)
    {
        var server = new TcpServer(port, _router);
        await server.StartAsync();
    }
    public static WebApplicationBuilder CreateBuilder()
    {
        return new WebApplicationBuilder();
    }
}