namespace MarkdownLinkCheckLogParserCli.MarkdownLinkCheck;

internal static class MarkdownLinkCheckOutputParser
{
    public static IReadOnlyList<MarkdownFileLog> Parse(ReadOnlyMemory<char> log)
    {
        MarkdownFileLog? current = null;
        var logs = new List<MarkdownFileLog>();

        foreach (ReadOnlySpan<char> line in log.SplitLines())
        // foreach (var line in log.Span.EnumerateLines())
        {
            var indexOfSpace = line.IndexOf(' ');
            var withoutTimestamp = line
                .Slice(indexOfSpace + 1)
                .TrimStart();
            if (withoutTimestamp.IsEmpty)
            {
                continue;
            }

            // 2022-06-12T01:19:01.3757480Z FILE: ./tests/liquid-test-logger-template.md
            var startOfFileSummaryIdx = withoutTimestamp.IndexOf("FILE: ");
            if (startOfFileSummaryIdx >= 0)
            {
                if (current is not null)
                {
                    logs.Add(current);
                }

                var filename = withoutTimestamp
                    .Slice(startOfFileSummaryIdx + 6)
                    .ToString();
                current = new MarkdownFileLog(filename);
                continue;
            }

            // 2022-06-12T01:19:01.3758016Z 0 links checked.
            var linksCheckedIdx = withoutTimestamp.IndexOf(" links checked.");
            if (linksCheckedIdx >= 0 && current is not null)
            {
                var endOfLinesCheckedIdx = withoutTimestamp.Length - 15;
                var linesCheckedSpan = withoutTimestamp.Slice(0, endOfLinesCheckedIdx); //trim to be safe before parse?
                if (int.TryParse(linesCheckedSpan, out var linesChecked))
                {
                    current.LinksChecked = linesChecked;
                }

                continue;
            }

            // 2022-06-12T01:19:01.3778708Z [✖] https://github.com/edumserrano/dotnet-sdk-extensions/actions/workflows/pr-dotnet-format-check.yml/badge.svg → Status: 404            
            var errorIdx = withoutTimestamp.IndexOf("[✖] ");
            var statusCodeIdx = withoutTimestamp.IndexOf(" → Status: ");
            if (errorIdx >= 0 && statusCodeIdx >= 0 && current is not null)
            {
                var range = new Range(errorIdx + 4, statusCodeIdx);
                var linkSpan = withoutTimestamp[range];
                var statusSpan = withoutTimestamp.Slice(statusCodeIdx + 11); //trim to be safe before parse?
                if (int.TryParse(statusSpan, out var statusCode))
                {
                    var error = new MarkdownLinkError(linkSpan.ToString(), statusCode);
                    current.Errors.Add(error);
                }

                continue;
            }
        }

        if (current is not null)
        {
            logs.Add(current);
        }

        return logs;
        //var parserState = new ParserState();
        //var markdownLogLines = logLines
        //    .Select(logLine => logLine.WithoutTimestamp)
        //    //.Where(logLineWithoutTimestamp => !string.IsNullOrWhiteSpace(logLineWithoutTimestamp))
        //    .Select(logLineWithoutTimestamp => MarkdownLinkCheckLogLineFactory.Create(logLineWithoutTimestamp))
        //    .Where(markdownLinkCheckLogLine => markdownLinkCheckLogLine is not NotValidLogLine);
        //await foreach (var line in markdownLogLines)
        //{
        //    line.Handle(parserState);
        //}

        //parserState.EndOfLog();
        //return parserState.Logs;
    }
}
