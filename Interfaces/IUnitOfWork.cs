using backend_dotnet.Models;

namespace backend_dotnet.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<User> Users { get; }
        IGenericRepository<Case> Cases { get; }
        IGenericRepository<Category> Categories { get; }
        IGenericRepository<Country> Countries { get; }
        IGenericRepository<CountryCategoryPrice> CountryCategoryPrices { get; }
        IGenericRepository<FileMetadata> FileMetadatas { get; }
        
        Task<int> SaveChangesAsync();
    }
} 