namespace MarkdownLinkCheckLogParserCli.MarkdownLinkCheck.LogLines;

internal interface IMarkdownLinkCheckLogLine
{
    void Handle(ParserState state);
}
