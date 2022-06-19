namespace MarkdownLinkCheckLogParserCli.GitHub;

internal class GitHubLogLine
{
    private readonly string _logLine;
    private readonly Lazy<ReadOnlyMemory<char>> _logLineWithoutTimestamp;

    public GitHubLogLine(string logLine)
    {
        _logLine = logLine;
        _logLineWithoutTimestamp = new Lazy<ReadOnlyMemory<char>>(() => TrimTimestamp(logLine));
    }

    public static implicit operator string(GitHubLogLine logLine)
    {
        return logLine._logLine;
    }

    public ReadOnlyMemory<char> WithoutTimestamp => _logLineWithoutTimestamp.Value;

    private static ReadOnlyMemory<char> TrimTimestamp(string logLine)
    {
        var indexOfFirstSpace = logLine.IndexOf(' ', StringComparison.InvariantCulture);
        var spanWithoutTimestamp = logLine.AsMemory(start: indexOfFirstSpace);
        

        //var splits = logLine.Split(" ");
        //return string.Join(" ", splits.Skip(1));

        return spanWithoutTimestamp;
    }
}
