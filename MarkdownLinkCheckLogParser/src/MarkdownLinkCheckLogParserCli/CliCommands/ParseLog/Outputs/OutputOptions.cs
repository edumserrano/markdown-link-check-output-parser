namespace MarkdownLinkCheckLogParserCli.CliCommands.ParseLog.Outputs;

internal class OutputOptions
{
    private readonly List<OutputOptionsTypes> _values;

    public OutputOptions(string value)
    {
        value.NotNullOrWhiteSpace();
        _values = new List<OutputOptionsTypes>();
        var outputOptions = value.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        if (outputOptions.Contains("step"))
        {
            _values.Add(OutputOptionsTypes.Step);
        }

        if (outputOptions.Contains("json"))
        {
            _values.Add(OutputOptionsTypes.JsonFile);
        }

        if (outputOptions.Contains("md"))
        {
            _values.Add(OutputOptionsTypes.MarkdownFile);
        }
    }

    public bool HasOption(OutputOptionsTypes option)
    {
        return _values.Contains(option);
    }
}
