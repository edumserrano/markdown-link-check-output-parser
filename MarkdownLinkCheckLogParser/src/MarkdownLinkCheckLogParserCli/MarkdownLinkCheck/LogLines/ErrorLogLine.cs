namespace MarkdownLinkCheckLogParserCli.MarkdownLinkCheck.LogLines;

internal sealed class ErrorLogLine : MarkdownLinkCheckLogLine
{
    public ErrorLogLine(MarkdownLinkError error)
    {
        Error = error.NotNull();
    }

    public MarkdownLinkError Error { get; }

    public override void Handle(ParserState state)
    {
        state.NotNull();
        state.VisitErrorLogLine(this);
    }
}
