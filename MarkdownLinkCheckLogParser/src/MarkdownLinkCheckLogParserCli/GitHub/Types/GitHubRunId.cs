namespace MarkdownLinkCheckLogParserCli.GitHub.Types;

internal sealed class GitHubRunId
{
    private readonly string _value;

    public GitHubRunId(string gitHubRunId)
    {
        _value = gitHubRunId.NotNullOrWhiteSpace();
    }

    public static implicit operator string(GitHubRunId gitHubAuthToken)
    {
        return gitHubAuthToken._value;
    }

    public override string ToString() => (string)this;
}
