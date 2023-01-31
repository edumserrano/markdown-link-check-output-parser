namespace MarkdownLinkCheckLogParserCli.Tests.Auxiliary;

internal sealed class StatusCodeHandler : DelegatingHandler
{
    private readonly HttpStatusCode _statusCode;
    private readonly string _responseBody;

    public StatusCodeHandler(HttpStatusCode statusCode, string responseBody)
    {
        _statusCode = statusCode;
        _responseBody = responseBody;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var httpResponseMessage = new HttpResponseMessage(_statusCode)
        {
            Content = new StringContent(_responseBody),
        };
        return Task.FromResult(httpResponseMessage);
    }
}
