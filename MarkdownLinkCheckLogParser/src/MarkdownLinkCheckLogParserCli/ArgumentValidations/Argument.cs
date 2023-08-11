namespace MarkdownLinkCheckLogParserCli.ArgumentValidations;

internal static class Argument
{
    public static int Positive(this int value, [CallerArgumentExpression(nameof(value))] string name = "")
    {
        var message = $"{name} must be a positive value.";
        return value >= 0
            ? value
            : throw new ArgumentException(message, name);
    }

    public static T NotNull<T>([NotNull] this T? value, [CallerArgumentExpression(nameof(value))] string name = "")
        where T : class
    {
        return value ?? throw new ArgumentNullException(name);
    }

    internal static string NotNullOrWhiteSpace([NotNull] this string? value, [CallerArgumentExpression(nameof(value))] string name = "")
    {
        var message = $"{name} cannot be null or whitespace.";
        return string.IsNullOrWhiteSpace(value)
            ? throw new ArgumentException(message, name)
            : value;
    }
}
