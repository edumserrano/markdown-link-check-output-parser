namespace MarkdownLinkCheckLogParserCli.GitHub;

internal sealed class GitHubStepLog
{
    private readonly Memory<char> _log;

    public GitHubStepLog(Memory<char> log)
    {
        _log = log;
    }

    public IEnumerable<GitHubLogLine> GetLogLines()
    {
        foreach (ReadOnlyMemory<char> line in _log.SplitLines())
        {
            yield return new GitHubLogLine(line);
        }
    }
}
