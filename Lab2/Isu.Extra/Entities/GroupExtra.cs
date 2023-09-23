using System.Collections.Generic;
using Isu.Entities;
using Isu.Models;

namespace Isu.Extra;

public class GroupExtra : Group
{
    private Sсhedule _schedule;
    public GroupExtra(GroupName name)
        : base(name)
    { }

    public Sсhedule Sсhedule
    {
        get => _schedule ?? throw new ArgumentNullException(nameof(_schedule));
        set => _schedule = value;
    }
}