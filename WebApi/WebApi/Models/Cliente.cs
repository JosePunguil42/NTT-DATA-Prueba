using System;
using System.Collections.Generic;

#nullable disable

namespace WebApi.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            Cuenta = new HashSet<Cuenta>();
        }

        public int ClIdCliente { get; set; }
        public string ClIdentificacion { get; set; }
        public string ClContrasenia { get; set; }
        public bool ClEstado { get; set; }
        public string ClNombre { get; set; }
        public string ClGenero { get; set; }
        public string ClEdad { get; set; }
        public string ClDireccion { get; set; }
        public string ClTelefono { get; set; }

        public virtual ICollection<Cuenta> Cuenta { get; set; }
    }
}
