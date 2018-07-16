using CarsMVC.Models;
using System.Data.Entity;

namespace Corron.MVC.Models
{
    public class CarServiceContext : DbContext
    {
        public CarServiceContext() : base("JoesTechService")
        {

        }

        public DbSet<CarModel> Cars { get; set; }
        public DbSet<ServiceModel> Services { get; set; }
    }
}