using System;

namespace Isu.Extra.Exceptions;

public class UncompabilityLesson : Exception
{
    public UncompabilityLesson(string lessonName)
        : base($"Can't add lesson ({lessonName})")
    {
    }
}