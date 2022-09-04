namespace MarkdownLinkCheckLogParserCli.Tests.Auxiliary;

internal static class GitHubStepOutputExtensions
{
    public static string UnEscapeGitHubStepOutput(this string input)
    {
        // need to replace newline characters because this output is set as a GitHub
        // step output and without this all newlines are lost.
        // See https://github.community/t/set-output-truncates-multiline-strings/16852/3
        return input
            .Replace("%0A", "\n", StringComparison.InvariantCulture)
            .Replace("%0D", "\r", StringComparison.InvariantCulture);
    }
}
