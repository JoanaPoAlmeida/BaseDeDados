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
    public partial class AddGrauSensibilidadeComponent : ComponentBase
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

        Radnet.Models.RadnetBd.GrauSensibilidade _grausensibilidade;
        protected Radnet.Models.RadnetBd.GrauSensibilidade grausensibilidade
        {
            get
            {
                return _grausensibilidade;
            }
            set
            {
                if (!object.Equals(_grausensibilidade, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "grausensibilidade", NewValue = value, OldValue = _grausensibilidade };
                    _grausensibilidade = value;
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
            grausensibilidade = new Radnet.Models.RadnetBd.GrauSensibilidade(){};
        }

        protected async System.Threading.Tasks.Task Form0Submit(Radnet.Models.RadnetBd.GrauSensibilidade args)
        {
            try
            {
                var radnetBdCreateGrauSensibilidadeResult = await RadnetBd.CreateGrauSensibilidade(grausensibilidade);
                DialogService.Close(grausensibilidade);
            }
            catch (System.Exception radnetBdCreateGrauSensibilidadeException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to create new GrauSensibilidade!" });
            }
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
