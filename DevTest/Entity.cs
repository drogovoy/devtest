using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace DevTest
{
    public abstract class Entity
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
        public void Save()
        {

            IStore<Entity> store = GetStore();

            PropertyInfo[] props = this.GetType().GetProperties();
            IEnumerable<PropertyInfo> affectedProps = props.Where(p => p.PropertyType == typeof(Entity) || MatchDerived(p));
            foreach (var prop in affectedProps)
            {
                var value = prop.GetValue(this) as Entity;
                if (value.Number == 0)
                {
                    value.Number = store.GetNewNumber();
                }
            }
            
            store.Save(this);
        }

        private bool MatchDerived(PropertyInfo propertyInfo)
        {
            var derivedTypes = FileStorage.GetEntitySubclassTypes();
            var target = derivedTypes.FirstOrDefault(t => t.FullName == propertyInfo.PropertyType.FullName);
            return target != null;
        }

        public void Delete()
        {
            IStore<Entity> store = GetStore();
            store.Delete(this);
        }
       
        protected static Entity Find(Type type, string id)
        {
            IStore<Entity> store = GetStore(type);
            return store.FindById(id);
        }
        public Entity Clone()
        {
            return this.MemberwiseClone() as Entity;
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
        public override bool Equals(object obj)
        {
            // Check for null values and compare run-time types.
            if (obj == null || GetType() != obj.GetType())
                return false;

            Entity entity = (Entity)obj;
            string json = JsonConvert.SerializeObject(this);
            string json2 = JsonConvert.SerializeObject(entity);
            return string.Compare(json, json2, StringComparison.InvariantCulture) == 0;
        }
        public override int GetHashCode()
        {
            //return this.Foo.GetHashCode() * 17 + this.Bar.GetHashCode();
            return GetType().GetHashCode();
        }
    }
}
