using MarkdownLinkCheckLogParserCli.CliCommands.ParseLog.Outputs.Types;

namespace MarkdownLinkCheckLogParserCli.CliCommands.ParseLog.Outputs;

internal sealed class OutputFormats : IReadOnlyList<IOutputFormat>
{
    private readonly IReadOnlyList<IOutputFormat> _outputFormats;

    private OutputFormats(IReadOnlyList<IOutputFormat> outputFormats)
    {
        _outputFormats = outputFormats;
    }

    public IOutputFormat this[int index] => _outputFormats[index];

    public int Count => _outputFormats.Count;

    public static OutputFormats Create(
        OutputOptions outputOptions,
        IFile file,
        IConsole console,
        OutputJsonFilepath outputJsonFilepath,
        OutputMarkdownFilepath outputMarkdownFilepath)
    {
        var outputFormats = new List<IOutputFormat>();
        if (outputOptions.HasOption(OutputOptionsTypes.Step))
        {
            outputFormats.Add(new JsonConsoleOutputFormat(console));
        }

        if (outputOptions.HasOption(OutputOptionsTypes.JsonFile))
        {
            outputFormats.Add(new JsonFileOutputFormat(file, outputJsonFilepath));
        }

        if (outputOptions.HasOption(OutputOptionsTypes.MarkdownFile))
        {
            outputFormats.Add(new MarkdownFileOutputFormat(file, outputMarkdownFilepath));
        }

        return new OutputFormats(outputFormats);
    }

    public IEnumerator<IOutputFormat> GetEnumerator() => _outputFormats.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => _outputFormats.GetEnumerator();
}
