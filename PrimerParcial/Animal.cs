using System.Diagnostics.Contracts;
using System.Text;
using System.Text.Json.Serialization;

namespace PrimerParcial
{
    public abstract class Animal
    {
        public bool esPeludo { get; set; }
        public Eespecies especie { get; set; }
        public string nombre { get; set; }

        public static int idCounter = 0;


        public int Id { get; set; }

        public abstract string EmitirSonido();

        public Animal()
        {
            this.esPeludo = false;
            this.nombre = "SIN NOMBRE";
            this.especie = Eespecies.Anfibio;
            this.Id = idCounter++;
        }
        public Animal(string nombre,bool esPeludo): this()
        {
            this.nombre = nombre;
            this.esPeludo = esPeludo;
        }
        public Animal(string nombre, bool esPeludo, Eespecies especie)
        {
            this.esPeludo = esPeludo;
            this.nombre = nombre;
            this.especie = especie;
        }
        public virtual string Mostrar()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append($"Id: {this.Id} - Nombre: {this.nombre} - Es Peludo: {this.esPeludo} - Especie: {this.especie.ToString()} ");

            return sb.ToString();
        }

        public override string ToString()
        {
            return this.Mostrar();
        }

        public static bool operator ==(Animal a, Animal b)
        {
            return a.nombre == b.nombre && a.esPeludo == b.esPeludo && a.especie == b.especie;
        }

        public static bool operator !=(Animal a, Animal b)
        {
            return !(a == b);
        }
        public override bool Equals(object? obj)
        {
            if (obj is Animal animal)
            {
                return this == animal;
            }
            return false;
        }
        public virtual int CantidadExtremidades()
        {
            return 0;
        }

        public static implicit operator bool(Animal a)
        {
            return a.esPeludo;
        }
    }
}