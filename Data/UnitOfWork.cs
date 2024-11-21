using backend_dotnet.Interfaces;
using backend_dotnet.Models;
using backend_dotnet.Repositories;

namespace backend_dotnet.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private bool disposed = false;

        public IGenericRepository<User> Users { get; private set; }
        public IGenericRepository<Case> Cases { get; private set; }
        public IGenericRepository<Category> Categories { get; private set; }
        public IGenericRepository<Country> Countries { get; private set; }
        public IGenericRepository<CountryCategoryPrice> CountryCategoryPrices { get; private set; }
        public IGenericRepository<FileMetadata> FileMetadatas { get; private set; }
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Users = new GenericRepository<User>(context);
            Cases = new GenericRepository<Case>(context);
            Categories = new GenericRepository<Category>(context);
            Countries = new GenericRepository<Country>(context);
            CountryCategoryPrices = new GenericRepository<CountryCategoryPrice>(context);
            FileMetadatas = new GenericRepository<FileMetadata>(context);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}