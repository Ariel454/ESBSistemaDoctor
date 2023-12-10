using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using SistemaPago.Application;
using SistemaPago.Domain;
using SistemaPago.Infraestructure;
using System;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        Console.WriteLine("Sistema del Doctor: Emitiendo receta médica...");

        // Configuración de servicios
        var recetaMedicaService = new RecetaMedicaService();
        var serviceBusSender = new ServiceBusSender("Endpoint=sb://arquitecturaar.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=liz2Rdnrrk71JmuXcI3xyWnanI36FTj2h+ASbNqRNXI="); // Reemplazar con tu cadena de conexión

        var recetaMedicaApplicationService = new RecetaMedicaApplicationService(recetaMedicaService, serviceBusSender);

        // Ejemplo de emisión de receta médica
        await recetaMedicaApplicationService.EmitirRecetaMedicaAsync("Ariel Raura", "IBUPROFENO", "2 tabletas cada 6 horas");

        Console.WriteLine("Receta médica enviada con éxito. Presiona cualquier tecla para salir.");
        Console.ReadKey();
    }
}