namespace MarkdownLinkCheckLogParserCli.GitHub;

internal readonly struct GitHubLogLine
{
    public GitHubLogLine(ReadOnlyMemory<char> line)
    {
        Value = RemoveTimestamp(line);
    }

    public ReadOnlyMemory<char> Value { get; }

    public bool IsEmpty => Value.IsEmpty;

    public static implicit operator ReadOnlyMemory<char>(GitHubLogLine line) => line.Value;

    private static ReadOnlyMemory<char> RemoveTimestamp(ReadOnlyMemory<char> line)
    {
        var indexOfSpace = line.Span.IndexOf(' ');
        return line
            .Slice(indexOfSpace + 1)
            .TrimStart();
    }
}
