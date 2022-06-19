namespace MarkdownLinkCheckLogParserCli.MarkdownLinkCheck;

internal static class MarkdownLinkCheckOutputParser
{
    public static IReadOnlyList<MarkdownFileLog> Parse(GitHubStepLog log)
    {
        log.NotNull();
        var parserState = new ParserState();
        var markdownLinkCheckLogLines = log.GetLogLines()
            .Where(ghLogLine => !ghLogLine.IsEmpty)
            .Select(ghLogLine => MarkdownLinkCheckLogLineFactory.Create(ghLogLine))
            .Where(mlcLogLine => mlcLogLine is not UnknownLogLine);
        foreach (var line in markdownLinkCheckLogLines)
        {
            line.Handle(parserState);
        }

        parserState.EndOfLog();
        return parserState.Logs;
    }
}
