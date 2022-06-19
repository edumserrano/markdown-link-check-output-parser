namespace MarkdownLinkCheckLogParserCli.GitHub;

internal class GitHubRunId
{
    private readonly string _value;

    public GitHubRunId(string value)
    {
        _value = value.NotNullOrWhiteSpace();
    }

    public static implicit operator string(GitHubRunId gitHubAuthToken)
    {
        return gitHubAuthToken._value;
    }

    public override string ToString() => (string)this;
}
