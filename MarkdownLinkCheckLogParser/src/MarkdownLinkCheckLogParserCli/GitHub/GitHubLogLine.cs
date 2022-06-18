namespace MarkdownLinkCheckLogParserCli.GitHub;

internal class GitHubLogLine
{
    private readonly string _logLine;
    private readonly Lazy<string> _logLineWithoutTimestamp;

    public GitHubLogLine(string logLine)
    {
        _logLine = logLine;
        _logLineWithoutTimestamp = new Lazy<string>(() => TrimTimestamp(logLine));
    }

    public static implicit operator string(GitHubLogLine logLine)
    {
        return logLine._logLine;
    }

    public string WithoutTimestamp => _logLineWithoutTimestamp.Value;

    private static string TrimTimestamp(string logLine)
    {
        var splits = logLine.Split(" ");
        return string.Join(" ", splits.Skip(1));
    }
}
