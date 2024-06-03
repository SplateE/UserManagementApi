using Application.Interfaces;

namespace Application.IGenericInterfaces
{
    public interface IUnitOfWork : IDisposable
    { 
        IUser Users { get; }
        Task<int> SaveAsync(CancellationToken cancellationToken);

    }
}
