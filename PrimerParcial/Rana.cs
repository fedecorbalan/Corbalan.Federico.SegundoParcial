using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PrimerParcial
{
    public class Rana : Animal
    {
        public bool esVenenosa;
        public bool esArboricola;

        public Rana() { }
        public Rana(bool esVenenosa,string nombre, bool esPeludo, Eespecies especie) : base(nombre, esPeludo, especie)
        { 
            this.esVenenosa = esVenenosa;
        }
        public Rana(bool esArboricola,bool esVenenosa, bool esPeludo, Eespecies especie,string nombre) : this(esVenenosa,nombre, esPeludo, especie)
        {
            this.esArboricola = esArboricola;
        }

        public override string Mostrar()
        {
            StringBuilder sb = new StringBuilder();

            string rta = base.Mostrar();

            sb.Append(rta);
            sb.AppendLine($"- Animal: Rana - Es venenosa: {this.esVenenosa} - Es arboricola: {this.esArboricola}");

            return sb.ToString();
        }
        public override string ToString()
        {
            return this.Mostrar();
        }
        public override string EmitirSonido()
        {
            string sonidoRana = "Croac!";
            return sonidoRana;
        }
        public override int CantidadExtremidades()
        {
            return 4;
        }
        public int CantidadExtremidades(int cantidadExtremidades)
        {
            return cantidadExtremidades;
        }
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
