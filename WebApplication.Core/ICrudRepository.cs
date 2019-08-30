namespace WebApplication.Core
{
    public interface ICrudRepository<T>
    {
        T Get(int id);
        T Insert(T item);
        T Update(T item);
        void Delete(T item);
    }
}