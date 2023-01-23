namespace MarkdownLinkCheckLogParserCli.CliCommands.ParseLog.Outputs;

internal sealed class JsonConsoleOutputFormat : IOutputFormat
{
    private readonly IConsole _console;

    public JsonConsoleOutputFormat(IConsole console)
    {
        _console = console.NotNull();
    }

    public async Task WriteAsync(MarkdownLinkCheckOutput output)
    {
        output.NotNull();
        var serializeOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
        };
        var outputAsJson = JsonSerializer
            .Serialize(output, serializeOptions)
            .EscapeGitHubStepOutput();
        await _console.Output.WriteLineAsync(outputAsJson);
    }
}
