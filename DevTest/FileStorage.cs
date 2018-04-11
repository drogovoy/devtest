using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DevTest
{
    public static class FileStorage
    {
        private static readonly Dictionary<Type, IStore<Entity>> _storage = new Dictionary<Type, IStore<Entity>>();
        static FileStorage()
        {
            IEnumerable<Type> types = GetEntitySubclassTypes();

            foreach (var type in types)
            {
                Type classType = typeof(FileStore<>);
                Type[] typeParams = new Type[] { type };
                Type constructedType = classType.MakeGenericType(typeParams);

                var fileStore = Activator.CreateInstance(constructedType) as IStore<Entity>;
                _storage.Add(type, fileStore);

            }
        }
      

        internal static IEnumerable<Type> GetEntitySubclassTypes()
        {
          
            IEnumerable<Type> typesOfIStore = from x in Assembly.GetAssembly(typeof(Entity)).GetTypes()
                                              where x.IsClass && !x.IsAbstract 
                                              && x.IsSubclassOf(typeof(Entity))
                                              select x;
            return typesOfIStore;
        }

        public static IStore<Entity> GetByType(Type type)
        {
            return _storage[type];
        }
    }
}
