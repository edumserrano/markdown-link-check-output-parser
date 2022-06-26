namespace MarkdownLinkCheckLogParserCli.CliCommands.ParseLog.Outputs.Exceptions;

public sealed class FailedToCreateJsonFileException : Exception
{
    internal FailedToCreateJsonFileException(OutputJsonFilepath filepath, Exception exception)
        : base($"Failed to output to JSON file at '{filepath}'. Cause: {exception.Message}.", exception)
    {
    }
}
