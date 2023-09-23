using System.Collections.Generic;
using Isu.Entities;
using Isu.Exceptions;

namespace Isu.Extra;

public class Stream
{
    private readonly List<Lesson> _ognpLessons;
    private readonly List<Student> _students;
    private readonly int _maxStudent;

    public Stream(IEnumerable<Lesson> ognpLesson, int maxStudent)
    {
        if (ognpLesson == null) throw new ArgumentNullException(nameof(ognpLesson));
        _ognpLessons = ognpLesson.ToList();
        _students = new List<Student>();
        _maxStudent = maxStudent;
    }

    public IReadOnlyCollection<Lesson> OgnpLesson => _ognpLessons.AsReadOnly();

    public IReadOnlyCollection<Student> Students => _students.AsReadOnly();

    public void AddStudent(Student student)
    {
        if (_students.Count == _maxStudent)
            throw new LimitStudentException();
        if (_students.Contains(student))
            throw new StudentException(student.Id);
        _students.Add(student);
    }
}