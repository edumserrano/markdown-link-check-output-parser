using System.Net;
using BenchmarkDotNet.Attributes;
using CliFx.Infrastructure;
using MarkdownLinkCheckLogParserCli.CliCommands;

namespace MarkdownLinkCheckLogParserCli.Benchmarks;

[MemoryDiagnoser]
public class ParsingBenchmark
{
    [Benchmark]
    public async Task Parse()
    {
        var handler = new InMemoryGitHubWorkflowRunHandler();
        var httpClient = new HttpClient(handler);
        var command = new ParseLogCommand(httpClient)
        {
            AuthToken = "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",
            Repo = "edumserrano/dotnet-sdk-extensions",
            RunId = "2481867680",
            JobName = "Markdown link check",
            StepName = "Markdown link check",
        };
        var console = new FakeInMemoryConsole();
        await command.ExecuteAsync(console);
    } 
}

public class InMemoryGitHubWorkflowRunHandler : DelegatingHandler
{
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var stream = File.OpenRead("D:/logs.zip");
        var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StreamContent(stream),
        };
        return Task.FromResult(httpResponseMessage);
    }
}
