namespace MarkdownLinkCheckLogParserCli.CliCommands.ParseLog.Outputs;

internal sealed class JsonFileOutputFormat : IOutputFormat
{
    private readonly IFile _file;
    private readonly OutputJsonFilepath _filepath;

    public JsonFileOutputFormat(IFile file, OutputJsonFilepath filepath)
    {
        _file = file.NotNull();
        _filepath = filepath.NotNull();
    }

    public async Task WriteAsync(MarkdownLinkCheckOutput output)
    {
        output.NotNull();
        var serializeOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
        };
        var outputAsJson = JsonSerializer.Serialize(output, serializeOptions);
        try
        {
            await _file.WriteAllTextAsync(filename: _filepath, text: outputAsJson);
        }
        catch (Exception e)
        {
            throw new FailedToCreateJsonFileException(_filepath, e);
        }
    }
}
