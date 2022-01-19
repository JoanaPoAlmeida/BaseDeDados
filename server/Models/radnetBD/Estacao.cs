using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Radnet.Models.RadnetBd
{
  [Table("Estacao", Schema = "dbo")]
  public partial class Estacao
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id_estacao
    {
      get;
      set;
    }

    public ICollection<Sensor> Sensors { get; set; }
    public string tipo_estacao
    {
      get;
      set;
    }
    public string freq_leitura
    {
      get;
      set;
    }
    public string localizacao
    {
      get;
      set;
    }
    public DateTime data_instalacao
    {
      get;
      set;
    }
  }
}
