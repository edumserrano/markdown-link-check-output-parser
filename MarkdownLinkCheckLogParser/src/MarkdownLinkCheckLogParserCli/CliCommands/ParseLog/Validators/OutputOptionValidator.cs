namespace MarkdownLinkCheckLogParserCli.CliCommands.ParseLog.Validators;

#pragma warning disable CA1812 // Avoid uninstantiated internal classes. Referenced via typeof(OutputOptionValidator) usage
internal class OutputOptionValidator : BindingValidator<string>
{
    public override BindingValidationError? Validate(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return new BindingValidationError("Cannot be null or whitespace.");
        }

        var outputOptions = value.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        var containsStep = outputOptions.Contains("step");
        var containsJson = outputOptions.Contains("json");
        var containsMd = outputOptions.Contains("md");
        if (containsStep || containsJson || containsMd)
        {
            return null;
        }

        return new BindingValidationError("--output must contain a valid value. It must be one of or a comma separated list of values which can be: step,json,md.");
    }
}
#pragma warning restore CA1812 // Avoid uninstantiated internal classes. Referenced via typeof(OutputOptionValidator) usage
