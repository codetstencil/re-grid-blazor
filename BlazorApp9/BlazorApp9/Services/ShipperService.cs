using GridBlazorServerSide.Data;
using GridShared;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using BlazorApp9.Data;

namespace GridBlazorServerSide.Services
{
    public class ShipperService : IShipperService
    {
        private readonly DbContextOptions<NorthwindDbContext> _options;

        public ShipperService(DbContextOptions<NorthwindDbContext> options)
        {
            _options = options;
        }

        public IEnumerable<SelectItem> GetAllShippers()
        {
            using (var context = new NorthwindDbContext(_options))
            {
                ShipperRepository repository = new ShipperRepository(context);
                return repository.GetAll()
                    .Select(r => new SelectItem(r.ShipperID.ToString(), r.ShipperID.ToString() + " - " 
                        + r.CompanyName))
                    .ToList();
            }
        }
    }

    public interface IShipperService
    {
        IEnumerable<SelectItem> GetAllShippers();
    }
}
