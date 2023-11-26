namespace MarkdownLinkCheckLogParserCli.CliCommands.ParseLog.Outputs;

internal sealed class JsonConsoleOutputFormat : IOutputFormat
{
    private static readonly JsonSerializerOptions _serializeOptions = new JsonSerializerOptions
    {
        WriteIndented = true,
    };

    private readonly IConsole _console;

    public JsonConsoleOutputFormat(IConsole console)
    {
        _console = console.NotNull();
    }

    public async Task WriteAsync(MarkdownLinkCheckOutput output)
    {
        output.NotNull();
        var outputAsJson = JsonSerializer.Serialize(output, _serializeOptions);
        await _console.Output.WriteLineAsync(outputAsJson);
    }
}
