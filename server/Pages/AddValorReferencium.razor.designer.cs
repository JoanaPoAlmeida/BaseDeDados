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
    public partial class AddValorReferenciumComponent : ComponentBase
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

        Radnet.Models.RadnetBd.ValorReferencium _valorreferencium;
        protected Radnet.Models.RadnetBd.ValorReferencium valorreferencium
        {
            get
            {
                return _valorreferencium;
            }
            set
            {
                if (!object.Equals(_valorreferencium, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "valorreferencium", NewValue = value, OldValue = _valorreferencium };
                    _valorreferencium = value;
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
            valorreferencium = new Radnet.Models.RadnetBd.ValorReferencium(){};
        }

        protected async System.Threading.Tasks.Task Form0Submit(Radnet.Models.RadnetBd.ValorReferencium args)
        {
            try
            {
                var radnetBdCreateValorReferenciumResult = await RadnetBd.CreateValorReferencium(valorreferencium);
                DialogService.Close(valorreferencium);
            }
            catch (System.Exception radnetBdCreateValorReferenciumException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to create new ValorReferencium!" });
            }
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
