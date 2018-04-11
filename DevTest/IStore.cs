namespace DevTest
{
    public interface IStore<T> where T : Entity
    {
        void Save(T instance);
        void Delete(T instance);

        T FindById(string id);
        
    }
}
