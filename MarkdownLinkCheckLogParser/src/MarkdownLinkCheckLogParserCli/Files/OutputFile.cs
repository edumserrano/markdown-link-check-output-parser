namespace MarkdownLinkCheckLogParserCli.Files;

internal sealed class OutputFile : IFile
{
    public StreamWriter CreateFileStreamWriter(string filename)
    {
        File.Delete(filename);
        return new StreamWriter(filename);
    }

    public async Task WriteAllTextAsync(string filename, string text)
    {
        File.Delete(filename);
        await File.WriteAllTextAsync(filename, text);
    }
}
