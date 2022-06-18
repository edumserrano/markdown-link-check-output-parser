namespace MarkdownLinkCheckLogParserCli.MarkdownLinkCheck.LogLines;

internal class ErrorLogLine : MarkdownLinkCheckLogLine
{
    public ErrorLogLine(MarkdownLinkError error)
        : base(MarkdownLinkCheckLogLineTypes.Error)
    {
        Error = error.NotNull();
    }

    public MarkdownLinkError Error { get; }
}
