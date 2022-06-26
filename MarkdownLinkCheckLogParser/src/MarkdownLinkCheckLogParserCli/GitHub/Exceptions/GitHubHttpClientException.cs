namespace MarkdownLinkCheckLogParserCli.GitHub.Exceptions;

public class GitHubHttpClientException : Exception
{
    internal GitHubHttpClientException(HttpStatusCode statusCode, HttpMethod method, Uri? requestUri)
        : base($"Failed to download workflow run logs. Got {(int)statusCode} {statusCode} from {method} {requestUri}.")
    {
    }
}
