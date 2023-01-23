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
        // need to replace newline characters because this output is set as a GitHub
        // step output and without this all newlines are lost.
        // See https://github.community/t/set-output-truncates-multiline-strings/16852/3
        var markdownText = output.ToMarkdownText();
        await _console.Output.WriteAsync(markdownText);
    }
}
