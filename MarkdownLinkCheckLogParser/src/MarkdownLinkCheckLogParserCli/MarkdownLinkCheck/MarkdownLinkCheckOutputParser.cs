using MarkdownLinkCheckLogParserCli.MarkdownLinkCheck.LogLines;

namespace MarkdownLinkCheckLogParserCli.MarkdownLinkCheck;

internal static class MarkdownLinkCheckOutputParser
{
    public static async Task<List<MarkdownFileLog>> ParseAsync(IAsyncEnumerable<GitHubLogLine> logLines)
    {
        logLines.NotNull();

        MarkdownFileLog? current = null;
        var logs = new List<MarkdownFileLog>();

        var markdownLogLines = logLines
            .Select(logLine => logLine.WithoutTimestamp)
            .Where(logLineWithoutTimestamp => !string.IsNullOrWhiteSpace(logLineWithoutTimestamp))
            .Select(logLineWithoutTimestamp => MarkdownLinkCheckLogLineFactory.Create(logLineWithoutTimestamp))
            .Where(x => x.Type is not MarkdownLinkCheckLogLineTypes.NotValid);
        await foreach (var line in markdownLogLines)
        {
            if (line.Type == MarkdownLinkCheckLogLineTypes.StartOfFileSummary)
            {
                var startOfFileSummaryLine = (StartOfFileSummaryLogLine)line;
                if (current is not null)
                {
                    logs.Add(current);
                }

                current = new MarkdownFileLog
                {
                    Filename = startOfFileSummaryLine.Filename,
                };
            }
            else if (line.Type == MarkdownLinkCheckLogLineTypes.LinesChecked && current is not null)
            {
                var linksCheckedLine = (LinksCheckedLogLine)line;
                current.LinksChecked = linksCheckedLine.LinksChecked;
            }
            else if (line.Type == MarkdownLinkCheckLogLineTypes.Error && current is not null)
            {
                var errorLogLine = (ErrorLogLine)line;
                current.MarkdownLinkErrors.Add(errorLogLine.Error);
            }
        }

        // end of log
        if (current is not null)
        {
            logs.Add(current);
        }

        return logs;
    }
}
