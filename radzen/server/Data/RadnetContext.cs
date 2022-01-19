using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

using RadnetBd.Models.Radnet;

namespace RadnetBd.Data
{
    public partial class RadnetContext : Microsoft.EntityFrameworkCore.DbContext
    {
        private readonly IHttpContextAccessor httpAccessor;

        public RadnetContext(IHttpContextAccessor httpAccessor, DbContextOptions<RadnetContext> options):base(options)
        {
            this.httpAccessor = httpAccessor;
        }

        public RadnetContext(IHttpContextAccessor httpAccessor)
        {
            this.httpAccessor = httpAccessor;
        }

        partial void OnModelBuilding(ModelBuilder builder);

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<RadnetBd.Models.Radnet.NivelRadiacao>()
                  .HasOne(i => i.Sensor)
                  .WithMany(i => i.NivelRadiacaos)
                  .HasForeignKey(i => i.id_sensor)
                  .HasPrincipalKey(i => i.id_sensor);
            builder.Entity<RadnetBd.Models.Radnet.NivelRadiacao>()
                  .HasOne(i => i.ValorReferencium)
                  .WithMany(i => i.NivelRadiacaos)
                  .HasForeignKey(i => i.id_referencia)
                  .HasPrincipalKey(i => i.id_referencia);
            builder.Entity<RadnetBd.Models.Radnet.Sensor>()
                  .HasOne(i => i.Estacao)
                  .WithMany(i => i.Sensors)
                  .HasForeignKey(i => i.id_estacao)
                  .HasPrincipalKey(i => i.id_estacao);
            builder.Entity<RadnetBd.Models.Radnet.Sensor>()
                  .HasOne(i => i.GrauSensibilidade)
                  .WithMany(i => i.Sensors)
                  .HasForeignKey(i => i.id_Grau)
                  .HasPrincipalKey(i => i.id_Grau);


            builder.Entity<RadnetBd.Models.Radnet.DboLeitura>()
                  .Property(p => p.hora)
                  .HasColumnType("datetime2");

            builder.Entity<RadnetBd.Models.Radnet.Estacao>()
                  .Property(p => p.data_instalacao)
                  .HasColumnType("date");

            builder.Entity<RadnetBd.Models.Radnet.Leitura>()
                  .Property(p => p.data_leitura)
                  .HasColumnType("date");

            builder.Entity<RadnetBd.Models.Radnet.Estacao>()
                  .Property(p => p.id_estacao)
                  .HasPrecision(10, 0);

            builder.Entity<RadnetBd.Models.Radnet.GrauSensibilidade>()
                  .Property(p => p.id_Grau)
                  .HasPrecision(10, 0);

            builder.Entity<RadnetBd.Models.Radnet.GrauSensibilidade>()
                  .Property(p => p.limite_maximo)
                  .HasPrecision(53, 0);

            builder.Entity<RadnetBd.Models.Radnet.GrauSensibilidade>()
                  .Property(p => p.limite_minimo)
                  .HasPrecision(53, 0);

            builder.Entity<RadnetBd.Models.Radnet.Leitura>()
                  .Property(p => p.id_leitura)
                  .HasPrecision(10, 0);

            builder.Entity<RadnetBd.Models.Radnet.Leitura>()
                  .Property(p => p.valor_leitura)
                  .HasPrecision(53, 0);

            builder.Entity<RadnetBd.Models.Radnet.Leitura>()
                  .Property(p => p.id_estacao)
                  .HasPrecision(10, 0);

            builder.Entity<RadnetBd.Models.Radnet.NivelRadiacao>()
                  .Property(p => p.id_nivel)
                  .HasPrecision(10, 0);

            builder.Entity<RadnetBd.Models.Radnet.NivelRadiacao>()
                  .Property(p => p.freq_radiacao)
                  .HasPrecision(10, 0);

            builder.Entity<RadnetBd.Models.Radnet.NivelRadiacao>()
                  .Property(p => p.id_sensor)
                  .HasPrecision(10, 0);

            builder.Entity<RadnetBd.Models.Radnet.NivelRadiacao>()
                  .Property(p => p.id_referencia)
                  .HasPrecision(10, 0);

            builder.Entity<RadnetBd.Models.Radnet.Sensor>()
                  .Property(p => p.id_sensor)
                  .HasPrecision(10, 0);

            builder.Entity<RadnetBd.Models.Radnet.Sensor>()
                  .Property(p => p.id_estacao)
                  .HasPrecision(10, 0);

            builder.Entity<RadnetBd.Models.Radnet.Sensor>()
                  .Property(p => p.id_Grau)
                  .HasPrecision(10, 0);

            builder.Entity<RadnetBd.Models.Radnet.ValorReferencium>()
                  .Property(p => p.id_referencia)
                  .HasPrecision(10, 0);

            builder.Entity<RadnetBd.Models.Radnet.ValorReferencium>()
                  .Property(p => p.VR_diario)
                  .HasPrecision(53, 0);

            builder.Entity<RadnetBd.Models.Radnet.ValorReferencium>()
                  .Property(p => p.VR_mensal)
                  .HasPrecision(53, 0);

            builder.Entity<RadnetBd.Models.Radnet.ValorReferencium>()
                  .Property(p => p.VR_anual)
                  .HasPrecision(53, 0);
            this.OnModelBuilding(builder);
        }


        public DbSet<RadnetBd.Models.Radnet.DboLeitura> DboLeituras
        {
          get;
          set;
        }

        public DbSet<RadnetBd.Models.Radnet.Estacao> Estacaos
        {
          get;
          set;
        }

        public DbSet<RadnetBd.Models.Radnet.GrauSensibilidade> GrauSensibilidades
        {
          get;
          set;
        }

        public DbSet<RadnetBd.Models.Radnet.Leitura> Leituras
        {
          get;
          set;
        }

        public DbSet<RadnetBd.Models.Radnet.NivelRadiacao> NivelRadiacaos
        {
          get;
          set;
        }

        public DbSet<RadnetBd.Models.Radnet.Sensor> Sensors
        {
          get;
          set;
        }

        public DbSet<RadnetBd.Models.Radnet.ValorReferencium> ValorReferencia
        {
          get;
          set;
        }
    }
}
