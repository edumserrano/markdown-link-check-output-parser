namespace MarkdownLinkCheckLogParserCli.GitHub;

// copied and adapted to Memory<char> from https://www.meziantou.net/split-a-string-into-lines-without-allocation.htm
internal static class MemoryExtensions
{
    public static LineSplitEnumerator SplitLines(this Memory<char> memory)
    {
        return new LineSplitEnumerator(memory);
    }

    public sealed class LineSplitEnumerator
    {
        private ReadOnlyMemory<char> _str;

        public LineSplitEnumerator(ReadOnlyMemory<char> str)
        {
            _str = str;
            Current = default;
        }

        // Needed to be compatible with the foreach operator
        public LineSplitEnumerator GetEnumerator() => this;

        public LineSplitEntry Current { get; private set; }

        public bool MoveNext()
        {
            var memory = _str;
            if (memory.Length == 0) // Reach the end of the string
            {
                return false;
            }

            var index = memory.Span.IndexOfAny('\r', '\n');
            if (index == -1) // The string is composed of only one line
            {
                _str = Memory<char>.Empty; // The remaining string is an empty string
                Current = new LineSplitEntry(memory, ReadOnlyMemory<char>.Empty);
                return true;
            }

            if (index < memory.Length - 1 && memory.Span[index] == '\r')
            {
                // Try to consume the '\n' associated to the '\r'
                var next = memory.Span[index + 1];
                if (next == '\n')
                {
                    Current = new LineSplitEntry(memory.Slice(0, index), memory.Slice(index, 2));
                    _str = memory.Slice(index + 2);
                    return true;
                }
            }

            Current = new LineSplitEntry(memory.Slice(0, index), memory.Slice(index, 1));
            _str = memory.Slice(index + 1);
            return true;
        }
    }

    [StructLayout(LayoutKind.Auto)]
    public readonly struct LineSplitEntry
    {
        public LineSplitEntry(ReadOnlyMemory<char> line, ReadOnlyMemory<char> separator)
        {
            Line = line;
            Separator = separator;
        }

        public ReadOnlyMemory<char> Line { get; }

        public ReadOnlyMemory<char> Separator { get; }

        // This method allow to deconstruct the type, so you can write any of the following code
        // foreach (var entry in str.SplitLines()) { _ = entry.Line; }
        // foreach (var (line, endOfLine) in str.SplitLines()) { _ = line; }
        // https://docs.microsoft.com/en-us/dotnet/csharp/deconstruct?WT.mc_id=DT-MVP-5003978#deconstructing-user-defined-types
        public void Deconstruct(out ReadOnlyMemory<char> line, out ReadOnlyMemory<char> separator)
        {
            line = Line;
            separator = Separator;
        }

        // This method allow to implicitly cast the type into a ReadOnlySpan<char>, so you can write the following code
        // foreach (ReadOnlySpan<char> entry in str.SplitLines())
        public static implicit operator ReadOnlyMemory<char>(LineSplitEntry entry) => entry.Line;
    }
}
