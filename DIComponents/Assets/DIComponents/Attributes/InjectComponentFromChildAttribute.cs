using System;

namespace DIComponents
{
    public class InjectComponentFromChildAttribute : Attribute
    {
        public string ChildName { get; private set; }

        public InjectComponentFromChildAttribute(string childName)
        {
            ChildName = childName;
        }
    }
}