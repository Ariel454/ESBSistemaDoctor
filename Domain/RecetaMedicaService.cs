using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaPago.Domain
{
    public class RecetaMedicaService
    {
        public RecetaMedica CrearRecetaMedica(string paciente, string medicamento, string dosis)
        {
            return new RecetaMedica
            {
                Paciente = paciente,
                Medicamento = medicamento,
                Dosis = dosis,
                FechaEmision = DateTime.UtcNow
            };
        }
    }
}
