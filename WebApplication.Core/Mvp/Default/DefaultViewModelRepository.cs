namespace WebApplication.Core.Mvp.Default
{
    public class DefaultViewModelRepository 
        : IProjection<DefaultViewModel>
    {
        public DefaultViewModel GetData()
        {
            //get data from database,
            return new DefaultViewModel
            {
                Message = "Welcome Jason"
            };
        }
    }
}