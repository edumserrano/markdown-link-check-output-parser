namespace MarkdownLinkCheckLogParserCli.MarkdownLinkCheck.LogLines;

internal sealed class LinksCheckedLogLine : MarkdownLinkCheckLogLine
{
    public LinksCheckedLogLine(int linksChecked)
    {
        LinksChecked = linksChecked.Positive();
    }

    public int LinksChecked { get; }

    public override void Handle(ParserState state)
    {
        state.NotNull();
        state.VisitLinksCheckedLogLine(this);
    }
}
