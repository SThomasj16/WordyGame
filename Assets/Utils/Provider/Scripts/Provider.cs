using System;

namespace Utils.Provider.Scripts
{
    public static class Provider
    {
        private static readonly ProviderInstance Instance = new();

        public static T Get<T>(string id = null)
        {
            return Instance.Get<T>(id);
        }

        public static void Set<T>(T singletonObj, string id = null)
        {
            Instance.Set<T>(singletonObj, id);
        }

        public static T GetOrInstanciate<T>(Func<T> instanciator, string id = null)
        {
            return Instance.GetOrInstanciate<T>(instanciator, id);
        }

        public static T Instanciate<T>(Func<T> instanciator, string id)
        {
            return Instance.Instanciate<T>(instanciator, id);
        }

        public static void Flush()
        {
            Instance.Flush();
        }
    }

    public static class coso
    {
        
    }
}