namespace MarkdownLinkCheckLogParserCli.CliCommands.ParseLog.Outputs.Types;

internal sealed class OutputOptions
{
    private readonly List<OutputOptionsTypes> _values;

    public OutputOptions(string outputOptions)
    {
        outputOptions.NotNullOrWhiteSpace();
        _values = new List<OutputOptionsTypes>();
        var outputOptionsSplit = outputOptions.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        if (outputOptionsSplit.Contains("step"))
        {
            _values.Add(OutputOptionsTypes.Step);
        }

        if (outputOptionsSplit.Contains("json"))
        {
            _values.Add(OutputOptionsTypes.JsonFile);
        }

        if (outputOptionsSplit.Contains("md"))
        {
            _values.Add(OutputOptionsTypes.MarkdownFile);
        }
    }

    public bool HasOption(OutputOptionsTypes option)
    {
        return _values.Contains(option);
    }
}
