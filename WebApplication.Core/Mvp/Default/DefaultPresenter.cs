namespace WebApplication.Core.Mvp.Default
{
    public class DefaultPresenter
    {
        private readonly IRepository<DefaultViewModel> _repository;
        private readonly IValidator _validator;
        private readonly IService _service;
        private IDefaultView _view;

        public DefaultPresenter(IRepository<DefaultViewModel> repository, IValidator validator, IService service)
        {
            _repository = repository;
            _validator = validator;
            _service = service;
        }

        public void Init(IDefaultView view)
        {
            _view = view;
            view.Model = _repository.GetData();
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