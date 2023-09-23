using Isu.Entities;

namespace Isu.Extra.Exceptions;

public class FacultyException : Exception
{
    public FacultyException(Student student)
        : base($"Can't add student to ognp({student})")
    {
    }
}