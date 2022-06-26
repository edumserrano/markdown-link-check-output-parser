namespace MarkdownLinkCheckLogParserCli.CliCommands.ParseLog.Outputs.Exceptions;

public sealed class FailedToCreateMarkdownFileException : Exception
{
    internal FailedToCreateMarkdownFileException(OutputMarkdownFilepath filepath, Exception exception)
        : base($"Failed to output to markdown file at '{filepath}'. Cause: {exception.Message}.", exception)
    {
    }
}
