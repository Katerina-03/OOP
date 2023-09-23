using System;
using System.Collections.Generic;
using System.Linq;
using Isu.Entities;
using Isu.Exceptions;
using Isu.Models;

namespace Isu.Services;

public class IsuService : IIsuService
{
    private Dictionary<string, Group> _groups = new ();
    private int _lastId;

    public Group AddGroup(GroupName name)
    {
        if (_groups.ContainsKey(name.Name))
            throw new FoundGroupException($"This group already exist");
        Group group = new Group(name);
        _groups[name.Name] = group;
        return group;
    }

    public Student AddStudent(Group group, string name)
    {
        Student student = new Student(name, _lastId++);
        group.AddStudent(student);
        return student;
    }

    public Student GetStudent(int id)
    {
        return FindStudent(id) ?? throw new FoundStudentException(id);
    }

    public Student FindStudent(int id)
    {
        return _groups.Values
            .SelectMany(group => group.Students)
            .SingleOrDefault(student => student.Id == id);
    }

    public IReadOnlyCollection<Student> FindStudents(GroupName groupName)
    {
        return FindGroup(groupName)?.Students ?? new List<Student>();
    }

    public IReadOnlyCollection<Student> FindStudents(CourseNumber courseNumber)
    {
        return FindGroups(courseNumber).SelectMany(g => g.Students).ToList();
    }

    public Group FindGroup(GroupName groupName)
    {
        return _groups.Values.SingleOrDefault(group => group.Name == groupName);
    }

    public IReadOnlyCollection<Group> FindGroups(CourseNumber courseNumber)
    {
        return _groups.Where(group => group.Value.Name.CourseNumber == courseNumber)
            .Select(groups => groups.Value)
            .ToList();
    }

    public void ChangeStudentGroup(Student student, Group newGroup)
    {
        ArgumentNullException.ThrowIfNull(student);
        ArgumentNullException.ThrowIfNull(newGroup);
        GetStudent(student.Id);
        if (FindGroup(newGroup.Name) is null) throw new FoundGroupException(newGroup.Name.ToString());

        Group group = _groups.Values
            .SingleOrDefault(group => group.Students.Contains(student));

        if (group is null)
            throw new FoundStudentException(student.Id);

        newGroup.AddStudent(student);
        group.RemoveStudent(student.Id);
    }
}