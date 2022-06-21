namespace MarkdownLinkCheckLogParserCli.CliCommands.ParseLog.Outputs;

internal class OutputOptions
{
    private readonly List<OutputOptionsTypes> _values;

    public OutputOptions(string value)
    {
        value.NotNullOrWhiteSpace();
        _values = new List<OutputOptionsTypes>();
        if (value.Contains("step", StringComparison.InvariantCultureIgnoreCase))
        {
            _values.Add(OutputOptionsTypes.Step);
        }

        if (value.Contains("json", StringComparison.InvariantCultureIgnoreCase))
        {
            _values.Add(OutputOptionsTypes.JsonFile);
        }

        if (value.Contains("md", StringComparison.InvariantCultureIgnoreCase))
        {
            _values.Add(OutputOptionsTypes.MarkdownFile);
        }
    }

    public bool HasOption(OutputOptionsTypes option)
    {
        return _values.Contains(option);
    }
}
