using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Radnet.Data;

namespace Radnet
{
    public partial class ExportRadnetBdController : ExportController
    {
        private readonly RadnetBdContext context;
        private readonly RadnetBdService service;
        public ExportRadnetBdController(RadnetBdContext context, RadnetBdService service)
        {
            this.service = service;
            this.context = context;
        }

        [HttpGet("/export/RadnetBd/estacaos/csv")]
        [HttpGet("/export/RadnetBd/estacaos/csv(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportEstacaosToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetEstacaos(), Request.Query), fileName);
        }

        [HttpGet("/export/RadnetBd/estacaos/excel")]
        [HttpGet("/export/RadnetBd/estacaos/excel(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportEstacaosToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetEstacaos(), Request.Query), fileName);
        }
        [HttpGet("/export/RadnetBd/grausensibilidades/csv")]
        [HttpGet("/export/RadnetBd/grausensibilidades/csv(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportGrauSensibilidadesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetGrauSensibilidades(), Request.Query), fileName);
        }

        [HttpGet("/export/RadnetBd/grausensibilidades/excel")]
        [HttpGet("/export/RadnetBd/grausensibilidades/excel(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportGrauSensibilidadesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetGrauSensibilidades(), Request.Query), fileName);
        }
        [HttpGet("/export/RadnetBd/leituras/csv")]
        [HttpGet("/export/RadnetBd/leituras/csv(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportLeiturasToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetLeituras(), Request.Query), fileName);
        }

        [HttpGet("/export/RadnetBd/leituras/excel")]
        [HttpGet("/export/RadnetBd/leituras/excel(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportLeiturasToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetLeituras(), Request.Query), fileName);
        }
        [HttpGet("/export/RadnetBd/nivelradiacaos/csv")]
        [HttpGet("/export/RadnetBd/nivelradiacaos/csv(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportNivelRadiacaosToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetNivelRadiacaos(), Request.Query), fileName);
        }

        [HttpGet("/export/RadnetBd/nivelradiacaos/excel")]
        [HttpGet("/export/RadnetBd/nivelradiacaos/excel(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportNivelRadiacaosToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetNivelRadiacaos(), Request.Query), fileName);
        }
        [HttpGet("/export/RadnetBd/sensors/csv")]
        [HttpGet("/export/RadnetBd/sensors/csv(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportSensorsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetSensors(), Request.Query), fileName);
        }

        [HttpGet("/export/RadnetBd/sensors/excel")]
        [HttpGet("/export/RadnetBd/sensors/excel(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportSensorsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetSensors(), Request.Query), fileName);
        }
        [HttpGet("/export/RadnetBd/valorreferencia/csv")]
        [HttpGet("/export/RadnetBd/valorreferencia/csv(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportValorReferenciaToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetValorReferencia(), Request.Query), fileName);
        }

        [HttpGet("/export/RadnetBd/valorreferencia/excel")]
        [HttpGet("/export/RadnetBd/valorreferencia/excel(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportValorReferenciaToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetValorReferencia(), Request.Query), fileName);
        }
    }
}
