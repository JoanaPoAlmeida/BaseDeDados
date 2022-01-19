using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Radnet.Models.RadnetBd
{
  [Table("valor_referencia", Schema = "dbo")]
  public partial class ValorReferencium
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id_referencia
    {
      get;
      set;
    }

    public ICollection<NivelRadiacao> NivelRadiacaos { get; set; }
    public double VR_diario
    {
      get;
      set;
    }
    public double VR_mensal
    {
      get;
      set;
    }
    public double VR_anual
    {
      get;
      set;
    }
  }
}
