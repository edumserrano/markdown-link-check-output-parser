namespace MarkdownLinkCheckLogParserCli.GitHub.Exceptions;

public sealed class JobOrStepMoreThanOneMatchException : Exception
{
    internal JobOrStepMoreThanOneMatchException(GitHubJobName jobName, GitHubStepName stepName)
        : base($"Found more than one match in the logs for a step with name '{stepName}' as part of the job with name '{jobName}'.")
    {
    }
}
