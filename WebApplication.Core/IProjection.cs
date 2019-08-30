namespace WebApplication.Core
{
    public interface IProjection<out T>
    {
        T GetData();
    }
}