namespace MarkdownLinkCheckLogParserCli.MarkdownLinkCheck;

internal static class MarkdownLinkCheckOutputExtensions
{
    public static string ToJson(this MarkdownLinkCheckOutput output)
    {
        output.NotNull();
        var serializeOptions = new JsonSerializerOptions
        {
            WriteIndented = false,
        };
        return JsonSerializer.Serialize(output, serializeOptions);
    }

    public static void ToJsonFile(this MarkdownLinkCheckOutput output)
    {
        output.NotNull();
        var serializeOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
        };
        var outputAsJson = JsonSerializer.Serialize(output, serializeOptions);
        File.Delete("filename.json");
        File.WriteAllText("filename.json", outputAsJson);
    }

    public static void ToMarkdownFile(this MarkdownLinkCheckOutput output)
    {
        output.NotNull();
        File.Delete("filename.md");
        using var file = new StreamWriter("filename.md", append: true);
        file.WriteLine("## Markdown link check results");
        file.WriteLine();
        if (output.HasErrors)
        {
            file.WriteLine(":x: Markdown link check found broken links in your markdown files.");
        }
        else
        {
            file.WriteLine(":heavy_check_mark: Markdown link check didn't findany broken links in your markdown files.");
        }

        file.WriteLine();
        file.WriteLine("| Statistic | Value");
        file.WriteLine("| --- | --- |");
        file.WriteLine($"| total files checked | {output.TotalFilesChecked}");
        file.WriteLine($"| total links checked | {output.TotalLinksChecked}");
        file.WriteLine($"| total errors | {output.TotalErrors}");
        file.WriteLine();
        file.WriteLine("<details>");
        file.WriteLine("<summary><strong>Files</strong></summary>");
        for (var i = 0; i < output.Files.Count; i++)
        {
            file.WriteLine();
            var checkedFile = output.Files[i];
            file.WriteLine($"### {checkedFile.Filename}");
            file.WriteLine();
            file.WriteLine($"Links checked: {checkedFile.LinksChecked}");
            file.WriteLine($"Errors: {checkedFile.ErrorCount}");
            file.WriteLine();
            if (checkedFile.HasErrors)
            {
                file.WriteLine("| Link | Status code");
                file.WriteLine("| --- | --- |");
                foreach (var (link, statusCode) in checkedFile.Errors)
                {
                    file.WriteLine($"| {link} | {statusCode}");
                }

                file.WriteLine();
            }

            if (i != output.Files.Count - 1)
            {
                file.WriteLine("---");
            }
        }
    }
}
