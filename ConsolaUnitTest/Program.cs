using Entidades;
using PrimerParcial;
using PruebasUnitarias;
namespace ConsolaUnitTest
{
    public class Program
    {
        static void Main(string[] args)
        {
           EjecutarPruebas();

        }

        public static void EjecutarPruebas()
        {
            Console.WriteLine("Ejecutando pruebas");

            Pruebas pruebas = new Pruebas();

            Console.WriteLine("Ejecutando prueba Extremidades");
            Pruebas.TestComparacion1();

            Console.WriteLine("Ejecutando prueba Especie");
            Pruebas.TestComparacion2();

            Console.WriteLine("Ejecutando prueba Sonido Hornero");
            Pruebas.TestSonido();


        }
    }
}