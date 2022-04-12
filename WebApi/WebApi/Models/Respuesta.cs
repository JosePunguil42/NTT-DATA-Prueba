using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class Respuesta
    {
        public bool Ejecutado { get; set; }
        public string Mensaje { get; set; }
        public string Codigo { get; set; }
        public object Objeto { get; set; }

    }
}
