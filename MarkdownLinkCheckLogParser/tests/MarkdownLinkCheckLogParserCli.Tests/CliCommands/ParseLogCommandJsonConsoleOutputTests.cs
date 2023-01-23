namespace MarkdownLinkCheckLogParserCli.Tests.CliCommands;

/// <summary>
/// These tests make sure that the <see cref="ParseLogCommand"/> outputs the expected JSON value to the console.
/// </summary>
[Trait("Category", XUnitCategories.Commands)]
public class ParseLogCommandJsonConsoleOutputTests
{
    /// <summary>
    /// Tests that the <see cref="ParseLogCommand"/> produces the expected JSON output to the console.
    /// Uses all the default options.
    /// Uses logs that contain files with markdown errors.
    /// </summary>
    [Fact]
    public async Task ParseLogCommandJsonConsoleTest1()
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
        };
        using var console = new FakeInMemoryConsole();
        await command.ExecuteAsync(console);
        var output = console.ReadOutputString();

        var markdownLinkCheckOutputJson = JsonSerializer.Deserialize<MarkdownLinkCheckOutputJsonModel>(output);
        markdownLinkCheckOutputJson.ShouldNotBeNull();
        markdownLinkCheckOutputJson.TotalFilesChecked.ShouldBe(42);
        markdownLinkCheckOutputJson.TotalLinksChecked.ShouldBe(380);
        markdownLinkCheckOutputJson.HasErrors.ShouldBeTrue();
        markdownLinkCheckOutputJson.FilesWithErrors.ShouldBe(13);
        markdownLinkCheckOutputJson.TotalErrors.ShouldBe(53);
        markdownLinkCheckOutputJson.Files.ShouldNotBeNull();
        markdownLinkCheckOutputJson.Files.Count.ShouldBe(13);
        // just going to check the first 2 out of 13 files
        markdownLinkCheckOutputJson.Files[7].Filename.ShouldBe("./docs/dev-notes/workflows/pr-dotnet-format-check-workflow.md");
        markdownLinkCheckOutputJson.Files[7].LinksChecked.ShouldBe(5);
        markdownLinkCheckOutputJson.Files[7].ErrorCount.ShouldBe(2);
        markdownLinkCheckOutputJson.Files[7].HasErrors.ShouldBeTrue();
        markdownLinkCheckOutputJson.Files[7].Errors.ShouldNotBeNull();
        markdownLinkCheckOutputJson.Files[7].Errors.Count.ShouldBe(2);
        markdownLinkCheckOutputJson.Files[7].Errors[1].Link.ShouldBe("https://github.com/edumserrano/dotnet-sdk-extensions/actions/workflows/pr-dotnet-format-check.yml/badge.svg");
        markdownLinkCheckOutputJson.Files[7].Errors[1].StatusCode.ShouldBe(404);
        markdownLinkCheckOutputJson.Files[7].Errors[0].Link.ShouldBe("file:///github/workspace/.github/workflows/pr-dotnet-format-check.yml");
        markdownLinkCheckOutputJson.Files[7].Errors[0].StatusCode.ShouldBe(400);

        markdownLinkCheckOutputJson.Files[9].Filename.ShouldBe("./docs/dev-notes/workflows/pr-test-results-comment-workflow.md");
        markdownLinkCheckOutputJson.Files[9].LinksChecked.ShouldBe(5);
        markdownLinkCheckOutputJson.Files[9].ErrorCount.ShouldBe(1);
        markdownLinkCheckOutputJson.Files[9].HasErrors.ShouldBeTrue();
        markdownLinkCheckOutputJson.Files[9].Errors.ShouldNotBeNull();
        markdownLinkCheckOutputJson.Files[9].Errors.Count.ShouldBe(1);
        markdownLinkCheckOutputJson.Files[9].Errors[0].Link.ShouldBe("file:///github/workspace/.github/workflows/pr-pr-test-results-comment.yml");
        markdownLinkCheckOutputJson.Files[9].Errors[0].StatusCode.ShouldBe(400);
    }

    /// <summary>
    /// Tests that the <see cref="ParseLogCommand"/> produces the expected JSON output to the console.
    /// Uses all the default options EXCEPT --only-errors which is set to false.
    /// Uses logs that contain files with markdown errors.
    /// </summary>
    [Fact]
    public async Task ParseLogCommandJsonConsoleTest2()
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
            CaptureErrorsOnly = false,
        };
        using var console = new FakeInMemoryConsole();
        await command.ExecuteAsync(console);
        var output = console.ReadOutputString();

        var markdownLinkCheckOutputJson = JsonSerializer.Deserialize<MarkdownLinkCheckOutputJsonModel>(output);
        markdownLinkCheckOutputJson.ShouldNotBeNull();
        markdownLinkCheckOutputJson.TotalFilesChecked.ShouldBe(42);
        markdownLinkCheckOutputJson.TotalLinksChecked.ShouldBe(380);
        markdownLinkCheckOutputJson.HasErrors.ShouldBeTrue();
        markdownLinkCheckOutputJson.FilesWithErrors.ShouldBe(13);
        markdownLinkCheckOutputJson.TotalErrors.ShouldBe(53);
        markdownLinkCheckOutputJson.Files.ShouldNotBeNull();
        markdownLinkCheckOutputJson.Files.Count.ShouldBe(42);
        // just going to check the first 2 out of 42 files
        markdownLinkCheckOutputJson.Files[23].Filename.ShouldBe("./docs/integration-tests/disable-logs-integration-tests.md");
        markdownLinkCheckOutputJson.Files[23].LinksChecked.ShouldBe(2);
        markdownLinkCheckOutputJson.Files[23].ErrorCount.ShouldBe(0);
        markdownLinkCheckOutputJson.Files[23].HasErrors.ShouldBeFalse();
        markdownLinkCheckOutputJson.Files[23].Errors.ShouldNotBeNull();
        markdownLinkCheckOutputJson.Files[23].Errors.Count.ShouldBe(0);

        markdownLinkCheckOutputJson.Files[26].Filename.ShouldBe("./docs/integration-tests/http-mocking-in-process.md");
        markdownLinkCheckOutputJson.Files[26].LinksChecked.ShouldBe(9);
        markdownLinkCheckOutputJson.Files[26].ErrorCount.ShouldBe(0);
        markdownLinkCheckOutputJson.Files[26].HasErrors.ShouldBeFalse();
        markdownLinkCheckOutputJson.Files[26].Errors.ShouldNotBeNull();
        markdownLinkCheckOutputJson.Files[26].Errors.Count.ShouldBe(0);
    }

    /// <summary>
    /// Tests that the <see cref="ParseLogCommand"/> produces the expected JSON output to the console.
    /// Uses all the default options.
    /// Uses logs that do NOT contain files with markdown errors.
    /// When there are zero errors, the Markdown Link Check action does NOT output any information about the files checked
    /// so all the statistics will be zero.
    /// </summary>
    [Fact]
    public async Task ParseLogCommandJsonConsoleTest3()
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
        };
        using var console = new FakeInMemoryConsole();
        await command.ExecuteAsync(console);
        var output = console.ReadOutputString();

        var markdownLinkCheckOutputJson = JsonSerializer.Deserialize<MarkdownLinkCheckOutputJsonModel>(output);
        markdownLinkCheckOutputJson.ShouldNotBeNull();
        markdownLinkCheckOutputJson.TotalFilesChecked.ShouldBe(0);
        markdownLinkCheckOutputJson.TotalLinksChecked.ShouldBe(0);
        markdownLinkCheckOutputJson.HasErrors.ShouldBeFalse();
        markdownLinkCheckOutputJson.FilesWithErrors.ShouldBe(0);
        markdownLinkCheckOutputJson.TotalErrors.ShouldBe(0);
        markdownLinkCheckOutputJson.Files.ShouldNotBeNull();
        markdownLinkCheckOutputJson.Files.Count.ShouldBe(0);
    }
}
