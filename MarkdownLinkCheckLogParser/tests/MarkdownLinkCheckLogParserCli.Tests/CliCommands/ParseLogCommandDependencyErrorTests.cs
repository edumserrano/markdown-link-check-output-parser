namespace MarkdownLinkCheckLogParserCli.Tests.CliCommands;

/// <summary>
/// These tests check what happens when a dependency of the <see cref="ParseLogCommand"/> fails.
/// </summary>
[Trait("Category", XUnitCategories.DependencyFailure)]
public class ParseLogCommandDependencyErrorTests
{
    /// <summary>
    /// Tests that the <see cref="ParseLogCommand"/> shows expected error message when it fails to download the workflow logs.
    /// Testing some non-success status code.
    /// </summary>
    [Theory]
    [InlineData(HttpStatusCode.Unauthorized)]
    [InlineData(HttpStatusCode.BadRequest)]
    [InlineData(HttpStatusCode.InternalServerError)]
    public async Task GitHubHttpClientFailsToDownloadLogs(HttpStatusCode gitHubHttpClientResponseStatusCode)
    {
        var handler = new StatusCodeHandler(gitHubHttpClientResponseStatusCode);
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
        };
        using var console = new FakeInMemoryConsole();
        var exception = await Should.ThrowAsync<CommandException>(() => command.ExecuteAsync(console).AsTask());
        var expectedErrorMessage = @$"An error occurred trying to execute the command to parse the log from a Markdown link check step.
Error:
- Failed to download workflow run logs. Got {(int)gitHubHttpClientResponseStatusCode} {gitHubHttpClientResponseStatusCode} from GET https://api.github.com/repos/repo-name/actions/runs/run-id/logs.";
        exception.Message.ShouldBe(expectedErrorMessage);
        exception.InnerException.ShouldNotBeNull();
        exception.InnerException.ShouldBeAssignableTo<GitHubHttpClientException>();
        exception.InnerException.Message.ShouldBe($"Failed to download workflow run logs. Got {(int)gitHubHttpClientResponseStatusCode} {gitHubHttpClientResponseStatusCode} from GET https://api.github.com/repos/repo-name/actions/runs/run-id/logs.");
    }

    /// <summary>
    /// Tests that the <see cref="ParseLogCommand"/> shows expected error message when it fails to write the JSON file.
    /// This test will simulate an exception when writing to the file, in reality the most likely exception is something like
    /// the filepath provided is invalid.
    /// This test guarantees that whatever the exception is, its error message get's propagated.
    /// </summary>
    [Fact]
    public async Task ParseLogCommandJsonFileTest()
    {
        var handler = new InMemoryGitHubWorkflowRunHandler("./TestFiles/logs-with-errors.zip");
        var httpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri("https://api.github.com"),
        };
        var file = Substitute.For<IFile>();
        file.WriteAllTextAsync(Arg.Any<string>(), Arg.Any<string>())
            .Returns(_ => throw new ArgumentException("Oops"));
        var command = new ParseLogCommand(httpClient, file)
        {
            AuthToken = "auth-token",
            Repo = "repo-name",
            RunId = "run-id",
            JobName = "Markdown link check",
            StepName = "Markdown link check",
            OutputOptions = "file-json",
            OutputJsonFilepath = "some-file-path",
        };
        using var console = new FakeInMemoryConsole();
        var exception = await Should.ThrowAsync<CommandException>(() => command.ExecuteAsync(console).AsTask());
        const string expectedErrorMessage = @"An error occurred trying to execute the command to parse the log from a Markdown link check step.
Error:
- Failed to output to JSON file at 'some-file-path'. Cause: Oops.";
        exception.Message.ShouldBe(expectedErrorMessage);
        exception.InnerException.ShouldNotBeNull();
        exception.InnerException.ShouldBeAssignableTo<FailedToCreateJsonFileException>();
        exception.InnerException.Message.ShouldBe("Failed to output to JSON file at 'some-file-path'. Cause: Oops.");
    }

    /// <summary>
    /// Tests that the <see cref="ParseLogCommand"/> shows expected error message when it fails to write the markdown file.
    /// This test will simulate an exception when writing to the file, in reality the most likely exception is something like
    /// the filepath provided is invalid.
    /// This test guarantees that whatever the exception is, its error message get's propagated.
    /// </summary>
    [Fact]
    public async Task ParseLogCommandMarkdownFileTest()
    {
        var handler = new InMemoryGitHubWorkflowRunHandler("./TestFiles/logs-with-errors.zip");
        var httpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri("https://api.github.com"),
        };
        var file = Substitute.For<IFile>();
        file.CreateFileStreamWriter(Arg.Any<string>())
            .Returns(_ => throw new ArgumentException("Oops"));
        var command = new ParseLogCommand(httpClient, file)
        {
            AuthToken = "auth-token",
            Repo = "repo-name",
            RunId = "run-id",
            JobName = "Markdown link check",
            StepName = "Markdown link check",
            OutputOptions = "file-md",
            OutputMarkdownFilepath = "some-file-path",
        };
        using var console = new FakeInMemoryConsole();
        var exception = await Should.ThrowAsync<CommandException>(() => command.ExecuteAsync(console).AsTask());
        const string expectedErrorMessage = @"An error occurred trying to execute the command to parse the log from a Markdown link check step.
Error:
- Failed to output to markdown file at 'some-file-path'. Cause: Oops.";
        exception.Message.ShouldBe(expectedErrorMessage);
        exception.InnerException.ShouldNotBeNull();
        exception.InnerException.ShouldBeAssignableTo<FailedToCreateMarkdownFileException>();
        exception.InnerException.Message.ShouldBe("Failed to output to markdown file at 'some-file-path'. Cause: Oops.");
    }
}
