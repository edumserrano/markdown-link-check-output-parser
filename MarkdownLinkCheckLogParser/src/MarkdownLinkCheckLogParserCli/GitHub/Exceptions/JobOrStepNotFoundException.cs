namespace MarkdownLinkCheckLogParserCli.GitHub.Exceptions;

public sealed class JobOrStepNotFoundException : Exception
{
    internal JobOrStepNotFoundException(GitHubJobName jobName, GitHubStepName stepName)
        : base($"Couldn't find in the logs a step with name '{stepName}' as part of the job with name '{jobName}'.")
    {
    }
}
