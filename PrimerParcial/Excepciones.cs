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
    public class ExcepcionTieneAlasVacio : Exception
    {
        public ExcepcionTieneAlasVacio() : base("El campo Tiene Alas esta vacio") { }
    }
    public class ExcepcionVelocidadVacio : Exception
    {
        public ExcepcionVelocidadVacio() : base("El campo Velocidad en KM/H esta vacio") { }
    }
    public class ExcepcionIdErroneo : Exception
    {
        public ExcepcionIdErroneo() : base("El tipo de dato del identificador es incorrecto.") { }
    }
    public class ExcepcionIdExistente : Exception
    {
        public ExcepcionIdExistente() : base("El identificador ya esta registrado.") { }
    }
}
