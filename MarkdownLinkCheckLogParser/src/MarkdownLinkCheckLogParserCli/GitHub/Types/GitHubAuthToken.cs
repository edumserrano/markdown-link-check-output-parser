namespace MarkdownLinkCheckLogParserCli.GitHub.Types;

internal sealed class GitHubAuthToken
{
    private readonly string _value;

    public GitHubAuthToken(string value)
    {
        _value = value.NotNullOrWhiteSpace();
    }

    public static implicit operator string(GitHubAuthToken gitHubAuthToken)
    {
        return gitHubAuthToken._value;
    }

    public override string ToString() => (string)this;
}
