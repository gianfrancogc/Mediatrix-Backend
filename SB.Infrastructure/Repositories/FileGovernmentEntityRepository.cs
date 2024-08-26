using SB.Domain.Entities;
using SB.Domain.Interfaces;
using System.Text.Json;

namespace SB.Infrastructure.Repositories
{
    public class FileGovernmentEntityRepository : IGovernmentEntityRepository
    {
        private readonly string _filePath = "Database/government_entities.json";

        public FileGovernmentEntityRepository()
        {
            if (!File.Exists(_filePath))
            {
                Directory.CreateDirectory("Database");
                File.WriteAllText(_filePath, "[]");
            }
        }

        public async Task<IEnumerable<GovernmentEntity>> GetAllAsync()
        {
            var jsonData = await File.ReadAllTextAsync(_filePath);
            return JsonSerializer.Deserialize<IEnumerable<GovernmentEntity>>(jsonData) ?? new List<GovernmentEntity>();
        }

        public async Task<GovernmentEntity?> GetByIdAsync(int id)
        {
            var entities = await GetAllAsync();
            return entities.FirstOrDefault(e => e.Id == id);
        }

        public async Task AddAsync(GovernmentEntity entity)
        {
            var entities = (await GetAllAsync()).ToList();
            entity.Id = entities.Any() ? entities.Max(e => e.Id) + 1 : 1;
            entities.Add(entity);
            await SaveAsync(entities);
        }

        public async Task UpdateAsync(GovernmentEntity entity)
        {
            var entities = (await GetAllAsync()).ToList();
            var existingEntity = entities.FirstOrDefault(e => e.Id == entity.Id);
            if (existingEntity == null) return;

            existingEntity.Name = entity.Name;
            existingEntity.Description = entity.Description;

            await SaveAsync(entities);
        }

        public async Task DeleteAsync(int id)
        {
            var entities = (await GetAllAsync()).ToList();
            var entityToRemove = entities.FirstOrDefault(e => e.Id == id);
            if (entityToRemove == null) return;

            entities.Remove(entityToRemove);
            await SaveAsync(entities);
        }

        private async Task SaveAsync(IEnumerable<GovernmentEntity> entities)
        {
            var jsonData = JsonSerializer.Serialize(entities);
            await File.WriteAllTextAsync(_filePath, jsonData);
        }
    }
}
