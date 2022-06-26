namespace MarkdownLinkCheckLogParserCli.Tests.CliCommands;

/// <summary>
/// These tests make sure that the <see cref="ParseLogCommand"/> outputs as expected to a markdown file.
/// </summary>
[Trait("Category", XUnitCategories.Commands)]
public class ParseLogCommandMarkdownFileOutputTests
{
    /// <summary>
    /// Tests that the <see cref="ParseLogCommand"/> produces the expected output to a markdown file.
    /// Uses default option for --only-errors.
    /// Uses logs that contain files with markdown errors.
    /// </summary>
    [Fact]
    public async Task ParseLogCommandMarkdownFileTest1()
    {
        var handler = new InMemoryGitHubWorkflowRunHandler("./TestFiles/logs-with-errors.zip");
        var httpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri("https://api.github.com"),
        };
        const string mdFilepath = "output.md";
        var file = Substitute.For<IFile>();
        var markdownMemoryStream = new MemoryStream();
        file.CreateFileStreamWriter(Arg.Any<string>())
            .Returns(new StreamWriter(markdownMemoryStream, Encoding.UTF8, bufferSize: -1, leaveOpen: true));
        var command = new ParseLogCommand(httpClient, file)
        {
            AuthToken = "auth-token",
            Repo = "repo-name",
            RunId = "run-id",
            JobName = "Markdown link check",
            StepName = "Markdown link check",
            OutputOptions = "md",
            OutputMarkdownFilepath = mdFilepath,
        };
        using var console = new FakeInMemoryConsole();
        await command.ExecuteAsync(console);
        var consoleOutput = console.ReadOutputString();

        consoleOutput.ShouldBeNullOrEmpty();
        markdownMemoryStream.Seek(0, SeekOrigin.Begin);
        var markdownStreamReader = new StreamReader(markdownMemoryStream);
        var markdownAsString = await markdownStreamReader.ReadToEndAsync();
        var expectedMarkdown = NormalizedLineEndingsFileReader.ReadAllText("./TestFiles/output-with-errors-capture-errors-only.md");
        markdownAsString.ShouldBe(expectedMarkdown);
    }

    /// <summary>
    /// Tests that the <see cref="ParseLogCommand"/> produces the expected output to a markdown file.
    /// Sets --only-errors to false.
    /// Uses logs that contain files with markdown errors.
    /// </summary>
    [Fact]
    public async Task ParseLogCommandMarkdownFileTest2()
    {
        var handler = new InMemoryGitHubWorkflowRunHandler("./TestFiles/logs-with-errors.zip");
        var httpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri("https://api.github.com"),
        };
        const string mdFilepath = "output.md";
        var file = Substitute.For<IFile>();
        var markdownMemoryStream = new MemoryStream();
        file.CreateFileStreamWriter(Arg.Any<string>())
            .Returns(new StreamWriter(markdownMemoryStream, Encoding.UTF8, bufferSize: -1, leaveOpen: true));
        var command = new ParseLogCommand(httpClient, file)
        {
            AuthToken = "auth-token",
            Repo = "repo-name",
            RunId = "run-id",
            JobName = "Markdown link check",
            StepName = "Markdown link check",
            CaptureErrorsOnly = false,
            OutputOptions = "md",
            OutputMarkdownFilepath = mdFilepath,
        };
        using var console = new FakeInMemoryConsole();
        await command.ExecuteAsync(console);
        var consoleOutput = console.ReadOutputString();

        consoleOutput.ShouldBeNullOrEmpty();
        markdownMemoryStream.Seek(0, SeekOrigin.Begin);
        var markdownStreamReader = new StreamReader(markdownMemoryStream);
        var markdownAsString = await markdownStreamReader.ReadToEndAsync();
        var expectedMarkdown = NormalizedLineEndingsFileReader.ReadAllText("./TestFiles/output-with-errors-all-files.md");
        markdownAsString.ShouldBe(expectedMarkdown);
    }

    /// <summary>
    /// Tests that the <see cref="ParseLogCommand"/> produces the expected output to a markdown file.
    /// Uses default option for --only-errors.
    /// Uses logs that does NOT contain files with markdown errors.
    /// </summary>
    [Fact]
    public async Task ParseLogCommandMarkdownFileTest3()
    {
        var handler = new InMemoryGitHubWorkflowRunHandler("./TestFiles/logs-without-errors.zip");
        var httpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri("https://api.github.com"),
        };
        const string mdFilepath = "output.md";
        var file = Substitute.For<IFile>();
        var markdownMemoryStream = new MemoryStream();
        file.CreateFileStreamWriter(Arg.Any<string>())
            .Returns(new StreamWriter(markdownMemoryStream, Encoding.UTF8, bufferSize: -1, leaveOpen: true));
        var command = new ParseLogCommand(httpClient, file)
        {
            AuthToken = "auth-token",
            Repo = "repo-name",
            RunId = "run-id",
            JobName = "Markdown link check",
            StepName = "Markdown link check",
            CaptureErrorsOnly = false,
            OutputOptions = "md",
            OutputMarkdownFilepath = mdFilepath,
        };
        using var console = new FakeInMemoryConsole();
        await command.ExecuteAsync(console);
        var consoleOutput = console.ReadOutputString();

        consoleOutput.ShouldBeNullOrEmpty();
        markdownMemoryStream.Seek(0, SeekOrigin.Begin);
        var markdownStreamReader = new StreamReader(markdownMemoryStream);
        var markdownAsString = await markdownStreamReader.ReadToEndAsync();
        var expectedMarkdown = NormalizedLineEndingsFileReader.ReadAllText("./TestFiles/output-without-errors-capture-errors-only.md");
        markdownAsString.ShouldBe(expectedMarkdown);
    }


    /// <summary>
    /// Tests that the <see cref="ParseLogCommand"/> throws an error if output is set to markdown file but no filepath is provided.
    /// </summary>
    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public async Task ParseLogCommandMarkdownFileTest4(string markdownFilepath)
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
            OutputOptions = "md",
            OutputJsonFilepath = markdownFilepath,
        };
        using var console = new FakeInMemoryConsole();
        var exception = await Should.ThrowAsync<CommandException>(() => command.ExecuteAsync(console).AsTask());
        const string expectedErrorMessage = @"An error occurred trying to execute the command to parse the log from a Markdown link check step.
Error:
- --markdown-filepath must have a value if --output contains 'md'";

        exception.Message.ShouldBe(expectedErrorMessage);
        exception.InnerException.ShouldNotBeNull();
        exception.InnerException.ShouldBeAssignableTo<OutputMarkdownFilepathException>();
        exception.InnerException.Message.ShouldBe("--markdown-filepath must have a value if --output contains 'md'");
    }
}
