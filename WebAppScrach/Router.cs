using System.Runtime.CompilerServices;

public class Router
{
    private readonly List<EndPoints> _endpoints = [];
    public void MapGet(string path, Func<RequestContext, string> handler)
    {
        _endpoints.Add(new EndPoints(path, "GET", handler));
    }

    public void MapGet(string path, Action<RequestContext> handler)
    {
        // overloading Mapget to show , Action does not
        // require any return type as Func
    }

    public void MapPost(string path, Func<RequestContext, string> handler)
    {
        _endpoints.Add(new EndPoints(path, "POST", handler));
    }

    public string Resolve(RequestContext context)
    {
        var endpoint = _endpoints.FirstOrDefault(ep => ep.Path == context.Path && ep.Method == context.Method);
        return endpoint != null ? endpoint.Handler(context) : "Not found 404!";
    }
}