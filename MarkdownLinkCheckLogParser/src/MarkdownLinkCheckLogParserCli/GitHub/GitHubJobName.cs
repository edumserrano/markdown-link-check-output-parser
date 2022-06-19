namespace MarkdownLinkCheckLogParserCli.GitHub;

internal class GitHubJobName
{
    private readonly string _value;

    public GitHubJobName(string value)
    {
        _value = value.NotNullOrWhiteSpace();
    }

    public static implicit operator string(GitHubJobName gitHubAuthToken)
    {
        return gitHubAuthToken._value;
    }

    public override string ToString() => (string)this;
}
