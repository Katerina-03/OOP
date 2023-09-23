using System;

namespace Isu.Exceptions;

public class LimitStudentException : Exception
{
    public LimitStudentException()
        : base("More than limit of student")
    { }
}