namespace MarkdownLinkCheckLogParserCli.GitHub;

internal class GitHubHttpClientException : Exception
{
    public GitHubHttpClientException(string message)
        : base(message)
    {
    }
}
