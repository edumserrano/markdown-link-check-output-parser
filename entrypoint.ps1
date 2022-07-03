param 
(
    $authToken,
    $repo,
    $runId,
    $jobName,
    $stepName,
    $onlyErrors,
    $output,
    $jsonFilepath,
    $markdownFilepath
)

$command = "dotnet '/app/MarkdownLinkCheckLogParserCli.dll' parse-log --auth-token $authToken --repo $repo --run-id $runId --job-name '$jobName' --step-name '$stepName' --only-errors $onlyErrors --output $output"
if(![string]::IsNullOrWhitespace($jsonFilepath)) 
{
    $command += " --json-filepath $jsonFilepath"
}
if(![string]::IsNullOrWhitespace($markdownFilepath))
{
    $command += " --markdown-filepath $markdownFilepath"
}

$output = Invoke-Expression $command
if($LASTEXITCODE -ne 0 ) {
    Write-Output "::error::Markdown link check log parser didn't complete successfully. See the step's log for more details."
    exit $LASTEXITCODE
}

Write-Output "::set-output name=mlc-result::$output"
