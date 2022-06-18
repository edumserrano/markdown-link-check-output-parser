namespace MarkdownLinkCheckLogParserCli.MarkdownLinkCheck.LogLines;

internal sealed class StartOfFileSummaryLogLine : MarkdownLinkCheckLogLine
{
    public StartOfFileSummaryLogLine(string filename)
    {
        Filename = filename.NotNull();
    }

    public string Filename { get; }

    public override void Handle(ParserState state)
    {
        state.NotNull();
        state.VisitStartOfFileSummaryLogLine(this);
    }
}
