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
    public partial class AddEstacaoComponent : ComponentBase
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

        Radnet.Models.RadnetBd.Estacao _estacao;
        protected Radnet.Models.RadnetBd.Estacao estacao
        {
            get
            {
                return _estacao;
            }
            set
            {
                if (!object.Equals(_estacao, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "estacao", NewValue = value, OldValue = _estacao };
                    _estacao = value;
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
            estacao = new Radnet.Models.RadnetBd.Estacao(){};
        }

        protected async System.Threading.Tasks.Task Form0Submit(Radnet.Models.RadnetBd.Estacao args)
        {
            try
            {
                var radnetBdCreateEstacaoResult = await RadnetBd.CreateEstacao(estacao);
                DialogService.Close(estacao);
            }
            catch (System.Exception radnetBdCreateEstacaoException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to create new Estacao!" });
            }
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
