namespace MarkdownLinkCheckLogParserCli.CliCommands.ParseLog.Outputs.Types;

internal sealed class OutputMarkdownFilepath
{
    private readonly string _value;

    public OutputMarkdownFilepath(string value)
    {
        _value = value.NotNull();
    }

    public static implicit operator string(OutputMarkdownFilepath filepath)
    {
        return filepath._value;
    }

    public override string ToString() => (string)this;
}
