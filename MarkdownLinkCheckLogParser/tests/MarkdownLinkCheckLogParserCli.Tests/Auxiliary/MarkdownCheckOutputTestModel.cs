namespace MarkdownLinkCheckLogParserCli.Tests.Auxiliary;

#pragma warning disable CA1812 // Avoid uninstantiated internal classes. Referenced via JsonSerializer.Deserialize<MarkdownLinkCheckOutputJsonModel> usage
internal class MarkdownLinkCheckOutputJsonModel
{
    public MarkdownLinkCheckOutputJsonModel()
    {
        Files = new List<MarkdownFileCheckJsonModel>();
    }

    public int TotalFilesChecked { get; set; }

    public int TotalLinksChecked { get; set; }

    public bool HasErrors { get; set; }

    public int FilesWithErrors { get; set; }

    public int TotalErrors { get; set; }

    public List<MarkdownFileCheckJsonModel> Files { get; set; }
}

internal class MarkdownFileCheckJsonModel
{
    public MarkdownFileCheckJsonModel()
    {
        Errors = new List<MarkdownLinkErrorJsonModel>();
    }

    public string? Filename { get; set; }

    public int LinksChecked { get; set; }

    public int ErrorCount { get; set; }

    public bool HasErrors { get; set; }

    public List<MarkdownLinkErrorJsonModel> Errors { get; set; }
}

internal class MarkdownLinkErrorJsonModel
{
    public string? Link { get; set; }

    public int StatusCode { get; set; }
}
#pragma warning restore CA1812 // Avoid uninstantiated internal classes. Referenced via JsonSerializer.Deserialize<MarkdownLinkCheckOutputJsonModel> usage
