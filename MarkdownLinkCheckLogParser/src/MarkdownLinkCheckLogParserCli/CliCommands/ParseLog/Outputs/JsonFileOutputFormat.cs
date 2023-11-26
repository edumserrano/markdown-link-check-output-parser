namespace MarkdownLinkCheckLogParserCli.CliCommands.ParseLog.Outputs;

internal sealed class JsonFileOutputFormat : IOutputFormat
{
    private static readonly JsonSerializerOptions _serializeOptions = new JsonSerializerOptions
    {
        WriteIndented = true,
    };

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
        var outputAsJson = JsonSerializer.Serialize(output, _serializeOptions);
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
