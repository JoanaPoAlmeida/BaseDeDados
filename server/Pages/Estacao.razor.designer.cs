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
    public partial class EstacaoComponent : ComponentBase
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
        protected RadzenDataGrid<Radnet.Models.RadnetBd.Estacao> grid0;

        IEnumerable<Radnet.Models.RadnetBd.Estacao> _getEstacaosResult;
        protected IEnumerable<Radnet.Models.RadnetBd.Estacao> getEstacaosResult
        {
            get
            {
                return _getEstacaosResult;
            }
            set
            {
                if (!object.Equals(_getEstacaosResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getEstacaosResult", NewValue = value, OldValue = _getEstacaosResult };
                    _getEstacaosResult = value;
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
            var radnetBdGetEstacaosResult = await RadnetBd.GetEstacaos(new Query() { Expand = "Sensors" });
            getEstacaosResult = radnetBdGetEstacaosResult;
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            var dialogResult = await DialogService.OpenAsync<AddEstacao>("Add Estacao", null);
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Grid0RowSelect(Radnet.Models.RadnetBd.Estacao args)
        {
            var dialogResult = await DialogService.OpenAsync<EditEstacao>("Edit Estacao", new Dictionary<string, object>() { {"id_estacao", args.id_estacao} });
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var radnetBdDeleteEstacaoResult = await RadnetBd.DeleteEstacao(data.id_estacao);
                    if (radnetBdDeleteEstacaoResult != null)
                    {
                        await grid0.Reload();
                    }
                }
            }
            catch (System.Exception radnetBdDeleteEstacaoException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to delete Estacao" });
            }
        }
    }
}
