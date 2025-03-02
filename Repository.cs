namespace GrpcPersonService
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly List<T> _dbSet = new();

        public async Task<IEnumerable<T>> GetAllAsync() => await Task.FromResult(_dbSet);

        public async Task<T> GetByIdAsync(int id) =>
            await Task.FromResult(_dbSet.FirstOrDefault(e => (int)e.GetType().GetProperty("Id").GetValue(e) == id));

        public async Task AddAsync(T entity)
        {
            _dbSet.Add(entity);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(T entity)
        {
            var existing = await GetByIdAsync((int)entity.GetType().GetProperty("Id").GetValue(entity));
            if (existing != null)
            {
                _dbSet.Remove(existing);
                _dbSet.Add(entity);
            }
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null) _dbSet.Remove(entity);
        }
    }
}
