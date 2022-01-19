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

  [ODataRoutePrefix("odata/radnet/DboLeituras")]
  [Route("mvc/odata/radnet/DboLeituras")]
  public partial class DboLeiturasController : ODataController
  {
    private Data.RadnetContext context;

    public DboLeiturasController(Data.RadnetContext context)
    {
      this.context = context;
    }
    // GET /odata/Radnet/DboLeituras
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.Radnet.DboLeitura> GetDboLeituras()
    {
      var items = this.context.DboLeituras.AsNoTracking().AsQueryable<Models.Radnet.DboLeitura>();
      this.OnDboLeiturasRead(ref items);

      return items;
    }

    partial void OnDboLeiturasRead(ref IQueryable<Models.Radnet.DboLeitura> items);

    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet("{hora}")]
    public SingleResult<DboLeitura> GetDboLeitura(DateTime? key)
    {
        var items = this.context.DboLeituras.AsNoTracking().Where(i=>i.hora == key);
        this.OnDboLeiturasGet(ref items);

        return SingleResult.Create(items);
    }

    partial void OnDboLeiturasGet(ref IQueryable<Models.Radnet.DboLeitura> items);

  }
}
