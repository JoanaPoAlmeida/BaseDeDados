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
    public partial class ValorReferenciumComponent : ComponentBase
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
        protected RadzenDataGrid<Radnet.Models.RadnetBd.ValorReferencium> grid0;

        IEnumerable<Radnet.Models.RadnetBd.ValorReferencium> _getValorReferenciaResult;
        protected IEnumerable<Radnet.Models.RadnetBd.ValorReferencium> getValorReferenciaResult
        {
            get
            {
                return _getValorReferenciaResult;
            }
            set
            {
                if (!object.Equals(_getValorReferenciaResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getValorReferenciaResult", NewValue = value, OldValue = _getValorReferenciaResult };
                    _getValorReferenciaResult = value;
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
            var radnetBdGetValorReferenciaResult = await RadnetBd.GetValorReferencia(new Query() { Expand = "NivelRadiacaos" });
            getValorReferenciaResult = radnetBdGetValorReferenciaResult;
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            var dialogResult = await DialogService.OpenAsync<AddValorReferencium>("Add Valor Referencium", null);
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Grid0RowSelect(Radnet.Models.RadnetBd.ValorReferencium args)
        {
            var dialogResult = await DialogService.OpenAsync<EditValorReferencium>("Edit Valor Referencium", new Dictionary<string, object>() { {"id_referencia", args.id_referencia} });
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var radnetBdDeleteValorReferenciumResult = await RadnetBd.DeleteValorReferencium(data.id_referencia);
                    if (radnetBdDeleteValorReferenciumResult != null)
                    {
                        await grid0.Reload();
                    }
                }
            }
            catch (System.Exception radnetBdDeleteValorReferenciumException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to delete ValorReferencium" });
            }
        }
    }
}
