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

  [ODataRoutePrefix("odata/radnet/NivelRadiacaos")]
  [Route("mvc/odata/radnet/NivelRadiacaos")]
  public partial class NivelRadiacaosController : ODataController
  {
    private Data.RadnetContext context;

    public NivelRadiacaosController(Data.RadnetContext context)
    {
      this.context = context;
    }
    // GET /odata/Radnet/NivelRadiacaos
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.Radnet.NivelRadiacao> GetNivelRadiacaos()
    {
      var items = this.context.NivelRadiacaos.AsQueryable<Models.Radnet.NivelRadiacao>();
      this.OnNivelRadiacaosRead(ref items);

      return items;
    }

    partial void OnNivelRadiacaosRead(ref IQueryable<Models.Radnet.NivelRadiacao> items);

    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet("{id_nivel}")]
    public SingleResult<NivelRadiacao> GetNivelRadiacao(int key)
    {
        var items = this.context.NivelRadiacaos.Where(i=>i.id_nivel == key);
        this.OnNivelRadiacaosGet(ref items);

        return SingleResult.Create(items);
    }

    partial void OnNivelRadiacaosGet(ref IQueryable<Models.Radnet.NivelRadiacao> items);

    partial void OnNivelRadiacaoDeleted(Models.Radnet.NivelRadiacao item);

    [HttpDelete("{id_nivel}")]
    public IActionResult DeleteNivelRadiacao(int key)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var itemToDelete = this.context.NivelRadiacaos
                .Where(i => i.id_nivel == key)
                .FirstOrDefault();

            if (itemToDelete == null)
            {
                ModelState.AddModelError("", "Item no longer available");
                return BadRequest(ModelState);
            }

            this.OnNivelRadiacaoDeleted(itemToDelete);
            this.context.NivelRadiacaos.Remove(itemToDelete);
            this.context.SaveChanges();

            return new NoContentResult();
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnNivelRadiacaoUpdated(Models.Radnet.NivelRadiacao item);

    [HttpPut("{id_nivel}")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PutNivelRadiacao(int key, [FromBody]Models.Radnet.NivelRadiacao newItem)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (newItem == null || (newItem.id_nivel != key))
            {
                return BadRequest();
            }

            this.OnNivelRadiacaoUpdated(newItem);
            this.context.NivelRadiacaos.Update(newItem);
            this.context.SaveChanges();

            var itemToReturn = this.context.NivelRadiacaos.Where(i => i.id_nivel == key);
            Request.QueryString = Request.QueryString.Add("$expand", "Sensor,ValorReferencium");
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    [HttpPatch("{id_nivel}")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PatchNivelRadiacao(int key, [FromBody]Delta<Models.Radnet.NivelRadiacao> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var itemToUpdate = this.context.NivelRadiacaos.Where(i => i.id_nivel == key).FirstOrDefault();

            if (itemToUpdate == null)
            {
                ModelState.AddModelError("", "Item no longer available");
                return BadRequest(ModelState);
            }

            patch.Patch(itemToUpdate);

            this.OnNivelRadiacaoUpdated(itemToUpdate);
            this.context.NivelRadiacaos.Update(itemToUpdate);
            this.context.SaveChanges();

            var itemToReturn = this.context.NivelRadiacaos.Where(i => i.id_nivel == key);
            Request.QueryString = Request.QueryString.Add("$expand", "Sensor,ValorReferencium");
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnNivelRadiacaoCreated(Models.Radnet.NivelRadiacao item);

    [HttpPost]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult Post([FromBody] Models.Radnet.NivelRadiacao item)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (item == null)
            {
                return BadRequest();
            }

            this.OnNivelRadiacaoCreated(item);
            this.context.NivelRadiacaos.Add(item);
            this.context.SaveChanges();

            var key = item.id_nivel;

            var itemToReturn = this.context.NivelRadiacaos.Where(i => i.id_nivel == key);

            Request.QueryString = Request.QueryString.Add("$expand", "Sensor,ValorReferencium");

            return new ObjectResult(SingleResult.Create(itemToReturn))
            {
                StatusCode = 201
            };
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }
  }
}
