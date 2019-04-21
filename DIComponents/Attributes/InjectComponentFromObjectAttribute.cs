using System;

public class InjectComponentFromObjectAttribute : Attribute
{
    public string objectName;

    public InjectComponentFromObjectAttribute(string objectName)
    {
        this.objectName = objectName;
    }
}