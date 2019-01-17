using Microsoft.EntityFrameworkCore;
using SEWKTand.Data.Entities;
using SEWKTand.Interfaces.Data;

namespace SEWKTand.Data
{
    public class DataContext : DbContext, IDataContext
    {
        /*
            DbContextOptions<TContext> tells the context all of its settings, such as which database to connect to.
            This is the same object that is built by running the OnConfiguring method in your context.
        */

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }

        public DbSet<EntityAdmin> Admin { get; set; }
        public DbSet<EntityDentist> Dentist { get; set; }
        public DbSet<EntityPatient> Patient { get; set; }
        public DbSet<EntityMedicalRecord> MedicalRecord { get; set; }
    }
}
