using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;

using Radnet.Models.RadnetBd;

namespace Radnet.Data
{
  public partial class RadnetBdContext : Microsoft.EntityFrameworkCore.DbContext
  {
    public RadnetBdContext(DbContextOptions<RadnetBdContext> options):base(options)
    {
    }

    public RadnetBdContext()
    {
    }

    partial void OnModelBuilding(ModelBuilder builder);

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Radnet.Models.RadnetBd.Leitura>().HasNoKey();
        builder.Entity<Radnet.Models.RadnetBd.NivelRadiacao>()
              .HasOne(i => i.Sensor)
              .WithMany(i => i.NivelRadiacaos)
              .HasForeignKey(i => i.id_sensor)
              .HasPrincipalKey(i => i.id_sensor);
        builder.Entity<Radnet.Models.RadnetBd.NivelRadiacao>()
              .HasOne(i => i.ValorReferencium)
              .WithMany(i => i.NivelRadiacaos)
              .HasForeignKey(i => i.id_referencia)
              .HasPrincipalKey(i => i.id_referencia);
        builder.Entity<Radnet.Models.RadnetBd.Sensor>()
              .HasOne(i => i.Estacao)
              .WithMany(i => i.Sensors)
              .HasForeignKey(i => i.id_estacao)
              .HasPrincipalKey(i => i.id_estacao);
        builder.Entity<Radnet.Models.RadnetBd.Sensor>()
              .HasOne(i => i.GrauSensibilidade)
              .WithMany(i => i.Sensors)
              .HasForeignKey(i => i.id_Grau)
              .HasPrincipalKey(i => i.id_Grau);


        builder.Entity<Radnet.Models.RadnetBd.Estacao>()
              .Property(p => p.data_instalacao)
              .HasColumnType("date");

        builder.Entity<Radnet.Models.RadnetBd.Leitura>()
              .Property(p => p.data_leitura)
              .HasColumnType("date");

        builder.Entity<Radnet.Models.RadnetBd.Estacao>()
              .Property(p => p.id_estacao)
              .HasPrecision(10, 0);

        builder.Entity<Radnet.Models.RadnetBd.GrauSensibilidade>()
              .Property(p => p.id_Grau)
              .HasPrecision(10, 0);

        builder.Entity<Radnet.Models.RadnetBd.GrauSensibilidade>()
              .Property(p => p.limite_maximo)
              .HasPrecision(53, 0);

        builder.Entity<Radnet.Models.RadnetBd.GrauSensibilidade>()
              .Property(p => p.limite_minimo)
              .HasPrecision(53, 0);

        builder.Entity<Radnet.Models.RadnetBd.Leitura>()
              .Property(p => p.id_leitura)
              .HasPrecision(10, 0);

        builder.Entity<Radnet.Models.RadnetBd.Leitura>()
              .Property(p => p.valor_leitura)
              .HasPrecision(53, 0);

        builder.Entity<Radnet.Models.RadnetBd.Leitura>()
              .Property(p => p.id_estacao)
              .HasPrecision(10, 0);

        builder.Entity<Radnet.Models.RadnetBd.NivelRadiacao>()
              .Property(p => p.id_nivel)
              .HasPrecision(10, 0);

        builder.Entity<Radnet.Models.RadnetBd.NivelRadiacao>()
              .Property(p => p.freq_radiacao)
              .HasPrecision(10, 0);

        builder.Entity<Radnet.Models.RadnetBd.NivelRadiacao>()
              .Property(p => p.id_sensor)
              .HasPrecision(10, 0);

        builder.Entity<Radnet.Models.RadnetBd.NivelRadiacao>()
              .Property(p => p.id_referencia)
              .HasPrecision(10, 0);

        builder.Entity<Radnet.Models.RadnetBd.Sensor>()
              .Property(p => p.id_sensor)
              .HasPrecision(10, 0);

        builder.Entity<Radnet.Models.RadnetBd.Sensor>()
              .Property(p => p.id_estacao)
              .HasPrecision(10, 0);

        builder.Entity<Radnet.Models.RadnetBd.Sensor>()
              .Property(p => p.id_Grau)
              .HasPrecision(10, 0);

        builder.Entity<Radnet.Models.RadnetBd.ValorReferencium>()
              .Property(p => p.id_referencia)
              .HasPrecision(10, 0);

        builder.Entity<Radnet.Models.RadnetBd.ValorReferencium>()
              .Property(p => p.VR_diario)
              .HasPrecision(53, 0);

        builder.Entity<Radnet.Models.RadnetBd.ValorReferencium>()
              .Property(p => p.VR_mensal)
              .HasPrecision(53, 0);

        builder.Entity<Radnet.Models.RadnetBd.ValorReferencium>()
              .Property(p => p.VR_anual)
              .HasPrecision(53, 0);
        this.OnModelBuilding(builder);
    }


    public DbSet<Radnet.Models.RadnetBd.Estacao> Estacaos
    {
      get;
      set;
    }

    public DbSet<Radnet.Models.RadnetBd.GrauSensibilidade> GrauSensibilidades
    {
      get;
      set;
    }

    public DbSet<Radnet.Models.RadnetBd.Leitura> Leituras
    {
      get;
      set;
    }

    public DbSet<Radnet.Models.RadnetBd.NivelRadiacao> NivelRadiacaos
    {
      get;
      set;
    }

    public DbSet<Radnet.Models.RadnetBd.Sensor> Sensors
    {
      get;
      set;
    }

    public DbSet<Radnet.Models.RadnetBd.ValorReferencium> ValorReferencia
    {
      get;
      set;
    }
  }
}
