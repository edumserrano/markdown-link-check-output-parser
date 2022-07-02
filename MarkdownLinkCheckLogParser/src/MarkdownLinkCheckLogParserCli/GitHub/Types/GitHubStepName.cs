namespace MarkdownLinkCheckLogParserCli.GitHub.Types;

internal sealed class GitHubStepName
{
    private readonly string _value;

    public GitHubStepName(string gitHubStepName)
    {
        _value = gitHubStepName.NotNullOrWhiteSpace();
    }

    public static implicit operator string(GitHubStepName gitHubAuthToken)
    {
        return gitHubAuthToken._value;
    }

    public override string ToString() => (string)this;
}
