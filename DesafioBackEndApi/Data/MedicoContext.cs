using DesafioBackEndApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioBackEndApi.Data
{
    public class MedicoContext : IdentityDbContext
    {
        public MedicoContext(DbContextOptions<MedicoContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ValueComparer<List<string>> comparer = new ValueComparer<List<string>>(
            (l, r) => JsonConvert.SerializeObject(l) == JsonConvert.SerializeObject(r),
            v => v == null ? 0 : JsonConvert.SerializeObject(v).GetHashCode(),
            v => JsonConvert.DeserializeObject<List<string>>(JsonConvert.SerializeObject(v)));

            modelBuilder.Entity<Medico>()
                .Property(e => e.Especialidades)
                .HasConversion(
                v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject<List<string>>(v))
                .Metadata.SetValueComparer(comparer);
        }

        public DbSet<Medico> Medicos { get; set; }

    }
}
