using System;

namespace Flamingo.Fishes.Advanced.Attributes
{
    /// <summary>
    /// If this chat member updated is MyChatMember
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class IsMyChatMemberAttribute : Attribute 
    { }
}
