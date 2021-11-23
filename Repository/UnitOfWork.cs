using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using WebApIHotelListing.Data;
using WebApIHotelListing.IRepository;

namespace WebApIHotelListing.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDataContext _appDataContext;
        private readonly ILogger _logger;
        private IRepository<Country> _countries;
        private IRepository<Hotel> _hotels;
        private bool _disposed;
        public IRepository<Country> Countries => _countries ??= new Repository<Country>(_appDataContext, _logger);
        public IRepository<Hotel> Hotels => _hotels ??= new Repository<Hotel>(_appDataContext, _logger);
        public UnitOfWork(AppDataContext appDataContext, ILogger logger)
        {
            _appDataContext = appDataContext;
            _logger = logger;
        }
        public void Dispose()
        {
            if(_disposed)
            {
                return;
            }

            _appDataContext.Dispose();
            _disposed = true;
            GC.SuppressFinalize(this);
        }        
        public async Task Save()
        {
            try
            {
                await _appDataContext.SaveChangesAsync();
                _logger.LogInformation($"[UnitOfWork] Save Success");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[UnitOfWork] Exception Occurred while Save");
            }            
        }
    }
}
