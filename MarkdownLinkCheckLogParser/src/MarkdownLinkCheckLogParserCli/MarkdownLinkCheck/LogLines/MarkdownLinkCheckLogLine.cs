namespace MarkdownLinkCheckLogParserCli.MarkdownLinkCheck.LogLines;

internal enum MarkdownLinkCheckLogLineTypes
{
    StartOfFileSummary,
    LinesChecked,
    Error,
    NotValid,
}

internal abstract class MarkdownLinkCheckLogLine
{
    internal MarkdownLinkCheckLogLine(MarkdownLinkCheckLogLineTypes type)
    {
        Type = type;
    }

    public static NotValidLogLine NotValidLogLine { get; } = new NotValidLogLine();

    public MarkdownLinkCheckLogLineTypes Type { get; }
}
