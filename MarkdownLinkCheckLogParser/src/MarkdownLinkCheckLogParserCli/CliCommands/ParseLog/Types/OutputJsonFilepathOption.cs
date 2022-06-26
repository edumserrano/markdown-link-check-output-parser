namespace MarkdownLinkCheckLogParserCli.CliCommands.ParseLog.Types;

internal sealed class OutputJsonFilepathOption
{
    public OutputJsonFilepathOption(string value)
    {
        Value = value.NotNull();
    }

    public string Value { get; }

    public static implicit operator string(OutputJsonFilepathOption filepath)
    {
        return filepath.Value;
    }

    public override string ToString() => (string)this;
}
