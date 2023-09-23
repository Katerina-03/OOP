using System;

namespace Isu.Models;

public class CourseNumber
{
    private int _course;
    private int _type;

    public CourseNumber(int type, int course)
    {
        Course = course;
        Type = type;
    }

    public int Course
    {
        get
        {
            return _course;
        }
        private set
        {
            if (value > 5)
                throw new ArgumentException("Invalid number for course");
            _course = value;
        }
    }

    public int Type
    {
        get => _type;

        private set
        {
            if (value > 4 || value < 3)
                throw new ArgumentException("This is not our type");
            _type = value;
        }
    }
}