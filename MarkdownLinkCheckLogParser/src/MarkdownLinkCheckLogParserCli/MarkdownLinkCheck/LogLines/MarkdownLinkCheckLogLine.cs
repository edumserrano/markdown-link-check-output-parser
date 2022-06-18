namespace MarkdownLinkCheckLogParserCli.MarkdownLinkCheck.LogLines;

internal abstract class MarkdownLinkCheckLogLine
{
    public static NotValidLogLine NotValidLogLine { get; } = new NotValidLogLine();

    public abstract void Handle(ParserState state);
}
