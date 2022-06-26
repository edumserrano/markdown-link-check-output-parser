namespace MarkdownLinkCheckLogParserCli.Tests.CliCommands;

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
}
