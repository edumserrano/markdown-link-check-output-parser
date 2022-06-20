namespace MarkdownLinkCheckLogParserCli.CliCommands;

[Command("parse-log")]
public class ParseLogCommand : ICommand
{
    private readonly HttpClient? _httpClient;

    public ParseLogCommand()
    {
    }

    // used for test purposes to be able to mock the HttpClient calls
    public ParseLogCommand(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    [CommandOption(
        "auth-token",
        't',
        IsRequired = true,
        Validators = new Type[] { typeof(NotNullOrWhitespaceOptionValidator) },
        Description = "GitHub token used to access workflow run logs.")]
    public string AuthToken { get; init; } = default!;

    [CommandOption(
        "repo",
        'r',
        IsRequired = true,
        Validators = new Type[] { typeof(NotNullOrWhitespaceOptionValidator) },
        Description = "The repository for the workflow run in the format of {owner}/{repo}.")]
    public string Repo { get; init; } = default!;

    [CommandOption(
        "run-id",
        'i',
        IsRequired = true,
        Validators = new Type[] { typeof(NotNullOrWhitespaceOptionValidator) },
        Description = "The unique identifier of the workflow run that contains the markdown link check step.")]
    public string RunId { get; init; } = default!;

    [CommandOption(
        "job-name",
        'j',
        IsRequired = true,
        Validators = new Type[] { typeof(NotNullOrWhitespaceOptionValidator) },
        Description = "The name of the job that contains the markdown link check step.")]
    public string JobName { get; init; } = default!;

    [CommandOption(
        "step-name",
        's',
        IsRequired = true,
        Validators = new Type[] { typeof(NotNullOrWhitespaceOptionValidator) },
        Description = "The name of the markdown link check step.")]
    public string StepName { get; init; } = default!;

    [CommandOption("only-errors", 'e', Description = "Don't output file information unless it's an error.")]
    public bool CaptureErrorsOnly { get; init; } = default!;

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

            using var httpClient = _httpClient ?? GitHubHttpClient.Create(authToken);
            var gitHubHttpClient = new GitHubHttpClient(httpClient);
            var gitHubWorkflowRunLogs = new GitHubWorkflowRunLogs(gitHubHttpClient);
            var stepLog = await gitHubWorkflowRunLogs.GetStepLogAsync(repo, runId, jobName, stepName);
            var output = MarkdownLinkCheckOutputParser.Parse(stepLog, CaptureErrorsOnly);
            var outputAsJson = output.ToJson();
            await console.Output.WriteLineAsync(outputAsJson);
            File.Delete("test.txt");
            using var _ = File.Create("test.txt");
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
