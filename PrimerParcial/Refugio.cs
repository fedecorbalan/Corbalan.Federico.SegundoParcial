using Newtonsoft.Json;
using PrimerParcial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Entidades
{
    /// <summary>
    /// Representa un refugio que almacena animales de tipo T.
    /// </summary>
    /// <typeparam name="T">Tipo de animal que puede ser almacenado en el refugio.</typeparam>
    public class Refugio<T> where T : Animal
    {
        /// <summary>
        /// Lista que almacena los animales refugiados.
        /// </summary>
        public List<T> animalesRefugiados;
        /// <summary>
        /// Constructor predeterminado de la clase Refugio.
        /// </summary>
        public Refugio()
        {
            this.animalesRefugiados = new List<T>();
        }
        /// <summary>
        /// Sobrecarga del operador de igualdad para verificar si un animal ya está en el refugio.
        /// </summary>
        /// <param name="refugio">Refugio a comparar.</param>
        /// <param name="animal">Animal a verificar.</param>
        /// <returns>True si el animal ya está en el refugio, false en caso contrario.</returns>
        public static bool operator ==(Refugio<T> refugio, T animal)
        {
            foreach (T elemento in refugio.animalesRefugiados)
            {
                if (elemento == animal)
                {
                    return true;
                }
            }
            return false;

        }
        /// <summary>
        /// Sobrecarga del operador de desigualdad para verificar si un animal no está en el refugio.
        /// </summary>
        /// <param name="refugio">Refugio a comparar.</param>
        /// <param name="animal">Animal a verificar.</param>
        /// <returns>True si el animal no está en el refugio, false en caso contrario.</returns>
        public static bool operator !=(Refugio<T> refugio, T animal)
        {
            return !(refugio == animal);
        }
        /// <summary>
        /// Sobrecarga del operador de adición para agregar un animal al refugio.
        /// </summary>
        /// <param name="refugio">Refugio al que se agregará el animal.</param>
        /// <param name="animal">Animal a agregar.</param>
        /// <returns>True si se agregó el animal, false en caso contrario.</returns>
        public static bool operator +(Refugio<T> refugio, T animal)
        {
            if ((refugio is not null && animal is not null) && refugio != animal)
            {
                refugio.animalesRefugiados.Add(animal);
                return true;
            }
            return false;
        }
        /// <summary>
        /// Sobrecarga del operador de sustracción para quitar un animal del refugio.
        /// </summary>
        /// <param name="refugio">Refugio del que se quitará el animal.</param>
        /// <param name="animal">Animal a quitar.</param>
        /// <returns>True si se quitó el animal, false en caso contrario.</returns>
        public static bool operator -(Refugio<T> refugio, T animal)
        {
            if (refugio == animal)
            {
                refugio.animalesRefugiados.Remove(animal);
                return true;
            }
            return false;
        }
        /// <summary>
        /// Ordena los animales por cantidad de extremidades.
        /// </summary>
        /// <param name="a1">Primer animal a comparar.</param>
        /// <param name="a2">Segundo animal a comparar.</param>
        /// <returns>0 si tienen la misma cantidad de extremidades, 1 si a1 tiene más extremidades, -1 si a2 tiene más extremidades.</returns>
        public int OrdenarAnimalesPorCantidadDeExtremidades(Animal a1, Animal a2)
        {
            if (a1.CantidadExtremidades() == a2.CantidadExtremidades())
            {
                return 0;
            }
            else if (a1.CantidadExtremidades() > a2.CantidadExtremidades())
            {
                return 1;
            }
            else
            {
                return -1;
            }

        }
        public int OrdenarAnimalesPorCantidadDeExtremidadesDesc(Animal a1, Animal a2)
        {
            if (a1.CantidadExtremidades() == a2.CantidadExtremidades())
            {
                return 0;
            }
            else if (a1.CantidadExtremidades() < a2.CantidadExtremidades())
            {
                return -1;
            }
            else
            {
                return 1;
            }

        }

        /// <summary>
        /// Agrega un animal al refugio.
        /// </summary>
        /// <param name="animal">Animal a agregar.</param>
        public void AgregarAnimal(T animal)
        {
            animalesRefugiados.Add(animal);
        }
        /// <summary>
        /// Actualiza un animal en el refugio en la posición indicada.
        /// </summary>
        /// <param name="animalModificado">Animal con las propiedades actualizadas.</param>
        /// <param name="indice">Índice del animal a actualizar.</param>
        public void ActualizarAnimal(T animalModificado, int indice)
        {
            if (indice >= 0 && indice < animalesRefugiados.Count)
            {
                animalesRefugiados[indice] = animalModificado;
            }
            else
            {
                throw new IndexOutOfRangeException("El índice está fuera de rango.");
            }
        }
        /// <summary>
        /// Ordena los animales en orden alfabetico de forma Ascendente.
        /// </summary>
        public void OrdenarAnimalesPorNombre()
        {
            animalesRefugiados.Sort((a1, a2) => String.Compare(a1.nombre, a2.nombre, StringComparison.Ordinal));
        }
        /// <summary>
        /// Ordena los animales en orden alfabetico de forma Descendente.
        /// </summary>
        public void OrdenarAnimalesPorNombreDescendente()
        {
            animalesRefugiados.Sort((a1, a2) => String.Compare(a2.nombre, a1.nombre, StringComparison.Ordinal));
        }

    }
}
