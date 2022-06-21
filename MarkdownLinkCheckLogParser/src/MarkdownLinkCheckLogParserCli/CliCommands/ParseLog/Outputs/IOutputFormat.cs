namespace MarkdownLinkCheckLogParserCli.CliCommands.ParseLog.Outputs;

internal interface IOutputFormat
{
    Task WriteAsync(MarkdownLinkCheckOutput output);
}
