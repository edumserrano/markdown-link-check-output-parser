namespace MarkdownLinkCheckLogParserCli.CliCommands.ParseLog;

[Command("parse-log")]
public class ParseLogCommand : ICommand
{
    private readonly HttpClient? _httpClient;
    private readonly IFile _file;

    public ParseLogCommand()
    {
        _file = new OutputFile();
    }

    // used for test purposes to be able to mock external dependencies
    public ParseLogCommand(HttpClient httpClient, IFile file)
    {
        _httpClient = httpClient.NotNull();
        _file = file.NotNull();
    }

    [CommandOption(
        "auth-token",
        IsRequired = true,
        Validators = new Type[] { typeof(NotNullOrWhitespaceOptionValidator) },
        Description = "GitHub token used to access workflow run logs.")]
    public string AuthToken { get; init; } = default!;

    [CommandOption(
        "repo",
        IsRequired = true,
        Validators = new Type[] { typeof(NotNullOrWhitespaceOptionValidator) },
        Description = "The repository for the workflow run in the format of {owner}/{repo}.")]
    public string Repo { get; init; } = default!;

    [CommandOption(
        "run-id",
        IsRequired = true,
        Validators = new Type[] { typeof(NotNullOrWhitespaceOptionValidator) },
        Description = "The unique identifier of the workflow run that contains the markdown link check step.")]
    public string RunId { get; init; } = default!;

    [CommandOption(
        "job-name",
        IsRequired = true,
        Validators = new Type[] { typeof(NotNullOrWhitespaceOptionValidator) },
        Description = "The name of the job that contains the markdown link check step.")]
    public string JobName { get; init; } = default!;

    [CommandOption(
        "step-name",
        IsRequired = true,
        Validators = new Type[] { typeof(NotNullOrWhitespaceOptionValidator) },
        Description = "The name of the markdown link check step.")]
    public string StepName { get; init; } = default!;

    [CommandOption("only-errors", Description = "Whether the output information contains file errors only or all files.")]
    public bool CaptureErrorsOnly { get; init; } = true;

    [CommandOption(
        "output",
        Validators = new Type[] { typeof(OutputOptionValidator) },
        Description = "How to output the markdown file check result.")]
    public string OutputOptions { get; init; } = "step";

    [CommandOption("json-filepath", Description = "The filepath for the output JSON file.")]
    public string OutputJsonFilepath { get; init; } = string.Empty;

    [CommandOption("markdown-filepath", Description = "The filepath for the output markdown file.")]
    public string OutputMarkdownFilepath { get; init; } = string.Empty;

    public async ValueTask ExecuteAsync(IConsole console)
    {
        try
        {
            console.NotNull();
            var authToken = new GitHubAuthToken(AuthToken);
            var repo = new GitHubRepository(Repo);
            var runId = new GitHubRunId(RunId);
            var jobName = new GitHubJobName(JobName);
            var stepName = new GitHubStepName(StepName);
            var outputOptions = new OutputOptions(OutputOptions);
            var outputJsonFilePath = new OutputJsonFilepath(OutputJsonFilepath);
            var outputMarkdownFilePath = new OutputMarkdownFilepath(OutputMarkdownFilepath);

            using var httpClient = _httpClient ?? GitHubHttpClient.Create(authToken);
            var gitHubHttpClient = new GitHubHttpClient(httpClient);
            var gitHubWorkflowRunLogs = new GitHubWorkflowRunLogs(gitHubHttpClient);
            var stepLog = await gitHubWorkflowRunLogs.GetStepLogAsync(repo, runId, jobName, stepName);
            var output = MarkdownLinkCheckOutputParser.Parse(stepLog, CaptureErrorsOnly);
            var outputFormats = OutputFormats.Create(outputOptions, _file, console, outputJsonFilePath, outputMarkdownFilePath);
            foreach (var outputFormat in outputFormats)
            {
                await outputFormat.WriteAsync(output);
            }
        }
        catch (Exception e)
        {
            var message = @$"An error occurred trying to execute the command to parse the log from a Markdown link check step.
Error:
- {e.Message}";
            throw new CommandException(message, innerException: e);
        }
    }
}
