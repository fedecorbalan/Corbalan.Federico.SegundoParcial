using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PrimerParcial
{
    /// <summary>
    /// Representa un Ornitorrinco, que es una clase derivada de Animal.
    /// </summary>
    public class Ornitorrinco : Animal
    {
        /// <summary>
        /// Se declaran variables que indican si el Ornitorrinco es ovíparo y/o tiene cola.
        /// </summary>
        public bool oviparo;
        public bool tieneCola;

        /// <summary>
        /// Constructor predeterminado de la clase Ornitorrinco.
        /// </summary>
        public Ornitorrinco() 
        { 
            this.oviparo = true;
            this.tieneCola = true;
        }

        /// <summary>
        /// Constructor parametrizado de la clase Ornitorrinco.
        /// </summary>
        /// <param name="tieneCola">Indica si el Ornitorrinco tiene cola.</param>
        /// <param name="oviparo">Indica si el Ornitorrinco es ovíparo.</param>
        /// <param name="esPeludo">Indica si el Ornitorrinco es peludo.</param>
        /// <param name="especie">La especie del Ornitorrinco.</param>
        /// <param name="nombre">El nombre del Ornitorrinco.</param>
        public Ornitorrinco(bool tieneCola, bool oviparo, bool esPeludo, Eespecies especie, string nombre) : base(nombre, esPeludo, especie)
        {
            this.oviparo = oviparo;
            this.tieneCola = tieneCola;
        }
        /// <summary>
        /// Devuelve una representación en cadena del Ornitorrinco.
        /// </summary>
        /// <returns>Una cadena que representa el Ornitorrinco.</returns>
        public override string Mostrar()
        {
            StringBuilder sb = new StringBuilder();

            string rta = base.Mostrar();

            sb.Append(rta);
            sb.AppendLine($"- Animal: Ornitorrinco - Es oviparo: {this.oviparo} - Tiene cola: {this.tieneCola}");

            return sb.ToString();
        }
        /// <summary>
        /// Devuelve una representación en cadena del Ornitorrinco utilizando el método Mostrar.
        /// </summary>
        /// <returns>Una cadena que representa el Ornitorrinco.</returns>
        public override string ToString()
        {
            return this.Mostrar();
        }
        /// <summary>
        /// Implementación del método para emitir el sonido del Ornitorrinco.
        /// </summary>
        /// <returns>El sonido emitido por el Ornitorrinco.</returns>
        public override string EmitirSonido()
        {
            string sonidoOrnitorrinco = "Grrrr!";
            return sonidoOrnitorrinco;
        }

        /// <summary>
        /// Devuelve la cantidad de extremidades del Ornitorrinco, que es 4.
        /// </summary>
        /// <returns>La cantidad de extremidades del Ornitorrinco.</returns>
        public override int CantidadExtremidades()
        {
            return 4;
        }
        /// <summary>
        /// Actualiza las propiedades del Ornitorrinco con las propiedades del Ornitorrinco proporcionado.
        /// </summary>
        /// <param name="o">El Ornitorrinco con las propiedades actualizadas.</param>
        public void ActualizarOrnitorrinco(Ornitorrinco o)
        {
            this.nombre = o.nombre;
            this.especie = o.especie;
            this.esPeludo = o.esPeludo;
            this.tieneCola = o.tieneCola;
            this.oviparo = o.oviparo;
        }
    }
}
