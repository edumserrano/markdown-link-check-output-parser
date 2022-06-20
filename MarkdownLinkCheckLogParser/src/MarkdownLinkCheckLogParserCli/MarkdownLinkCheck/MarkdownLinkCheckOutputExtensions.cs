namespace MarkdownLinkCheckLogParserCli.MarkdownLinkCheck;

internal static class MarkdownLinkCheckOutputExtensions
{
    public static string ToJson(this MarkdownLinkCheckOutput output)
    {
        var serializeOptions = new JsonSerializerOptions
        {
            WriteIndented = false,
        };
        return JsonSerializer.Serialize(output, serializeOptions);
    }
}
