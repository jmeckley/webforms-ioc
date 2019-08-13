namespace WebApplication.Core.Mvp.SiteMaster
{
    public class SiteMasterPresenter
    {
        private readonly SiteSettings _settings;

        public SiteMasterPresenter(SiteSettings settings)
        {
            _settings = settings;
        }

        public void Init(ISiteMasterView view)
        {
            view.Model = new SiteMasterModel
            {
                Title = _settings.Title,
                Year = _settings.Year
            };
        }
    }
}