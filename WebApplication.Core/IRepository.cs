namespace WebApplication.Core
{
    public interface IRepository<out T>
    {
        T GetData();
    }
}