namespace MarkdownLinkCheckLogParserCli.MarkdownLinkCheck;

internal static class MarkdownLinkCheckOutputParser
{
    public static MarkdownLinkCheckOutput Parse(GitHubStepLog log, bool captureErrorsOnly)
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
        return new MarkdownLinkCheckOutput(parserState.Files, captureErrorsOnly);
    }
}
