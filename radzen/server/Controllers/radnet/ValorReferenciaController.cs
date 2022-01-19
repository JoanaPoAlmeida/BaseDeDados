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

  [ODataRoutePrefix("odata/radnet/ValorReferencia")]
  [Route("mvc/odata/radnet/ValorReferencia")]
  public partial class ValorReferenciaController : ODataController
  {
    private Data.RadnetContext context;

    public ValorReferenciaController(Data.RadnetContext context)
    {
      this.context = context;
    }
    // GET /odata/Radnet/ValorReferencia
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.Radnet.ValorReferencium> GetValorReferencia()
    {
      var items = this.context.ValorReferencia.AsQueryable<Models.Radnet.ValorReferencium>();
      this.OnValorReferenciaRead(ref items);

      return items;
    }

    partial void OnValorReferenciaRead(ref IQueryable<Models.Radnet.ValorReferencium> items);

    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet("{id_referencia}")]
    public SingleResult<ValorReferencium> GetValorReferencium(int key)
    {
        var items = this.context.ValorReferencia.Where(i=>i.id_referencia == key);
        this.OnValorReferenciaGet(ref items);

        return SingleResult.Create(items);
    }

    partial void OnValorReferenciaGet(ref IQueryable<Models.Radnet.ValorReferencium> items);

    partial void OnValorReferenciumDeleted(Models.Radnet.ValorReferencium item);

    [HttpDelete("{id_referencia}")]
    public IActionResult DeleteValorReferencium(int key)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var itemToDelete = this.context.ValorReferencia
                .Where(i => i.id_referencia == key)
                .Include(i => i.NivelRadiacaos)
                .FirstOrDefault();

            if (itemToDelete == null)
            {
                ModelState.AddModelError("", "Item no longer available");
                return BadRequest(ModelState);
            }

            this.OnValorReferenciumDeleted(itemToDelete);
            this.context.ValorReferencia.Remove(itemToDelete);
            this.context.SaveChanges();

            return new NoContentResult();
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnValorReferenciumUpdated(Models.Radnet.ValorReferencium item);

    [HttpPut("{id_referencia}")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PutValorReferencium(int key, [FromBody]Models.Radnet.ValorReferencium newItem)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (newItem == null || (newItem.id_referencia != key))
            {
                return BadRequest();
            }

            this.OnValorReferenciumUpdated(newItem);
            this.context.ValorReferencia.Update(newItem);
            this.context.SaveChanges();

            var itemToReturn = this.context.ValorReferencia.Where(i => i.id_referencia == key);
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    [HttpPatch("{id_referencia}")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PatchValorReferencium(int key, [FromBody]Delta<Models.Radnet.ValorReferencium> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var itemToUpdate = this.context.ValorReferencia.Where(i => i.id_referencia == key).FirstOrDefault();

            if (itemToUpdate == null)
            {
                ModelState.AddModelError("", "Item no longer available");
                return BadRequest(ModelState);
            }

            patch.Patch(itemToUpdate);

            this.OnValorReferenciumUpdated(itemToUpdate);
            this.context.ValorReferencia.Update(itemToUpdate);
            this.context.SaveChanges();

            var itemToReturn = this.context.ValorReferencia.Where(i => i.id_referencia == key);
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnValorReferenciumCreated(Models.Radnet.ValorReferencium item);

    [HttpPost]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult Post([FromBody] Models.Radnet.ValorReferencium item)
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

            this.OnValorReferenciumCreated(item);
            this.context.ValorReferencia.Add(item);
            this.context.SaveChanges();

            return Created($"odata/Radnet/ValorReferencia/{item.id_referencia}", item);
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }
  }
}
