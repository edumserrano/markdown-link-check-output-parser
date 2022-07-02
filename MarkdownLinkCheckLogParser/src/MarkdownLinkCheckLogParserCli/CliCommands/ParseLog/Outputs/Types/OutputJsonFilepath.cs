namespace MarkdownLinkCheckLogParserCli.CliCommands.ParseLog.Outputs.Types;

internal sealed class OutputJsonFilepath
{
    private readonly string _value;

    public OutputJsonFilepath(string jsonFilePath)
    {
        _value = jsonFilePath.NotNullOrWhiteSpace();
    }

    public static implicit operator string(OutputJsonFilepath filepath)
    {
        return filepath._value;
    }

    public override string ToString() => (string)this;
}
