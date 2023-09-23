using System;

namespace Isu.Entities;

public class Student
{
    public Student(string name, int id)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Id = id;
    }

    public string Name { get; }
    public int Id { get; }
}