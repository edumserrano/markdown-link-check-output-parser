namespace MarkdownLinkCheckLogParserCli.CliCommands.ParseLog.Outputs;

internal static class GitHubStepOutputExtensions
{
    public static string EscapeGitHubStepOutput(this string input)
    {
        input.NotNull();
        // need to replace newline characters because this output is set as a GitHub
        // step output and without this all newlines are lost.
        // See https://github.community/t/set-output-truncates-multiline-strings/16852/3
        return input
            .Replace("\n", "%0A", StringComparison.InvariantCulture)
            .Replace("\r", "%0D", StringComparison.InvariantCulture);
    }
}
