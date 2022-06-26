namespace MarkdownLinkCheckLogParserCli.Tests.CliIntegration;

/// <summary>
/// These tests make sure that the CLI interface is as expected.
/// IE: if the command name changes or the options change then these tests would pick that up.
/// </summary>
[Trait("Category", XUnitCategories.Integration)]
public class CliIntegrationTests
{
    /// <summary>
    /// Tests that if no arguments are passed the CLI returns the help text.
    /// </summary>
    [Fact]
    public async Task NoArguments()
    {
        using var console = new FakeInMemoryConsole();
        var app = new MlcLogParserCli();
        app.CliApplicationBuilder.UseConsole(console);
        await app.RunAsync();
        var output = console.ReadOutputString();
        var expectedOutput = OsDependantOutput.ReadAllText("./TestFiles/cli-output-no-args");
        output.ShouldEndWith(expectedOutput);
    }

    /// <summary>
    /// Tests that the --auth-token option is required for the 'parse-log' command.
    /// </summary>
    [Fact]
    public async Task AuthTokenOptionIsRequired()
    {
        using var console = new FakeInMemoryConsole();
        var app = new MlcLogParserCli();
        app.CliApplicationBuilder.UseConsole(console);

        var args = new[]
        {
            "parse-log",
            "--repo", "some repo",
            "--run-id", "some run id",
            "--job-name", "some job name",
            "--step-name", "some step name",
        };
        await app.RunAsync(args);
        var output = console.ReadOutputString();
        var expectedOutput = OsDependantOutput.ReadAllText("./TestFiles/cli-output-usage");
        output.ShouldEndWith(expectedOutput);
    }

    /// <summary>
    /// Tests that the --repo option is required for the 'parse-log' command.
    /// </summary>
    [Fact]
    public async Task RepoOptionIsRequired()
    {
        using var console = new FakeInMemoryConsole();
        var app = new MlcLogParserCli();
        app.CliApplicationBuilder.UseConsole(console);

        var args = new[]
        {
            "parse-log",
            "--auth-token", "some token",
            "--run-id", "some run id",
            "--job-name", "some job name",
            "--step-name", "some step name",
        };
        await app.RunAsync(args);
        var output = console.ReadOutputString();
        var expectedOutput = OsDependantOutput.ReadAllText("./TestFiles/cli-output-usage");
        output.ShouldEndWith(expectedOutput);
    }

    /// <summary>
    /// Tests that the --run-id option is required for the 'parse-log' command.
    /// </summary>
    [Fact]
    public async Task RunIdOptionIsRequired()
    {
        using var console = new FakeInMemoryConsole();
        var app = new MlcLogParserCli();
        app.CliApplicationBuilder.UseConsole(console);

        var args = new[]
        {
            "parse-log",
            "--auth-token", "some token",
            "--repo", "some repo",
            "--job-name", "some job name",
            "--step-name", "some step name",
        };
        await app.RunAsync(args);
        var output = console.ReadOutputString();
        var expectedOutput = OsDependantOutput.ReadAllText("./TestFiles/cli-output-usage");
        output.ShouldEndWith(expectedOutput);
    }

    /// <summary>
    /// Tests that the --job-name option is required for the 'parse-log' command.
    /// </summary>
    [Fact]
    public async Task JobNameOptionIsRequired()
    {
        using var console = new FakeInMemoryConsole();
        var app = new MlcLogParserCli();
        app.CliApplicationBuilder.UseConsole(console);

        var args = new[]
        {
            "parse-log",
            "--auth-token", "some token",
            "--repo", "some repo",
            "--run-id", "some run id",
            "--step-name", "some step name",
        };
        await app.RunAsync(args);
        var output = console.ReadOutputString();
        var expectedOutput = OsDependantOutput.ReadAllText("./TestFiles/cli-output-usage");
        output.ShouldEndWith(expectedOutput);
    }

    /// <summary>
    /// Tests that the --step-name option is required for the 'parse-log' command.
    /// </summary>
    [Fact]
    public async Task StepNameOptionIsRequired()
    {
        using var console = new FakeInMemoryConsole();
        var app = new MlcLogParserCli();
        app.CliApplicationBuilder.UseConsole(console);

        var args = new[]
        {
            "parse-log",
            "--auth-token", "some token",
            "--repo", "some repo",
            "--run-id", "some run id",
            "--job-name", "some job name",
        };
        await app.RunAsync(args);
        var output = console.ReadOutputString();
        var expectedOutput = OsDependantOutput.ReadAllText("./TestFiles/cli-output-usage");
        output.ShouldEndWith(expectedOutput);
    }

