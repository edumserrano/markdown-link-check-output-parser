# test-action workflow

[![Test GitHub action](https://github.com/edumserrano/markdown-link-check-log-parser/actions/workflows/test-action.yml/badge.svg)](https://github.com/edumserrano/markdown-link-check-log-parser/actions/workflows/test-action.yml)

[This workflow](/.github/workflows/test-action.yml):

- Is triggered from a workflow that runs the `Markdown Link Check` which **doesn't have errors** in the log.
- Is triggered from a workflow that runs the `Markdown Link Check` which **has errors** in the log.
- Contains a step that executes the GitHub action provided by this repo.
- Checks that the output produced by the action is as expected.
- Checks that action should have failed the workflow.

Since this workflow executes the [Docker container action](https://docs.github.com/en/actions/creating-actions/creating-a-docker-container-action) it will build and execute the docker container so if there are any issues with the action's [Dockerfile](/Dockerfile) this workflow will detect it.
