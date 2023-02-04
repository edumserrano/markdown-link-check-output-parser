# test-action-gh-marketplace workflow

[![Test GitHub action from GH Marketplace](https://github.com/edumserrano/markdown-link-check-log-parser/actions/workflows/test-action-gh-marketplace.yml/badge.svg)](https://github.com/edumserrano/markdown-link-check-log-parser/actions/workflows/test-action-gh-marketplace.yml)

[This workflow](/.github/workflows/test-action-gh-marketplace.yml):

- Is a copy of the [test-action workflow](/.github/workflows/test-action.yml). See the [documentation for that workflow](/docs/dev-notes/workflows/test-action-workflow.md) for more info.
- The main difference from this workflow and the `test-action workflow` are:
  - Tests the GitHub action from the Marketplace instead of building it from this repo. It makes sure that the published version is working.
  - Does not update a PR status because this is not a workflow that should add a status checks to PRs.
