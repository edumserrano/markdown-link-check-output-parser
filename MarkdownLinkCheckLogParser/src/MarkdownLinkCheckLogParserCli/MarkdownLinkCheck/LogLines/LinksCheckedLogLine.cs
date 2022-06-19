namespace MarkdownLinkCheckLogParserCli.MarkdownLinkCheck.LogLines;

internal sealed class LinksCheckedLogLine : IMarkdownLinkCheckLogLine
{
    public LinksCheckedLogLine(int linksChecked)
    {
        LinksChecked = linksChecked.Positive();
    }

    public int LinksChecked { get; }

    public void Handle(ParserState state)
    {
        state.NotNull();
        state.VisitLinksCheckedLogLine(this);
    }
}
