using System;
using System.Net;
using System.Data;
using System.Linq;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNet.OData.Query;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;



namespace RadnetBd.Controllers.Radnet
{
  using Models;
  using Data;
  using Models.Radnet;

  [ODataRoutePrefix("odata/radnet/Leituras")]
  [Route("mvc/odata/radnet/Leituras")]
  public partial class LeiturasController : ODataController
  {
    private Data.RadnetContext context;

    public LeiturasController(Data.RadnetContext context)
    {
      this.context = context;
    }
    // GET /odata/Radnet/Leituras
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.Radnet.Leitura> GetLeituras()
    {
      var items = this.context.Leituras.AsNoTracking().AsQueryable<Models.Radnet.Leitura>();
      this.OnLeiturasRead(ref items);

      return items;
    }

    partial void OnLeiturasRead(ref IQueryable<Models.Radnet.Leitura> items);

    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet("{id_leitura}")]
    public SingleResult<Leitura> GetLeitura(int key)
    {
        var items = this.context.Leituras.AsNoTracking().Where(i=>i.id_leitura == key);
        this.OnLeiturasGet(ref items);

        return SingleResult.Create(items);
    }

    partial void OnLeiturasGet(ref IQueryable<Models.Radnet.Leitura> items);

  }
}
