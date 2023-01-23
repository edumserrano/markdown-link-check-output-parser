namespace MarkdownLinkCheckLogParserCli.Tests.CliCommands;

/// <summary>
/// These tests make sure that the <see cref="ParseLogCommand"/> outputs the expected JSON value to the console.
/// </summary>
[Trait("Category", XUnitCategories.Commands)]
public class ParseLogCommandMarkdownConsoleOutputTests
{
    /// <summary>
    /// Tests that the <see cref="ParseLogCommand"/> produces the expected markdown output to the console.
    /// Uses all the default options.
    /// Uses logs that contain files with markdown errors.
    /// </summary>
    [Fact]
    public async Task ParseLogCommandMarkdownConsoleTest1()
    {
        using var handler = new InMemoryGitHubWorkflowRunHandler("./TestFiles/logs-with-errors.zip");
        using var httpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri("https://api.github.com"),
        };
        var file = Substitute.For<IFile>();
        var command = new ParseLogCommand(httpClient, file)
        {
            AuthToken = "auth-token",
            Repo = "repo-name",
            RunId = "run-id",
            JobName = "Markdown link check",
            StepName = "Markdown link check",
            OutputOptions = "step-md",
        };
        using var console = new FakeInMemoryConsole();
        await command.ExecuteAsync(console);
        var markdownLinkCheckOutputMd = console
            .ReadOutputString()
            .UnEscapeGitHubStepOutput();
        var expectedMarkdown = File.ReadAllText("./TestFiles/output-with-errors-capture-errors-only.md");
        markdownLinkCheckOutputMd.ShouldBeWithNormalizedNewlines(expectedMarkdown);
    }

    /// <summary>
    /// Tests that the <see cref="ParseLogCommand"/> produces the expected markdown output to the console.
    /// Uses all the default options EXCEPT --only-errors which is set to false.
    /// Uses logs that contain files with markdown errors.
    /// </summary>
    [Fact]
    public async Task ParseLogCommandMarkdownConsoleTest2()
    {
        using var handler = new InMemoryGitHubWorkflowRunHandler("./TestFiles/logs-with-errors.zip");
        using var httpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri("https://api.github.com"),
        };
        var file = Substitute.For<IFile>();
        var command = new ParseLogCommand(httpClient, file)
        {
            AuthToken = "auth-token",
            Repo = "repo-name",
            RunId = "run-id",
            JobName = "Markdown link check",
            StepName = "Markdown link check",
            OutputOptions = "step-md",
            CaptureErrorsOnly = false,
        };
        using var console = new FakeInMemoryConsole();
        await command.ExecuteAsync(console);
        var markdownLinkCheckOutputMd = console
            .ReadOutputString()
            .UnEscapeGitHubStepOutput();
        var expectedMarkdown = File.ReadAllText("./TestFiles/output-with-errors-all-files.md");
        markdownLinkCheckOutputMd.ShouldBeWithNormalizedNewlines(expectedMarkdown);
    }

    /// <summary>
    /// Tests that the <see cref="ParseLogCommand"/> produces the expected markdown output to the console.
    /// Uses all the default options.
    /// Uses logs that do NOT contain files with markdown errors.
    /// When there are zero errors, the Markdown Link Check action does NOT output any information about the files checked
    /// so all the statistics will be zero.
    /// </summary>
    [Fact]
    public async Task ParseLogCommandMarkdownConsoleTest3()
    {
        using var handler = new InMemoryGitHubWorkflowRunHandler("./TestFiles/logs-without-errors.zip");
        using var httpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri("https://api.github.com"),
        };
        var file = Substitute.For<IFile>();
        var command = new ParseLogCommand(httpClient, file)
        {
            AuthToken = "auth-token",
            Repo = "repo-name",
            RunId = "run-id",
            JobName = "Markdown link check",
            StepName = "Markdown link check",
            OutputOptions = "step-md",
        };
        using var console = new FakeInMemoryConsole();
        await command.ExecuteAsync(console);
        var markdownLinkCheckOutputMd = console
            .ReadOutputString()
            .UnEscapeGitHubStepOutput();
        var expectedMarkdown = File.ReadAllText("./TestFiles/output-without-errors.md");
        markdownLinkCheckOutputMd.ShouldBeWithNormalizedNewlines(expectedMarkdown);
    }
}
