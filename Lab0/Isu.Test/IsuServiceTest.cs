using System;
using System.IO.Enumeration;
using Isu.Entities;
using Isu.Exceptions;
using Isu.Models;
using Isu.Services;
using Xunit;

namespace Isu.Test;

public class IsuServiceTest
{
    private IIsuService _test = new IsuService();

    [Fact]
    public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
    {
        Group group = _test.AddGroup(new GroupName("M32121"));
        Student student = _test.AddStudent(group, "Senya");

        Assert.Contains(student, group.Students);
    }

    [Fact]
    public void ReachMaxStudentPerGroup_ThrowException()
    {
        Group group = _test.AddGroup(new GroupName("M32011"));
        for (int i = 0; i < Group.MaxStudent; i++)
            _test.AddStudent(group, $"Vasia");

        Assert.Throws<LimitStudentException>(() =>
        {
            _test.AddStudent(group, "Petia");
        });
    }

    [Fact]
    public void CreateGroupWithInvalidName_ThrowException()
    {
        Assert.Throws<CreateGroupWithInvalidName>(testCode: () =>
        {
            _test.AddGroup(new GroupName("M31123232"));
        });
    }

    [Fact]
    public void TransferStudentToAnotherGroup_GroupChanged()
    {
        Group group1 = _test.AddGroup(new GroupName("M3111"));
        Group group2 = _test.AddGroup(new GroupName("M3112"));
        Student student = _test.AddStudent(group1, "Катя");
        _test.ChangeStudentGroup(student, group2);
        Assert.Contains(student, group2.Students);
    }
}