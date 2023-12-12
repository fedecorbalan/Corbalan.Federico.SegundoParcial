using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsPrimerParcial
{
    /// <summary>
    /// Representa a un usuario del sistema.
    /// </summary>
    public class Usuario
    {
        /// <summary>
        /// Prooiedades que obtienen o establecen el nombre, apellio, correo,
        /// perfil, legajo y/o clave ddel usuario.
        /// </summary>
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string correo { get; set; }
        public string perfil { get; set; }
        public int legajo { get; set; }
        public string clave { get; set; }
        /// <summary>
        /// Constructor predeterminado de la clase Usuario.
        /// </summary>
        public Usuario() { }
        /// <summary>
        /// Constructor parametrizado de la clase Usuario.
        /// </summary>
        /// <param name="nombre">Nombre del usuario.</param>
        /// <param name="apellido">Apellido del usuario.</param>
        /// <param name="correo">Correo electrónico del usuario.</param>
        /// <param name="perfil">Perfil del usuario.</param>
        /// <param name="legajo">Número de legajo del usuario.</param>
        /// <param name="clave">Clave del usuario.</param>
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
