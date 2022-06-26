namespace MarkdownLinkCheckLogParserCli.CliCommands.ParseLog.Exceptions;

public sealed class OutputJsonFilepathException : Exception
{
    internal OutputJsonFilepathException()
        : base("--json-filepath must have a value if --output contains 'json'")
    {
    }
}
