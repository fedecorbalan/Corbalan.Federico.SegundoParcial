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
    public class Refugio<T> where T : Animal
    {
        public List<T> animalesRefugiados;

        public Refugio()
        {
            this.animalesRefugiados = new List<T>();
        }
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
        public static bool operator !=(Refugio<T> refugio, T animal)
        {
            return !(refugio == animal);
        }

        public static bool operator +(Refugio<T> refugio, T animal)
        {
            if ((refugio is not null && animal is not null) && refugio != animal)
            {
                refugio.animalesRefugiados.Add(animal);
                return true;
            }
            return false;
        }
        public static bool operator -(Refugio<T> refugio, T animal)
        {
            if (refugio == animal)
            {
                refugio.animalesRefugiados.Remove(animal);
                return true;
            }
            return false;
        }

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
        public int OrdenarAnimalesPorEspecie(Animal a1, Animal a2)
        {
            if (a1.especie == a2.especie)
            {
                return 0;
            }
            else if (a1.especie > a2.especie)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }
        public void AgregarAnimal(T animal)
        {
            animalesRefugiados.Add(animal);
        }
    }
}
