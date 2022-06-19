namespace MarkdownLinkCheckLogParserCli.MarkdownLinkCheck.LogLines;

internal sealed class UnknownLogLine : IMarkdownLinkCheckLogLine
{
    private UnknownLogLine()
    {
    }

    public void Handle(ParserState state)
    {
    }

    // used as a singleton, no need to have multiple instances of this
    public static UnknownLogLine Instance { get; } = new UnknownLogLine();
}
