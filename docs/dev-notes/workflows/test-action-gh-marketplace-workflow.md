# test-action-gh-marketplace workflow

[![Test GitHub action from GH Marketplace](https://github.com/edumserrano/markdown-link-check-log-parser/actions/workflows/test-action-gh-marketplace.yml/badge.svg)](https://github.com/edumserrano/markdown-link-check-log-parser/actions/workflows/test-action-gh-marketplace.yml)

[This workflow](/.github/workflows/test-action-gh-marketplace.yml):

- Is a copy of the [test-action workflow](/.github/workflows/test-action.yml). See the [documentation for that workflow](/docs/dev-notes/workflows/test-action-workflow.md) for more info.
- The main difference from this workflow and the `test-action workflow` are:
  - Tests the GitHub action from the Marketplace instead of building it from this repo. It makes sure that the published version is working.
  - Does not update a PR status because this is not a workflow that should add a status checks to PRs.

## Secrets

This workflow uses a custom secret `TEST_GITHUB_ACTION_GH_TOKEN`. This secret contains a GitHub token with permissions to access public repositories and has no expiration date. This token is required to run the app on this `test-action` workflow.

> **Note**
>
> Before the `GITHUB_TOKEN` from the workflow run was enough, there was no need for a custom token. However, around the time of commit 42bd774ebbf09450a4e7a2dcad33cc17995aa5af, the app started failing due to a `500` when downloading workflow run logs using the `GITHUB_TOKEN`.
>
> Couldn't find any information as to what might have changed in GitHub to cause this so I decided to fix the problem using a custom token. However, in the future, I might try again using the `GITHUB_TOKEN` to see if the problem has been remediated.
