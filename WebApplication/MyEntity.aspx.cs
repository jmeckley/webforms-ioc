using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.UI;
using WebApplication.Core;
using WebApplication.Core.Mvp.MyEntity;

namespace WebApplication
{
    public partial class MyEntity
        : Page
            , IMyEntityView
    {
        private readonly MyEntityPresenter _presenter = DependencyResolver.Current.Resolve<MyEntityPresenter>();

        protected void Page_Load(object sender, EventArgs e)
        {
            _presenter.Init(this);

            if (Page.IsPostBack) return;
            DataBind();
        }

        public Core.Model.MyEntity Model { get; set; }

        public int? EntityId
        {
            get
            {
                if (int.TryParse(Request.QueryString["id"], out var id)) return id;
                return null;
            }
        }

        public void SetValidationErrors(IEnumerable<ValidationResult> results) => Validators.AddValidationErrors(results);

        protected void Save(object sender, EventArgs e)
        {
            Model.Name = Name.Text;
            Model.Age = int.Parse(Age.Text);

            _presenter.Save(Model);
        }
    }
}