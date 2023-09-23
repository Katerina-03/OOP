using System;

namespace Isu.Exceptions;

public class FoundGroupException : Exception
{
    public FoundGroupException(string name)
        : base($"Not found group with this name ({name})")
    { }
}