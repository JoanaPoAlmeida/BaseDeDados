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
    public partial class EditNivelRadiacaoComponent : ComponentBase
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

        [Parameter]
        public dynamic id_nivel { get; set; }

        Radnet.Models.RadnetBd.NivelRadiacao _nivelradiacao;
        protected Radnet.Models.RadnetBd.NivelRadiacao nivelradiacao
        {
            get
            {
                return _nivelradiacao;
            }
            set
            {
                if (!object.Equals(_nivelradiacao, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "nivelradiacao", NewValue = value, OldValue = _nivelradiacao };
                    _nivelradiacao = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        IEnumerable<Radnet.Models.RadnetBd.Sensor> _getSensorsResult;
        protected IEnumerable<Radnet.Models.RadnetBd.Sensor> getSensorsResult
        {
            get
            {
                return _getSensorsResult;
            }
            set
            {
                if (!object.Equals(_getSensorsResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getSensorsResult", NewValue = value, OldValue = _getSensorsResult };
                    _getSensorsResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

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
            var radnetBdGetNivelRadiacaoByidNivelResult = await RadnetBd.GetNivelRadiacaoByidNivel(id_nivel);
            nivelradiacao = radnetBdGetNivelRadiacaoByidNivelResult;

            var radnetBdGetSensorsResult = await RadnetBd.GetSensors();
            getSensorsResult = radnetBdGetSensorsResult;

            var radnetBdGetValorReferenciaResult = await RadnetBd.GetValorReferencia();
            getValorReferenciaResult = radnetBdGetValorReferenciaResult;
        }

        protected async System.Threading.Tasks.Task Form0Submit(Radnet.Models.RadnetBd.NivelRadiacao args)
        {
            try
            {
                var radnetBdUpdateNivelRadiacaoResult = await RadnetBd.UpdateNivelRadiacao(id_nivel, nivelradiacao);
                DialogService.Close(nivelradiacao);
            }
            catch (System.Exception radnetBdUpdateNivelRadiacaoException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to update NivelRadiacao" });
            }
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
