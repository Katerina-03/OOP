using System;

namespace Isu.Exceptions;

public class InvalidSpecialization : Exception
{
    public InvalidSpecialization(int name)
        : base("Invalid name")
    { }
}