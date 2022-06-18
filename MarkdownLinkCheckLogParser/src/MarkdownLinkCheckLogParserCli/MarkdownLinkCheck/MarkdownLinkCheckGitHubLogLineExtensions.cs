namespace MarkdownLinkCheckLogParserCli.MarkdownLinkCheck;

internal static class MarkdownLinkCheckLogLineExtensions
{
    public static (bool isStartOfFile, string filename) IsStartOfFileSummary(this string line)
    {
        const string pattern = "FILE: (?<filename>.*)";
        var matches = Regex.Matches(line, pattern, RegexOptions.None);
        if (matches.Count > 0)
        {
            var filename = matches[0].Value;
            return (true, filename);
        }

        return (false, string.Empty);
    }
}
