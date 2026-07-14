namespace Fb2Library.Domain.Shared.Interfaces
{
    public interface IUnitOfWork : IDisposable, IAsyncDisposable
    {
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        public Task BeginTransactionAsync(CancellationToken cancellationToken = default);
        public Task CommitTransactionAsync(CancellationToken cancellationToken = default);
        public Task RollbackTransactionAsync(CancellationToken cancellationToken = default);

        // Дополнительные полезные методы
        public bool HasActiveTransaction { get; }
        public Task ExecuteInTransactionAsync(Func<Task> operation, CancellationToken cancellationToken = default);
    }
}
