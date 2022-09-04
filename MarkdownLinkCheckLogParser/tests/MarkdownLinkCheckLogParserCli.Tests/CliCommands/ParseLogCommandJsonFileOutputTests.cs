namespace MarkdownLinkCheckLogParserCli.Tests.CliCommands;

/// <summary>
/// These tests make sure that the <see cref="ParseLogCommand"/> outputs as expected to a JSON file.
/// </summary>
[Trait("Category", XUnitCategories.Commands)]
public class ParseLogCommandJsonFileOutputTests
{
    /// <summary>
    /// Tests that the <see cref="ParseLogCommand"/> produces the expected output to a JSON file.
    /// Uses default option for --only-errors.
    /// Uses logs that contain files with markdown errors.
    /// </summary>
    [Fact]
    public async Task ParseLogCommandJsonFileTest1()
    {
        var handler = new InMemoryGitHubWorkflowRunHandler("./TestFiles/logs-with-errors.zip");
        var httpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri("https://api.github.com"),
        };
        const string jsonFilepath = "output.json";
        var jsonFileText = string.Empty;
        var file = Substitute.For<IFile>();
        file.WriteAllTextAsync(jsonFilepath, Arg.Any<string>())
            .Returns(Task.CompletedTask)
            .AndDoes(x => jsonFileText = x.ArgAt<string>(1));
        var command = new ParseLogCommand(httpClient, file)
        {
            AuthToken = "auth-token",
            Repo = "repo-name",
            RunId = "run-id",
            JobName = "Markdown link check",
            StepName = "Markdown link check",
            OutputOptions = "file-json",
            OutputJsonFilepath = jsonFilepath,
        };
        using var console = new FakeInMemoryConsole();
        await command.ExecuteAsync(console);
        var consoleOutput = console.ReadOutputString();

        consoleOutput.ShouldBeNullOrEmpty();
        var markdownLinkCheckOutputJson = JsonSerializer.Deserialize<MarkdownLinkCheckOutputJsonModel>(jsonFileText);
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
        markdownLinkCheckOutputJson.Files[7].Errors[0].Link.ShouldBe("file:///github/workspace/.github/workflows/pr-dotnet-format-check.yml");
        markdownLinkCheckOutputJson.Files[7].Errors[0].StatusCode.ShouldBe(400);
        markdownLinkCheckOutputJson.Files[7].Errors[1].Link.ShouldBe("https://github.com/edumserrano/dotnet-sdk-extensions/actions/workflows/pr-dotnet-format-check.yml/badge.svg");
        markdownLinkCheckOutputJson.Files[7].Errors[1].StatusCode.ShouldBe(404);

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
    /// Tests that the <see cref="ParseLogCommand"/> throws an error if output is set to JSON file but no filepath is provided.
    /// </summary>
    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public async Task ParseLogCommandJsonFileTest2(string jsonFilepath)
    {
        var handler = new InMemoryGitHubWorkflowRunHandler("./TestFiles/logs-with-errors.zip");
        var httpClient = new HttpClient(handler)
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
            OutputOptions = "file-json",
            OutputJsonFilepath = jsonFilepath,
        };
        using var console = new FakeInMemoryConsole();
        var exception = await Should.ThrowAsync<CommandException>(() => command.ExecuteAsync(console).AsTask());
        const string expectedErrorMessage = @"An error occurred trying to execute the command to parse the log from a Markdown link check step.
Error:
- --json-filepath must have a value if --output contains 'json'";

        exception.Message.ShouldBe(expectedErrorMessage);
        exception.InnerException.ShouldNotBeNull();
        exception.InnerException.ShouldBeAssignableTo<OutputJsonFilepathException>();
        exception.InnerException.Message.ShouldBe("--json-filepath must have a value if --output contains 'json'");
    }
}
