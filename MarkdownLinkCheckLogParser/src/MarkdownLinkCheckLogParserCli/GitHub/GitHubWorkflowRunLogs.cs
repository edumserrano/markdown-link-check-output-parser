namespace MarkdownLinkCheckLogParserCli.GitHub;

internal class GitHubWorkflowRunLogs
{
    private readonly GitHubHttpClient _gitHubHttpClient;

    public GitHubWorkflowRunLogs(GitHubHttpClient gitHubHttpClient)
    {
        _gitHubHttpClient = gitHubHttpClient.NotNull();
    }

    public async Task<ZipArchiveEntry> GetWorkflowRunLogForStepAsync(string repo, string runId, string jobName, string stepName)
    {
        var workflowRunLogsZip = await _gitHubHttpClient.DownloadWorkflowRunLogsAsync(repo, runId);
        var markdownLinkCheckLogsZipEntrys = workflowRunLogsZip.Entries.
            Where(e => e.FullName.Contains($"{jobName}/", StringComparison.Ordinal) && e.Name.Contains(stepName, StringComparison.Ordinal))
            .ToList();
        if (markdownLinkCheckLogsZipEntrys.Count == 0)
        {
            // warning no match
            throw new Exception("TODO no matches");
        }

        if (markdownLinkCheckLogsZipEntrys.Count > 1)
        {
            // warning if there is more than one and fail
            throw new Exception("TODO more than one match");
        }

        return markdownLinkCheckLogsZipEntrys[0];      
    }
}
