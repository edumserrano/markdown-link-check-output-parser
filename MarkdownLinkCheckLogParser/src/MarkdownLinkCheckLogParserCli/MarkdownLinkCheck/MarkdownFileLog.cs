namespace MarkdownLinkCheckLogParserCli.MarkdownLinkCheck;

internal class MarkdownFileLog
{
    public string Filename { get; set; } = default!;

    public int LinksChecked { get; set; }

    public int Errors => MarkdownLinkErrors.Count;

    public List<MarkdownLinkError> MarkdownLinkErrors { get; set; } = new List<MarkdownLinkError>();
}

internal class MarkdownLinkError
{
    public string Link { get; set; } = default!;

    public int StatusCode { get; set; }
}
