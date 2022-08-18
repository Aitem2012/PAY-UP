namespace PAY_UP.Application.Abstracts.Repositories
{
    public interface IBaseRepository<T>
    {
        public Task<T> CreateAsync(T entity, CancellationToken cancellationToken);
        public Task<T> UpdateAsync(T entity, CancellationToken cancellationToken);
        public Task<IEnumerable<T>> GetAllAsync();
    }
}
