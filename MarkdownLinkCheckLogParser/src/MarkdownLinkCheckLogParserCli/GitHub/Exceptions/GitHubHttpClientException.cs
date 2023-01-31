namespace MarkdownLinkCheckLogParserCli.GitHub.Exceptions;

public class GitHubHttpClientException : Exception
{
    internal GitHubHttpClientException(HttpMethod method, Uri? requestUri, HttpStatusCode statusCode)
        : base($"Failed to download workflow run logs. Got {(int)statusCode} {statusCode} from {method} {requestUri}.")
    {
    }
}
