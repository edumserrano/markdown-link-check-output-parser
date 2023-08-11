namespace MarkdownLinkCheckLogParserCli.CliCommands.ParseLog.Outputs;

internal sealed class MarkdownFileOutputFormat : IOutputFormat
{
    private readonly IFile _file;
    private readonly OutputMarkdownFilepath _filepath;

    public MarkdownFileOutputFormat(IFile file, OutputMarkdownFilepath filepath)
    {
        _file = file.NotNull();
        _filepath = filepath.NotNull();
    }

    public async Task WriteAsync(MarkdownLinkCheckOutput output)
    {
        output.NotNull();
        try
        {
            var markdownText = output.ToMarkdownText();
            await using var streamWriter = _file.CreateFileStreamWriter(_filepath);
            await streamWriter.WriteAsync(markdownText);
        }
        catch (Exception e)
        {
            throw new FailedToCreateMarkdownFileException(_filepath, e);
        }
    }
}
