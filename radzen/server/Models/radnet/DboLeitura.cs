using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RadnetBd.Models.Radnet
{
  [Table("leitura", Schema = "dbo")]
  public partial class DboLeitura
  {
    [Key]
    public DateTime? hora
    {
      get;
      set;
    }
    public string Abrantes
    {
      get;
      set;
    }
    public string Beja
    {
      get;
      set;
    }

    [Column("Bragança")]
    public string Bragana
    {
      get;
      set;
    }
    public string Castelo_Branco
    {
      get;
      set;
    }
    public string Coimbra
    {
      get;
      set;
    }
    public string Elvas
    {
      get;
      set;
    }

    [Column("Évora")]
    public string vora
    {
      get;
      set;
    }
    public string Faro
    {
      get;
      set;
    }
    public string Funchal
    {
      get;
      set;
    }
    public string Junqueira
    {
      get;
      set;
    }
    public string Lisboa
    {
      get;
      set;
    }
    public string Meimoa
    {
      get;
      set;
    }
    public string Penhas_Douradas
    {
      get;
      set;
    }
    public string Pocinho
    {
      get;
      set;
    }
    public string Ponta_Delgada
    {
      get;
      set;
    }
    public string Portalegre
    {
      get;
      set;
    }
    public string Porto
    {
      get;
      set;
    }
    public string Sines
    {
      get;
      set;
    }
  }
}