    /// <summary>
    /// Tests the validation of the --auth-token option for the 'parse-log' command.
    /// </summary>
    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public async Task AuthTokenOptionValidation(string authToken)
    {
        using var console = new FakeInMemoryConsole();
        var app = new MlcLogParserCli();
        app.CliApplicationBuilder.UseConsole(console);

        var args = new[]
        {
            "parse-log",
            "--auth-token", authToken,
            "--repo", "some repo",
            "--run-id", "some run id",
            "--job-name", "some job name",
            "--step-name", "some step name",
        };
        await app.RunAsync(args);
        var error = console.ReadErrorString();
        var expectedError = NormalizedLineEndingsFileReader.ReadAllText("./TestFiles/cli-output-error-auth-token-validation.txt");
        error.ShouldBe(error);
    }

    /// <summary>
    /// Tests the validation of the --repo option for the 'parse-log' command.
    /// </summary>
    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public async Task RepoOptionValidation(string repo)
    {
        using var console = new FakeInMemoryConsole();
        var app = new MlcLogParserCli();
        app.CliApplicationBuilder.UseConsole(console);

        var args = new[]
        {
            "parse-log",
            "--auth-token", "some token",
            "--repo", repo,
            "--run-id", "some run id",
            "--job-name", "some job name",
            "--step-name", "some step name",
        };
        await app.RunAsync(args);
        var error = console.ReadErrorString();
        var expectedError = NormalizedLineEndingsFileReader.ReadAllText("./TestFiles/cli-output-error-repo-validation.txt");
        error.ShouldBe(expectedError);
    }

    /// <summary>
    /// Tests the validation of the --run-id option for the 'parse-log' command.
    /// </summary>
    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public async Task RunIdOptionValidation(string runId)
    {
        using var console = new FakeInMemoryConsole();
        var app = new MlcLogParserCli();
        app.CliApplicationBuilder.UseConsole(console);

        var args = new[]
        {
            "parse-log",
            "--auth-token", "some token",
            "--repo", "some repo",
            "--run-id", runId,
            "--job-name", "some job name",
            "--step-name", "some step name",
        };
        await app.RunAsync(args);
        var error = console.ReadErrorString();
        var expectedError = NormalizedLineEndingsFileReader.ReadAllText("./TestFiles/cli-output-error-run-id-validation.txt");
        error.ShouldBe(expectedError);
    }

    /// <summary>
    /// Tests the validation of the --job-name option for the 'parse-log' command.
    /// </summary>
    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public async Task JobNameOptionValidation(string jobName)
    {
        using var console = new FakeInMemoryConsole();
        var app = new MlcLogParserCli();
        app.CliApplicationBuilder.UseConsole(console);

        var args = new[]
        {
            "parse-log",
            "--auth-token", "some token",
            "--repo", "some repo",
            "--run-id", "some run id",
            "--job-name", jobName,
            "--step-name", "some step name",
        };
        await app.RunAsync(args);
        var error = console.ReadErrorString();
        var expectedError = NormalizedLineEndingsFileReader.ReadAllText("./TestFiles/cli-output-error-job-name-validation.txt");
        error.ShouldBe(expectedError);
    }

    /// <summary>
    /// Tests the validation of the --step-name option for the 'parse-log' command.
    /// </summary>
    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public async Task StepNameOptionValidation(string stepName)
    {
        using var console = new FakeInMemoryConsole();
        var app = new MlcLogParserCli();
        app.CliApplicationBuilder.UseConsole(console);

        var args = new[]
        {
            "parse-log",
            "--auth-token", "some token",
            "--repo", "some repo",
            "--run-id", "some run id",
            "--job-name", "some job name",
            "--step-name", stepName,
        };
        await app.RunAsync(args);
        var error = console.ReadErrorString();
        var expectedError = NormalizedLineEndingsFileReader.ReadAllText("./TestFiles/cli-output-error-step-name-validation.txt");
        error.ShouldBe(expectedError);
    }
}
