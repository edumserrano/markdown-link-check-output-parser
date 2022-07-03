namespace MarkdownLinkCheckLogParserCli.CliCommands.ParseLog.Outputs.Types;

internal sealed class OutputOptions
{
    private readonly List<OutputOptionsTypes> _values;

    public OutputOptions(string outputOptions)
    {
        outputOptions.NotNullOrWhiteSpace();
        _values = new List<OutputOptionsTypes>();
        var outputOptionsSplit = outputOptions.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        if (outputOptionsSplit.Contains("step-json"))
        {
            _values.Add(OutputOptionsTypes.StepJson);
        }

        if (outputOptionsSplit.Contains("step-md"))
        {
            _values.Add(OutputOptionsTypes.StepMd);
        }

        if (outputOptionsSplit.Contains("file-json"))
        {
            _values.Add(OutputOptionsTypes.FileJson);
        }

        if (outputOptionsSplit.Contains("file-md"))
        {
            _values.Add(OutputOptionsTypes.FileMarkdown);
        }
    }

    public bool HasOption(OutputOptionsTypes option)
    {
        return _values.Contains(option);
    }
}
