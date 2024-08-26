using SB.Domain.Entities;
using SB.Domain.Interfaces;

namespace SB.Application.Services
{
    public class GovernmentEntityService
    {
        private readonly IGovernmentEntityRepository _repository;

        public GovernmentEntityService(IGovernmentEntityRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<GovernmentEntity>> GetAllAsync() => _repository.GetAllAsync();
        public Task<GovernmentEntity?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);
        public Task AddAsync(GovernmentEntity entity) => _repository.AddAsync(entity);
        public Task UpdateAsync(GovernmentEntity entity) => _repository.UpdateAsync(entity);
        public Task DeleteAsync(int id) => _repository.DeleteAsync(id);
    }
}
