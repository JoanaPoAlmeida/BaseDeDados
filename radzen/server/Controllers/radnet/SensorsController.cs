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

  [ODataRoutePrefix("odata/radnet/Sensors")]
  [Route("mvc/odata/radnet/Sensors")]
  public partial class SensorsController : ODataController
  {
    private Data.RadnetContext context;

    public SensorsController(Data.RadnetContext context)
    {
      this.context = context;
    }
    // GET /odata/Radnet/Sensors
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.Radnet.Sensor> GetSensors()
    {
      var items = this.context.Sensors.AsQueryable<Models.Radnet.Sensor>();
      this.OnSensorsRead(ref items);

      return items;
    }

    partial void OnSensorsRead(ref IQueryable<Models.Radnet.Sensor> items);

    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet("{id_sensor}")]
    public SingleResult<Sensor> GetSensor(int key)
    {
        var items = this.context.Sensors.Where(i=>i.id_sensor == key);
        this.OnSensorsGet(ref items);

        return SingleResult.Create(items);
    }

    partial void OnSensorsGet(ref IQueryable<Models.Radnet.Sensor> items);

    partial void OnSensorDeleted(Models.Radnet.Sensor item);

    [HttpDelete("{id_sensor}")]
    public IActionResult DeleteSensor(int key)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var itemToDelete = this.context.Sensors
                .Where(i => i.id_sensor == key)
                .Include(i => i.NivelRadiacaos)
                .FirstOrDefault();

            if (itemToDelete == null)
            {
                ModelState.AddModelError("", "Item no longer available");
                return BadRequest(ModelState);
            }

            this.OnSensorDeleted(itemToDelete);
            this.context.Sensors.Remove(itemToDelete);
            this.context.SaveChanges();

            return new NoContentResult();
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnSensorUpdated(Models.Radnet.Sensor item);

    [HttpPut("{id_sensor}")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PutSensor(int key, [FromBody]Models.Radnet.Sensor newItem)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (newItem == null || (newItem.id_sensor != key))
            {
                return BadRequest();
            }

            this.OnSensorUpdated(newItem);
            this.context.Sensors.Update(newItem);
            this.context.SaveChanges();

            var itemToReturn = this.context.Sensors.Where(i => i.id_sensor == key);
            Request.QueryString = Request.QueryString.Add("$expand", "Estacao,GrauSensibilidade");
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    [HttpPatch("{id_sensor}")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PatchSensor(int key, [FromBody]Delta<Models.Radnet.Sensor> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var itemToUpdate = this.context.Sensors.Where(i => i.id_sensor == key).FirstOrDefault();

            if (itemToUpdate == null)
            {
                ModelState.AddModelError("", "Item no longer available");
                return BadRequest(ModelState);
            }

            patch.Patch(itemToUpdate);

            this.OnSensorUpdated(itemToUpdate);
            this.context.Sensors.Update(itemToUpdate);
            this.context.SaveChanges();

            var itemToReturn = this.context.Sensors.Where(i => i.id_sensor == key);
            Request.QueryString = Request.QueryString.Add("$expand", "Estacao,GrauSensibilidade");
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnSensorCreated(Models.Radnet.Sensor item);

    [HttpPost]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult Post([FromBody] Models.Radnet.Sensor item)
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

            this.OnSensorCreated(item);
            this.context.Sensors.Add(item);
            this.context.SaveChanges();

            var key = item.id_sensor;

            var itemToReturn = this.context.Sensors.Where(i => i.id_sensor == key);

            Request.QueryString = Request.QueryString.Add("$expand", "Estacao,GrauSensibilidade");

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
