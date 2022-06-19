namespace MarkdownLinkCheckLogParserCli.MarkdownLinkCheck.LogLines;

internal sealed class StartOfFileSummaryLogLine : IMarkdownLinkCheckLogLine
{
    public StartOfFileSummaryLogLine(string filename)
    {
        Filename = filename.NotNull();
    }

    public string Filename { get; }

    public void Handle(ParserState state)
    {
        state.NotNull();
        state.VisitStartOfFileSummaryLogLine(this);
    }
}
