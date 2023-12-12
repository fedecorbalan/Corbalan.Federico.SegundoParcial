using System.Diagnostics.Contracts;
using System.Text;
using System.Text.Json.Serialization;

namespace PrimerParcial
{
    /// <summary>
    /// Clase abstracta que representa a un animal.
    /// </summary>
    public abstract class Animal
    {
        /// <summary>
        /// Propiedades que obtienen o establecen valores que indican si el animal es peludo, su especie, su nombre y su id.
        /// </summary>
        public bool esPeludo { get; set; }
        public Eespecies especie { get; set; }
        public string nombre { get; set; }

        public static int idCounter = 0;

        /// <summary>
        /// Representa el contador de identificadores para asignar a los animales.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Método abstracto que devuelve el sonido emitido por el animal.
        /// </summary>
        /// <returns>Una cadena que representa el sonido del animal.</returns>
        public abstract string EmitirSonido();

        /// <summary>
        /// Constructor predeterminado de la clase Animal.
        /// </summary>
        public Animal()
        {
            this.esPeludo = false;
            this.nombre = "SIN NOMBRE";
            this.especie = Eespecies.Anfibio;
            this.Id = idCounter++;
        }
        /// <summary>
        /// Constructor parametrizado de la clase Animal.
        /// </summary>
        /// <param name="nombre">Nombre del animal.</param>
        /// <param name="esPeludo">Indica si el animal es peludo.</param>
        /// <param name="especie">Especie del animal.</param>
        public Animal(string nombre, bool esPeludo, Eespecies especie) : this()
        {
            this.esPeludo = esPeludo;
            this.nombre = nombre;
            this.especie = especie;
        }
        /// <summary>
        /// Método virtual que devuelve una cadena que representa la información del animal.
        /// </summary>
        /// <returns>Una cadena que contiene la información del animal.</returns>
        public virtual string Mostrar()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append($"Id: {this.Id} - Nombre: {this.nombre} - Es Peludo: {this.esPeludo} - Especie: {this.especie.ToString()} ");

            return sb.ToString();
        }
        /// <summary>
        /// Sobrecarga del método ToString para obtener una representación de cadena del animal.
        /// </summary>
        /// <returns>Una cadena que representa el animal.</returns>
        public override string ToString()
        {
            return this.Mostrar();
        }

        /// <summary>
        /// Sobrecarga del operador de igualdad para comparar dos animales.
        /// </summary>
        public static bool operator ==(Animal a, Animal b)
        {
            return a.nombre == b.nombre && a.esPeludo == b.esPeludo && a.especie == b.especie;
        }
        /// <summary>
        /// Sobrecarga del operador de desigualdad para comparar dos animales.
        /// </summary>
        public static bool operator !=(Animal a, Animal b)
        {
            return !(a == b);
        }
        /// <summary>
        /// Sobrescritura del método Equals para comparar dos objetos Animal.
        /// </summary>
        public override bool Equals(object? obj)
        {
            if (obj is Animal animal)
            {
                return this == animal;
            }
            return false;
        }

        /// <summary>
        /// Método virtual que devuelve la cantidad de extremidades del animal.
        /// </summary>
        /// <returns>La cantidad de extremidades del animal.</returns>
        public virtual int CantidadExtremidades()
        {
            return 0;
        }
        /// <summary>
        /// Conversión implícita a tipo booleano. Devuelve true si el animal es peludo.
        /// </summary>
        public static implicit operator bool(Animal a)
        {
            return a.esPeludo;
        }
    }
}