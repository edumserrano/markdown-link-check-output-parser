namespace MarkdownLinkCheckLogParserCli.Tests.CliCommands;

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
            OutputOptions = "json",
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
        markdownLinkCheckOutputJson.Files[0].Filename.ShouldBe("./docs/dev-notes/workflows/pr-dotnet-format-check-workflow.md");
        markdownLinkCheckOutputJson.Files[0].LinksChecked.ShouldBe(5);
        markdownLinkCheckOutputJson.Files[0].ErrorCount.ShouldBe(2);
        markdownLinkCheckOutputJson.Files[0].HasErrors.ShouldBeTrue();
        markdownLinkCheckOutputJson.Files[0].Errors.ShouldNotBeNull();
        markdownLinkCheckOutputJson.Files[0].Errors.Count.ShouldBe(2);
        markdownLinkCheckOutputJson.Files[0].Errors[0].Link.ShouldBe("https://github.com/edumserrano/dotnet-sdk-extensions/actions/workflows/pr-dotnet-format-check.yml/badge.svg");
        markdownLinkCheckOutputJson.Files[0].Errors[0].StatusCode.ShouldBe(404);
        markdownLinkCheckOutputJson.Files[0].Errors[1].Link.ShouldBe("file:///github/workspace/.github/workflows/pr-dotnet-format-check.yml");
        markdownLinkCheckOutputJson.Files[0].Errors[1].StatusCode.ShouldBe(400);

        markdownLinkCheckOutputJson.Files[1].Filename.ShouldBe("./docs/dev-notes/workflows/pr-test-results-comment-workflow.md");
        markdownLinkCheckOutputJson.Files[1].LinksChecked.ShouldBe(5);
        markdownLinkCheckOutputJson.Files[1].ErrorCount.ShouldBe(1);
        markdownLinkCheckOutputJson.Files[1].HasErrors.ShouldBeTrue();
        markdownLinkCheckOutputJson.Files[1].Errors.ShouldNotBeNull();
        markdownLinkCheckOutputJson.Files[1].Errors.Count.ShouldBe(1);
        markdownLinkCheckOutputJson.Files[1].Errors[0].Link.ShouldBe("file:///github/workspace/.github/workflows/pr-pr-test-results-comment.yml");
        markdownLinkCheckOutputJson.Files[1].Errors[0].StatusCode.ShouldBe(400);
    }
}
