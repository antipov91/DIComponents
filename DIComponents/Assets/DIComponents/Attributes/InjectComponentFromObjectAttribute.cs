using System;

public class InjectComponentFromObjectAttribute : Attribute
{
    public string ObjectName { get; private set; }

    public InjectComponentFromObjectAttribute(string objectName)
    {
        ObjectName = objectName;
    }
}