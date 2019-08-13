namespace WebApplication.Core.Mvp.Default
{
    public interface IDefaultView
        : IValidatedView
    {
        DefaultViewModel Model { set; }
    }
}