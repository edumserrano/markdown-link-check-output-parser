//namespace MarkdownLinkCheckLogParserCli.MarkdownLinkCheck.LogLines;

//internal static class MarkdownLinkCheckLogLineFactory
//{
//    public static MarkdownLinkCheckLogLine Create(ReadOnlyMemory<char> logLine)
//    {
//        (var isStartOfFileSummary, var filename) = IsStartOfFileSummary(logLine);
//        if (isStartOfFileSummary)
//        {
//            return new StartOfFileSummaryLogLine(filename);
//        }

//        (var isLinksChecked, var linksChecked) = IsLinksCheckedLine(logLine);
//        if (isLinksChecked)
//        {
//            return new LinksCheckedLogLine(linksChecked);
//        }

//        if (IsErrorLine(logLine, out var error))
//        {
//            return new ErrorLogLine(error);
//        }

//        return MarkdownLinkCheckLogLine.NotValidLogLine;
//    }

//    private static (bool isStartOfFile, string filename) IsStartOfFileSummary(ReadOnlyMemory<char> logLine)
//    {
//        var b = logLine.Span.IndexOf(' ');

//        //const string pattern = "FILE: (?<filename>.*)";
//        //var matches = Regex.Matches(logLine, pattern, RegexOptions.None);
//        //if (matches.Count > 0)
//        //{
//        //    var filename = matches[0].Value;
//        //    return (true, filename);
//        //}

//        //return (false, string.Empty);
//    }

//    private static (bool isLinksChecked, int linksChecked) IsLinksCheckedLine(string logLine)
//    {
//        const string pattern = @"(?<linksChecked>\d+) links checked.";
//        var namedGroup = Regex
//            .Matches(logLine, pattern, RegexOptions.None)
//            .SelectMany<Match, Group>(x => x.Groups)
//            .FirstOrDefault(x => "linksChecked".Equals(x.Name, StringComparison.Ordinal));
//        if (namedGroup is not null)
//        {
//            var linksChecked = namedGroup!.Value;
//            return (true, int.Parse(linksChecked, CultureInfo.InvariantCulture));
//        }

//        return (false, -1);
//    }

//    private static bool IsErrorLine(
//        string logLine,
//        [NotNullWhen(returnValue: true)] out MarkdownLinkError? error)
//    {
//        const string pattern = @"\[✖\] (?<link>.*) → Status: (?<statusCode>\d+)";
//        var groups = Regex
//            .Matches(logLine, pattern, RegexOptions.None)
//            .SelectMany<Match, Group>(x => x.Groups);
//        var linkNamedGroup = groups.FirstOrDefault(x => "link".Equals(x.Name, StringComparison.Ordinal));
//        var statusCodeNamedGroup = groups.FirstOrDefault(x => "statusCode".Equals(x.Name, StringComparison.Ordinal));
//        if (linkNamedGroup is not null && statusCodeNamedGroup is not null)
//        {
//            var link = linkNamedGroup.Value;
//            var statusCode = int.Parse(statusCodeNamedGroup.Value, CultureInfo.InvariantCulture);
//            error = new MarkdownLinkError(link, statusCode);
//            return true;
//        }

//        error = null;
//        return false;
//    }
//}
