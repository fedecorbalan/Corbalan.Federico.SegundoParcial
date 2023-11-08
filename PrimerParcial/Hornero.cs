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
        public int velocidadVueloKMporH;

        public Hornero() 
        {
            this.tieneAlas = true;
            this.velocidadVueloKMporH = 30;
        }
        public Hornero(bool tieneAlas, string nombre, bool esPeludo, Eespecies especie) : base(nombre, esPeludo, especie)
        {
            this.tieneAlas = tieneAlas;
        }
        public Hornero(int velocidadVueloKMporH,bool tieneAlas, bool esPeludo, Eespecies especie, string nombre) : this(tieneAlas, nombre, esPeludo, especie)
        {
            this.tieneAlas = tieneAlas;
            this.velocidadVueloKMporH = velocidadVueloKMporH;
        }

        public override string Mostrar()
        {
            StringBuilder sb = new StringBuilder();

            string rta = base.Mostrar();

            sb.Append(rta);
            sb.Append($"- Animal: Hornero - Tiene alas: {this.tieneAlas} - Velocidad de vuelo en KM/H{this.velocidadVueloKMporH}");

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
    }
}