using System;
using Isu.Exceptions;

namespace Isu.Models;

public class GroupName
{
    private int _specialization;

    public GroupName(string name)
    {
        Predix = char.Parse(name[0].ToString());
        if (name.Length > 6 || name.Length < 5)
            throw new CreateGroupWithInvalidName(name);
        CourseNumber = new CourseNumber(int.Parse(name[1].ToString()), int.Parse(name[2].ToString()));
        GroupNumber = name[3].ToString() + name[4].ToString();
        if (name.Length == 6)
            Specialization = int.Parse(name[5].ToString());
        Name = name;
    }

    public string Name { get; }

    public CourseNumber CourseNumber { get; }
    public string GroupNumber { get; }
    public char Predix { get; }

    public int Specialization
    {
        get
        {
            return _specialization;
        }
        private set
        {
            if (value > 2 || value < 1)
                throw new ArgumentException($"No this specialization");
            _specialization = value;
        }
    }

    public override string ToString() => Name;
}