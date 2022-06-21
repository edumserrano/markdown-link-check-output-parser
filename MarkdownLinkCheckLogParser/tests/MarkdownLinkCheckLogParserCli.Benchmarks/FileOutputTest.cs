using MarkdownLinkCheckLogParserCli.Files;

namespace MarkdownLinkCheckLogParserCli.Benchmarks;

internal class FileOutputTest : IFile
{
    public StreamWriter CreateFileStreamWriter(string filename)
    {
        var memStream = new MemoryStream();
        return new StreamWriter(memStream);
    }

    public Task WriteAllTextAsync(string filename, string text)
    {
        return Task.CompletedTask;
    }
}
