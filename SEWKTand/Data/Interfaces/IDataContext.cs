using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using SEWKTand.Data.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SEWKTand.Interfaces.Data
{
    public interface IDataContext : IDisposable
    { 
        DbSet<EntityAdmin> Admin { get; set; }
        DbSet<EntityDentist> Dentist { get; set; }
        DbSet<EntityPatient> Patient { get; set; }
        DbSet<EntityMedicalRecord> MedicalRecord { get; set; }

        int SaveChanges();
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken));
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        int SaveChanges(bool acceptAllChangesOnSuccess);
    }
}
