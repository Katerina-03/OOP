using System;
using Isu.Extra.Exceptions;

namespace Isu.Extra;

public class Lesson
{
    public Lesson(string lessonName, TimeOnly startTime, TimeOnly endTime)
    {
        ArgumentNullException.ThrowIfNull(startTime);
        ArgumentNullException.ThrowIfNull(endTime);
        if (string.IsNullOrEmpty(lessonName))
            throw new ArgumentNullException(nameof(lessonName));
        if (endTime < startTime)
            throw new UncompabilityLesson(lessonName);

        LessonName = lessonName;
        StartTime = startTime;
        EndTime = endTime;
    }

    public TimeOnly StartTime { get; }
    public TimeOnly EndTime { get; }

    public string LessonName { get; }

    public bool CheckCompability(Lesson lesson)
    {
        return EndTime < lesson.StartTime || StartTime > lesson.EndTime;
    }
}