using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Radnet.Models.RadnetBd
{
  [Table("Sensor", Schema = "dbo")]
  public partial class Sensor
  {
    public string tipo_sensor
    {
      get;
      set;
    }
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id_sensor
    {
      get;
      set;
    }

    public ICollection<NivelRadiacao> NivelRadiacaos { get; set; }
    public int? id_estacao
    {
      get;
      set;
    }
    public Estacao Estacao { get; set; }
    public int id_Grau
    {
      get;
      set;
    }
    public GrauSensibilidade GrauSensibilidade { get; set; }
  }
}
