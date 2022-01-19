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
    public partial class LeituraComponent : ComponentBase
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
        protected RadzenDataGrid<Radnet.Models.RadnetBd.Leitura> grid0;

        IEnumerable<Radnet.Models.RadnetBd.Leitura> _getLeiturasResult;
        protected IEnumerable<Radnet.Models.RadnetBd.Leitura> getLeiturasResult
        {
            get
            {
                return _getLeiturasResult;
            }
            set
            {
                if (!object.Equals(_getLeiturasResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getLeiturasResult", NewValue = value, OldValue = _getLeiturasResult };
                    _getLeiturasResult = value;
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
            var radnetBdGetLeiturasResult = await RadnetBd.GetLeituras();
            getLeiturasResult = radnetBdGetLeiturasResult;
        }
    }
}
