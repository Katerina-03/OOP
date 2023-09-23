namespace Isu.Extra.Exceptions;

public class UncompabilityLessonInDay : Exception
{
    public UncompabilityLessonInDay(Lesson lesson, DateOnly date)
        : base($"Can't add lesson ({lesson.LessonName})")
    {
    }
}