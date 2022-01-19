using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Radnet.Models.RadnetBd
{
  [Table("leituras", Schema = "dbo")]
  public partial class Leitura
  {
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id_leitura
    {
      get;
      set;
    }
    public DateTime? data_leitura
    {
      get;
      set;
    }
    public TimeSpan? hora_leitura
    {
      get;
      set;
    }
    public double? valor_leitura
    {
      get;
      set;
    }
    public int id_estacao
    {
      get;
      set;
    }
  }
}
