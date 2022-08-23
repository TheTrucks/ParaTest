using Microsoft.EntityFrameworkCore;

namespace ParaTest.Models.Cityzen
{
    public class CityzenDbContext : DbContext
    {
        public CityzenDbContext(DbContextOptions<CityzenDbContext> opts) : base(opts) { }

        public DbSet<Cityzen> Cityzens => Set<Cityzen>();

        protected override void OnModelCreating(ModelBuilder MBuild)
        {
            MBuild.HasSequence<long>("cityzen_id_seq", schema: "registry")
                .StartsAt(1000)
                .IncrementsBy(1);

            MBuild.Entity<Cityzen>()
                .Property(x => x.Id)
                .HasDefaultValueSql("NEXT VALUE FOR registry.cityzen_id_seq");
        }
    }
}
