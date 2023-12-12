using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PrimerParcial
{
    /// <summary>
    /// Representa una Rana, que es una clase derivada de Animal.
    /// </summary>
    public class Rana : Animal
    {
        /// <summary>
        /// Se declaran variables que indican si la Rana es venenosa y/o arboricola.
        /// </summary>
        public bool esVenenosa;
        public bool esArboricola;
        /// <summary>
        /// Constructor predeterminado de la clase Rana.
        /// </summary>
        public Rana() 
        { 
            this.esVenenosa = false;
            this.esArboricola = false;
        }
        /// <summary>
        /// Constructor parametrizado de la clase Rana.
        /// </summary>
        /// <param name="esArboricola">Indica si la Rana es arborícola.</param>
        /// <param name="esVenenosa">Indica si la Rana es venenosa.</param>
        /// <param name="nombre">El nombre de la Rana.</param>
        /// <param name="esPeludo">Indica si la Rana es peluda.</param>
        /// <param name="especie">La especie de la Rana.</param>
        public Rana(bool esArboricola, bool esVenenosa,string nombre, bool esPeludo, Eespecies especie) : base(nombre, esPeludo, especie)
        { 
            this.esVenenosa = esVenenosa;
            this.esArboricola = esArboricola;
        }

        /// <summary>
        /// Devuelve una representación en cadena de la Rana.
        /// </summary>
        /// <returns>Una cadena que representa la Rana.</returns>
        public override string Mostrar()
        {
            StringBuilder sb = new StringBuilder();

            string rta = base.Mostrar();

            sb.Append(rta);
            sb.AppendLine($"- Animal: Rana - Es venenosa: {this.esVenenosa} - Es arboricola: {this.esArboricola}");

            return sb.ToString();
        }
        /// <summary>
        /// Devuelve una representación en cadena de la Rana utilizando el método Mostrar.
        /// </summary>
        /// <returns>Una cadena que representa la Rana.</returns>
        public override string ToString()
        {
            return this.Mostrar();
        }
        /// <summary>
        /// Implementación del método para emitir el sonido de la Rana.
        /// </summary>
        /// <returns>El sonido emitido por la Rana.</returns>
        public override string EmitirSonido()
        {
            string sonidoRana = "Croac!";
            return sonidoRana;
        }
        /// <summary>
        /// Devuelve la cantidad de extremidades de la Rana, que es 4.
        /// </summary>
        /// <returns>La cantidad de extremidades de la Rana.</returns>
        public override int CantidadExtremidades()
        {
            return 4;
        }
        /// <summary>
        /// Actualiza las propiedades de la Rana con las propiedades de la Rana proporcionada.
        /// </summary>
        /// <param name="r">La Rana con las propiedades actualizadas.</param>
        public void ActualizarRana(Rana r)
        {
            this.nombre = r.nombre;
            this.especie = r.especie;
            this.esPeludo = r.esPeludo;
            this.esVenenosa = r.esVenenosa;
            this.esArboricola = r.esArboricola;
        }
    }
}
