using Isu.Entities;
using Isu.Exceptions;
using Isu.Extra.Exceptions;
using Isu.Extra.Services;
using Isu.Models;
using Isu.Services;
using Xunit;
namespace Isu.Extra.Test;

public class IsuExtraServiceTest
{
    private IsuExtraService _test = new IsuExtraService();

    [Fact]
    public void SetOgnp_CheckOgnpInStudent()
    {
        Lesson fLesson = new Lesson("Math", new (13, 00), new (14, 30));
        Lesson sLesson = new Lesson("Russish", new (15, 00), new (16, 30));
        GroupExtra group = _test.AddGroup(new GroupName("M32081"));
        Student student = _test.AddStudent(group, "Pasha");
        List<Lesson> lessons = new ();
        lessons.Add(fLesson);
        lessons.Add(sLesson);

        _test.AddStudentOgnp(new Ognp('T', new Stream(lessons, 2)), student);
        Assert.Empty(_test.Ognps);
    }
}