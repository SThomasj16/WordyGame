using System;
using System.Collections.Generic;

namespace Features.Provider.Scripts
{
    public class ProviderInstance
    {
        private readonly Dictionary<string, object> instances = new();

        public T Get<T>(string id = null)
        {
            var type = typeof(T).FullName;
            if (id != null)
                type = id;
            object singletonObj;

            if (instances.TryGetValue(type, out singletonObj))
                return (T) singletonObj;

            return default(T);
        }

        public void Set<T>(T singletonObj, string id = null)
        {
            var type = typeof(T).FullName;
            if (id != null)
                type = id;
            instances[type] = singletonObj;
        }

        public T GetOrInstanciate<T>(Func<T> instanciator, string id = null)
        {
            var type = typeof(T).FullName;
            if (id != null)
                type = id;

            object singletonObj = Get<T>(type);
            object nullValue = default(T);

            if (singletonObj != nullValue)
                return (T) singletonObj;
            return Instanciate<T>(instanciator, type);
        }

        public T Instanciate<T>(Func<T> instanciator, string id)
        {
            var singletonObj = instanciator();

            instances[id] = singletonObj;
            return singletonObj;
        }

        public void Flush()
        {
            instances.Clear();
        }
    }
}
