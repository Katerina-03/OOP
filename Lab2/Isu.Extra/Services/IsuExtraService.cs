using Isu.Entities;
using Isu.Exceptions;
using Isu.Extra.Exceptions;
using Isu.Models;
using Isu.Services;

namespace Isu.Extra.Services;

public class IsuExtraService
{
    private IsuService _isu = new ();
    private Dictionary<int, Ognp> _ognps = new ();
    private Dictionary<string, GroupExtra> _groups = new ();

    public IReadOnlyCollection<Ognp> Ognps => _ognps.Values;
    public GroupExtra AddGroup(GroupName name)
    {
        _isu.AddGroup(name);
        GroupExtra group = new GroupExtra(name);
        _groups[name.Name] = group;
        return group;
    }

    public Student AddStudent(GroupExtra group, string name)
    {
        return _isu.AddStudent(group, name);
    }

    public Student GetStudent(int id)
    {
        return _isu.GetStudent(id);
    }

    public Student FindStudent(int id)
    {
        return _isu.FindStudent(id);
    }

    public IReadOnlyCollection<Student> FindStudents(GroupName groupName)
    {
        return _isu.FindStudents(groupName);
    }

    public IReadOnlyCollection<Student> FindStudents(CourseNumber courseNumber)
    {
        return _isu.FindStudents(courseNumber);
    }

    public GroupExtra FindGroup(GroupName groupName)
    {
        Group group = _isu.FindGroup(groupName);
        if (group is null)
            return null;
        return _groups[group.Name.Name];
    }

    public void AddStudentOgnp(Ognp ognp, Student student)
    {
        ArgumentNullException.ThrowIfNull(ognp);
        ArgumentNullException.ThrowIfNull(student);
        var group = _groups.Values.FirstOrDefault(x => x.Students.Contains(student));
        if (group.Name.Predix == ognp.Prefix)
            throw new FacultyException(student);
        ognp.Stream.AddStudent(student);
    }

    public void RemoveStudent(Ognp ognp, Student student)
    {
        ArgumentNullException.ThrowIfNull(ognp);
        ArgumentNullException.ThrowIfNull(student);
        if (!_ognps.ContainsKey(student.Id))
            throw new StudentException(student.Id);
        _ognps.Remove(student.Id);
    }

    public IReadOnlyCollection<Student> GetStudentsFromOgnp(Ognp ognp)
    {
        ArgumentNullException.ThrowIfNull(ognp);
        return ognp.Stream.Students;
    }

    public IReadOnlyCollection<Student> NotInAnyOgnp()
    {
        var allStudent = _groups.Values.SelectMany(x => x.Students).ToList();
        var enlistedStudents = _ognps.Values.SelectMany(x => x.Stream.Students).ToList();

        return allStudent.Except(enlistedStudents).ToArray();
    }
}