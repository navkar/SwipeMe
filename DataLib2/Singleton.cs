using System;

namespace DataLib2
{
    public class Singleton<T> where T : class, new()
    {
        private static readonly Lazy<T> StaticInstance = new Lazy<T>(() => new T());
        public static T Instance => StaticInstance.Value;
    }
}
