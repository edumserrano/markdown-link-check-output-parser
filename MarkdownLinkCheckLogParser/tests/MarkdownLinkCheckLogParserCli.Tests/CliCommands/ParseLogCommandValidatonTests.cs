namespace MarkdownLinkCheckLogParserCli.Tests.CliCommands;

/// <summary>
/// These tests check the validation on the options for the the <see cref="ParseLogCommand"/>.
/// </summary>
[Trait("Category", XUnitCategories.Validation)]
public class ParseLogCommandValidatonTests
{
    /// <summary>
    /// Validation test for the <see cref="ParseLogCommand.AuthToken"/> command option.
    /// </summary>
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public async Task ValidateAuthTokenOption(string authToken)
    {
        using var console = new FakeInMemoryConsole();
        var command = new ParseLogCommand
        {
            AuthToken = authToken,
            Repo = "repo-name",
            RunId = "run-id",
            JobName = "job-name",
            StepName = "step-name",
        };
        var exception = await Should.ThrowAsync<CommandException>(() => command.ExecuteAsync(console).AsTask());
        const string expectedErrorMessage = @"An error occurred trying to execute the command to parse the log from a Markdown link check step.
Error:
- gitHubAuthToken cannot be null or whitespace.";

        exception.Message.ShouldBe(expectedErrorMessage);
        exception.InnerException.ShouldNotBeNull();
        exception.InnerException.ShouldBeAssignableTo<ArgumentException>();
        exception.InnerException.Message.ShouldBe("gitHubAuthToken cannot be null or whitespace.");
    }

    /// <summary>
    /// Validation test for the <see cref="ParseLogCommand.Repo"/> command option.
    /// </summary>
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public async Task ValidateRepoOption(string repo)
    {
        using var console = new FakeInMemoryConsole();
        var command = new ParseLogCommand
        {
            AuthToken = "auth-token",
            Repo = repo,
            RunId = "run-id",
            JobName = "job-name",
            StepName = "step-name",
        };
        var exception = await Should.ThrowAsync<CommandException>(() => command.ExecuteAsync(console).AsTask());
        const string expectedErrorMessage = @"An error occurred trying to execute the command to parse the log from a Markdown link check step.
Error:
- repository cannot be null or whitespace.";

        exception.Message.ShouldBe(expectedErrorMessage);
        exception.InnerException.ShouldNotBeNull();
        exception.InnerException.ShouldBeAssignableTo<ArgumentException>();
        exception.InnerException.Message.ShouldBe("repository cannot be null or whitespace.");
    }

    /// <summary>
    /// Validation test for the <see cref="ParseLogCommand.RunId"/> command option.
    /// </summary>
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public async Task ValidateRunIdOption(string runId)
    {
        using var console = new FakeInMemoryConsole();
        var command = new ParseLogCommand
        {
            AuthToken = "auth-token",
            Repo = "repo-name",
            RunId = runId,
            JobName = "job-name",
            StepName = "step-name",
        };
        var exception = await Should.ThrowAsync<CommandException>(() => command.ExecuteAsync(console).AsTask());
        const string expectedErrorMessage = @"An error occurred trying to execute the command to parse the log from a Markdown link check step.
Error:
- runId cannot be null or whitespace.";

        exception.Message.ShouldBe(expectedErrorMessage);
        exception.InnerException.ShouldNotBeNull();
        exception.InnerException.ShouldBeAssignableTo<ArgumentException>();
        exception.InnerException.Message.ShouldBe("runId cannot be null or whitespace.");
    }

    /// <summary>
    /// Validation test for the <see cref="ParseLogCommand.AuthToken"/> command option.
    /// </summary>
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public async Task ValidateJobNameOption(string jobName)
    {
        using var console = new FakeInMemoryConsole();
        var command = new ParseLogCommand
        {
            AuthToken = "auth-token",
            Repo = "repo-name",
            RunId = "run-id",
            JobName = jobName,
            StepName = "step-name",
        };
        var exception = await Should.ThrowAsync<CommandException>(() => command.ExecuteAsync(console).AsTask());
        const string expectedErrorMessage = @"An error occurred trying to execute the command to parse the log from a Markdown link check step.
Error:
- jobName cannot be null or whitespace.";

        exception.Message.ShouldBe(expectedErrorMessage);
        exception.InnerException.ShouldNotBeNull();
        exception.InnerException.ShouldBeAssignableTo<ArgumentException>();
        exception.InnerException.Message.ShouldBe("jobName cannot be null or whitespace.");
    }

    /// <summary>
    /// Validation test for the <see cref="ParseLogCommand.StepName"/> command option.
    /// </summary>
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public async Task ValidateStepNameOption(string stepName)
    {
        using var console = new FakeInMemoryConsole();
        var command = new ParseLogCommand
        {
            AuthToken = "auth-token",
            Repo = "repo-name",
            RunId = "run-id",
            JobName = "job-name",
            StepName = stepName,
        };
        var exception = await Should.ThrowAsync<CommandException>(() => command.ExecuteAsync(console).AsTask());
        const string expectedErrorMessage = @"An error occurred trying to execute the command to parse the log from a Markdown link check step.
Error:
- stepName cannot be null or whitespace.";

        exception.Message.ShouldBe(expectedErrorMessage);
        exception.InnerException.ShouldNotBeNull();
        exception.InnerException.ShouldBeAssignableTo<ArgumentException>();
        exception.InnerException.Message.ShouldBe("stepName cannot be null or whitespace.");
    }

    /// <summary>
    /// Validation test for the <see cref="ParseLogCommand.OutputOptions"/> command option.
    /// </summary>
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public async Task ValidateOutputOptionsOption(string outputOptions)
    {
        using var console = new FakeInMemoryConsole();
        var command = new ParseLogCommand
        {
            AuthToken = "auth-token",
            Repo = "repo-name",
            RunId = "run-id",
            JobName = "job-name",
            StepName = "step-name",
            OutputOptions = outputOptions,
        };
        var exception = await Should.ThrowAsync<CommandException>(() => command.ExecuteAsync(console).AsTask());
        const string expectedErrorMessage = @"An error occurred trying to execute the command to parse the log from a Markdown link check step.
Error:
- outputOptions cannot be null or whitespace.";

        exception.Message.ShouldBe(expectedErrorMessage);
        exception.InnerException.ShouldNotBeNull();
        exception.InnerException.ShouldBeAssignableTo<ArgumentException>();
        exception.InnerException.Message.ShouldBe("outputOptions cannot be null or whitespace.");
    }
}
