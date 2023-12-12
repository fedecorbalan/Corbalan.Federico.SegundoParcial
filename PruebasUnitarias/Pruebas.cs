using Entidades;
using PrimerParcial;
using System.Text;
using WinFormsPrimerParcial;

namespace PruebasUnitarias
{
    /// <summary>
    /// Clase que contiene pruebas unitarias para la funcionalidad de la aplicación.
    /// </summary>
    [TestClass]
    public class Pruebas
    {
        /// <summary>
        /// Prueba unitaria para la comparación de animales por cantidad de extremidades.
        /// </summary>
        [TestMethod]
        public static void TestComparacion1()
        {
            //arrange
            Ornitorrinco ornitorrinco = new Ornitorrinco(true, true, true, Eespecies.Mamifero, "tomas");
            Rana rana = new Rana(true, true, "marcos", false, Eespecies.Anfibio);

            //act
            int resultado = OrdenarAnimalesPorCantidadDeExtremidades(ornitorrinco, rana);

            //assert
            Assert.AreEqual(0, resultado);

            Console.WriteLine(resultado);
        }
        /// <summary>
        /// Prueba unitaria para la comparación de animales por Especie.
        /// </summary>
        [TestMethod]
        public static void TestComparacion2()
        {
            //arrange
            Ornitorrinco ornitorrinco = new Ornitorrinco(true, true, true, Eespecies.Mamifero, "tomas");
            Rana rana = new Rana(true, true, "marcos", false, Eespecies.Anfibio);

            //act
            int resultado = OrdenarAnimalesPorEspecie(ornitorrinco, rana);

            //assert
            Assert.AreEqual(-1, resultado);

            Console.WriteLine(resultado);
        }
        /// <summary>
        /// Prueba unitaria para verificar el sonido emitido por un hornero.
        /// </summary>
        [TestMethod]
        public static void TestSonido()
        {
            try 
            {
                Hornero hornero = new Hornero();

                string sonido = hornero.EmitirSonido();


                Assert.AreEqual("Pi-pi-pi!", sonido);

                Console.WriteLine(sonido);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                Assert.Fail($"Se ha proucido un error:{ex.Message}");
              
            }
        }
        /// <summary>
        /// Método que realiza la comparación de animales por cantidad de extremidades.
        /// </summary> 
        public static int OrdenarAnimalesPorCantidadDeExtremidades(Ornitorrinco ornitorrinco, Rana rana)
        {

            if (ornitorrinco.CantidadExtremidades() == rana.CantidadExtremidades())
            {
                return 0;
            }
            else if (ornitorrinco.CantidadExtremidades() < rana.CantidadExtremidades())
            {
                return 1;
            }
            else
            {
                return -1;
            }

        }
        /// <summary>
        /// Método que realiza la comparación de animales por especie.
        /// </summary>
        public static int OrdenarAnimalesPorEspecie(Ornitorrinco ornitorrinco, Rana rana)
        {
            if (ornitorrinco.especie == rana.especie)
            {
                return 0;
            }
            else if (ornitorrinco.especie > rana.especie)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }
        /// <summary>
        /// Método que simula la emisión de sonido por parte de un hornero.
        /// </summary>
        public static string EmitirSonido()
        {
            string sonidoHornero = "Pi-pi-pi!";
            return sonidoHornero;
        }
    }
}