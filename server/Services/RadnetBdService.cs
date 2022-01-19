using Radzen;
using System;
using System.Web;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Data;
using System.Text.Encodings.Web;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components;
using Radnet.Data;

namespace Radnet
{
    public partial class RadnetBdService
    {
        RadnetBdContext Context
        {
           get
           {
             return this.context;
           }
        }

        private readonly RadnetBdContext context;
        private readonly NavigationManager navigationManager;

        public RadnetBdService(RadnetBdContext context, NavigationManager navigationManager)
        {
            this.context = context;
            this.navigationManager = navigationManager;
        }

        public void Reset() => Context.ChangeTracker.Entries().Where(e => e.Entity != null).ToList().ForEach(e => e.State = EntityState.Detached);

        public async Task ExportEstacaosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/radnetbd/estacaos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/radnetbd/estacaos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportEstacaosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/radnetbd/estacaos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/radnetbd/estacaos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnEstacaosRead(ref IQueryable<Models.RadnetBd.Estacao> items);

        public async Task<IQueryable<Models.RadnetBd.Estacao>> GetEstacaos(Query query = null)
        {
            var items = Context.Estacaos.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnEstacaosRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnEstacaoCreated(Models.RadnetBd.Estacao item);
        partial void OnAfterEstacaoCreated(Models.RadnetBd.Estacao item);

        public async Task<Models.RadnetBd.Estacao> CreateEstacao(Models.RadnetBd.Estacao estacao)
        {
            OnEstacaoCreated(estacao);

            var existingItem = Context.Estacaos
                              .Where(i => i.id_estacao == estacao.id_estacao)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Estacaos.Add(estacao);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(estacao).State = EntityState.Detached;
                throw;
            }

            OnAfterEstacaoCreated(estacao);

            return estacao;
        }
        public async Task ExportGrauSensibilidadesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/radnetbd/grausensibilidades/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/radnetbd/grausensibilidades/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportGrauSensibilidadesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/radnetbd/grausensibilidades/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/radnetbd/grausensibilidades/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGrauSensibilidadesRead(ref IQueryable<Models.RadnetBd.GrauSensibilidade> items);

        public async Task<IQueryable<Models.RadnetBd.GrauSensibilidade>> GetGrauSensibilidades(Query query = null)
        {
            var items = Context.GrauSensibilidades.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnGrauSensibilidadesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnGrauSensibilidadeCreated(Models.RadnetBd.GrauSensibilidade item);
        partial void OnAfterGrauSensibilidadeCreated(Models.RadnetBd.GrauSensibilidade item);

        public async Task<Models.RadnetBd.GrauSensibilidade> CreateGrauSensibilidade(Models.RadnetBd.GrauSensibilidade grauSensibilidade)
        {
            OnGrauSensibilidadeCreated(grauSensibilidade);

            var existingItem = Context.GrauSensibilidades
                              .Where(i => i.id_Grau == grauSensibilidade.id_Grau)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.GrauSensibilidades.Add(grauSensibilidade);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(grauSensibilidade).State = EntityState.Detached;
                throw;
            }

            OnAfterGrauSensibilidadeCreated(grauSensibilidade);

            return grauSensibilidade;
        }
        public async Task ExportLeiturasToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/radnetbd/leituras/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/radnetbd/leituras/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportLeiturasToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/radnetbd/leituras/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/radnetbd/leituras/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnLeiturasRead(ref IQueryable<Models.RadnetBd.Leitura> items);

        public async Task<IQueryable<Models.RadnetBd.Leitura>> GetLeituras(Query query = null)
        {
            var items = Context.Leituras.AsQueryable();
            items = items.AsNoTracking();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnLeiturasRead(ref items);

            return await Task.FromResult(items);
        }
        public async Task ExportNivelRadiacaosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/radnetbd/nivelradiacaos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/radnetbd/nivelradiacaos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportNivelRadiacaosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/radnetbd/nivelradiacaos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/radnetbd/nivelradiacaos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnNivelRadiacaosRead(ref IQueryable<Models.RadnetBd.NivelRadiacao> items);

