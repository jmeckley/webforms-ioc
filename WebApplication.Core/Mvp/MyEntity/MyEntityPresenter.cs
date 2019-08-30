namespace WebApplication.Core.Mvp.MyEntity
{
    public class MyEntityPresenter
    {
        private readonly IValidator _validator;
        private readonly IUnitOfWork _uow;
        private readonly ICrudRepository<Model.MyEntity> _repository;
        private IMyEntityView _view;

        public MyEntityPresenter(IValidator validator, IUnitOfWork uow, ICrudRepository<Model.MyEntity> repository)
        {
            _validator = validator;
            _uow = uow;
            _repository = repository;
        }

        public void Init(IMyEntityView view)
        {
            _view = view;

            var entityId = _view.EntityId;
            view.Model = entityId.HasValue ? _repository.Get(entityId.Value) : new Model.MyEntity();
        }

        public void Save(Model.MyEntity model)
        {
            _view.Model = _uow.Execute(() =>
            {
                if (_validator.Validate(model, out var results))
                {
                    return _view.EntityId.HasValue ? _repository.Update(model) : _repository.Insert(model);
                }

                _view.SetValidationErrors(results);
                return null;
            });
        }
    }
}
