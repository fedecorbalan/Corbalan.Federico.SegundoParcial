using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class ExcepcionNombreVacio : Exception
    {
        public ExcepcionNombreVacio() : base("El campo Nombre esta vacio") { }
    }
    public class ExcepcionAnimalNoSeleccionado : Exception
    {
        public ExcepcionAnimalNoSeleccionado() : base("No se ha seleccionado ningun animal") { }
    }
    public class ExcepcionPeludoVacio : Exception
    {
        public ExcepcionPeludoVacio() : base("El campo Es peludo esta vacio") { }
    }
    public class ExcepcionPeludoErroneo : Exception
    {
        public ExcepcionPeludoErroneo() : base("El campo Es peludo es invalido. Ingrese si o no") { }
    }
    public class ExcepcionEsVenenosaVacio : Exception
    {
        public ExcepcionEsVenenosaVacio() : base("El campo Es Venenosa esta vacio") { }
    }
    public class ExcepcionEsVenenosaErroneo : Exception
    {
        public ExcepcionEsVenenosaErroneo() : base("El campo Es Venenosa es invalido. Ingresar si o no") { }
    }
    public class ExcepcionEsArboricolaVacio : Exception
    {
        public ExcepcionEsArboricolaVacio() : base("El campo Es Arboricola esta vacio") { }
    }
    public class ExcepcionEsArboricolaErroneo : Exception
    {
        public ExcepcionEsArboricolaErroneo() : base("El campo Es Arboricola es invalido. Ingresar si o no.") { }
    }
    public class ExcepcionEsOviparoVacio : Exception
    {
        public ExcepcionEsOviparoVacio() : base("El campo Es Oviparo esta vacio") { }
    }
    public class ExcepcionTieneColaVacio : Exception
    {
        public ExcepcionTieneColaVacio() : base("El campo Tiene Cola esta vacio") { }
    }
    public class ExcepcionEsOviparoErroneo : Exception
    {
        public ExcepcionEsOviparoErroneo() : base("El campo Es Oviparo es invalido") { }
    }
    public class ExcepcionTieneColaErroneo : Exception
    {
        public ExcepcionTieneColaErroneo() : base("El campo Tiene Cola es invalido") { }
    }
    public class ExcepcionTieneAlasVacio : Exception
    {
        public ExcepcionTieneAlasVacio() : base("El campo Tiene Alas esta vacio") { }
    }
    public class ExcepcionTieneAlasErroneo : Exception
    {
        public ExcepcionTieneAlasErroneo() : base("El campo Tiene Alas es invalido") { }
    }
    public class ExcepcionVelocidadVacio : Exception
    {
        public ExcepcionVelocidadVacio() : base("El campo Velocidad en KM/H esta vacio") { }
    }
    public class ExcepcionVelocidadErroneo : Exception
    {
        public ExcepcionVelocidadErroneo() : base("El campo Velocidad en KM/H es invalido") { }
    }
    
}
