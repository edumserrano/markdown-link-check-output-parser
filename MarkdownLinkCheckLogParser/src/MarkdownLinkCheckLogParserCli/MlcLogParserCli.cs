namespace MarkdownLinkCheckLogParserCli;

public class MlcLogParserCli
{
    public MlcLogParserCli()
    {
        CliApplicationBuilder = new CliApplicationBuilder().AddCommandsFromThisAssembly();
    }

    public CliApplicationBuilder CliApplicationBuilder { get; }

    public ValueTask<int> RunAsync(IReadOnlyList<string> args = default!)
    {
        args ??= [];
        return CliApplicationBuilder
            .Build()
            .RunAsync(args);
    }
}
