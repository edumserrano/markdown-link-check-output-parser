namespace MarkdownLinkCheckLogParserCli.CliCommands.ParseLog.Exceptions;

public sealed class OutputJsonFilepathException : Exception
{
    public OutputJsonFilepathException()
        : base("--json-filepath must have a value if --output contains 'json'")
    {
    }
}
