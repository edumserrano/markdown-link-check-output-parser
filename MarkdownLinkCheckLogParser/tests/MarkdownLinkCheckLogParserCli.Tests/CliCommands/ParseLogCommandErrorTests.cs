namespace MarkdownLinkCheckLogParserCli.Tests.CliCommands;

/// <summary>
/// These tests check what happens when a logic violation occurs when running the <see cref="ParseLogCommand"/>.
/// </summary>
[Trait("Category", XUnitCategories.LogicFailure)]
public class ParseLogCommandErrorTests
{
    /// <summary>
    /// Tests that the <see cref="ParseLogCommand"/> fails if it can't find a job with the name provided.
    /// </summary>
    [Fact]
    public async Task JobNameNotFound()
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
            JobName = "invalid-job",
            StepName = "Markdown link check",
        };
        using var console = new FakeInMemoryConsole();
        var exception = await Should.ThrowAsync<CommandException>(() => command.ExecuteAsync(console).AsTask());
        const string expectedErrorMessage = @"An error occurred trying to execute the command to parse the log from a Markdown link check step.
Error:
- Couldn't find in the logs a step with name 'Markdown link check' as part of the job with name 'invalid-job'.";

        exception.Message.ShouldBe(expectedErrorMessage);
        exception.InnerException.ShouldNotBeNull();
        exception.InnerException.ShouldBeAssignableTo<JobOrStepNotFoundException>();
        exception.InnerException.Message.ShouldBe("Couldn't find in the logs a step with name 'Markdown link check' as part of the job with name 'invalid-job'.");
    }

    /// <summary>
    /// Tests that the <see cref="ParseLogCommand"/> fails if it can't find a step with the name provided.
    /// </summary>
    [Fact]
    public async Task StepNameNotFound()
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
            StepName = "invalid-step",
        };
        using var console = new FakeInMemoryConsole();
        var exception = await Should.ThrowAsync<CommandException>(() => command.ExecuteAsync(console).AsTask());
        const string expectedErrorMessage = @"An error occurred trying to execute the command to parse the log from a Markdown link check step.
Error:
- Couldn't find in the logs a step with name 'invalid-step' as part of the job with name 'Markdown link check'.";

        exception.Message.ShouldBe(expectedErrorMessage);
        exception.InnerException.ShouldNotBeNull();
        exception.InnerException.ShouldBeAssignableTo<JobOrStepNotFoundException>();
        exception.InnerException.Message.ShouldBe("Couldn't find in the logs a step with name 'invalid-step' as part of the job with name 'Markdown link check'.");
    }

    /// <summary>
    /// Tests that the <see cref="ParseLogCommand"/> fails if it can't find a unique match for job and step name.
    /// </summary>
    [Fact]
    public async Task AmbiguousStepName()
    {
        var handler = new InMemoryGitHubWorkflowRunHandler("./TestFiles/logs-ambiguous-step.zip");
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
        const string expectedErrorMessage = @"An error occurred trying to execute the command to parse the log from a Markdown link check step.
Error:
- Found more than one match in the logs for a step with name 'Markdown link check' as part of the job with name 'Markdown link check'.";

        exception.Message.ShouldBe(expectedErrorMessage);
        exception.InnerException.ShouldNotBeNull();
        exception.InnerException.ShouldBeAssignableTo<JobOrStepMoreThanOneMatchException>();
        exception.InnerException.Message.ShouldBe("Found more than one match in the logs for a step with name 'Markdown link check' as part of the job with name 'Markdown link check'.");
    }
}
