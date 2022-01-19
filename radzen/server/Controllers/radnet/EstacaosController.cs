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

  [ODataRoutePrefix("odata/radnet/Estacaos")]
  [Route("mvc/odata/radnet/Estacaos")]
  public partial class EstacaosController : ODataController
  {
    private Data.RadnetContext context;

    public EstacaosController(Data.RadnetContext context)
    {
      this.context = context;
    }
    // GET /odata/Radnet/Estacaos
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.Radnet.Estacao> GetEstacaos()
    {
      var items = this.context.Estacaos.AsQueryable<Models.Radnet.Estacao>();
      this.OnEstacaosRead(ref items);

      return items;
    }

    partial void OnEstacaosRead(ref IQueryable<Models.Radnet.Estacao> items);

    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet("{id_estacao}")]
    public SingleResult<Estacao> GetEstacao(int key)
    {
        var items = this.context.Estacaos.Where(i=>i.id_estacao == key);
        this.OnEstacaosGet(ref items);

        return SingleResult.Create(items);
    }

    partial void OnEstacaosGet(ref IQueryable<Models.Radnet.Estacao> items);

    partial void OnEstacaoDeleted(Models.Radnet.Estacao item);

    [HttpDelete("{id_estacao}")]
    public IActionResult DeleteEstacao(int key)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var itemToDelete = this.context.Estacaos
                .Where(i => i.id_estacao == key)
                .Include(i => i.Sensors)
                .FirstOrDefault();

            if (itemToDelete == null)
            {
                ModelState.AddModelError("", "Item no longer available");
                return BadRequest(ModelState);
            }

            this.OnEstacaoDeleted(itemToDelete);
            this.context.Estacaos.Remove(itemToDelete);
            this.context.SaveChanges();

            return new NoContentResult();
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnEstacaoUpdated(Models.Radnet.Estacao item);

    [HttpPut("{id_estacao}")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PutEstacao(int key, [FromBody]Models.Radnet.Estacao newItem)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (newItem == null || (newItem.id_estacao != key))
            {
                return BadRequest();
            }

            this.OnEstacaoUpdated(newItem);
            this.context.Estacaos.Update(newItem);
            this.context.SaveChanges();

            var itemToReturn = this.context.Estacaos.Where(i => i.id_estacao == key);
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    [HttpPatch("{id_estacao}")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PatchEstacao(int key, [FromBody]Delta<Models.Radnet.Estacao> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var itemToUpdate = this.context.Estacaos.Where(i => i.id_estacao == key).FirstOrDefault();

            if (itemToUpdate == null)
            {
                ModelState.AddModelError("", "Item no longer available");
                return BadRequest(ModelState);
            }

            patch.Patch(itemToUpdate);

            this.OnEstacaoUpdated(itemToUpdate);
            this.context.Estacaos.Update(itemToUpdate);
            this.context.SaveChanges();

            var itemToReturn = this.context.Estacaos.Where(i => i.id_estacao == key);
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnEstacaoCreated(Models.Radnet.Estacao item);

    [HttpPost]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult Post([FromBody] Models.Radnet.Estacao item)
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

            this.OnEstacaoCreated(item);
            this.context.Estacaos.Add(item);
            this.context.SaveChanges();

            return Created($"odata/Radnet/Estacaos/{item.id_estacao}", item);
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }
  }
}
