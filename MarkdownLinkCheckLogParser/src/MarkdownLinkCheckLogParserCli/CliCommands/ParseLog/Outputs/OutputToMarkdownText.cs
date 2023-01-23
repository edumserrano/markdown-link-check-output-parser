namespace MarkdownLinkCheckLogParserCli.CliCommands.ParseLog.Outputs;

internal static class OutputToMarkdownText
{
#pragma warning disable CA1305 // Specify IFormatProvider
#pragma warning disable RCS1197 // Optimize StringBuilder.Append/AppendLine call.
    public static string ToMarkdownText(this MarkdownLinkCheckOutput output)
    {
        output.NotNull();
        var sb = new StringBuilder();
        sb.AppendLine("## Markdown link check results");
        sb.AppendLine();
        if (output.HasErrors)
        {
            sb.AppendLine(":x: Markdown link check found broken links in your markdown files.");
        }
        else
        {
            /* The Markdown Link Check action doesn't output anything when there aren't any broken links.
             * It only outputs:
             * =========================> MARKDOWN LINK CHECK <=========================
             * [✔] All links are good!
             * =========================================================================
             * As such the statistics will all be zero so there's no point in outputting anyting else to the markdown file.
             */
            sb.AppendLine(":heavy_check_mark: Markdown link check didn't find any broken links in your markdown files.");
            sb.AppendLine();
            return sb.ToString();
        }

        sb.AppendLine();
        sb.AppendLine("| Statistic | Value");
        sb.AppendLine("| --- | --- |");
        sb.AppendLine($"| total files checked | {output.TotalFilesChecked}");
        sb.AppendLine($"| total links checked | {output.TotalLinksChecked}");
        sb.AppendLine($"| total errors | {output.TotalErrors}");
        sb.AppendLine();
        sb.AppendLine("<details>");
        sb.AppendLine("<summary><strong>Files</strong></summary>");
        for (var i = 0; i < output.Files.Count; i++)
        {
            sb.AppendLine();
            var checkedFile = output.Files[i];
            sb.AppendLine($"### {checkedFile.Filename}");
            sb.AppendLine();
            sb.AppendLine("| Links checked | Errors");
            sb.AppendLine("| --- | --- |");
            sb.AppendLine($"| {checkedFile.LinksChecked} | {checkedFile.ErrorCount} |");
            if (checkedFile.HasErrors)
            {
                sb.AppendLine();
                sb.AppendLine("| Link | Status code");
                sb.AppendLine("| --- | --- |");
                foreach (var (link, statusCode) in checkedFile.Errors)
                {
                    sb.AppendLine($"| {link} | {statusCode}");
                }
            }

            if (i != output.Files.Count - 1)
            {
                sb.AppendLine();
                sb.AppendLine("---");
            }
        }

        sb.AppendLine();
        return sb.ToString();
    }
}
#pragma warning restore RCS1197 // Optimize StringBuilder.Append/AppendLine call.
#pragma warning restore CA1305 // Specify IFormatProvider
