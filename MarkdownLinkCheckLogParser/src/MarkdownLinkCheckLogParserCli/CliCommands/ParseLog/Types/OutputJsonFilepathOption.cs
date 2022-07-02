namespace MarkdownLinkCheckLogParserCli.CliCommands.ParseLog.Types;

internal sealed class OutputJsonFilepathOption
{
    public OutputJsonFilepathOption(string jsonFilePathOption)
    {
        Value = jsonFilePathOption.NotNull();
    }

    public string Value { get; }

    public static implicit operator string(OutputJsonFilepathOption filepath)
    {
        return filepath.Value;
    }

    public override string ToString() => (string)this;
}
