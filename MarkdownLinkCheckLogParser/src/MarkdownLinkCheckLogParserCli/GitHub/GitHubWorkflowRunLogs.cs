namespace MarkdownLinkCheckLogParserCli.GitHub;

internal sealed class GitHubWorkflowRunLogs
{
    private readonly GitHubHttpClient _gitHubHttpClient;

    public GitHubWorkflowRunLogs(GitHubHttpClient gitHubHttpClient)
    {
        _gitHubHttpClient = gitHubHttpClient.NotNull();
    }

    public async Task<GitHubStepLog> GetStepLogAsync(
        GitHubRepository repo,
        GitHubRunId runId,
        GitHubJobName jobName,
        GitHubStepName stepName)
    {
        repo.NotNull();
        runId.NotNull();
        jobName.NotNull();
        stepName.NotNull();

        using var workflowRunLogsZip = await _gitHubHttpClient.DownloadWorkflowRunLogsAsync(repo, runId);
        var markdownLinkCheckLogsZipEntrys = workflowRunLogsZip.Entries.
            Where(e => e.FullName.Contains($"{jobName}/", StringComparison.OrdinalIgnoreCase) && e.Name.Contains(stepName, StringComparison.OrdinalIgnoreCase))
            .ToList();
        if (markdownLinkCheckLogsZipEntrys.Count == 0)
        {
            throw new JobOrStepNotFoundException(jobName, stepName);
        }

        if (markdownLinkCheckLogsZipEntrys.Count > 1)
        {
            throw new JobOrStepMoreThanOneMatchException(jobName, stepName);
        }

        var logAsZip = markdownLinkCheckLogsZipEntrys[0];
        await using var logAsStream = logAsZip.Open();
        using var streamReader = new StreamReader(logAsStream, Encoding.UTF8);
        var memory = new Memory<char>(new char[logAsZip.Length]);
        await streamReader.ReadAsync(memory);
        return new GitHubStepLog(memory);
    }
}
