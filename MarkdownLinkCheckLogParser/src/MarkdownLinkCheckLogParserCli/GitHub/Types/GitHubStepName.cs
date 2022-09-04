namespace MarkdownLinkCheckLogParserCli.GitHub.Types;

internal sealed class GitHubStepName
{
    private readonly string _value;

    public GitHubStepName(string stepName)
    {
        _value = stepName.NotNullOrWhiteSpace();
    }

    public static implicit operator string(GitHubStepName stepaName)
    {
        return stepaName._value;
    }

    public override string ToString() => (string)this;
}
