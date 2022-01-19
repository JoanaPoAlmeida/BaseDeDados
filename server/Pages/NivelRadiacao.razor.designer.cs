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
    public partial class NivelRadiacaoComponent : ComponentBase
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
        protected RadzenDataGrid<Radnet.Models.RadnetBd.NivelRadiacao> grid0;

        IEnumerable<Radnet.Models.RadnetBd.NivelRadiacao> _getNivelRadiacaosResult;
        protected IEnumerable<Radnet.Models.RadnetBd.NivelRadiacao> getNivelRadiacaosResult
        {
            get
            {
                return _getNivelRadiacaosResult;
            }
            set
            {
                if (!object.Equals(_getNivelRadiacaosResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getNivelRadiacaosResult", NewValue = value, OldValue = _getNivelRadiacaosResult };
                    _getNivelRadiacaosResult = value;
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
            var radnetBdGetNivelRadiacaosResult = await RadnetBd.GetNivelRadiacaos(new Query() { Expand = "ValorReferencium,Sensor" });
            getNivelRadiacaosResult = radnetBdGetNivelRadiacaosResult;
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            var dialogResult = await DialogService.OpenAsync<AddNivelRadiacao>("Add Nivel Radiacao", null);
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Grid0RowSelect(Radnet.Models.RadnetBd.NivelRadiacao args)
        {
            var dialogResult = await DialogService.OpenAsync<EditNivelRadiacao>("Edit Nivel Radiacao", new Dictionary<string, object>() { {"id_nivel", args.id_nivel} });
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var radnetBdDeleteNivelRadiacaoResult = await RadnetBd.DeleteNivelRadiacao(data.id_nivel);
                    if (radnetBdDeleteNivelRadiacaoResult != null)
                    {
                        await grid0.Reload();
                    }
                }
            }
            catch (System.Exception radnetBdDeleteNivelRadiacaoException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to delete NivelRadiacao" });
            }
        }
    }
}
