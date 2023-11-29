using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

class Program
{
    const string ServiceBusConnectionString = "Endpoint=sb://arquitecturaar.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=liz2Rdnrrk71JmuXcI3xyWnanI36FTj2h+ASbNqRNXI="; // Reemplaza con tu cadena de conexión
    const string QueueNameFarmacia = "farmacia";
    const string QueueNamePago = "pago";
    const string QueueNameDietas = "dietas";

    static async Task Main()
    {
        Console.WriteLine("Sistema del Doctor: Emitiendo receta médica...");

        var recetaMedica = CrearRecetaMedicaEjemplo();

        await EnviarRecetaAsync(recetaMedica, QueueNameFarmacia);
        await EnviarRecetaAsync(recetaMedica, QueueNamePago);
        await EnviarRecetaAsync(recetaMedica, QueueNameDietas);

        Console.WriteLine("Receta médica enviada con éxito. Presiona cualquier tecla para salir.");
        Console.ReadKey();
    }

    static async Task EnviarRecetaAsync(MensajeRecetaMedica receta, string cola)
    {
        var client = new QueueClient(ServiceBusConnectionString, cola);

        var message = new Message(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(receta)));

        await client.SendAsync(message);

        await client.CloseAsync();
    }

    static MensajeRecetaMedica CrearRecetaMedicaEjemplo()
    {
        // Puedes personalizar esta función para crear una receta médica de ejemplo según tus necesidades.
        return new MensajeRecetaMedica
        {
            Paciente = "Ariel Raura",
            Medicamento = "IBUPROFENO",
            Dosis = "2 tabletas cada 6 horas",
            FechaEmision = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ")
        };
    }
}

class MensajeRecetaMedica
{
    public string Paciente { get; set; }
    public string Medicamento { get; set; }
    public string Dosis { get; set; }
    public string FechaEmision { get; set; }
}
