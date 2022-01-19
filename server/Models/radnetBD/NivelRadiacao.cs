using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Radnet.Models.RadnetBd
{
  [Table("nivel_radiacao", Schema = "dbo")]
  public partial class NivelRadiacao
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id_nivel
    {
      get;
      set;
    }
    public int freq_radiacao
    {
      get;
      set;
    }
    public int id_sensor
    {
      get;
      set;
    }
    public Sensor Sensor { get; set; }
    public int id_referencia
    {
      get;
      set;
    }
    public ValorReferencium ValorReferencium { get; set; }
  }
}
