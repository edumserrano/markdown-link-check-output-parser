namespace MarkdownLinkCheckLogParserCli.MarkdownLinkCheck.LogLines;

internal sealed class ErrorLogLine : IMarkdownLinkCheckLogLine
{
    public ErrorLogLine(string link, int statusCode)
    {
        Link = link.NotNull();
        StatusCode = statusCode;
    }

    public string Link { get; }

    public int StatusCode { get; }

    public void Handle(ParserState state)
    {
        state.NotNull();
        state.VisitErrorLogLine(this);
    }
}
