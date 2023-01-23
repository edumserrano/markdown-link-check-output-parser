namespace MarkdownLinkCheckLogParserCli.GitHub;

internal sealed class GitHubHttpClient
{
    private readonly HttpClient _httpClient;

    public GitHubHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient.NotNull();
    }

    public static HttpClient CreateHttpClient(GitHubAuthToken authToken)
    {
        authToken.NotNull();
        var httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://api.github.com"),
        };
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("token", authToken);
        httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "application/vnd.github.v3+json");
        httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "edumserrano/markdown-link-check-log-parser");
        return httpClient;
    }

    // see https://docs.github.com/en/rest/actions/workflow-runs#download-workflow-run-logs
    public async Task<ZipArchive> DownloadWorkflowRunLogsAsync(GitHubRepository repo, GitHubRunId runId)
    {
        repo.NotNull();
        runId.NotNull();

        using var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"repos/{repo}/actions/runs/{runId}/logs");
        var httpResponse = await _httpClient.SendAsync(httpRequest, HttpCompletionOption.ResponseHeadersRead);
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new GitHubHttpClientException(httpResponse.StatusCode, httpRequest.Method, httpRequest.RequestUri);
        }

        var responseStream = await httpResponse.Content.ReadAsStreamAsync();
        return new ZipArchive(responseStream, ZipArchiveMode.Read);
    }
}
