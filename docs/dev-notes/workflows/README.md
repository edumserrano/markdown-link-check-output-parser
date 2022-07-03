# GitHub worflows

There are two workflows setup on this repo:

<!-- the &nbsp; is a trick to expand the width of the table column. You add as many &nbsp; as required to get the width you want. -->
| Worflow &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; | Status and link                                                                                                                                                                                                                                                                                   | Description                                                                    |
| --------------------------------------------------------------------------------------------------------- | :------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ | :----------------------------------------------------------------------------- |
| [build-and-test](/.github/workflows/build-test.yml)                                                       | [![Build and test](https://github.com/edumserrano/markdown-link-check-log-parser/actions/workflows/build-test.yml/badge.svg)](https://github.com/edumserrano/markdown-link-check-log-parser/actions/workflows/build-test.yml)                                                                     | Builds the solution and runs tests                                             |
| [test-action](/.github/workflows/test-action.yml)                                                         | [![Test GitHub action](https://github.com/edumserrano/markdown-link-check-log-parser/actions/workflows/test-action.yml/badge.svg)](https://github.com/edumserrano/markdown-link-check-log-parser/actions/workflows/test-action.yml)                                                               | Builds and tests the GitHub action                                             |
| [Markdown Link Check with errors](/.github/workflows/markdown-link-check-with-errors.yml)                 | [![Markdown Link Check with errors](https://github.com/edumserrano/markdown-link-check-log-parser/actions/workflows/markdown-link-check-with-errors.yml/badge.svg)](https://github.com/edumserrano/markdown-link-check-log-parser/actions/workflows/markdown-link-check-with-errors.yml)          | Used to trigger the GitHub action for a Markdown Link Check log with errors    |
| [Markdown Link Check without errors](/.github/workflows/markdown-link-check-without-errors.yml)           | [![Markdown Link Check without errors](https://github.com/edumserrano/markdown-link-check-log-parser/actions/workflows/markdown-link-check-without-errors.yml/badge.svg)](https://github.com/edumserrano/markdown-link-check-log-parser/actions/workflows/markdown-link-check-without-errors.yml) | Used to trigger the GitHub action for a Markdown Link Check log without errors |

## Workflows' documentation

- [build-and-test](/docs/dev-notes/workflows/build-and-test-workflow.md)
- [test-actions](/docs/dev-notes/workflows/test-action-workflow.md)

## Note about status badges

I couldn't create a status badge by executing the steps documented in [Adding a workflow status badge](https://docs.github.com/en/actions/monitoring-and-troubleshooting-workflows/adding-a-workflow-status-badge). Following thedocumentation was giving me a status badge which would always say `no status` isntead of the pass/fail status.

There are some articles online explaining that this happens when the workflow has a `name` defined. Although these articles are old and the current GitHub documentation does not mention this workaround, formatting the `svg` link for the status badge as follows successfully produced the status badge:

- https://github.com/{repo}/workflows/{workflow-name-URI-encoded}/badge.svg

For instance, for this repo and for the [build-and-test](/.github/workflows/build-test.yml) workflow the `svg` link is:

- https://github.com/edumserrano/github-issue-forms-parser/workflows/Build%20and%20test/badge.svg
