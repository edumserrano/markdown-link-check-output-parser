namespace MarkdownLinkCheckLogParserCli.CliCommands.ParseLog.Exceptions;

public sealed class OutputMarkdownFilepathException : Exception
{
    public OutputMarkdownFilepathException()
        : base("--markdown-filepath must have a value if --output contains 'md'")
    {
    }
}
