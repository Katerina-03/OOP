using System;

namespace Isu.Exceptions;

public class StudentException : Exception
{
    public StudentException(int id)
        : base($"Student in group({id})")
    { }
}