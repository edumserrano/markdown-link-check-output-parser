namespace MarkdownLinkCheckLogParserCli.Tests.Auxiliary;

internal class StatusCodeHandler : DelegatingHandler
{
    private readonly HttpStatusCode _statusCode;

    public StatusCodeHandler(HttpStatusCode statusCode)
    {
        _statusCode = statusCode;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var httpResponseMessage = new HttpResponseMessage(_statusCode);
        return Task.FromResult(httpResponseMessage);
    }
}
