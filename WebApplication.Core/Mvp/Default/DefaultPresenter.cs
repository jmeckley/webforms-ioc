namespace WebApplication.Core.Mvp.Default
{
    public class DefaultPresenter
    {
        private readonly IProjection<DefaultViewModel> _projection;
        private readonly IValidator _validator;
        private readonly IService _service;
        private IDefaultView _view;

        public DefaultPresenter(IProjection<DefaultViewModel> projection, IValidator validator, IService service)
        {
            _projection = projection;
            _validator = validator;
            _service = service;
        }

        public void Init(IDefaultView view)
        {
            _view = view;
            view.Model = _projection.GetData();
        }

        public void Execute(Input input)
        {
            if(_validator.Validate(input, out var results))
            {
                _service.Execute(input);
            }

            _view.SetValidationErrors(results);
        }
    }
}