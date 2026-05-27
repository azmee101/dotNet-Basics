public class EndPoint
{
    public string Path {get; set;} = string.Empty;
    public string Method {get; set;} = string.Empty;
    public readonly Func<RequestContext, string> Handler;

    public EndPoint(string path, string method, Func<RequestContext, string> handler)
    {
        Path = path;
        Method = method;
        Handler = handler;
    }
}