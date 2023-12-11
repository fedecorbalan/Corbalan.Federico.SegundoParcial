using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PrimerParcial
{
    public class Ornitorrinco : Animal
    {
        public bool oviparo;
        public bool tieneCola;

        public Ornitorrinco() 
        { 
            this.oviparo = true;
            this.tieneCola = true;
        }
        public Ornitorrinco(bool tieneCola, bool oviparo, bool esPeludo, Eespecies especie, string nombre) : base(nombre, esPeludo, especie)
        {
            this.oviparo = oviparo;
            this.tieneCola = tieneCola;
        }

        public override string Mostrar()
        {
            StringBuilder sb = new StringBuilder();

            string rta = base.Mostrar();

            sb.Append(rta);
            sb.AppendLine($"- Animal: Ornitorrinco - Es oviparo: {this.oviparo} - Tiene cola: {this.tieneCola}");

            return sb.ToString();

        }
        public override string ToString()
        {
            return this.Mostrar();
        }
        public override string EmitirSonido()
        {
            string sonidoOrnitorrinco = "Grrrr!";
            return sonidoOrnitorrinco;
        }
        public override int CantidadExtremidades()
        {
            return 4;
        }
        public int CantidadExtremidades(int cantidadExtremidades)
        {
            return cantidadExtremidades;
        }

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
