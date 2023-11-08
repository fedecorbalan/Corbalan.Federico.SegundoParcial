using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsPrimerParcial
{
    public class Usuario
    { 
        public string nombre { get; set; }

        public string apellido { get; set; }

        public string correo { get; set; }
        public string perfil { get; set; }

        public int legajo { get; set; }
        public string clave { get; set; }

        public Usuario() { }
        public Usuario(string nombre, string apellido, string correo, string perfil, int legajo, string clave) 
        { 
            this.perfil = perfil;
            this.nombre = nombre;
            this.apellido = apellido;
            this.correo = correo;
            this.clave = clave;
            this.legajo = legajo;
        }
        

    }
}
