using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace DevTest
{
    public class FileStore<T> : IStore<Entity> where T: Entity
    {
        private const string STORE_PATH = @"C:\";
        private IList<T> _list;
        private readonly string _fullPath;

        public FileStore()
        {
            _list = new List<T>();
            _fullPath = STORE_PATH + typeof(T).Name + ".json";
             Load();
        }
    
        public void Save(Entity instance)
        {
           
            if (instance.Number == 0)
            {
                instance.Number = GetNewNumber();
                var clone = instance.Clone();
                _list.Add(clone as T);
            }
            else
            {
                
                Entity entity = FindById(instance.Id);
                if (entity != null)
                {
                    Update(entity, instance);
                }
                else
                {
                    var clone = instance.Clone();
                    _list.Add(clone as T);
                }
            }
            Save();
        }

        public void Delete(Entity instance)
        {
            Entity target = FindById(instance.Id);
            _list.Remove(target as T);
            instance.Number = 0;
            Save();
        }

        public Entity FindById(string id)
        {
           return _list.FirstOrDefault(r => r.Id == id);
        }

        private void Save()
        {
            using (StreamWriter sw = File.CreateText(_fullPath))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(sw, _list);
            }
        }
        private void Load()
        {
            if (!File.Exists(_fullPath))
            {
                _list = new List<T>();
                return;
            }
            using (StreamReader sr = new StreamReader(_fullPath))
            {
                string json = sr.ReadToEnd();
                _list = JsonConvert.DeserializeObject<IList<T>>(json);
            }
        }

        private void Update(Entity dest, Entity source)
        {
            source.CopyProperties(dest);
        }
        public long GetNewNumber()
        {
            long maxId = 0;
            if (_list.Any())
            {
                maxId = _list.Max(r => r.Number);
            }
            return maxId + 1;
        }
        
    }
}
