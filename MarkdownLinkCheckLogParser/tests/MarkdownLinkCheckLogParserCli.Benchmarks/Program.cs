using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using MarkdownLinkCheckLogParserCli.Benchmarks;

BenchmarkRunner.Run<ParsingBenchmark>();
//BenchmarkRunner.Run<ParsingBenchmark>(new DebugInProcessConfig());

