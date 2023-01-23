namespace MarkdownLinkCheckLogParserCli.MarkdownLinkCheck;

// This is part of the Visitor pattern implement to handle the parsing of each log line of the mlc output
// This class would be equivalent to the IVisitor in this example https://refactoring.guru/design-patterns/visitor/csharp/example
internal sealed class ParserState
{
    private MarkdownFileCheck? _current;
    private readonly List<MarkdownFileCheck> _files = new List<MarkdownFileCheck>();

    public IReadOnlyList<MarkdownFileCheck> Files => _files;

    public void VisitStartOfFileSummaryLogLine(StartOfFileSummaryLogLine logLine)
    {
        logLine.NotNull();

        // if there is no current then set the current.
        // if there's already a current being tracked then it means we need to save
        // it to the parsed logs lines before we start tracking a new current
        if (_current is not null)
        {
            _files.Add(_current);
        }

        _current = new MarkdownFileCheck(logLine.Filename);
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

        _current.AddError(logLine.Link, logLine.StatusCode);
    }

    public void EndOfLog()
    {
        // make sure we don't loose the current item (last log line we parsed)
        if (_current is not null)
        {
            _files.Add(_current);
            _current = null;
        }
    }
}
