namespace MarkdownLinkCheckLogParserCli.GitHub.Exceptions;

public sealed class JobOrStepMoreThanOneMatchException : Exception
{
    internal JobOrStepMoreThanOneMatchException(GitHubJobName jobName, GitHubStepName stepName)
        : base($"Found more than one match in the logs for a step name that contains '{stepName}' as part of the job with name '{jobName}'.")
    {
    }
}
