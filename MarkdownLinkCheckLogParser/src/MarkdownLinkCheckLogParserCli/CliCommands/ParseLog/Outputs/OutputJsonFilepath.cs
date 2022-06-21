namespace MarkdownLinkCheckLogParserCli.CliCommands.ParseLog.Outputs;

internal class OutputJsonFilepath
{
    private readonly string _value;

    public OutputJsonFilepath(string value)
    {
        _value = value.NotNull();
    }

    public static implicit operator string(OutputJsonFilepath filepath)
    {
        return filepath._value;
    }

    public override string ToString() => (string)this;
}
