namespace MarkdownLinkCheckLogParserCli.GitHub;

internal class GitHubHttpClient
{
    private readonly HttpClient _httpClient;

    public GitHubHttpClient(HttpClient httpClient, string authToken)
    {
        authToken.NotNull();
        _httpClient = httpClient.NotNull();
        _httpClient.BaseAddress = new Uri("https://api.github.com");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("token", authToken);
        _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "application/vnd.github.v3+json");
        _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "markdown-link-check-log-parser");
    }

    // see https://docs.github.com/en/rest/actions/workflow-runs#download-workflow-run-logs
    public async Task<ZipArchive> DownloadWorkflowRunLogsAsync(string repo, string runId)
    {
        using var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"repos/{repo}/actions/runs/{runId}/logs");
        var httpResponse = await _httpClient.SendAsync(httpRequest, HttpCompletionOption.ResponseHeadersRead);
        var responseStream = await httpResponse.Content.ReadAsStreamAsync();
        return new ZipArchive(responseStream, ZipArchiveMode.Read);
    }
}
