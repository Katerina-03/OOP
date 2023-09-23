using System;
using System.Collections.Generic;
using System.Linq;
using Isu.Extra;
using Isu.Extra.Exceptions;

namespace Isu.Extra;

public class Sсhedule
{
    private Dictionary<string, List<Lesson>> _sсhedules = new ();

    public void AddLesson(Lesson lesson, DateOnly date)
    {
        if (_sсhedules.ContainsKey(date.ToString()))
        {
            if (!CheckCompability(lesson, date))
                throw new UncompabilityLessonInDay(lesson, date);
        }
        else
        {
            _sсhedules[date.ToString()] = new List<Lesson>();
        }

        _sсhedules[date.ToString()].Add(lesson);
    }

    public bool CheckCompability(Lesson lesson, DateOnly date)
    {
        foreach (Lesson checkedLesson in _sсhedules[date.ToString()])
        {
            if (!checkedLesson.CheckCompability(lesson))
                return false;
        }

        return true;
    }

    public IReadOnlyCollection<Lesson> FindLessonsDay(DateOnly date)
    {
        if (!_sсhedules.ContainsKey(date.ToString()))
            return null;
        return _sсhedules[date.ToString()];
    }
}