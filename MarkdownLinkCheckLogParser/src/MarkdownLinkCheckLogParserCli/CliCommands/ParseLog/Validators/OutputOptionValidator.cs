namespace MarkdownLinkCheckLogParserCli.CliCommands.ParseLog.Validators;

#pragma warning disable CA1812 // Avoid uninstantiated internal classes. Referenced via typeof(OutputOptionValidator) usage
internal sealed class OutputOptionValidator : BindingValidator<string>
{
    public override BindingValidationError? Validate(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return new BindingValidationError("Cannot be null or whitespace.");
        }

        var outputOptions = value.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        var containsStepJson = outputOptions.Contains("step-json", StringComparer.InvariantCulture);
        var containsStepMd = outputOptions.Contains("step-md", StringComparer.InvariantCulture);
        var containsFileJson = outputOptions.Contains("file-json", StringComparer.InvariantCulture);
        var containsFileMd = outputOptions.Contains("file-md", StringComparer.InvariantCulture);
        if (containsStepJson && containsStepMd)
        {
            return new BindingValidationError("Invalid value. Cannot specify 'step-json' and 'step-md' at the same time. They are mutually exclusive.");
        }

        if (containsStepJson || containsStepMd || containsFileJson || containsFileMd)
        {
            return null;
        }

        return new BindingValidationError("Invalid value. It must be one of or a comma separated list of the following values: step-json, step-md, file-json, file-md.");
    }
}
#pragma warning restore CA1812 // Avoid uninstantiated internal classes. Referenced via typeof(OutputOptionValidator) usage
