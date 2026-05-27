using System.Reflection.Metadata;

public class Router
{
    private readonly List<EndPoint> _endpoints = [];

    public void MapGet(string path, Func<RequestContext, string> handler)
    {
        _endpoints.Add(new EndPoint(path, "GET", handler));
    }

    public void MapPost(string path, Func<RequestContext, string> handler)
    {
        _endpoints.Add(new EndPoint(path, "POST", handler));
    }

    public string Resolve(RequestContext context)
    {
        var endPoint = _endpoints.FirstOrDefault(ep=> ep.Path == context.Path && ep.Method == context.Method);
        return (endPoint != null) ? endPoint.Handler(context) : "Nothing found 404";
    }
}