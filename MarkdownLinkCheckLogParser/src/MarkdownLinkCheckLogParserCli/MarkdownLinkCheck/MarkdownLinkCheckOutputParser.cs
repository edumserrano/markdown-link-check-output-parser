namespace MarkdownLinkCheckLogParserCli.MarkdownLinkCheck;

internal static class MarkdownLinkCheckOutputParser
{
    public static async Task<IReadOnlyList<MarkdownFileLog>> ParseAsync(IAsyncEnumerable<GitHubLogLine> logLines)
    {
        logLines.NotNull();
        var parserState = new ParserState();
        var markdownLogLines = logLines
            .Select(logLine => logLine.WithoutTimestamp)
            .Where(logLineWithoutTimestamp => !string.IsNullOrWhiteSpace(logLineWithoutTimestamp))
            .Select(logLineWithoutTimestamp => MarkdownLinkCheckLogLineFactory.Create(logLineWithoutTimestamp))
            .Where(markdownLinkCheckLogLine => markdownLinkCheckLogLine is not NotValidLogLine);
        await foreach (var line in markdownLogLines)
        {
            line.Handle(parserState);
        }

        parserState.EndOfLog();
        return parserState.Logs;
    }
}
