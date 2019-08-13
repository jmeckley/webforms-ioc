using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.UI;
using WebApplication.Core;
using WebApplication.Core.Mvp.Default;

namespace WebApplication
{
    public partial class _Default
        : Page
            , IDefaultView
    {
        private readonly DefaultPresenter _presenter = DependencyResolver.Current.Resolve<DefaultPresenter>();

        protected void Page_Load(object sender, EventArgs e) => _presenter.Init(this);

        public DefaultViewModel Model { protected get; set; }

        public void SetValidationErrors(IEnumerable<ValidationResult> results) => Validators.AddValidationErrors(results);

        protected void UploadFile(object sender, EventArgs e) => _presenter.Execute(new Input
        {
            FileName = FileUpload1.FileName,
            Content = FileUpload1.PostedFile.InputStream
        });
    }
}