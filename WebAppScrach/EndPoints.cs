public class EndPoints
{
    public string Path {get; set;}
    public string Method {get; set;}
    public readonly Func<RequestContext, string> Handler;

    public EndPoints(string path, string method, Func<RequestContext, string> handler)
    {
        Path = path;
        Method = method;
        Handler = handler;
    }
}