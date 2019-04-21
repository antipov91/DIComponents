using System;

namespace DIComponents
{
    public class InjectComponentFromChildAttribute : Attribute
    {
        public string childName;

        public InjectComponentFromChildAttribute(string childName)
        {
            this.childName = childName;
        }
    }
}