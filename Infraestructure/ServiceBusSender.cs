using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using SistemaPago.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaPago.Infraestructure
{
    public class ServiceBusSender
    {
        private readonly string _serviceBusConnectionString;
        private const string QueueNameFarmacia = "farmacia";
        private const string QueueNamePago = "pago";
        private const string QueueNameDietas = "dietas";

        public ServiceBusSender(string serviceBusConnectionString)
        {
            _serviceBusConnectionString = serviceBusConnectionString;
        }

        public async Task EnviarRecetaAsync(RecetaMedica receta, string cola)
        {
            var client = new QueueClient(_serviceBusConnectionString, cola);

            var message = new Message(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(receta)));

            await client.SendAsync(message);

            await client.CloseAsync();
        }


        public async Task EnviarRecetaFarmaciaAsync(RecetaMedica receta)
        {
            await EnviarRecetaAsync(receta, QueueNameFarmacia);
        }

        public async Task EnviarRecetaPagoAsync(RecetaMedica receta)
        {
            await EnviarRecetaAsync(receta, QueueNamePago);
        }

        public async Task EnviarRecetaDietasAsync(RecetaMedica receta)
        {
            await EnviarRecetaAsync(receta, QueueNameDietas);
        }
    }
}
