$output = dotnet '/app/MarkdownLinkCheckLogParserCli.dll' $args

if($LASTEXITCODE -ne 0 ) {
    Write-Output "::error::Markdown link check log parser didn't complete successfully. See the step's log for more details."
    exit $LASTEXITCODE
}

Write-Output "::set-output name=mlc-result::$output"