        public async Task<IQueryable<Models.RadnetBd.NivelRadiacao>> GetNivelRadiacaos(Query query = null)
        {
            var items = Context.NivelRadiacaos.AsQueryable();

            items = items.Include(i => i.Sensor);

            items = items.Include(i => i.ValorReferencium);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnNivelRadiacaosRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnNivelRadiacaoCreated(Models.RadnetBd.NivelRadiacao item);
        partial void OnAfterNivelRadiacaoCreated(Models.RadnetBd.NivelRadiacao item);

        public async Task<Models.RadnetBd.NivelRadiacao> CreateNivelRadiacao(Models.RadnetBd.NivelRadiacao nivelRadiacao)
        {
            OnNivelRadiacaoCreated(nivelRadiacao);

            var existingItem = Context.NivelRadiacaos
                              .Where(i => i.id_nivel == nivelRadiacao.id_nivel)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.NivelRadiacaos.Add(nivelRadiacao);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(nivelRadiacao).State = EntityState.Detached;
                nivelRadiacao.Sensor = null;
                nivelRadiacao.ValorReferencium = null;
                throw;
            }

            OnAfterNivelRadiacaoCreated(nivelRadiacao);

            return nivelRadiacao;
        }
        public async Task ExportSensorsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/radnetbd/sensors/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/radnetbd/sensors/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportSensorsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/radnetbd/sensors/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/radnetbd/sensors/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnSensorsRead(ref IQueryable<Models.RadnetBd.Sensor> items);

        public async Task<IQueryable<Models.RadnetBd.Sensor>> GetSensors(Query query = null)
        {
            var items = Context.Sensors.AsQueryable();

            items = items.Include(i => i.Estacao);

            items = items.Include(i => i.GrauSensibilidade);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnSensorsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnSensorCreated(Models.RadnetBd.Sensor item);
        partial void OnAfterSensorCreated(Models.RadnetBd.Sensor item);

        public async Task<Models.RadnetBd.Sensor> CreateSensor(Models.RadnetBd.Sensor sensor)
        {
            OnSensorCreated(sensor);

            var existingItem = Context.Sensors
                              .Where(i => i.id_sensor == sensor.id_sensor)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Sensors.Add(sensor);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(sensor).State = EntityState.Detached;
                sensor.Estacao = null;
                sensor.GrauSensibilidade = null;
                throw;
            }

            OnAfterSensorCreated(sensor);

            return sensor;
        }
        public async Task ExportValorReferenciaToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/radnetbd/valorreferencia/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/radnetbd/valorreferencia/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportValorReferenciaToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/radnetbd/valorreferencia/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/radnetbd/valorreferencia/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnValorReferenciaRead(ref IQueryable<Models.RadnetBd.ValorReferencium> items);

        public async Task<IQueryable<Models.RadnetBd.ValorReferencium>> GetValorReferencia(Query query = null)
        {
            var items = Context.ValorReferencia.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnValorReferenciaRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnValorReferenciumCreated(Models.RadnetBd.ValorReferencium item);
        partial void OnAfterValorReferenciumCreated(Models.RadnetBd.ValorReferencium item);

        public async Task<Models.RadnetBd.ValorReferencium> CreateValorReferencium(Models.RadnetBd.ValorReferencium valorReferencium)
        {
            OnValorReferenciumCreated(valorReferencium);

            var existingItem = Context.ValorReferencia
                              .Where(i => i.id_referencia == valorReferencium.id_referencia)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.ValorReferencia.Add(valorReferencium);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(valorReferencium).State = EntityState.Detached;
                throw;
            }

            OnAfterValorReferenciumCreated(valorReferencium);

            return valorReferencium;
        }

        partial void OnEstacaoDeleted(Models.RadnetBd.Estacao item);
        partial void OnAfterEstacaoDeleted(Models.RadnetBd.Estacao item);

