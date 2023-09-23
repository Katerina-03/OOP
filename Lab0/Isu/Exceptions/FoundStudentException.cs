using System;

namespace Isu.Exceptions;

public class FoundStudentException : Exception
{
    public FoundStudentException(int id)
        : base($"Not found student with id ({id})")
    { }
}