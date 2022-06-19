namespace MarkdownLinkCheckLogParserCli.GitHub;

internal class GitHubStepName
{
    private readonly string _value;

    public GitHubStepName(string value)
    {
        _value = value.NotNullOrWhiteSpace();
    }

    public static implicit operator string(GitHubStepName gitHubAuthToken)
    {
        return gitHubAuthToken._value;
    }

    public override string ToString() => (string)this;
}
