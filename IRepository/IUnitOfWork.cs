using System;
using System.Threading.Tasks;
using WebApIHotelListing.Data;

namespace WebApIHotelListing.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Country> Countries { get; }
        IRepository<Hotel> Hotels { get; }
        Task Save();        

    }
}
