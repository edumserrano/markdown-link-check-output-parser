namespace MarkdownLinkCheckLogParserCli.MarkdownLinkCheck.LogLines;

internal class LinksCheckedLogLine : MarkdownLinkCheckLogLine
{
    public LinksCheckedLogLine(int linksChecked)
        : base(MarkdownLinkCheckLogLineTypes.LinesChecked)
    {
        LinksChecked = linksChecked.Positive();
    }

    public int LinksChecked { get; }
}
