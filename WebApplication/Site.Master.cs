using System;
using System.Web.UI;
using WebApplication.Core;
using WebApplication.Core.Mvp.SiteMaster;

namespace WebApplication
{
    public partial class SiteMaster 
        : MasterPage
        , ISiteMasterView
    {
        private readonly SiteMasterPresenter _presenter = DependencyResolver.Current.Resolve<SiteMasterPresenter>();

        protected void Page_Load(object sender, EventArgs e)
        {
            _presenter.Init(this);
        }

        public SiteMasterModel Model { protected get; set; }
    }
}