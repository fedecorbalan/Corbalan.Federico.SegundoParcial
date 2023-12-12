using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PrimerParcial
{
    /// <summary>
    /// Representa un Hornero, que es una clase derivada de Animal.
    /// </summary>
    public class Hornero : Animal
    {
        /// <summary>
        /// Se declaran variables que indican si el Hornero tiene alas y su velocidad en Km/H.
        /// </summary>
        public bool tieneAlas;
        public int velocidadKmH;

        /// <summary>
        /// Constructor predeterminado de la clase Hornero.
        /// </summary>
        public Hornero() 
        {
            this.tieneAlas = true;
            this.velocidadKmH = 30;
        }
        /// <summary>
        /// Constructor parametrizado de la clase Hornero.
        /// </summary>
        /// <param name="velocidadKmH">La velocidad de vuelo en kilómetros por hora.</param>
        /// <param name="tieneAlas">Indica si el Hornero tiene alas.</param>
        /// <param name="nombre">El nombre del Hornero.</param>
        /// <param name="esPeludo">Indica si el Hornero es peludo.</param>
        /// <param name="especie">La especie del Hornero.</param>
        public Hornero(int velocidadKmH, bool tieneAlas, string nombre, bool esPeludo, Eespecies especie) : base(nombre, esPeludo, especie)
        {
            this.tieneAlas = tieneAlas;
            this.velocidadKmH = velocidadKmH;
        }
        /// <summary>
        /// Devuelve una representación en cadena del Hornero.
        /// </summary>
        /// <returns>Una cadena que representa el Hornero.</returns>
        public override string Mostrar()
        {
            StringBuilder sb = new StringBuilder();

            string rta = base.Mostrar();

            sb.Append(rta);
            sb.Append($"- Animal: Hornero - Tiene alas: {this.tieneAlas} - Velocidad de vuelo en KM/H: {this.velocidadKmH}");

            return sb.ToString();

        }

        /// <summary>
        /// Devuelve una representación en cadena del Hornero utilizando el método Mostrar.
        /// </summary>
        /// <returns>Una cadena que representa el Hornero.</returns>
        public override string ToString()
        {
            return this.Mostrar();
        }
        /// <summary>
        /// Implementación del método para emitir el sonido del Hornero.
        /// </summary>
        /// <returns>El sonido emitido por el Hornero.</returns>
        public override string EmitirSonido()
        {
            string sonidoHornero = "Pi-pi-pi!";
            return sonidoHornero;
        }
        /// <summary>
        /// Devuelve la cantidad de extremidades del Hornero, que es 2.
        /// </summary>
        /// <returns>La cantidad de extremidades del Hornero.</returns>
        public override int CantidadExtremidades()
        {
            return 2;
        }
        /// <summary>
        /// Actualiza las propiedades del Hornero con las propiedades del Hornero proporcionado.
        /// </summary>
        /// <param name="h">El Hornero con las propiedades actualizadas.</param>
        public void ActualizarHornero(Hornero h)
        {
            this.nombre = h.nombre;
            this.especie = h.especie;
            this.esPeludo = h.esPeludo;
            this.tieneAlas = h.tieneAlas;
            this.velocidadKmH = h.velocidadKmH;
        }

    }
}