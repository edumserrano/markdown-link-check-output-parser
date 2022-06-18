namespace MarkdownLinkCheckLogParserCli.MarkdownLinkCheck;

internal class ParserState
{
    private MarkdownFileLog? _current;
    private readonly List<MarkdownFileLog> _logs = new List<MarkdownFileLog>();

    public IReadOnlyList<MarkdownFileLog> Logs => _logs;

    public void VisitStartOfFileSummaryLogLine(StartOfFileSummaryLogLine logLine)
    {
        logLine.NotNull();
        if (_current is not null)
        {
            _logs.Add(_current);
        }

        _current = new MarkdownFileLog(logLine.Filename);
    }

    public void VisitLinksCheckedLogLine(LinksCheckedLogLine logLine)
    {
        logLine.NotNull();
        if (_current is null)
        {
            return;
        }

        _current.LinksChecked = logLine.LinksChecked;
    }

    public void VisitErrorLogLine(ErrorLogLine logLine)
    {
        logLine.NotNull();
        if (_current is null)
        {
            return;
        }

        _current.Errors.Add(logLine.Error);
    }

    public void EndOfLog()
    {
        // make sure we don't loose the current item (last item we parsed)
        if (_current is not null)
        {
            _logs.Add(_current);
            _current = null;
        }
    }
}
