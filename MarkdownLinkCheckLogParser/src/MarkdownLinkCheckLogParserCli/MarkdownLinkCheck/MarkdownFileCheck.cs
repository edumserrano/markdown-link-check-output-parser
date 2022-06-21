namespace MarkdownLinkCheckLogParserCli.MarkdownLinkCheck;

internal class MarkdownFileCheck
{
    private readonly List<MarkdownLinkError> _errors;

    public MarkdownFileCheck(string filename)
    {
        Filename = filename.NotNull();
        _errors = new List<MarkdownLinkError>();
    }

    public string Filename { get; }

    public int LinksChecked { get; set; }

    public int ErrorCount => Errors.Count;

    public bool HasErrors => ErrorCount > 0;

    public IReadOnlyList<MarkdownLinkError> Errors => _errors;

    internal void AddError(string link, int statusCode)
    {
        link.NotNullOrWhiteSpace();
        var error = new MarkdownLinkError(link, statusCode);
        _errors.Add(error);
    }
}

internal record MarkdownLinkError(string Link, int StatusCode);
