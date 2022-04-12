using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class Persona
    {
        public string Identificacion { get; set; }
        public string Nombre { get; set; }
        public string Genero { get; set; }
        public string Edad { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
    }
}
