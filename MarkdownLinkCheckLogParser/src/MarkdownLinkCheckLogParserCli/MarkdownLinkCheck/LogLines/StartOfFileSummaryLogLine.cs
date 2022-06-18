namespace MarkdownLinkCheckLogParserCli.MarkdownLinkCheck.LogLines;

internal class StartOfFileSummaryLogLine : MarkdownLinkCheckLogLine
{
    public StartOfFileSummaryLogLine(string filename)
        : base(MarkdownLinkCheckLogLineTypes.StartOfFileSummary)
    {
        Filename = filename.NotNull();
    }

    public string Filename { get; }
}
