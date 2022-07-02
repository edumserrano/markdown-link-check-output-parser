namespace MarkdownLinkCheckLogParserCli.CliCommands.ParseLog.Outputs.Types;

internal sealed class OutputMarkdownFilepath
{
    private readonly string _value;

    public OutputMarkdownFilepath(string markdownFilePath)
    {
        _value = markdownFilePath.NotNullOrWhiteSpace();
    }

    public static implicit operator string(OutputMarkdownFilepath filepath)
    {
        return filepath._value;
    }

    public override string ToString() => (string)this;
}
