using SistemaPago.Domain;
using SistemaPago.Infraestructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaPago.Application
{
    public class RecetaMedicaApplicationService
    {
        private readonly RecetaMedicaService _recetaMedicaService;
        private readonly ServiceBusSender _serviceBusSender;

        public RecetaMedicaApplicationService(RecetaMedicaService recetaMedicaService, ServiceBusSender serviceBusSender)
        {
            _recetaMedicaService = recetaMedicaService;
            _serviceBusSender = serviceBusSender;
        }

        public async Task EmitirRecetaMedicaAsync(string paciente, string medicamento, string dosis)
        {
            var recetaMedica = _recetaMedicaService.CrearRecetaMedica(paciente, medicamento, dosis);

            await _serviceBusSender.EnviarRecetaAsync(recetaMedica, "farmacia");
            await _serviceBusSender.EnviarRecetaAsync(recetaMedica, "pago");
            await _serviceBusSender.EnviarRecetaAsync(recetaMedica, "dietas");
        }
    }
}
