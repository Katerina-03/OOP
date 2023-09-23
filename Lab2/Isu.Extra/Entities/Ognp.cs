using Isu.Entities;

namespace Isu.Extra;

public class Ognp
{
    private readonly char _prefix;

    public Ognp(char prefix, Stream stream1)
    {
        _prefix = prefix;
        Stream = stream1;
    }

    public char Prefix => _prefix;
    public Stream Stream { get; }
}