namespace MarkdownLinkCheckLogParserCli.CliCommands.ParseLog.Outputs;

internal sealed class MarkdownConsoleOutputFormat : IOutputFormat
{
    private readonly IConsole _console;

    public MarkdownConsoleOutputFormat(IConsole console)
    {
        _console = console.NotNull();
    }

    public async Task WriteAsync(MarkdownLinkCheckOutput output)
    {
        output.NotNull();
        var markdownText = output.ToMarkdownText();
        await _console.Output.WriteAsync(markdownText);
    }
}
