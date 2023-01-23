namespace MarkdownLinkCheckLogParserCli.Tests.Auxiliary;

internal sealed class InMemoryGitHubWorkflowRunHandler : DelegatingHandler
{
    private readonly string _logZipFilepath;

    public InMemoryGitHubWorkflowRunHandler(string logZipFilepath)
    {
        _logZipFilepath = logZipFilepath;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var stream = File.OpenRead(_logZipFilepath);
        var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StreamContent(stream),
        };
        return Task.FromResult(httpResponseMessage);
    }
}
