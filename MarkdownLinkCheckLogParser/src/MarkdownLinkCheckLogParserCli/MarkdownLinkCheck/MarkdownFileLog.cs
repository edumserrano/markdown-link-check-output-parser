namespace MarkdownLinkCheckLogParserCli.MarkdownLinkCheck;

internal class MarkdownFileLog
{
    private readonly List<MarkdownLinkError> _errors;

    public MarkdownFileLog(string filename)
    {
        Filename = filename;
        _errors = new List<MarkdownLinkError>();
    }

    public string Filename { get; }

    public int LinksChecked { get; set; }

    public IReadOnlyList<MarkdownLinkError> Errors => _errors;

    internal void AddError(string link, int statusCode)
    {
        var error = new MarkdownLinkError(link, statusCode);
        _errors.Add(error);
    }
}

internal record MarkdownLinkError(string Link, int StatusCode);
