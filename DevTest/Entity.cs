using System;
using System.Collections.Generic;

namespace DevTest
{
    public abstract class Entity : ICloneable
    {
     
        protected Entity(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                long numId;
                long.TryParse(id, out numId);
                Number = numId;
            }
        }
        public string Id =>  (Number > 0) ? Number.ToString() : null;
        public long Number { get; set; }
        public long numericId { get; set; }
        //private IStore store = new FileStore();
        public void Save()
        {
            IStore<Entity> store = GetStore();
            store.Save(this);
        }
        public void Delete()
        {
            IStore<Entity> store = GetStore();
            store.Delete(this);
            Save();
            
        }
       
        protected static Entity Find(Type type, string id)
        {
            IStore<Entity> store = GetStore(type);
            return store.FindById(id);
        }
        public object Clone()
        {
            return this.MemberwiseClone();
        }

        private  IStore<Entity> GetStore()
        {
            Type t = this.GetType();
            return GetStore(t);
        }
        private static IStore<Entity> GetStore(Type type)
        {
            return FileStorage.GetByType(type);
        }

    }
}
