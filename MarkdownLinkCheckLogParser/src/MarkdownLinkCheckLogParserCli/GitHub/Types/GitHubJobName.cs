namespace MarkdownLinkCheckLogParserCli.GitHub.Types;

internal sealed class GitHubJobName
{
    private readonly string _value;

    public GitHubJobName(string jobName)
    {
        _value = jobName.NotNullOrWhiteSpace();
    }

    public static implicit operator string(GitHubJobName jobName)
    {
        return jobName._value;
    }

    public override string ToString() => (string)this;
}