        public async Task<Models.RadnetBd.Estacao> DeleteEstacao(int? idEstacao)
        {
            var itemToDelete = Context.Estacaos
                              .Where(i => i.id_estacao == idEstacao)
                              .Include(i => i.Sensors)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnEstacaoDeleted(itemToDelete);

            Context.Estacaos.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterEstacaoDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnEstacaoGet(Models.RadnetBd.Estacao item);

        public async Task<Models.RadnetBd.Estacao> GetEstacaoByidEstacao(int? idEstacao)
        {
            var items = Context.Estacaos
                              .AsNoTracking()
                              .Where(i => i.id_estacao == idEstacao);

            var itemToReturn = items.FirstOrDefault();

            OnEstacaoGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.RadnetBd.Estacao> CancelEstacaoChanges(Models.RadnetBd.Estacao item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnEstacaoUpdated(Models.RadnetBd.Estacao item);
        partial void OnAfterEstacaoUpdated(Models.RadnetBd.Estacao item);

        public async Task<Models.RadnetBd.Estacao> UpdateEstacao(int? idEstacao, Models.RadnetBd.Estacao estacao)
        {
            OnEstacaoUpdated(estacao);

            var itemToUpdate = Context.Estacaos
                              .Where(i => i.id_estacao == idEstacao)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(estacao);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterEstacaoUpdated(estacao);

            return estacao;
        }

        partial void OnGrauSensibilidadeDeleted(Models.RadnetBd.GrauSensibilidade item);
        partial void OnAfterGrauSensibilidadeDeleted(Models.RadnetBd.GrauSensibilidade item);

        public async Task<Models.RadnetBd.GrauSensibilidade> DeleteGrauSensibilidade(int? idGrau)
        {
            var itemToDelete = Context.GrauSensibilidades
                              .Where(i => i.id_Grau == idGrau)
                              .Include(i => i.Sensors)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnGrauSensibilidadeDeleted(itemToDelete);

            Context.GrauSensibilidades.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterGrauSensibilidadeDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnGrauSensibilidadeGet(Models.RadnetBd.GrauSensibilidade item);

        public async Task<Models.RadnetBd.GrauSensibilidade> GetGrauSensibilidadeByidGrau(int? idGrau)
        {
            var items = Context.GrauSensibilidades
                              .AsNoTracking()
                              .Where(i => i.id_Grau == idGrau);

            var itemToReturn = items.FirstOrDefault();

            OnGrauSensibilidadeGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.RadnetBd.GrauSensibilidade> CancelGrauSensibilidadeChanges(Models.RadnetBd.GrauSensibilidade item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnGrauSensibilidadeUpdated(Models.RadnetBd.GrauSensibilidade item);
        partial void OnAfterGrauSensibilidadeUpdated(Models.RadnetBd.GrauSensibilidade item);

        public async Task<Models.RadnetBd.GrauSensibilidade> UpdateGrauSensibilidade(int? idGrau, Models.RadnetBd.GrauSensibilidade grauSensibilidade)
        {
            OnGrauSensibilidadeUpdated(grauSensibilidade);

            var itemToUpdate = Context.GrauSensibilidades
                              .Where(i => i.id_Grau == idGrau)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(grauSensibilidade);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterGrauSensibilidadeUpdated(grauSensibilidade);

            return grauSensibilidade;
        }

        partial void OnNivelRadiacaoDeleted(Models.RadnetBd.NivelRadiacao item);
        partial void OnAfterNivelRadiacaoDeleted(Models.RadnetBd.NivelRadiacao item);

        public async Task<Models.RadnetBd.NivelRadiacao> DeleteNivelRadiacao(int? idNivel)
        {
            var itemToDelete = Context.NivelRadiacaos
                              .Where(i => i.id_nivel == idNivel)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnNivelRadiacaoDeleted(itemToDelete);

            Context.NivelRadiacaos.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterNivelRadiacaoDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnNivelRadiacaoGet(Models.RadnetBd.NivelRadiacao item);

        public async Task<Models.RadnetBd.NivelRadiacao> GetNivelRadiacaoByidNivel(int? idNivel)
        {
            var items = Context.NivelRadiacaos
                              .AsNoTracking()
                              .Where(i => i.id_nivel == idNivel);

            items = items.Include(i => i.Sensor);

            items = items.Include(i => i.ValorReferencium);

            var itemToReturn = items.FirstOrDefault();

            OnNivelRadiacaoGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.RadnetBd.NivelRadiacao> CancelNivelRadiacaoChanges(Models.RadnetBd.NivelRadiacao item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnNivelRadiacaoUpdated(Models.RadnetBd.NivelRadiacao item);
        partial void OnAfterNivelRadiacaoUpdated(Models.RadnetBd.NivelRadiacao item);

        public async Task<Models.RadnetBd.NivelRadiacao> UpdateNivelRadiacao(int? idNivel, Models.RadnetBd.NivelRadiacao nivelRadiacao)
        {
            OnNivelRadiacaoUpdated(nivelRadiacao);

            var itemToUpdate = Context.NivelRadiacaos
                              .Where(i => i.id_nivel == idNivel)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(nivelRadiacao);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterNivelRadiacaoUpdated(nivelRadiacao);

            return nivelRadiacao;
        }

        partial void OnSensorDeleted(Models.RadnetBd.Sensor item);
        partial void OnAfterSensorDeleted(Models.RadnetBd.Sensor item);

        public async Task<Models.RadnetBd.Sensor> DeleteSensor(int? idSensor)
        {
            var itemToDelete = Context.Sensors
                              .Where(i => i.id_sensor == idSensor)
                              .Include(i => i.NivelRadiacaos)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnSensorDeleted(itemToDelete);

            Context.Sensors.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterSensorDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnSensorGet(Models.RadnetBd.Sensor item);

        public async Task<Models.RadnetBd.Sensor> GetSensorByidSensor(int? idSensor)
        {
            var items = Context.Sensors
                              .AsNoTracking()
                              .Where(i => i.id_sensor == idSensor);

            items = items.Include(i => i.Estacao);

            items = items.Include(i => i.GrauSensibilidade);

            var itemToReturn = items.FirstOrDefault();

            OnSensorGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.RadnetBd.Sensor> CancelSensorChanges(Models.RadnetBd.Sensor item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnSensorUpdated(Models.RadnetBd.Sensor item);
        partial void OnAfterSensorUpdated(Models.RadnetBd.Sensor item);

        public async Task<Models.RadnetBd.Sensor> UpdateSensor(int? idSensor, Models.RadnetBd.Sensor sensor)
        {
            OnSensorUpdated(sensor);

            var itemToUpdate = Context.Sensors
                              .Where(i => i.id_sensor == idSensor)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(sensor);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterSensorUpdated(sensor);

            return sensor;
        }

        partial void OnValorReferenciumDeleted(Models.RadnetBd.ValorReferencium item);
        partial void OnAfterValorReferenciumDeleted(Models.RadnetBd.ValorReferencium item);

        public async Task<Models.RadnetBd.ValorReferencium> DeleteValorReferencium(int? idReferencia)
        {
            var itemToDelete = Context.ValorReferencia
                              .Where(i => i.id_referencia == idReferencia)
                              .Include(i => i.NivelRadiacaos)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnValorReferenciumDeleted(itemToDelete);

            Context.ValorReferencia.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterValorReferenciumDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnValorReferenciumGet(Models.RadnetBd.ValorReferencium item);

        public async Task<Models.RadnetBd.ValorReferencium> GetValorReferenciumByidReferencia(int? idReferencia)
        {
            var items = Context.ValorReferencia
                              .AsNoTracking()
                              .Where(i => i.id_referencia == idReferencia);

            var itemToReturn = items.FirstOrDefault();

            OnValorReferenciumGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.RadnetBd.ValorReferencium> CancelValorReferenciumChanges(Models.RadnetBd.ValorReferencium item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnValorReferenciumUpdated(Models.RadnetBd.ValorReferencium item);
        partial void OnAfterValorReferenciumUpdated(Models.RadnetBd.ValorReferencium item);

        public async Task<Models.RadnetBd.ValorReferencium> UpdateValorReferencium(int? idReferencia, Models.RadnetBd.ValorReferencium valorReferencium)
        {
            OnValorReferenciumUpdated(valorReferencium);

            var itemToUpdate = Context.ValorReferencia
                              .Where(i => i.id_referencia == idReferencia)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(valorReferencium);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterValorReferenciumUpdated(valorReferencium);

            return valorReferencium;
        }
    }
}
