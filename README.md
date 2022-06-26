# Markdown link check log parser

[![Build and test](https://github.com/edumserrano/markdown-link-check-log-parser/actions/workflows/build-test.yml/badge.svg)](https://github.com/edumserrano/markdown-link-check-log-parser/actions/workflows/build-test.yml)
[![codecov](https://codecov.io/gh/edumserrano/markdown-link-check-log-parser/branch/main/graph/badge.svg?token=4gWKUGwz7V)](https://codecov.io/gh/edumserrano/markdown-link-check-log-parser)
[![GitHub Marketplace](https://img.shields.io/badge/Marketplace-Markdown%20Link%20Check%20log%20parser-blue.svg?colorA=24292e&colorB=0366d6&style=flat&longCache=true&logo=data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAA4AAAAOCAYAAAAfSC3RAAAABHNCSVQICAgIfAhkiAAAAAlwSFlzAAAM6wAADOsB5dZE0gAAABl0RVh0U29mdHdhcmUAd3d3Lmlua3NjYXBlLm9yZ5vuPBoAAAERSURBVCiRhZG/SsMxFEZPfsVJ61jbxaF0cRQRcRJ9hlYn30IHN/+9iquDCOIsblIrOjqKgy5aKoJQj4O3EEtbPwhJbr6Te28CmdSKeqzeqr0YbfVIrTBKakvtOl5dtTkK+v4HfA9PEyBFCY9AGVgCBLaBp1jPAyfAJ/AAdIEG0dNAiyP7+K1qIfMdonZic6+WJoBJvQlvuwDqcXadUuqPA1NKAlexbRTAIMvMOCjTbMwl1LtI/6KWJ5Q6rT6Ht1MA58AX8Apcqqt5r2qhrgAXQC3CZ6i1+KMd9TRu3MvA3aH/fFPnBodb6oe6HM8+lYHrGdRXW8M9bMZtPXUji69lmf5Cmamq7quNLFZXD9Rq7v0Bpc1o/tp0fisAAAAASUVORK5CYII=)](https://github.com/marketplace/actions/markdown-link-check-log-parser)
<!-- 
[![Test GitHub action](https://github.com/edumserrano/github-issue-forms-parser/workflows/Test%20GitHub%20action/badge.svg)](https://github.com/edumserrano/github-issue-forms-parser/actions/workflows/test-action.yml)
[![codecov](https://codecov.io/gh/edumserrano/github-issue-forms-parser/branch/main/graph/badge.svg?token=B9nrGE2Ine)](https://codecov.io/gh/edumserrano/github-issue-forms-parser)
-->

[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](./LICENSE)
[![GitHub Sponsors](https://img.shields.io/github/sponsors/edumserrano)](https://github.com/sponsors/edumserrano)
[![LinkedIn](https://img.shields.io/badge/LinkedIn-Eduardo%20Serrano-blue.svg)](https://www.linkedin.com/in/eduardomserrano/)

A Docker container [GitHub action](https://docs.github.com/en/actions/learn-github-actions/finding-and-customizing-actions) that can be used to parse the log from a [GitHub Action - Markdown link check](https://github.com/gaurav-nelson/github-action-markdown-link-check) step.

As of writing this, the [GitHub Action - Markdown link check](https://github.com/gaurav-nelson/github-action-markdown-link-check) doesn't provide information from the Markdown link check as output of the action and there's even a long outstanding [open issue about it](https://github.com/gaurav-nelson/github-action-markdown-link-check/issues/9). This action is an alternative solution for that issue.

## Usage

```yml
- name: Run Markdown Link Check log parser
  id: mlc-log-parser
  uses: edumserrano/markdown-link-check-log-parser@v1
  with:
    auth-token:  ${{ secrets.GITHUB_TOKEN }}
    repo: '<repo-name>'
    run-id: '<run-id>'
    job-name: '<job-name>'
    step-name: '<step-name>'
# The next step is using powershell to parse the action's output but you can use whatever you prefer.
# Note that in order to read the step outputs the action step must have an id.
- name: Output parsed issue
  shell: pwsh
  run: |
    $result = '${{ steps.mlc-log-parser.outputs.mlc-result }}' | ConvertFrom-Json
    $resultAsJsonIndented = ConvertTo-Json $result
    Write-Output $resultAsJsonIndented # outputs the markdown link check result as an indented JSON string
    Write-Output "Total files checked: $($result.TotalFilesChecked)"
    Write-Output "Total links chedked: $($result.TotalLinksChecked)"
    Write-Output "Has erros: $($result.HasErrors)"
    ...
```

### Action inputs

| Name | Description | Required | Default
| --- | --- | --- | --- |
| `auth-token` | GitHub token used to access workflow run logs. | yes | ---
| `repo` | The repository for the workflow run in the format of {owner}/{repo}. | yes | ---
| `run-id` | The unique identifier of the workflow run that contains the markdown link check step. | yes | ---
| `job-name` | The name of the job that contains the markdown link check step. | yes | ---
| `step-name` | The name of the markdown link check step. | yes | ---
| `only-errors` | Whether the output information contains file errors only or all files. | no | true
| `output` | How to output the markdown file check result. It must be one of or a comma separated list of the following values: step,json,md. | no | step
| `json-filepath` | The filepath for the output JSON file. | no | ---
| `markdown-filepath` | The filepath for the output markdown file. | no | ---

### Action outputs

| Name | Description
| --- | --- |
| `mlc-result` | The result from the Markdown Link Check action in JSON format. |

Note that the action also allows outputing the result from the Markdown Link Check action into a JSON file or into a Markdown files through the use of the appropriate action inputs.

<!--
## Example usages of this action in GitHub workflows

- [This workflow](https://github.com/edumserrano/github-issue-forms-parser/blob/main/.github/workflows/test-action.yml) is used to test that the action works as expected.
- [This workflow](https://github.com/edumserrano/dot-net-sdk-extensions/blob/33303189c564989fd40fcb1fa5086ca443f7bd92/.github/workflows/nuget-release.yml#L69-L73) shows the usage of the action as part of the release flow of a NuGet package.
-->

## I can't figure out the right job name and step name to use

You can download the logs from your workflow by:

1) Go into the job of of the workflow where you are running the [Markdown Link Check](https://github.com/gaurav-nelson/github-action-markdown-link-check) action.
2) Select the cog wheel that shows on the top right and you will see an option to download the logs as shown by the image below.

![how-to-get-logs](docs/main-readme-assets/how-to-get-logs.png "How to get logs")

You can then extract the log zip file and you can find which file contains the log for the Markdown Link Check step.
The job name will be equal to the folder name where the Markdown Link Check step log is.
The step name will be equal to the name of the log file for the Markdown Link Check step but without the prefix for the 'number_'.

**Example:**

If the log file for the Markdown Link Check step is named `5_Markdown link check.txt` and this file is inside a folder named `Check links` then the action inputs should be:

- job-name: 'Check links'
- step-name: 'Markdown link check'

## Dev notes

For notes aimed at developers working on this repo or just trying to understand it go [here](/docs/dev-notes/README.md). It will show you how to build and run the solution among other things.
