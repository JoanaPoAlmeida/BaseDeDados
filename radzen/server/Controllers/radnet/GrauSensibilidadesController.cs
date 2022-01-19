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

  [ODataRoutePrefix("odata/radnet/GrauSensibilidades")]
  [Route("mvc/odata/radnet/GrauSensibilidades")]
  public partial class GrauSensibilidadesController : ODataController
  {
    private Data.RadnetContext context;

    public GrauSensibilidadesController(Data.RadnetContext context)
    {
      this.context = context;
    }
    // GET /odata/Radnet/GrauSensibilidades
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.Radnet.GrauSensibilidade> GetGrauSensibilidades()
    {
      var items = this.context.GrauSensibilidades.AsQueryable<Models.Radnet.GrauSensibilidade>();
      this.OnGrauSensibilidadesRead(ref items);

      return items;
    }

    partial void OnGrauSensibilidadesRead(ref IQueryable<Models.Radnet.GrauSensibilidade> items);

    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet("{id_Grau}")]
    public SingleResult<GrauSensibilidade> GetGrauSensibilidade(int key)
    {
        var items = this.context.GrauSensibilidades.Where(i=>i.id_Grau == key);
        this.OnGrauSensibilidadesGet(ref items);

        return SingleResult.Create(items);
    }

    partial void OnGrauSensibilidadesGet(ref IQueryable<Models.Radnet.GrauSensibilidade> items);

    partial void OnGrauSensibilidadeDeleted(Models.Radnet.GrauSensibilidade item);

    [HttpDelete("{id_Grau}")]
    public IActionResult DeleteGrauSensibilidade(int key)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var itemToDelete = this.context.GrauSensibilidades
                .Where(i => i.id_Grau == key)
                .Include(i => i.Sensors)
                .FirstOrDefault();

            if (itemToDelete == null)
            {
                ModelState.AddModelError("", "Item no longer available");
                return BadRequest(ModelState);
            }

            this.OnGrauSensibilidadeDeleted(itemToDelete);
            this.context.GrauSensibilidades.Remove(itemToDelete);
            this.context.SaveChanges();

            return new NoContentResult();
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnGrauSensibilidadeUpdated(Models.Radnet.GrauSensibilidade item);

    [HttpPut("{id_Grau}")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PutGrauSensibilidade(int key, [FromBody]Models.Radnet.GrauSensibilidade newItem)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (newItem == null || (newItem.id_Grau != key))
            {
                return BadRequest();
            }

            this.OnGrauSensibilidadeUpdated(newItem);
            this.context.GrauSensibilidades.Update(newItem);
            this.context.SaveChanges();

            var itemToReturn = this.context.GrauSensibilidades.Where(i => i.id_Grau == key);
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    [HttpPatch("{id_Grau}")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PatchGrauSensibilidade(int key, [FromBody]Delta<Models.Radnet.GrauSensibilidade> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var itemToUpdate = this.context.GrauSensibilidades.Where(i => i.id_Grau == key).FirstOrDefault();

            if (itemToUpdate == null)
            {
                ModelState.AddModelError("", "Item no longer available");
                return BadRequest(ModelState);
            }

            patch.Patch(itemToUpdate);

            this.OnGrauSensibilidadeUpdated(itemToUpdate);
            this.context.GrauSensibilidades.Update(itemToUpdate);
            this.context.SaveChanges();

            var itemToReturn = this.context.GrauSensibilidades.Where(i => i.id_Grau == key);
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnGrauSensibilidadeCreated(Models.Radnet.GrauSensibilidade item);

    [HttpPost]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult Post([FromBody] Models.Radnet.GrauSensibilidade item)
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

            this.OnGrauSensibilidadeCreated(item);
            this.context.GrauSensibilidades.Add(item);
            this.context.SaveChanges();

            return Created($"odata/Radnet/GrauSensibilidades/{item.id_Grau}", item);
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }
  }
}
