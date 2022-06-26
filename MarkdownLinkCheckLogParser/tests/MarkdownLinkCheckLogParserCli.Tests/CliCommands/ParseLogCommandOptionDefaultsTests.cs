namespace MarkdownLinkCheckLogParserCli.Tests.CliCommands;

/// <summary>
/// These tests check the default values for the the <see cref="ParseLogCommand"/>.
/// </summary>
[Trait("Category", XUnitCategories.Validation)]
public class ParseLogCommandOptionDefaultsTests
{
    /// <summary>
    /// Validation test for the <see cref="ParseLogCommand.AuthToken"/> command option default value.
    /// </summary>
    [Fact]
    public void AuthTokenDefaultsToNull()
    {
        using var console = new FakeInMemoryConsole();
        var command = new ParseLogCommand();
        command.AuthToken.ShouldBeNull();
    }

    /// <summary>
    /// Validation test for the <see cref="ParseLogCommand.Repo"/> command option default value.
    /// </summary>
    [Fact]
    public void RepoDefaultsToNull()
    {
        using var console = new FakeInMemoryConsole();
        var command = new ParseLogCommand();
        command.Repo.ShouldBeNull();
    }

    /// <summary>
    /// Validation test for the <see cref="ParseLogCommand.RunId"/> command option default value.
    /// </summary>
    [Fact]
    public void RunIdDefaultsToNull()
    {
        using var console = new FakeInMemoryConsole();
        var command = new ParseLogCommand();
        command.RunId.ShouldBeNull();
    }

    /// <summary>
    /// Validation test for the <see cref="ParseLogCommand.JobName"/> command option default value.
    /// </summary>
    [Fact]
    public void JobNameDefaultsToNull()
    {
        using var console = new FakeInMemoryConsole();
        var command = new ParseLogCommand();
        command.JobName.ShouldBeNull();
    }

    /// <summary>
    /// Validation test for the <see cref="ParseLogCommand.StepName"/> command option default value.
    /// </summary>
    [Fact]
    public void StepNameDefaultsToNull()
    {
        using var console = new FakeInMemoryConsole();
        var command = new ParseLogCommand();
        command.StepName.ShouldBeNull();
    }

    /// <summary>
    /// Validation test for the <see cref="ParseLogCommand.CaptureErrorsOnly"/> command option default value.
    /// </summary>
    [Fact]
    public void CaptureErrorsOnlyDefaultsToTrue()
    {
        using var console = new FakeInMemoryConsole();
        var command = new ParseLogCommand();
        command.CaptureErrorsOnly.ShouldBeTrue();
    }

    /// <summary>
    /// Validation test for the <see cref="ParseLogCommand.OutputOptions"/> command option default value.
    /// </summary>
    [Fact]
    public void OutputOptionsDefaultsToStep()
    {
        using var console = new FakeInMemoryConsole();
        var command = new ParseLogCommand();
        command.OutputOptions.ShouldBe("step");
    }

    /// <summary>
    /// Validation test for the <see cref="ParseLogCommand.OutputJsonFilepath"/> command option default value.
    /// </summary>
    [Fact]
    public void OutputJsonFilepathDefaultsToEmptyString()
    {
        using var console = new FakeInMemoryConsole();
        var command = new ParseLogCommand();
        command.OutputJsonFilepath.ShouldBe(string.Empty);
    }

    /// <summary>
    /// Validation test for the <see cref="ParseLogCommand.OutputMarkdownFilepath"/> command option default value.
    /// </summary>
    [Fact]
    public void OutputMarkdownFilepathDefaultsToEmptyString()
    {
        using var console = new FakeInMemoryConsole();
        var command = new ParseLogCommand();
        command.OutputMarkdownFilepath.ShouldBe(string.Empty);
    }
}
