namespace WebApplication.Core.Mvp.Default
{
    public class DefaultViewModelRepository 
        : IRepository<DefaultViewModel>
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