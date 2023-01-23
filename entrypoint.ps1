function Main()
{
  [OutputType([Void])]
  param ([string[]] $inputArgs)

  # Remove any arg that is empty or whitespace. This removes empty strings that are passed from the action.yml.
  # This is a workaround to deal with non-required action inputs when this script is called via the action.yml.
  $argsAsList = [Collections.Generic.List[String]]::new()
  foreach ($arg in $inputArgs)
  {
    if(![string]::IsNullOrWhiteSpace($arg))
    {
      $argsAsList.Add($arg)
    }
  }

  Write-Output "Executing: dotnet '/app/MarkdownLinkCheckLogParserCli.dll' $argsAsList"
  $output = dotnet '/app/MarkdownLinkCheckLogParserCli.dll' $argsAsList

  if($LASTEXITCODE -ne 0 ) {
      Write-Output "::error::Markdown link check log parser didn't complete successfully. See the step's log for more details."
      exit $LASTEXITCODE
  }

  $random = Get-Random
  $delimiter = "EOF_$random"
  Write-Output "mlc-result<<$delimiter" >> $env:GITHUB_OUTPUT
  Write-Output $output >> $env:GITHUB_OUTPUT
  Write-Output $delimiter >> $env:GITHUB_OUTPUT
}

# invoke entrypoint function
Main $args
