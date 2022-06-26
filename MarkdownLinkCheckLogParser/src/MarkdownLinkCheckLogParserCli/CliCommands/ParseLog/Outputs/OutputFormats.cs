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
        OutputJsonFilepathOption jsonFilepathOption,
        OutputMarkdownFilepathOption markdownFilepathOption)
    {
        var outputFormats = new List<IOutputFormat>();
        if (outputOptions.HasOption(OutputOptionsTypes.Step))
        {
            outputFormats.Add(new JsonConsoleOutputFormat(console));
        }

        if (outputOptions.HasOption(OutputOptionsTypes.JsonFile))
        {
            if (string.IsNullOrWhiteSpace(jsonFilepathOption.Value))
            {
                throw new OutputJsonFilepathException();
            }

            var jsonFilePath = new OutputJsonFilepath(jsonFilepathOption);
            outputFormats.Add(new JsonFileOutputFormat(file, jsonFilePath));
        }

        if (outputOptions.HasOption(OutputOptionsTypes.MarkdownFile))
        {
            if (string.IsNullOrWhiteSpace(markdownFilepathOption.Value))
            {
                throw new OutputMarkdownFilepathException();
            }

            var markdownFilePath = new OutputMarkdownFilepath(markdownFilepathOption);
            outputFormats.Add(new MarkdownFileOutputFormat(file, markdownFilePath));
        }

        return new OutputFormats(outputFormats);
    }

    public IEnumerator<IOutputFormat> GetEnumerator() => _outputFormats.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => _outputFormats.GetEnumerator();
}
