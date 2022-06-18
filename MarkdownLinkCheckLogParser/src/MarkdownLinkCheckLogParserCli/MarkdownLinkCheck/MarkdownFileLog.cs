namespace MarkdownLinkCheckLogParserCli.MarkdownLinkCheck;

internal class MarkdownFileLog
{
    public MarkdownFileLog(string filename)
    {
        Filename = filename;
        Errors = new List<MarkdownLinkError>();
    }

    public string Filename { get; }

    public int LinksChecked { get; set; }

    public List<MarkdownLinkError> Errors { get; }
}

internal record MarkdownLinkError(string Link, int StatusCode);
