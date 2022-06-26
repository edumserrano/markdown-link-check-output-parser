namespace MarkdownLinkCheckLogParserCli.CliCommands.ParseLog.Exceptions;

public sealed class OutputMarkdownFilepathException : Exception
{
    internal OutputMarkdownFilepathException()
        : base("--markdown-filepath must have a value if --output contains 'md'")
    {
    }
}
