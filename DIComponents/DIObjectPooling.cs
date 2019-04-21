using System.Collections.Generic;

namespace DIComponents
{
    public class DIObjectPooling
    {
        private Dictionary<int, object> objectPooling = new Dictionary<int, object>();

        public void AddObject(string name, object obj)
        {
            var key = name.GetHashCode();
            objectPooling.Add(key, obj);
        }

        public object GetObject(string name)
        {
            var key = name.GetHashCode();
            return objectPooling[key];
        }

        public bool Contains(string name)
        {
            var key = name.GetHashCode();
            return objectPooling.ContainsKey(key);
        }
    }
}
