namespace MarkdownLinkCheckLogParserCli.MarkdownLinkCheck.LogLines;

// This is part of the Visitor pattern implement to handle the parsing of each log line of the mlc output
// This interface would be equivalent to the IComponent in this example https://refactoring.guru/design-patterns/visitor/csharp/example
// Also, the Handle method in this interface is equivalent to the Accept method in the IComponent interface
internal interface IMarkdownLinkCheckLogLine
{
    void Handle(ParserState state);
}
