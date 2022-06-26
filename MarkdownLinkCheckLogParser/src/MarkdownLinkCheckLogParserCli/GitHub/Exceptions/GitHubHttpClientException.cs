namespace MarkdownLinkCheckLogParserCli.GitHub.Exceptions;

public class GitHubHttpClientException : Exception
{
    internal GitHubHttpClientException(string message)
        : base(message)
    {
    }
}
