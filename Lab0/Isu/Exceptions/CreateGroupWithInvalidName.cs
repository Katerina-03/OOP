using System;

namespace Isu.Exceptions;

public class CreateGroupWithInvalidName : Exception
{
    public CreateGroupWithInvalidName(string name)
   : base($"Invalid name ({name})")
    { }
}