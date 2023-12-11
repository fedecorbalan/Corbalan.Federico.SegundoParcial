using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PrimerParcial
{
    public class Hornero : Animal
    {
        public bool tieneAlas;
        public int velocidadKmH;

        public Hornero() 
        {
            this.tieneAlas = true;
            this.velocidadKmH = 30;
        }
        public Hornero(int velocidadKmH, bool tieneAlas, string nombre, bool esPeludo, Eespecies especie) : base(nombre, esPeludo, especie)
        {
            this.tieneAlas = tieneAlas;
            this.velocidadKmH = velocidadKmH;
        }

        public override string Mostrar()
        {
            StringBuilder sb = new StringBuilder();

            string rta = base.Mostrar();

            sb.Append(rta);
            sb.Append($"- Animal: Hornero - Tiene alas: {this.tieneAlas} - Velocidad de vuelo en KM/H: {this.velocidadKmH}");

            return sb.ToString();

        }
        public override string ToString()
        {
            return this.Mostrar();
        }
        public override string EmitirSonido()
        {
            string sonidoHornero = "Pi-pi-pi!";
            return sonidoHornero;
        }

        public override int CantidadExtremidades()
        {
            return 2;
        }
        public int CantidadExtremidades(int cantidadExtremidades)
        {
            return cantidadExtremidades;
        }

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