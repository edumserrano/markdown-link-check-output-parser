namespace MarkdownLinkCheckLogParserCli.GitHub.Types;

internal sealed class GitHubJobName
{
    private readonly string _value;

    public GitHubJobName(string gitHubJobName)
    {
        _value = gitHubJobName.NotNullOrWhiteSpace();
    }

    public static implicit operator string(GitHubJobName gitHubAuthToken)
    {
        return gitHubAuthToken._value;
    }

    public override string ToString() => (string)this;
}
