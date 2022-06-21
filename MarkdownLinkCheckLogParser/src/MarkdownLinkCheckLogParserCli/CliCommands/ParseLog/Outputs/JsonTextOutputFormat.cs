namespace MarkdownLinkCheckLogParserCli.CliCommands.ParseLog.Outputs;

internal class JsonTextOutputFormat : IOutputFormat
{
    private readonly IConsole _console;

    public JsonTextOutputFormat(IConsole console)
    {
        _console = console.NotNull();
    }

    public async Task WriteAsync(MarkdownLinkCheckOutput output)
    {
        output.NotNull();
        var serializeOptions = new JsonSerializerOptions
        {
            WriteIndented = false,
        };
        var outputAsJson = JsonSerializer.Serialize(output, serializeOptions);
        await _console.Output.WriteLineAsync(outputAsJson);
    }
}
