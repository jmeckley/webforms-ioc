namespace WebApplication.Core.Mvp.MyEntity
{
    public interface IMyEntityView
        : IValidatedView
    {
        int? EntityId { get; }
        Model.MyEntity Model { get; set; }
    }
}