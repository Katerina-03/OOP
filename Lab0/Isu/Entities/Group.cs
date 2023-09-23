using System;
using System.Collections.Generic;
using Isu.Exceptions;
using Isu.Models;

namespace Isu.Entities;

public class Group
{
    public const int MaxStudent = 21;
    private Dictionary<int, Student> _students;

    public Group(GroupName name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        _students = new Dictionary<int, Student>();
    }

    public GroupName Name { get; }

    public IReadOnlyCollection<Student> Students => _students.Values;

    public void RemoveStudent(int id)
    {
        if (!_students.ContainsKey(id))
        {
            throw new FoundStudentException(id);
        }

        _students.Remove(id);
    }

    public void AddStudent(Student student)
    {
        if (_students.ContainsValue(student))
            throw new StudentException(student.Id);

        if (_students.Count >= MaxStudent)
            throw new LimitStudentException();
        _students[student.Id] = student;
    }
}