using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RadnetBd.Models.Radnet
{
  [Table("Grau__sensibilidade", Schema = "dbo")]
  public partial class GrauSensibilidade
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id_Grau
    {
      get;
      set;
    }


    public ICollection<Sensor> Sensors { get; set; }
    public double limite_maximo
    {
      get;
      set;
    }
    public double limite_minimo
    {
      get;
      set;
    }
  }
}
