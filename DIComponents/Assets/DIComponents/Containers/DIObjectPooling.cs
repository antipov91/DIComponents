using System.Collections.Generic;

namespace DIComponents.Core
{
    public class DIObjectPooling
    {
        private Dictionary<string, object> objectPooling = new Dictionary<string, object>();

        public void AddObject(string name, object obj)
        {
            objectPooling.Add(name, obj);
        }

        public object GetObject(string name)
        {
            return objectPooling[name];
        }

        public bool Contains(string name)
        {
            return objectPooling.ContainsKey(name);
        }
    }
}
