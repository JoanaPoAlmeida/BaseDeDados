using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;
using Radnet.Models.RadnetBd;
using Microsoft.EntityFrameworkCore;

namespace Radnet.Pages
{
    public partial class GrauSensibilidadeComponent : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)]
        public IReadOnlyDictionary<string, dynamic> Attributes { get; set; }

        public void Reload()
        {
            InvokeAsync(StateHasChanged);
        }

        public void OnPropertyChanged(PropertyChangedEventArgs args)
        {
        }

        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected NavigationManager UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected TooltipService TooltipService { get; set; }

        [Inject]
        protected ContextMenuService ContextMenuService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        [Inject]
        protected RadnetBdService RadnetBd { get; set; }
        protected RadzenDataGrid<Radnet.Models.RadnetBd.GrauSensibilidade> grid0;

        IEnumerable<Radnet.Models.RadnetBd.GrauSensibilidade> _getGrauSensibilidadesResult;
        protected IEnumerable<Radnet.Models.RadnetBd.GrauSensibilidade> getGrauSensibilidadesResult
        {
            get
            {
                return _getGrauSensibilidadesResult;
            }
            set
            {
                if (!object.Equals(_getGrauSensibilidadesResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getGrauSensibilidadesResult", NewValue = value, OldValue = _getGrauSensibilidadesResult };
                    _getGrauSensibilidadesResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
            await Load();
        }
        protected async System.Threading.Tasks.Task Load()
        {
            var radnetBdGetGrauSensibilidadesResult = await RadnetBd.GetGrauSensibilidades(new Query() { Expand = "Sensors" });
            getGrauSensibilidadesResult = radnetBdGetGrauSensibilidadesResult;
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            var dialogResult = await DialogService.OpenAsync<AddGrauSensibilidade>("Add Grau Sensibilidade", null);
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Grid0RowSelect(Radnet.Models.RadnetBd.GrauSensibilidade args)
        {
            var dialogResult = await DialogService.OpenAsync<EditGrauSensibilidade>("Edit Grau Sensibilidade", new Dictionary<string, object>() { {"id_Grau", args.id_Grau} });
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var radnetBdDeleteGrauSensibilidadeResult = await RadnetBd.DeleteGrauSensibilidade(data.id_Grau);
                    if (radnetBdDeleteGrauSensibilidadeResult != null)
                    {
                        await grid0.Reload();
                    }
                }
            }
            catch (System.Exception radnetBdDeleteGrauSensibilidadeException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to delete GrauSensibilidade" });
            }
        }
    }
}
