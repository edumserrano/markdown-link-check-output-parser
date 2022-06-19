namespace MarkdownLinkCheckLogParserCli.GitHub;

internal class GitHubRepository
{
    private readonly string _value;

    public GitHubRepository(string value)
    {
        _value = value.NotNullOrWhiteSpace();
    }

    public static implicit operator string(GitHubRepository gitHubAuthToken)
    {
        return gitHubAuthToken._value;
    }

    public override string ToString() => (string)this;
}
