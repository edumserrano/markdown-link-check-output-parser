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

$output = dotnet '/app/MarkdownLinkCheckLogParserCli.dll' parse-log `
--authToken $authToken `
--repo $repo `
--run-id $runId `
--job-name $jobName `
--stepName $stepName `
--only-errors $onlyErrors `
--output $output `
# --json-filepath $jsonFilepath `
# --markdown-filepath $markdownFilepath

if($LASTEXITCODE -ne 0 ) {
    Write-Output "::error::Markdown link check log parser didn't complete successfully. See the step's log for more details."
    exit $LASTEXITCODE
}

Write-Output "::set-output name=mlc-result::$output"
