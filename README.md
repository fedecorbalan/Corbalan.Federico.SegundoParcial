# CRUD - Refugio de Animales
Este repositorio contiene un proyecto de C# que simula un refugio de animales. En el refugio se pueden alojar tres tipos de animales: Ornitorrincos, Ranas y Horneros.
## Sobre mi
- Mi nombre es Federico Corbalán, tengo 18 años y actualmente me encuentro cursando la Tecnicatura en Programación en UTN FRA, con el objetivo de poder expandir mis horizontes para poder mejorar mis habilidades como programador.
# Resumen
- En este programa, lo que se busca es poder listar a los diferentes animales que se encuentran en el refugio en un formulario, al que se ingresa mediante un Login de Usuarios.
  
![image](https://github.com/fedecorbalan/Corbalan.Federico.PrimerParcial/assets/123754871/29d64494-dc0b-4302-a47a-b0765d65d423)

Esto se logra mediante la deserializacion del archivo MOCK_DATA.json y algunos ajustes dentro de la clase Usuario

```c#
using Entidades;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsPrimerParcial
{
    public partial class FormLogin : Form
    {
        public Usuario usuario;

        public Usuario UsuarioForm { get { return this.usuario; } }
        public FormLogin()
        {
            InitializeComponent();
        }

        public FormLogin(Usuario usuario) : this()
        {
            this.usuario = usuario;
            this.txtMailUser.Focus();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            this.usuario = this.Verificar();

            if (this.usuario != null)
            {
                RegistrarAcceso(this.usuario);
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Error al iniciar sesion");
            }
            this.DialogResult = DialogResult.OK;
        }

        private Usuario Verificar()
        {
            Usuario? rta = null;

            using (System.IO.StreamReader sr = new System.IO.StreamReader(@"..\..\..\MOCK_DATA.json"))
            {
                System.Text.Json.JsonSerializerOptions opciones = new System.Text.Json.JsonSerializerOptions();
                opciones.WriteIndented = true;

                string json_str = sr.ReadToEnd();

                List<Usuario> users = System.Text.Json.JsonSerializer.Deserialize<List<Usuario>>(json_str, opciones);

                foreach (Usuario item in users)
                {
                    if (item.correo == this.txtMailUser.Text && item.clave == this.txtPassword.Text)
                    {
                        rta = item;
                        break;
                    }
                }
            }
            return rta;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {

            DialogResult resultado = MessageBox.Show("¿Estas seguro que quieres salir?", "SALIR", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                MessageBox.Show("Aplicacion Finalizada");
                Application.Exit();
            }
            else
            {
                MessageBox.Show("App no cerrada");
            }


        }
        public static void RegistrarAcceso(Usuario usuario)
        {
            string registro = $"{DateTime.Now}: Usuario {usuario.nombre} {usuario.apellido} ({usuario.correo}) ha ingresado.";

            // Especifica la ruta del archivo de registro (usuarios.log)
            string rutaArchivoLog = @"..\..\..\usuarios.log";

            try
            {
                using (StreamWriter sw = File.AppendText(rutaArchivoLog))
                {
                    sw.WriteLine(registro);
                }
            }
            catch (Exception ex)
            {
                // Maneja cualquier error de registro
                Console.WriteLine($"Error al registrar acceso: {ex.Message}");
            }
        }
    }
}

```
### Clase `Usuario`

```c#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsPrimerParcial
{
    public class Usuario
    { 
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string correo { get; set; }
        public string perfil { get; set; }
        public int legajo { get; set; }
        public string clave { get; set; }
        public Usuario() { }

        public Usuario(string nombre, string apellido, string correo, string perfil, int legajo, string clave) 
        { 
            this.perfil = perfil;
            this.nombre = nombre;
            this.apellido = apellido;
            this.correo = correo;
            this.clave = clave;
            this.legajo = legajo;
        }
    }
}
```

## Clases

### Clase `Animal`

La clase `Animal` es una clase abstracta que representa a todos los animales en el refugio. Tiene propiedades como id, nombre, especie, si es peludo, y métodos para mostrar información, emitir un sonido y contar la cantidad de extremidades.

```c#
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
```

### Clase `Rana`

La clase `Rana` hereda de `Animal` y representa a las ranas en el refugio. Tiene propiedades específicas como si es venenosa, si es arborícola y métodos para emitir un sonido y contar la cantidad de extremidades.

```c#
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
```

### Clase `Ornitorrinco`

La clase `Ornitorrinco` hereda de `Animal` y representa a los ornitorrincos en el refugio. Tiene propiedades específicas como si es ovíparo, si tiene cola y métodos para emitir un sonido y contar la cantidad de extremidades.

```c#
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

        public Ornitorrinco() { }
        public Ornitorrinco(bool oviparo, bool esPeludo, Eespecies especie, string nombre) : base(nombre, esPeludo, especie)
        {
            this.oviparo = oviparo;
        }
        public Ornitorrinco(bool tieneCola, bool oviparo, bool esPeludo, Eespecies especie, string nombre) : this(oviparo,esPeludo, especie, nombre)
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

```

### Clase `Hornero`

La clase `Hornero` hereda de `Animal` y representa a los horneros en el refugio. Tiene propiedades específicas como si tiene alas y la velocidad de vuelo en kilómetros por hora. También tiene métodos para emitir un sonido y contar la cantidad de extremidades.

```c#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PrimerParcial
{
    public class Hornero : Animal
    {
        public bool tieneAlas;
        public int velocidadKmH;

        public Hornero() 
        {
            this.tieneAlas = true;
            this.velocidadKmH = 30;
        }
        public Hornero(bool tieneAlas, string nombre, bool esPeludo, Eespecies especie) : base(nombre, esPeludo, especie)
        {
            this.tieneAlas = tieneAlas;
        }
        public Hornero(int velocidadKmH, bool tieneAlas, bool esPeludo, Eespecies especie, string nombre) : this(tieneAlas, nombre, esPeludo, especie)
        {
            this.tieneAlas = tieneAlas;
            this.velocidadKmH = velocidadKmH;
        }

        public override string Mostrar()
        {
            StringBuilder sb = new StringBuilder();

            string rta = base.Mostrar();

            sb.Append(rta);
            sb.Append($"- Animal: Hornero - Tiene alas: {this.tieneAlas} - Velocidad de vuelo en KM/H: {this.velocidadKmH}");

            return sb.ToString();

        }
        public override string ToString()
        {
            return this.Mostrar();
        }
        public override string EmitirSonido()
        {
            string sonidoHornero = "Pi-pi-pi!";
            return sonidoHornero;
        }

        public override int CantidadExtremidades()
        {
            return 2;
        }
        public int CantidadExtremidades(int cantidadExtremidades)
        {
            return cantidadExtremidades;
        }

        public void ActualizarHornero(Hornero h)
        {
            this.nombre = h.nombre;
            this.especie = h.especie;
            this.esPeludo = h.esPeludo;
            this.tieneAlas = h.tieneAlas;
            this.velocidadKmH = h.velocidadKmH;
        }

    }
}
```

### Clase `Refugio`

La clase `Refugio` representa el refugio de animales y presenta la utilizacion de Generics, cosa que le pemite crear listas de clase Animal y sus clases hijas, más adelante, se podra observar la implementacion de esta clase. Ademas, contiene métodos que sirven para agregar, eliminar y buscar animales en el refugio, así como métodos para ordenar los animales por cantidad de extremidades y especie. 

```c#
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
        // Dentro de la clase Refugio<T>
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

    }
}

```

### Clase `AccesoDatos`

Esta clase contienen metodos que se comunican con las bases de datos correspondientes al CRUD, los cuales derivan de Interfaces, las cuales corresponden a una por clase derivada
```c#
public interface ICrudRana
{
    public Refugio<Rana> ObtenerListaRanas(Refugio<Rana> lista);
    public bool AgregarRana(Rana r);
    public bool ModificarRana(Rana r);
    public bool EliminarRana(Rana r);
}
public interface ICrudOrnitorrinco
{
    public Refugio<Ornitorrinco> ObtenerListaOrnitorrincos(Refugio<Ornitorrinco> lista);
    public bool AgregarOrnitorrinco(Ornitorrinco o);
    public bool ModificarOrnitorrinco(Ornitorrinco o);
    public bool EliminarOrnitorrinco(Ornitorrinco o);
}
public interface ICrudHornero
{
    public Refugio<Hornero> ObtenerListaHorneros(Refugio<Hornero> lista);
    public bool AgregarHornero(Hornero rh);
    public bool ModificarHornero(Hornero h);
    public bool EliminarHornero(Hornero h);
}
```
```c#
using Microsoft.Data.SqlClient;
using PrimerParcial;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class AccesoDatos: ICrudRana,ICrudHornero,ICrudOrnitorrinco
    {
        private SqlConnection conexion;
        private static string cadena_conexion;
        private SqlCommand comando;
        private SqlDataReader lector;
        static AccesoDatos()
        {
            AccesoDatos.cadena_conexion = Properties.Resources.miConexion;
        }
        public AccesoDatos()
        {
            this.conexion = new SqlConnection(AccesoDatos.cadena_conexion);
        }
        public bool PruebaConexion()
        {
            bool retorno = false;

            try
            {
                this.conexion.Open();
                retorno = true;
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (this.conexion.State == System.Data.ConnectionState.Open)
                {
                    this.conexion.Close();
                }
            }
            return retorno;
        }

        public Refugio<Rana> ObtenerListaRanas(Refugio<Rana> lista)
        {
            try
            {
                this.comando = new SqlCommand();
                this.comando.CommandType = System.Data.CommandType.Text;
                this.comando.CommandText = "select id,nombre,especie,esPeludo,esVenenosa,esArboricola from ranas";
                this.comando.Connection = this.conexion;
                this.conexion.Open();

                this.lector = this.comando.ExecuteReader();
                while (lector.Read())
                {
                    Rana rana = new Rana();
                    rana.Id = (int)this.lector["id"];
                    rana.nombre = this.lector[1].ToString();
                    rana.especie = (Eespecies)this.lector["especie"];
                    rana.esPeludo = (bool)this.lector["esPeludo"];
                    rana.esVenenosa = (bool)this.lector["esVenenosa"];
                    rana.esArboricola = (bool)this.lector["esArboricola"];

                    lista.animalesRefugiados.Add(rana);
                }
                this.lector.Close();
            }
            catch (Exception e){ }
            finally
            {
                if (this.conexion.State == System.Data.ConnectionState.Open)
                {
                    this.conexion.Close();
                }
            }
            return lista;
        }
        public Refugio<Hornero> ObtenerListaHorneros(Refugio<Hornero> lista)
        {
            try
            {
                this.comando = new SqlCommand();
                this.comando.CommandType = System.Data.CommandType.Text;
                this.comando.CommandText = "select id,nombre,especie,esPeludo,tieneAlas,velocidadKmH from horneros";
                this.comando.Connection = this.conexion;
                this.conexion.Open();

                this.lector = this.comando.ExecuteReader(); 
                while (lector.Read())
                {
                    Hornero hornero = new Hornero();
                    hornero.Id = (int)this.lector["id"];
                    hornero.nombre = this.lector[1].ToString();
                    hornero.especie = (Eespecies)this.lector["especie"];
                    hornero.esPeludo = (bool)this.lector["esPeludo"];
                    hornero.tieneAlas = (bool)this.lector["tieneAlas"];
                    hornero.velocidadKmH = (int)this.lector["velocidadKmH"];

                    lista.animalesRefugiados.Add(hornero);
                }
                this.lector.Close();
            }
            catch (Exception e) {}
            finally
            {
                if (this.conexion.State == System.Data.ConnectionState.Open)
                {
                    this.conexion.Close();
                }
            }
            return lista;
        }
        public Refugio<Ornitorrinco> ObtenerListaOrnitorrincos(Refugio<Ornitorrinco> lista)
        {
            try
            {
                this.comando = new SqlCommand();
                this.comando.CommandType = System.Data.CommandType.Text;
                this.comando.CommandText = "select id,nombre,especie,esPeludo,esOviparo,tieneCola from ornitorrincos";
                this.comando.Connection = this.conexion;
                this.conexion.Open();

                this.lector = this.comando.ExecuteReader(); 
                while (lector.Read())
                {
                    Ornitorrinco ornitorrinco = new Ornitorrinco();
                    ornitorrinco.Id = (int)this.lector["id"];
                    ornitorrinco.nombre = this.lector[1].ToString();
                    ornitorrinco.especie = (Eespecies)this.lector["especie"];
                    ornitorrinco.esPeludo = (bool)this.lector["esPeludo"];
                    ornitorrinco.oviparo = (bool)this.lector["esOviparo"];
                    ornitorrinco.tieneCola = (bool)this.lector["tieneCola"];

                    lista.animalesRefugiados.Add(ornitorrinco);
                }
                this.lector.Close();
            }
            catch (Exception e) {}
            finally
            {
                if (this.conexion.State == System.Data.ConnectionState.Open)
                {
                    this.conexion.Close();
                }
            }
            return lista;
        }
       
        public bool AgregarRana(Rana r)
        {
            bool retorno = false;

            try
            {
                this.comando = new SqlCommand();
                this.comando.CommandType = System.Data.CommandType.Text;

                this.comando.CommandText = "INSERT INTO ranas(nombre, especie, esPeludo, esVenenosa, esArboricola) " +
                    "VALUES(@nombre, @especie, @esPeludo, @esVenenosa, @esArboricola)";

                this.comando.Parameters.AddWithValue("@nombre", r.nombre);
                this.comando.Parameters.AddWithValue("@especie", (int)r.especie);
                this.comando.Parameters.AddWithValue("@esPeludo", r.esPeludo);
                this.comando.Parameters.AddWithValue("@esVenenosa", r.esVenenosa);
                this.comando.Parameters.AddWithValue("@esArboricola", r.esArboricola);

                this.comando.Connection = this.conexion;

                this.conexion.Open();

                int filasAfectadas = this.comando.ExecuteNonQuery();

                if (filasAfectadas > 0)
                {
                    // Recuperar el último ID insertado
                    this.comando.CommandText = "SELECT IDENT_CURRENT('ranas')";
                    int nuevoId = Convert.ToInt32(this.comando.ExecuteScalar());

                    r.Id = nuevoId;
                    retorno = true;
                }
            }
            catch (Exception w)
            {
                // Manejar la excepción
            }
            finally
            {
                if (this.conexion.State == System.Data.ConnectionState.Open)
                {
                    this.conexion.Close();
                }
            }
            return retorno;
        }


        public bool AgregarHornero(Hornero h)
        {
            bool retorno = false;

            try
            {
                this.comando = new SqlCommand();
                this.comando.CommandType = System.Data.CommandType.Text;

                // Utilizar parámetros para evitar la inyección de SQL
                this.comando.CommandText = "INSERT INTO horneros(nombre, especie, esPeludo, tieneAlas, velocidadKmH) " +
                    "VALUES(@nombre, @especie, @esPeludo, @tieneAlas, @velocidadKmH)"; 

                // Añadir parámetros
                this.comando.Parameters.AddWithValue("@nombre", h.nombre);
                this.comando.Parameters.AddWithValue("@especie", (int)h.especie);
                this.comando.Parameters.AddWithValue("@esPeludo", h.esPeludo);
                this.comando.Parameters.AddWithValue("@tieneAlas", h.tieneAlas);
                this.comando.Parameters.AddWithValue("@velocidadKmH", (int)h.velocidadKmH);

                this.comando.Connection = this.conexion;

                this.conexion.Open();

                int filasAfectadas = this.comando.ExecuteNonQuery();

                if (filasAfectadas > 0)
                {
                    this.comando.CommandText = "SELECT IDENT_CURRENT('horneros')";
                    int nuevoId = Convert.ToInt32(this.comando.ExecuteScalar());

                    h.Id = nuevoId;
                    retorno = true;
                }
            }
            catch (Exception w)
            {
                // Manejar la excepción
            }
            finally
            {
                if (this.conexion.State == System.Data.ConnectionState.Open)
                {
                    this.conexion.Close();
                }
            }
            return retorno;
        }
        public bool AgregarOrnitorrinco(Ornitorrinco o)
        {
            bool retorno = false;

            try
            {
                this.comando = new SqlCommand();
                this.comando.CommandType = System.Data.CommandType.Text;

                // Utilizar parámetros para evitar la inyección de SQL
                this.comando.CommandText = "INSERT INTO ornitorrincos(nombre, especie, esPeludo, esOviparo, tieneCola) " +
                    "VALUES(@nombre, @especie, @esPeludo, @esOviparo, @tieneCola)";

                // Añadir parámetros
                this.comando.Parameters.AddWithValue("@nombre", o.nombre);
                this.comando.Parameters.AddWithValue("@especie", (int)o.especie);
                this.comando.Parameters.AddWithValue("@esPeludo", o.esPeludo);
                this.comando.Parameters.AddWithValue("@esOviparo", o.oviparo);
                this.comando.Parameters.AddWithValue("@tieneCola", o.tieneCola);

                this.comando.Connection = this.conexion;

                this.conexion.Open();

                int filasAfectadas = this.comando.ExecuteNonQuery();

                if (filasAfectadas > 0)
                {
                    this.comando.CommandText = "SELECT IDENT_CURRENT('ornitorrincos')";
                    int nuevoId = Convert.ToInt32(this.comando.ExecuteScalar());

                    o.Id = nuevoId;
                    retorno = true;
                }

            }
            catch (Exception w)
            {
                // Manejar la excepción
            }
            finally
            {
                if (this.conexion.State == System.Data.ConnectionState.Open)
                {
                    this.conexion.Close();
                }
            }
            return retorno;
        }



        public bool ModificarRana(Rana r)
        {
            bool retorno = false;
            try
            {
                this.comando = new SqlCommand();

                this.comando.Parameters.AddWithValue("@id", r.Id);
                this.comando.Parameters.AddWithValue("@nombre", r.nombre);
                this.comando.Parameters.AddWithValue("@especie", r.especie);
                this.comando.Parameters.AddWithValue("@esPeludo", r.esPeludo);
                this.comando.Parameters.AddWithValue("@esVenenosa", r.esVenenosa);
                this.comando.Parameters.AddWithValue("@esArboricola", r.esArboricola);

                this.comando.CommandType = System.Data.CommandType.Text;

                this.comando.CommandText = "update ranas set nombre = @nombre, especie = @especie, esPeludo = @esPeludo, esVenenosa = @esVenenosa, esArboricola = @esArboricola WHERE id = @id";
                this.comando.Connection = this.conexion;

                this.conexion.Open();

                int filasAfectadas = this.comando.ExecuteNonQuery();

                if (filasAfectadas == 1)
                {
                    retorno = true;
                }

            }
            catch (Exception w) {}
            finally
            {
                if (this.conexion.State == System.Data.ConnectionState.Open)
                {
                    this.conexion.Close();
                }
            }
            return retorno;
        }

        public bool ModificarHornero(Hornero h)
        {
            bool retorno = false;
            try
            {
                this.comando = new SqlCommand();

                this.comando.Parameters.AddWithValue("@id", h.Id);
                this.comando.Parameters.AddWithValue("@nombre", h.nombre);
                this.comando.Parameters.AddWithValue("@especie", h.especie);
                this.comando.Parameters.AddWithValue("@esPeludo", h.esPeludo);
                this.comando.Parameters.AddWithValue("@tieneAlas", h.tieneAlas);
                this.comando.Parameters.AddWithValue("@velocidadKmH", h.velocidadKmH);

                this.comando.CommandType = System.Data.CommandType.Text;

                this.comando.CommandText = "update horneros set nombre = @nombre, especie = @especie, esPeludo = @esPeludo, tieneAlas = @tieneAlas, velocidadKmH = @velocidadKmH WHERE id = @id";
                this.comando.Connection = this.conexion;

                this.conexion.Open();

                int filasAfectadas = this.comando.ExecuteNonQuery();

                if (filasAfectadas == 1)
                {
                    retorno = true;
                }

            }
            catch (Exception w) 
            { 
            }
            finally
            {
                if (this.conexion.State == System.Data.ConnectionState.Open)
                {
                    this.conexion.Close();
                }
            }
            return retorno;
        }



        public bool ModificarOrnitorrinco(Ornitorrinco o)
        {
            bool retorno = false;
            try
            {
                this.comando = new SqlCommand();

                this.comando.Parameters.AddWithValue("@id", o.Id);
                this.comando.Parameters.AddWithValue("@nombre", o.nombre);
                this.comando.Parameters.AddWithValue("@especie", o.especie);
                this.comando.Parameters.AddWithValue("@esPeludo", o.esPeludo);
                this.comando.Parameters.AddWithValue("@esOviparo", o.oviparo);
                this.comando.Parameters.AddWithValue("@tieneCola", o.tieneCola);

                this.comando.CommandType = System.Data.CommandType.Text;

                this.comando.CommandText = "update ornitorrincos set nombre = @nombre, especie = @especie, esPeludo = @esPeludo, esOviparo = @esOviparo, tieneCola = @tieneCola WHERE id = @id";
                this.comando.Connection = this.conexion;

                this.conexion.Open();

                int filasAfectadas = this.comando.ExecuteNonQuery();

                if (filasAfectadas == 1)
                {
                    retorno = true;
                }

            }
            catch (Exception w) { }
            finally
            {
                if (this.conexion.State == System.Data.ConnectionState.Open)
                {
                    this.conexion.Close();
                }
            }
            return retorno;
        }
        public bool EliminarRana(Rana r)
        {
            bool retorno = false;
            try
            {
                this.comando = new SqlCommand();
                this.comando.Parameters.AddWithValue("@id", r.Id);
                this.comando.CommandType = System.Data.CommandType.Text;
                this.comando.CommandText = "DELETE FROM ranas WHERE id = @id";
                this.comando.Connection = this.conexion;

                this.conexion.Open();

                int filasAfectadas = this.comando.ExecuteNonQuery();

                if (filasAfectadas == 1)
                {
                    retorno = true;

                }
            }
            catch (Exception ex) { }
            finally
            {
                if (this.conexion.State == System.Data.ConnectionState.Open)
                {
                    this.conexion.Close();
                }
            }
            return retorno;
        }
        public bool EliminarHornero(Hornero h)
        {
            bool retorno = false;
            try
            {
                this.comando = new SqlCommand();
                this.comando.Parameters.AddWithValue("@id", h.Id);
                this.comando.CommandType = System.Data.CommandType.Text;
                this.comando.CommandText = "DELETE FROM horneros WHERE id = @id";
                this.comando.Connection = this.conexion;

                this.conexion.Open();

                int filasAfectadas = this.comando.ExecuteNonQuery();

                if (filasAfectadas == 1)
                {
                    retorno = true;
                }
            }
            catch (Exception ex) { }
            finally
            {
                if (this.conexion.State == System.Data.ConnectionState.Open)
                {
                    this.conexion.Close();
                }
            }
            return retorno;
        }
        public bool EliminarOrnitorrinco(Ornitorrinco o)
        {
            bool retorno = false;
            try
            {
                this.comando = new SqlCommand();
                this.comando.Parameters.AddWithValue("@id", o.Id);
                this.comando.CommandType = System.Data.CommandType.Text;
                this.comando.CommandText = "DELETE FROM ornitorrincos WHERE id = @id";
                this.comando.Connection = this.conexion;

                this.conexion.Open();

                int filasAfectadas = this.comando.ExecuteNonQuery();

                if (filasAfectadas == 1)
                {
                    retorno = true;
                }
            }
            catch (Exception ex){}
            finally
            {
                if (this.conexion.State == System.Data.ConnectionState.Open)
                {
                    this.conexion.Close();
                }
            }
            return retorno;
        }
    }
}


```


## Formularios

El proyecto contiene cinco formularios en total, incluyendo el Formulario de Login que se ha mencionado previamente:

1. `FormPrincipal`: Este formulario es la ventana principal de la aplicación. Permite agregar, modificar y eliminar animales en el refugio. Ademas, se pueden seleccionar los archivos que se quieren serializar y mostrarse en el visor, cabe aclarar que cuando se selecciona/deserializa un archivo, se traen los datos directamente desde la base de datos. También se cuenta con botones para poder ordenar los animales que han sido deserializados en el visor en base a su cantidad de extremidades y su especie (Número de enumerado) y un visualizador para poder observar quien ha iniciado sesión y en que fecha y horario.

![image](https://github.com/fedecorbalan/Corbalan.Federico.PrimerParcial/assets/123754871/c23c200b-88b5-4540-95e3-72ce5299e505)


```c#

using Entidades;
using Newtonsoft.Json;
using PrimerParcial;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.IO;
using Microsoft.Data.SqlClient;
using WinFormsSegundoParcial;

namespace WinFormsPrimerParcial
{

    public partial class FormPrincipal : Form
    {
        public Refugio<Rana> listaRanasRefugiadas;
        public Refugio<Hornero> listaHornerosRefugiados;
        public Refugio<Ornitorrinco> listaOrnitorrincosRefugiados;

        public string perfilUsuario;

        AccesoDatos ado = new AccesoDatos();
        public ListBox LstVisor { get; set; }
        public FormPrincipal()
        {
            InitializeComponent();
            MostrarFechaActual();

            this.listaHornerosRefugiados = new Refugio<Hornero>();
            this.listaOrnitorrincosRefugiados = new Refugio<Ornitorrinco>();
            this.listaRanasRefugiadas = new Refugio<Rana>();
        }
        public FormPrincipal(Usuario usuario) : this()
        {
            MessageBox.Show($"Bienvenido, {usuario.nombre}");
            this.lblInfo.Text = usuario.nombre;
            this.perfilUsuario = usuario.perfil;

        }
        private void MostrarFechaActual()
        {
            DateTime fechaActual = DateTime.Now;
            lblDateTime.Text = fechaActual.ToString("dd/MM/yyyy");
        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (perfilUsuario.ToLower() == "administrador" || perfilUsuario.ToLower() == "supervisor")
            {
                FormSeleccionAnimal frmSeleccionAnimal = new FormSeleccionAnimal();

                if (frmSeleccionAnimal.ShowDialog() == DialogResult.OK)
                {
                    Type selectedAnimalType = frmSeleccionAnimal.GetSelectedAnimalType();

                    if (selectedAnimalType != null)
                    {
                        // Abre el formulario correspondiente según el tipo de animal seleccionado
                        FormAgregar frmAgregar = Activator.CreateInstance(selectedAnimalType) as FormAgregar;
                        frmAgregar.FormPrincipalRef = this;
                        frmAgregar.StartPosition = FormStartPosition.CenterScreen;


                        if (frmAgregar.ShowDialog() == DialogResult.OK)
                        {
                            Animal nuevoAnimal = frmAgregar.NuevoAnimal;

                            // Agrega el nuevo animal a la lista correspondiente
                            if (nuevoAnimal is Rana)
                            {
                                listaRanasRefugiadas.AgregarAnimal((Rana)nuevoAnimal);

                            }
                            else if (nuevoAnimal is Hornero)
                            {
                                listaHornerosRefugiados.AgregarAnimal((Hornero)nuevoAnimal);

                            }
                            else if (nuevoAnimal is Ornitorrinco)
                            {
                                listaOrnitorrincosRefugiados.AgregarAnimal((Ornitorrinco)nuevoAnimal);

                            }
                        }
                    }
                }
                ActualizarVisor();
            }
            else
            {
                MessageBox.Show("Usted no es administrador ni supervisor, por lo tanto, no posee permisos para agregar elementos");
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (perfilUsuario.ToLower() == "administrador" || perfilUsuario.ToLower() == "supervisor")
            {
                int selectedIndex = lstVisor.SelectedIndex;

                if (selectedIndex >= 0)
                {
                    Animal animalAModificar;
                    Eespecies especie;

                    if (selectedIndex < listaRanasRefugiadas.animalesRefugiados.Count)
                    {
                        animalAModificar = listaRanasRefugiadas.animalesRefugiados[selectedIndex];
                        especie = Eespecies.Anfibio;
                    }
                    else if (selectedIndex < listaRanasRefugiadas.animalesRefugiados.Count + listaHornerosRefugiados.animalesRefugiados.Count)
                    {
                        int index = selectedIndex - listaRanasRefugiadas.animalesRefugiados.Count;
                        animalAModificar = listaHornerosRefugiados.animalesRefugiados[index];
                        especie = Eespecies.Ave;
                    }
                    else
                    {
                        int index = selectedIndex - listaRanasRefugiadas.animalesRefugiados.Count - listaHornerosRefugiados.animalesRefugiados.Count;
                        animalAModificar = listaOrnitorrincosRefugiados.animalesRefugiados[index];
                        especie = Eespecies.Mamifero;
                    }

                    // Verifica si el animal a modificar es una rana y abre el formulario correspondiente
                    if (especie == Eespecies.Anfibio && animalAModificar is Rana ranaAModificar)
                    {
                        FormModificarRana frmModificarRana = new FormModificarRana(ranaAModificar);
                        frmModificarRana.OperacionCompletada += ManejarOperacionCompleta;

                        frmModificarRana.StartPosition = FormStartPosition.CenterScreen;
                        frmModificarRana.ShowDialog();
                    }
                    if (especie == Eespecies.Ave && animalAModificar is Hornero horneroAModificar)
                    {
                        FormModificarHornero frmModificarHornero = new FormModificarHornero(horneroAModificar);
                        frmModificarHornero.OperacionCompletada += ManejarOperacionCompleta;

                        frmModificarHornero.StartPosition = FormStartPosition.CenterScreen;
                        frmModificarHornero.ShowDialog();
                    }
                    if (especie == Eespecies.Mamifero && animalAModificar is Ornitorrinco ornitorrincoAModificar)
                    {
                        FormModificarOrnitorrinco frmModificarOrnitorrinco = new FormModificarOrnitorrinco(ornitorrincoAModificar);
                        frmModificarOrnitorrinco.OperacionCompletada += ManejarOperacionCompleta;

                        frmModificarOrnitorrinco.StartPosition = FormStartPosition.CenterScreen;
                        frmModificarOrnitorrinco.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show("Selecciona un elemento para modificar.");
                }
                ActualizarVisor();
            }
            else
            {
                MessageBox.Show("Usted no es administrador ni supervisor, por lo tanto, no posee permisos para modificar elementos");
            }
        
        }
        private void MostrarMensajeID(int idAnimal)
        {
            MessageBox.Show($"ID del animal seleccionado: {idAnimal}", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            
            int selectedIndex = lstVisor.SelectedIndex;
            if (perfilUsuario.ToLower() == "administrador")
            {
                if (selectedIndex >= 0)
                {
                    DialogResult resultado = MessageBox.Show("¿Esta seguro que desea eliminar el registro?\nEsto tambien lo eliminara de la base de datos", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (resultado == DialogResult.Yes)
                    {
                        if (selectedIndex < listaRanasRefugiadas.animalesRefugiados.Count)
                        {
                            Rana ranaAEliminar = listaRanasRefugiadas.animalesRefugiados[selectedIndex];

                            bool eliminacionExitosa = ado.EliminarRana(ranaAEliminar);

                            if (eliminacionExitosa)
                            {
                                listaRanasRefugiadas.animalesRefugiados.RemoveAt(selectedIndex);
                                lstVisor.Items.RemoveAt(selectedIndex);
                            }
                        }
                        else if (selectedIndex < listaRanasRefugiadas.animalesRefugiados.Count + listaHornerosRefugiados.animalesRefugiados.Count)
                        {
                            Hornero horneroAEliminar = listaHornerosRefugiados.animalesRefugiados[selectedIndex];

                            bool eliminacionExitosa = ado.EliminarHornero(horneroAEliminar);

                            if (eliminacionExitosa)
                            {
                                listaHornerosRefugiados.animalesRefugiados.RemoveAt(selectedIndex);
                                lstVisor.Items.RemoveAt(selectedIndex);
                            }
                        }
                        else
                        {
                            Ornitorrinco ornitorrincoAEliminar = listaOrnitorrincosRefugiados.animalesRefugiados[selectedIndex];

                            bool eliminacionExitosa = ado.EliminarOrnitorrinco(ornitorrincoAEliminar);

                            if (eliminacionExitosa)
                            {
                                listaOrnitorrincosRefugiados.animalesRefugiados.RemoveAt(selectedIndex);
                                lstVisor.Items.RemoveAt(selectedIndex);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Selecciona un elemento para eliminar.");
                    }

                }
                else
                {
                    MessageBox.Show("Usted no es administrador por lo tanto, no posee permisos para eliminar elementos");
                }
            }
        }
        private void FormPrincipal_Load(object sender, EventArgs e) 
        {
            
        }
        private void btnSalir_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("¿Estas seguro que quieres salir?", "SALIR", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                MessageBox.Show("Aplicacion Finalizada");
                Application.Exit();
            }
            else
            {
                MessageBox.Show("App no cerrada");
            }
        }
        private void btnOrdenar1_Click(object sender, EventArgs e)
        {
            listaOrnitorrincosRefugiados.animalesRefugiados.Sort((a1, a2) => listaOrnitorrincosRefugiados.OrdenarAnimalesPorCantidadDeExtremidades(a1, a2));
            listaRanasRefugiadas.animalesRefugiados.Sort((a1, a2) => listaRanasRefugiadas.OrdenarAnimalesPorCantidadDeExtremidades(a1, a2));
            listaHornerosRefugiados.animalesRefugiados.Sort((a1, a2) => listaOrnitorrincosRefugiados.OrdenarAnimalesPorCantidadDeExtremidades(a1, a2));

            ActualizarVisor();
        }
        private void btnOrdenar2_Click(object sender, EventArgs e)
        {
            listaOrnitorrincosRefugiados.animalesRefugiados.Sort((a1, a2) => listaOrnitorrincosRefugiados.OrdenarAnimalesPorEspecie(a1, a2));
            listaRanasRefugiadas.animalesRefugiados.Sort((a1, a2) => listaRanasRefugiadas.OrdenarAnimalesPorEspecie(a1, a2));
            listaHornerosRefugiados.animalesRefugiados.Sort((a1, a2) => listaOrnitorrincosRefugiados.OrdenarAnimalesPorEspecie(a1, a2));

            ActualizarVisor();
        }
        private void ActualizarVisor()
        {
            lstVisor.Items.Clear();

            foreach (Ornitorrinco ornitorrinco in listaOrnitorrincosRefugiados.animalesRefugiados)
            {
                lstVisor.Items.Add(ornitorrinco.ToString());
            }
            foreach (Rana rana in listaRanasRefugiadas.animalesRefugiados)
            {
                lstVisor.Items.Add(rana.ToString());
            }
            foreach (Hornero hornero in listaHornerosRefugiados.animalesRefugiados)
            {
                lstVisor.Items.Add(hornero.ToString());
            }
        }
        private void ActualizarRanas()
        {
            lstVisor.Items.Clear();
            foreach (Rana rana in listaRanasRefugiadas.animalesRefugiados)
            {
                lstVisor.Items.Add(rana.ToString());
            }
        }
        private void ActualizarHorneros()
        {
            lstVisor.Items.Clear();
            foreach (Hornero h in listaHornerosRefugiados.animalesRefugiados)
            {
                lstVisor.Items.Add(h.ToString());
            }
        }
        private void ActualizarOrnitorrincos()
        {
            lstVisor.Items.Clear();
            foreach (Ornitorrinco o in listaOrnitorrincosRefugiados.animalesRefugiados)
            {
                lstVisor.Items.Add(o.ToString());
            }
        }
        private void ManejarOperacionCompleta(bool exito, string mensaje)
        {
            if (exito)
            {
                MessageBox.Show($"{mensaje}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show($"{mensaje}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnVisualizador_Click(object sender, EventArgs e)
        {
            string rutaArchivoLog = @"..\..\..\usuarios.log";

            try
            {
                if (File.Exists(rutaArchivoLog))
                {
                    string contenidoLog = File.ReadAllText(rutaArchivoLog);

                    FormVisualizador formVisualizador = new FormVisualizador(contenidoLog);
                    formVisualizador.StartPosition = FormStartPosition.CenterScreen;
                    formVisualizador.ShowDialog();
                }
                else
                {
                    MessageBox.Show("El archivo de registro no existe o no se ha generado aún.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir el archivo de registro: {ex.Message}");
            }
        }

        private void btnArchivoEntrada_Click(object sender, EventArgs e)
        {
            if (openFileDialogDeserializar.ShowDialog() == DialogResult.OK)
            {
                string rutaArchivoEntrada = openFileDialogDeserializar.FileName;

                string archivoSeleccionado = Path.GetFileName(rutaArchivoEntrada);

                if (archivoSeleccionado == "ranas.json")
                {
                    ado.ObtenerListaRanas(listaRanasRefugiadas);
                    ActualizarRanas();
                }
                else if (archivoSeleccionado == "ornitorrincos.json")
                { 
                    ado.ObtenerListaOrnitorrincos(listaOrnitorrincosRefugiados);
                    ActualizarOrnitorrincos();
                }
                else if (archivoSeleccionado == "horneros.json")
                {
                    ado.ObtenerListaHorneros(listaHornerosRefugiados);
                    ActualizarHorneros();
                }
            }
        }
        private void btnArchivoSalida_Click(object sender, EventArgs e)
        {
            if (perfilUsuario.ToLower() == "administrador" || perfilUsuario.ToLower() == "supervisor")
            {
                if (saveFileDialogSerializar.ShowDialog() == DialogResult.OK)
                {
                    string rutaArchivoSalida = saveFileDialogSerializar.FileName;

                    string archivoSeleccionado = Path.GetFileName(rutaArchivoSalida);
                    if (archivoSeleccionado == "ranas.json")
                    {
                        SerializarAArchivoRana(rutaArchivoSalida, listaRanasRefugiadas);
                    }
                    else if (archivoSeleccionado == "ornitorrincos.json")
                    {
                        SerializarAArchivoOrnitorrinco(rutaArchivoSalida, listaOrnitorrincosRefugiados);
                    }
                    else if (archivoSeleccionado == "horneros.json")
                    {
                        SerializarAArchivoHornero(rutaArchivoSalida, listaHornerosRefugiados);
                    }
                }
            }
            else
            {
                MessageBox.Show("Usted es vendedor, por lo que solo puede leer los elementos, no guardarlos");
            }
        }
        public void SerializarAArchivoRana(string rutaArchivo, Refugio<Rana> lista)
        {
            string json = JsonConvert.SerializeObject(lista, Formatting.Indented);
            File.WriteAllText(rutaArchivo, json);
        }
        public void SerializarAArchivoOrnitorrinco(string rutaArchivo, Refugio<Ornitorrinco> lista)
        {
            string json = JsonConvert.SerializeObject(lista, Formatting.Indented);
            File.WriteAllText(rutaArchivo, json);
        }
        public void SerializarAArchivoHornero(string rutaArchivo, Refugio<Hornero> lista)
        {
            string json = JsonConvert.SerializeObject(lista, Formatting.Indented);
            File.WriteAllText(rutaArchivo, json);
        }
    }
}
```
3. `FormSeleccionAnimal`: Este formulario sirve para poder elegir el tipo de animal que deseemos crear entre los tipos de animales posibles, luego de seleccionar uno, se pasa al FormAgregar para poder darle los atributos correspondientes a la clase seleccionada.
```c#
using Entidades;
using PrimerParcial;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace WinFormsSegundoParcial
{
    public partial class FormSeleccionAnimal : Form
    {
        public FormSeleccionAnimal()
        {
            InitializeComponent();
            SeleccionRadioButtons();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            SeleccionAnimal();
            this.Close();
        }

        public void SeleccionAnimal()
        {

            if (rbtnHornero.Checked)
            {
                FormAgregarHornero frmHornero = new FormAgregarHornero();
                frmHornero.ShowDialog();
                this.Close();
            }
            else if (rbtnOrnitorrinco.Checked)
            {
                FormAgregarOrnitorrinco frmOrnitorrinco = new FormAgregarOrnitorrinco();
                frmOrnitorrinco.ShowDialog();
                this.Close();
            }
            else if (rbtnRana.Checked)
            {
                FormAgregarRana frmRana = new FormAgregarRana();
                frmRana.ShowDialog();
                this.Close();
            }   
            else
            {
                MessageBox.Show("Seleccione un Animal.");
            }
        }
        public Type GetSelectedAnimalType()
        {
            if (rbtnHornero.Checked)
            {
                return typeof(Hornero);
            }
            else if (rbtnOrnitorrinco.Checked)
            {
                return typeof(Ornitorrinco);
            }
            else if (rbtnRana.Checked)
            {
                return typeof(Rana);
            }
            else
            {
                throw new ExcepcionAnimalNoSeleccionado();
            }
        }

        public void SeleccionRadioButtons()
        {
            if (rbtnHornero.Checked)
            {
                rbtnOrnitorrinco.Checked = false;
                rbtnRana.Checked = false;
            }
            else if (rbtnOrnitorrinco.Checked)
            {
                rbtnRana.Checked = false;
                rbtnHornero.Checked = false;
            }
            else if (rbtnRana.Checked)
            {
                rbtnOrnitorrinco.Checked = false;
                rbtnHornero.Checked = false;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

            this.Close();
        }
    }
}

```

4. `FormAgregar`: Este formulario permite agregar un nuevo animal al refugio. El usuario puede modificar el nombre, si es peludo, y atributos específicos del animal que ha sido seleccionado en el visor, en este caso, este sirve como fomulario base, ya que FormAgregarRana, Ornitorrinco y Hornero heredan de este.

![image](https://github.com/fedecorbalan/Corbalan.Federico.PrimerParcial/assets/123754871/fe109fb8-e409-4726-8542-bd9d9c176581)

- FormAgregar
```c#
using Entidades;
using PrimerParcial;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsPrimerParcial
{
    public partial class FormAgregar : Form
    {

        public Animal NuevoAnimal { get; private set; }
        
        public FormPrincipal FormPrincipalRef { get; set; }


        public bool modificar = false;


        public FormAgregar()
        {
            InitializeComponent();
        }

        public FormAgregar(Animal a) : this()
        {
            txtNombre.Text = a.nombre;
            if (a.esPeludo)
            {
                txtEsPeludo.Text = "si";
            }
            else
            {
                txtEsPeludo.Text = "no";
            }
            this.modificar = true;
        }

        public string LblTitulo { get { return lblTitulo.Text; } set {lblTitulo.Text = value; } }

        public Button BtnAceptar
        {
            get { return btnAceptar; }
            set { btnAceptar = value; }
        }
        public Button BtnCancelar
        {
            get { return btnCancelar;}
            set { btnCancelar = value; }
        }
        public string TxtNombre { get { return txtNombre.Text; } set {txtNombre.Text = value; } }

        public string TxtPeludo { get {return txtEsPeludo.Text; } set {txtEsPeludo.Text = value;} }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
           
        }
        public bool VerificarEsPeludo()
        {
            string textoPeludo = txtEsPeludo.Text.ToLower();
            bool esPeludo;

            if (textoPeludo == "si")
            {
                esPeludo = true;
            }
            else
            {
                esPeludo = false;
            }
            return esPeludo;
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        public void AvisoDeErrores(List<string> errores, List<Exception> excepciones)
        {
            foreach (Exception excepcion in excepciones)
            {
                errores.Add(excepcion.Message);
            }
            if (errores.Count > 0)
            {
                string mensajeError = string.Join("\n", errores);
                MessageBox.Show(mensajeError, "Errores al validar datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void ValidarDatosAnimal(List<Exception> excepciones)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                excepciones.Add(new ExcepcionNombreVacio());
            }
            if (string.IsNullOrWhiteSpace(txtEsPeludo.Text))
            {
                excepciones.Add(new ExcepcionPeludoVacio());
            }
            else if (txtEsPeludo.Text.ToLower() != "si" && txtEsPeludo.Text.ToLower() != "no")
            {
                excepciones.Add(new ExcepcionPeludoErroneo());
            }
        }

    }
}


```
- FormAgregarRana
```c#
using Entidades;
using PrimerParcial;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsPrimerParcial;

namespace WinFormsSegundoParcial
{
    public partial class FormAgregarRana : FormAgregar
    {

        public Rana nuevaRana;

        public delegate void OperacionCompletaEventHandler(bool exito, string mensaje);
       
        public event OperacionCompletaEventHandler OperacionCompletada;

        AccesoDatos ado = new AccesoDatos();
        public FormAgregarRana()
        {
            InitializeComponent();
            BtnAceptar.Enabled = true;
            BtnCancelar.Enabled = true;
            BtnAceptar.Click += BtnAceptar_Click;
            BtnCancelar.Click += BtnCancelar_Click;
            this.FormPrincipalRef = (FormPrincipal)Application.OpenForms["FormPrincipal"];
        }
        private async void BtnAceptar_Click(object? sender, EventArgs e)
        {
            List<string> errores = new List<string>();
            List<Exception> excepciones = new List<Exception>();


            base.ValidarDatosAnimal(excepciones);
            this.ValidarDatosRana(excepciones);

            if (excepciones.Count > 0)
            {
                base.AvisoDeErrores(errores, excepciones);
            }
            else
            {
                nuevaRana = CrearRana();
                await AgregarRanaAsync(nuevaRana);
                OperacionCompletada?.Invoke(true, "Agregado de datos exitoso");
                this.DialogResult = DialogResult.OK;
            }
        }
        private void BtnCancelar_Click(object? sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        public bool ValidarVenenosa()
        {
            bool esVenenosa;
            if (txtVenenosa.Text.ToLower() == "si" )
            {
                esVenenosa = true;
            }
            else if (txtVenenosa.Text.ToLower() == "no")
            {
                esVenenosa = false;
            }
            else 
            { 
                throw new ExcepcionEsArboricolaErroneo(); 
            }
            return esVenenosa;

        }
        public Rana CrearRana()
        {
            bool esPeludo = VerificarEsPeludo();
            bool esVenenosa = ValidarVenenosa();
            bool esArboricola = ValidarArboricola();
            string nombre = TxtNombre.ToString();

            nuevaRana = new Rana(esArboricola, esVenenosa, esPeludo, Eespecies.Anfibio, nombre);

            return nuevaRana;
        }

        public bool ValidarArboricola()
        {
            bool esArboricola;

            if (txtArboricola.Text.ToLower() == "si")
            {
                esArboricola = true;
            }
            else if  (txtArboricola.Text.ToLower() == "no")
            {
                esArboricola = false;
            }
            else
            {
                throw new ExcepcionEsArboricolaErroneo();
            }
            return esArboricola;
        }

        public void ValidarDatosRana(List<Exception> excepciones)
        {
            if (string.IsNullOrWhiteSpace(txtArboricola.Text))
            {
                excepciones.Add(new ExcepcionEsArboricolaVacio());
            }
            else if (txtArboricola.Text.ToLower() != "si" && txtArboricola.Text.ToLower() != "no")
            {
                excepciones.Add(new ExcepcionEsArboricolaErroneo());
            }
            if (string.IsNullOrWhiteSpace(txtVenenosa.Text))
            {
                excepciones.Add(new ExcepcionEsVenenosaVacio());
            }
            else if (txtVenenosa.Text.ToLower() != "si" && txtVenenosa.Text.ToLower() != "no")
            {
                excepciones.Add(new ExcepcionEsVenenosaErroneo());
            }
        }
        public async Task AgregarRanaAsync(Rana r)
        {
            try
            {
                await Task.Run(() =>
                {
                    nuevaRana.Id = ObtenerIdCorrecto();
                    this.ado.AgregarRana(r);
                    FormPrincipalRef.listaRanasRefugiadas.AgregarAnimal(nuevaRana);
                });
            }
            catch(Exception ex)
            {
                OperacionCompletada?.Invoke(false, $"Error al agregar la rana: {ex.Message}");
            }
        }
        public int ObtenerIdCorrecto()
        {
            var ultimaRana = FormPrincipalRef.listaRanasRefugiadas.animalesRefugiados.LastOrDefault();

            if (ultimaRana is not null)
            {
                return ultimaRana.Id + 1;
            }
            else
            {
                return 1;
            }
        }
    }
}


```
- FormAgregarOrnitorrinco
```c#

using Entidades;
using PrimerParcial;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsPrimerParcial;

namespace WinFormsSegundoParcial
{
    public partial class FormAgregarOrnitorrinco : FormAgregar
    {
        public Ornitorrinco nuevoOrnitorrinco;

        public delegate void OperacionCompletaEventHandler(bool exito, string mensaje);

        public event OperacionCompletaEventHandler OperacionCompletada;

        AccesoDatos ado = new AccesoDatos();

        public FormAgregarOrnitorrinco()
        {
            InitializeComponent();
            BtnAceptar.Enabled = true;
            BtnCancelar.Enabled = true;
            BtnAceptar.Click += BtnAceptar_Click;
            BtnCancelar.Click += BtnCancelar_Click;
            this.FormPrincipalRef = (FormPrincipal)Application.OpenForms["FormPrincipal"];
        }
        public FormAgregarOrnitorrinco(Ornitorrinco o) : this()
        {
            LblTitulo = "Modificar Ornitorrinco";

            TxtNombre = o.nombre;

            if (o.esPeludo == true)
            {
                TxtPeludo = "si";
            }
            else
            {
                TxtPeludo = "no";
            }


            if (o.oviparo)
            {
                txtOviparo.Text = "si";
            }
            else
            {
                txtOviparo.Text = "no";
            }

            if (o.tieneCola)
            {
                txtTieneCola.Text = "si";
            }
            else
            {
                txtTieneCola.Text = "no";
            }

            nuevoOrnitorrinco = o;
            this.modificar = true;
        }
        private async void BtnAceptar_Click(object? sender, EventArgs e)
        {
            List<string> errores = new List<string>();
            List<Exception> excepciones = new List<Exception>();


            base.ValidarDatosAnimal(excepciones);
            this.ValidarDatosOrnitorrinco(excepciones);

            if (excepciones.Count > 0)
            {
                base.AvisoDeErrores(errores, excepciones);
            }
            else
            {
                nuevoOrnitorrinco = CrearOrnitorrinco();
                await AgregarOrnitorrincoAsync(nuevoOrnitorrinco);
                OperacionCompletada?.Invoke(true, "Agregado de datos exitoso");
                this.DialogResult = DialogResult.OK;
            }
        }

        private void BtnCancelar_Click(object? sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
        public Ornitorrinco CrearOrnitorrinco()
        {
            string nombre = TxtNombre.ToString();
            bool esPeludo = VerificarEsPeludo();
            bool esOviparo = VerificarOviparo();
            bool tieneCola = VerificarTieneCola();

            nuevoOrnitorrinco = new Ornitorrinco(tieneCola, esOviparo, esPeludo, Eespecies.Mamifero, nombre);

            return nuevoOrnitorrinco;
        }

        public bool VerificarOviparo() 
        {
            bool esOviparo;

            if(txtOviparo.Text == "si")
            {
                esOviparo = true;
            }
            else if(txtOviparo.Text == "no")
            {
                esOviparo = false;
            }
            else
            {
                throw new ExcepcionEsOviparoErroneo();
            }
            return esOviparo;
        }
        public bool VerificarTieneCola()
        {
            bool tieneCola;

            if (txtTieneCola.Text == "si")
            {
                tieneCola = true;
            }
            else if (txtTieneCola.Text == "no")
            {
                tieneCola = false;
            }
            else
            {
                throw new ExcepcionTieneColaVacio();
            }
            return tieneCola;
        }


        public void ValidarDatosOrnitorrinco(List<Exception> excepciones)
        {
            if (string.IsNullOrWhiteSpace(txtTieneCola.Text))
            {
                excepciones.Add(new ExcepcionEsArboricolaVacio());
            }
            else if (txtTieneCola.Text.ToLower() != "si" && txtTieneCola.Text.ToLower() != "no")
            {
                excepciones.Add(new ExcepcionEsArboricolaErroneo());
            }
            if (string.IsNullOrWhiteSpace(txtOviparo.Text))
            {
                excepciones.Add(new ExcepcionEsVenenosaVacio());
            }
            else if (txtOviparo.Text.ToLower() != "si" && txtOviparo.Text.ToLower() != "no")
            {
                excepciones.Add(new ExcepcionEsVenenosaErroneo());
            }
        }

        public async Task AgregarOrnitorrincoAsync(Ornitorrinco o)
        {
            try
            {
                await Task.Run(() =>
                {
                    nuevoOrnitorrinco.Id = ObtenerIdCorrecto();
                    this.ado.AgregarOrnitorrinco(o);
                    FormPrincipalRef.listaOrnitorrincosRefugiados.AgregarAnimal(nuevoOrnitorrinco);
                });
            }
            catch (Exception ex)
            {
                OperacionCompletada?.Invoke(false, $"Error al agregar el ornitorrinco: {ex.Message}");
            }

        }

        public async Task ModificarOrnitorrincoAsync(Ornitorrinco o)
        {
            try
            {
                await Task.Run(() =>
                {
                    this.ado.ModificarOrnitorrinco(o);
                });
            }
            catch (Exception ex)
            {
                OperacionCompletada?.Invoke(false, $"Error al modificar el Ornitorrinco: {ex.Message}");
            }
        }
        public int ObtenerIdCorrecto()
        {
            var ultimoOrnitorrinco = FormPrincipalRef.listaOrnitorrincosRefugiados.animalesRefugiados.LastOrDefault();

            if (!(modificar) && ultimoOrnitorrinco is not null)
            {
                return ultimoOrnitorrinco.Id + 1;
            }
            // Si la lista está vacía, devuelve 1 como el primer ID
            else
            {
                return 1;
            }
        }
    }
}

```
- FormAgregarHornero
```c#
using Entidades;
using PrimerParcial;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsPrimerParcial;
namespace WinFormsSegundoParcial
{
    public partial class FormAgregarHornero : FormAgregar
    {
        public Hornero nuevoHornero;

        public delegate void OperacionCompletaEventHandler(bool exito, string mensaje);

        public event OperacionCompletaEventHandler OperacionCompletada;

        AccesoDatos ado = new AccesoDatos();

        public FormAgregarHornero()
        {
            InitializeComponent();
            BtnAceptar.Enabled = true;
            BtnCancelar.Enabled = true;
            BtnAceptar.Click += BtnAceptar_Click;
            BtnCancelar.Click += BtnCancelar_Click;
            this.FormPrincipalRef = (FormPrincipal)Application.OpenForms["FormPrincipal"];
        }
        private async void BtnAceptar_Click(object? sender, EventArgs e)
        {
            List<string> errores = new List<string>();
            List<Exception> excepciones = new List<Exception>();


            base.ValidarDatosAnimal(excepciones);
            this.ValidarDatosHornero(excepciones);

            if (excepciones.Count > 0)
            {
                base.AvisoDeErrores(errores, excepciones);
            }
            else
            {
                nuevoHornero = CrearHornero();
                await AgregarHorneroAsync(nuevoHornero);
                OperacionCompletada?.Invoke(true, "Agregado de datos exitoso");
                this.DialogResult = DialogResult.OK;
            }
        }
        private void BtnCancelar_Click(object? sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        public Hornero CrearHornero()
        {
            string nombre = TxtNombre.ToString();
            bool esPeludo = VerificarEsPeludo();
            int velocidad = int.Parse(txtVelocidad.Text);
            bool tieneAlas = ValidarTieneAlas();

            nuevoHornero = new Hornero(velocidad, tieneAlas, esPeludo, Eespecies.Ave, nombre);

            return nuevoHornero;
        }

        public async Task AgregarHorneroAsync(Hornero h)
        {
            try
            {
                await Task.Run(() =>
                {
                    nuevoHornero.Id = ObtenerIdCorrecto();
                    this.ado.AgregarHornero(h);
                    FormPrincipalRef.listaHornerosRefugiados.AgregarAnimal(nuevoHornero);
                });
            }
            catch (Exception ex)
            {
                OperacionCompletada?.Invoke(false, $"Error al agregar el hornero: {ex.Message}");
            }

        }
        public bool ValidarTieneAlas()
        {
            bool tieneAlas;
            if (txtTieneAlas.Text == "si")
            {
                tieneAlas = true;
            }
            else if (txtTieneAlas.Text == "no")
            {
                tieneAlas = false;
            }
            else
            {
                throw new ExcepcionTieneAlasErroneo();
            }
            return tieneAlas;
        }
        public void ValidarDatosHornero(List<Exception> excepciones)
        {
            if (string.IsNullOrWhiteSpace(txtTieneAlas.Text))
            {
                excepciones.Add(new ExcepcionTieneAlasVacio());
            }
            else if (txtTieneAlas.Text.ToLower() != "si" && txtTieneAlas.Text.ToLower() != "no")
            {
                excepciones.Add(new ExcepcionTieneAlasErroneo());
            }
            if (string.IsNullOrWhiteSpace(txtVelocidad.Text))
            {
                excepciones.Add(new ExcepcionVelocidadVacio());
            }
            else if (!int.TryParse(txtVelocidad.Text, out int velocidad) || velocidad <= 0)
            {
                excepciones.Add(new ExcepcionVelocidadErroneo());
            }
        }
        public int ObtenerIdCorrecto()
        {
            var ultimoHornero = FormPrincipalRef.listaHornerosRefugiados.animalesRefugiados.LastOrDefault();

            if (ultimoHornero is not null)
            {
                return ultimoHornero.Id + 1;
            }
            else
            {
                return 1;
            }
        }
    }
}


```

5. `FormModificar`: Este formulario permite modificar los atributos disponibles, que dependen del tipo de animal seleccionado, en este caso, tambien presenta herencia grafica para cada cada clase hija.

```c#
using Entidades;
using PrimerParcial;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsPrimerParcial;

namespace WinFormsSegundoParcial
{
    public partial class FormModificar : Form
    {

        public Animal animalAModificar { get; private set; }

        public FormPrincipal FormPrincipalRef { get; set; }

        public FormModificar()
        {
            InitializeComponent();
        }


        public FormModificar(Animal a) : this()
        {
            txtNombre.Text = a.nombre;
            if (a.esPeludo)
            {
                txtEsPeludo.Text = "si";
            }
            else
            {
                txtEsPeludo.Text = "no";
            }
        }

        public string LblTitulo { get { return lblTitulo.Text; } set { lblTitulo.Text = value; } }

        public Button BtnAceptar
        {
            get { return btnAceptar; }
            set { btnAceptar = value; }
        }
        public Button BtnCancelar
        {
            get { return btnCancelar; }
            set { btnCancelar = value; }
        }
        public string TxtNombre { get { return txtNombre.Text; } set { txtNombre.Text = value; } }

        public string TxtPeludo { get { return txtEsPeludo.Text; } set { txtEsPeludo.Text = value; } }

        private void btnAceptar_Click(object sender, EventArgs e)
        {

        }
        public bool VerificarEsPeludo()
        {
            string textoPeludo = txtEsPeludo.Text.ToLower();
            bool esPeludo;

            if (textoPeludo == "si")
            {
                esPeludo = true;
            }
            else
            {
                esPeludo = false;
            }
            return esPeludo;
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        public void AvisoDeErrores(List<string> errores, List<Exception> excepciones)
        {
            foreach (Exception excepcion in excepciones)
            {
                errores.Add(excepcion.Message);
            }
            if (errores.Count > 0)
            {
                string mensajeError = string.Join("\n", errores);
                MessageBox.Show(mensajeError, "Errores al validar datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void ValidarDatosAnimal(List<Exception> excepciones)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                excepciones.Add(new ExcepcionNombreVacio());
            }
            if (string.IsNullOrWhiteSpace(txtEsPeludo.Text))
            {
                excepciones.Add(new ExcepcionPeludoVacio());
            }
            else if (txtEsPeludo.Text.ToLower() != "si" && txtEsPeludo.Text.ToLower() != "no")
            {
                excepciones.Add(new ExcepcionPeludoErroneo());
            }
        }
        private void FormModificar_Load(object sender, EventArgs e)
        {

        }
    }
}

```
- FormModificarRana
```c#

using Entidades;
using PrimerParcial;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsPrimerParcial;

namespace WinFormsSegundoParcial
{
    public partial class FormModificarRana : FormModificar
    {
        public Rana ranaAModificar;

        public delegate void OperacionCompletaEventHandler(bool exito, string mensaje);

        public event OperacionCompletaEventHandler OperacionCompletada;

        AccesoDatos ado = new AccesoDatos();
        public FormModificarRana()
        {
            InitializeComponent();
            BtnAceptar.Enabled = true;
            BtnCancelar.Enabled = true;
            BtnAceptar.Click += BtnAceptar_Click;
            BtnCancelar.Click += BtnCancelar_Click;
            this.FormPrincipalRef = (FormPrincipal)Application.OpenForms["FormPrincipal"];
        }

        public FormModificarRana(Rana r) : this()
        {
            LblTitulo = "Modificar Rana";

            TxtNombre = r.nombre;

            if (r.esPeludo == true)
            {
                TxtPeludo = "si";
            }
            else
            {
                TxtPeludo = "no";
            }

            if (r.esVenenosa)
            {
                txtVenenosa.Text = "si";
            }
            else
            {
                txtVenenosa.Text = "no";
            }

            if (r.esArboricola)
            {
                txtArboricola.Text = "si";
            }
            else
            {
                txtArboricola.Text = "no";
            }

            ranaAModificar = r;
        }

        private async void BtnAceptar_Click(object? sender, EventArgs e)
        {

            List<string> errores = new List<string>();
            List<Exception> excepciones = new List<Exception>();


            base.ValidarDatosAnimal(excepciones);
            this.ValidarDatosRana(excepciones);

            if (excepciones.Count > 0)
            {
                base.AvisoDeErrores(errores, excepciones);
            }
            else
            {
                ranaAModificar.nombre = TxtNombre;
                ranaAModificar.esPeludo = VerificarEsPeludo();
                ranaAModificar.esVenenosa = ValidarVenenosa();
                ranaAModificar.esArboricola = ValidarArboricola();

                await ModificarRanaAsync(ranaAModificar);
                OperacionCompletada?.Invoke(true, "Modificación de datos exitoso");
                this.DialogResult = DialogResult.OK;
            }
        }
        private void BtnCancelar_Click(object? sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
        public bool ValidarVenenosa()
        {
            bool esVenenosa;
            if (txtVenenosa.Text.ToLower() == "si")
            {
                esVenenosa = true;
            }
            else if (txtVenenosa.Text.ToLower() == "no")
            {
                esVenenosa = false;
            }
            else
            {
                throw new ExcepcionEsArboricolaErroneo();
            }
            return esVenenosa;

        }
        public bool ValidarArboricola()
        {
            bool esArboricola;

            if (txtArboricola.Text.ToLower() == "si")
            {
                esArboricola = true;
            }
            else if (txtArboricola.Text.ToLower() == "no")
            {
                esArboricola = false;
            }
            else
            {
                throw new ExcepcionEsArboricolaErroneo();
            }
            return esArboricola;
        }

        public void ValidarDatosRana(List<Exception> excepciones)
        {
            if (string.IsNullOrWhiteSpace(txtArboricola.Text))
            {
                excepciones.Add(new ExcepcionEsArboricolaVacio());
            }
            else if (txtArboricola.Text.ToLower() != "si" && txtArboricola.Text.ToLower() != "no")
            {
                excepciones.Add(new ExcepcionEsArboricolaErroneo());
            }
            if (string.IsNullOrWhiteSpace(txtVenenosa.Text))
            {
                excepciones.Add(new ExcepcionEsVenenosaVacio());
            }
            else if (txtVenenosa.Text.ToLower() != "si" && txtVenenosa.Text.ToLower() != "no")
            {
                excepciones.Add(new ExcepcionEsVenenosaErroneo());
            }
        }
        public async Task ModificarRanaAsync(Rana r)
        {
            try
            {
                await Task.Run(() =>
                {
                    ranaAModificar.ActualizarRana(r);
                    this.ado.ModificarRana(r);
                });
            }
            catch (Exception ex)
            {
                OperacionCompletada?.Invoke(false, $"Error al modificar la rana: {ex.Message}");
            }
        }
    }
}

```
- FormModificarOrnitorrinco
```c#
using Entidades;
using PrimerParcial;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsPrimerParcial;

namespace WinFormsSegundoParcial
{
    public partial class FormModificarOrnitorrinco : FormModificar
    {
        public Ornitorrinco ornitorrincoAModificar;

        public delegate void OperacionCompletaEventHandler(bool exito, string mensaje);

        public event OperacionCompletaEventHandler OperacionCompletada;

        AccesoDatos ado = new AccesoDatos();

        public FormModificarOrnitorrinco()
        {
            InitializeComponent();
            BtnCancelar.Enabled = true;
            BtnAceptar.Click += BtnAceptar_Click;
            BtnCancelar.Click += BtnCancelar_Click;
            this.FormPrincipalRef = (FormPrincipal)Application.OpenForms["FormPrincipal"];
        }
        public FormModificarOrnitorrinco(Ornitorrinco o) : this()
        {
            LblTitulo = "Modificar Ornitorrinco";

            TxtNombre = o.nombre;

            if (o.esPeludo == true)
            {
                TxtPeludo = "si";
            }
            else
            {
                TxtPeludo = "no";
            }
            if (o.oviparo)
            {
                txtOviparo.Text = "si";
            }
            else
            {
                txtOviparo.Text = "no";
            }

            if (o.tieneCola)
            {
                txtTieneCola.Text = "si";
            }
            else
            {
                txtTieneCola.Text = "no";
            }

            ornitorrincoAModificar = o;
        }
        private async void BtnAceptar_Click(object? sender, EventArgs e)
        {
            List<string> errores = new List<string>();
            List<Exception> excepciones = new List<Exception>();


            base.ValidarDatosAnimal(excepciones);
            this.ValidarDatosOrnitorrinco(excepciones);

            if (excepciones.Count > 0)
            {
                base.AvisoDeErrores(errores, excepciones);
            }
            else
            {
                ornitorrincoAModificar.nombre = TxtNombre;
                ornitorrincoAModificar.esPeludo = VerificarEsPeludo();
                ornitorrincoAModificar.tieneCola = VerificarTieneCola();
                ornitorrincoAModificar.oviparo = VerificarOviparo();

                await ModificarOrnitorrincoAsync(ornitorrincoAModificar);
                OperacionCompletada?.Invoke(true, "Modificacion de datos exitoso");
                this.DialogResult = DialogResult.OK;
            }
        }
        private void BtnCancelar_Click(object? sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
        public bool VerificarOviparo()
        {
            bool esOviparo;

            if (txtOviparo.Text == "si")
            {
                esOviparo = true;
            }
            else if (txtOviparo.Text == "no")
            {
                esOviparo = false;
            }
            else
            {
                throw new ExcepcionEsOviparoErroneo();
            }
            return esOviparo;
        }
        public bool VerificarTieneCola()
        {
            bool tieneCola;

            if (txtTieneCola.Text == "si")
            {
                tieneCola = true;
            }
            else if (txtTieneCola.Text == "no")
            {
                tieneCola = false;
            }
            else
            {
                throw new ExcepcionTieneColaVacio();
            }
            return tieneCola;
        }
        public void ValidarDatosOrnitorrinco(List<Exception> excepciones)
        {
            if (string.IsNullOrWhiteSpace(txtTieneCola.Text))
            {
                excepciones.Add(new ExcepcionEsArboricolaVacio());
            }
            else if (txtTieneCola.Text.ToLower() != "si" && txtTieneCola.Text.ToLower() != "no")
            {
                excepciones.Add(new ExcepcionEsArboricolaErroneo());
            }
            if (string.IsNullOrWhiteSpace(txtOviparo.Text))
            {
                excepciones.Add(new ExcepcionEsVenenosaVacio());
            }
            else if (txtOviparo.Text.ToLower() != "si" && txtOviparo.Text.ToLower() != "no")
            {
                excepciones.Add(new ExcepcionEsVenenosaErroneo());
            }
        }
        public async Task ModificarOrnitorrincoAsync(Ornitorrinco o)
        {
            try
            {
                await Task.Run(() =>
                {
                    ornitorrincoAModificar.ActualizarOrnitorrinco(o);
                    this.ado.ModificarOrnitorrinco(o);
                });
            }
            catch (Exception ex)
            {
                OperacionCompletada?.Invoke(false, $"Error al modificar el Ornitorrinco: {ex.Message}");
            }
        }

    }
}
```
- FormModificarHornero
```c#

using Entidades;
using PrimerParcial;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsPrimerParcial;

namespace WinFormsSegundoParcial
{
    public partial class FormModificarHornero : FormModificar
    {
        public Hornero horneroAModificar;

        public delegate void OperacionCompletaEventHandler(bool exito, string mensaje);

        public event OperacionCompletaEventHandler OperacionCompletada;

        AccesoDatos ado = new AccesoDatos();
        public FormModificarHornero()
        {
            InitializeComponent();
            BtnAceptar.Enabled = true;
            BtnCancelar.Enabled = true;
            BtnAceptar.Click += BtnAceptar_Click;
            BtnCancelar.Click += BtnCancelar_Click;
            this.FormPrincipalRef = (FormPrincipal)Application.OpenForms["FormPrincipal"];
        }
        public FormModificarHornero(Hornero h) : this()
        {
            LblTitulo = "Modificar Hornero";

            TxtNombre = h.nombre;

            if (h.esPeludo == true)
            {
                TxtPeludo = "si";
            }
            else
            {
                TxtPeludo = "no";
            }

            if (h.tieneAlas)
            {
                txtAlas.Text = "si";
            }
            else
            {
                txtAlas.Text = "no";
            }

            txtVelocidad.Text = h.velocidadKmH.ToString();

            horneroAModificar = h;
        }

        private async void BtnAceptar_Click(object? sender, EventArgs e)
        {
            List<string> errores = new List<string>();
            List<Exception> excepciones = new List<Exception>();


            base.ValidarDatosAnimal(excepciones);
            this.ValidarDatosHornero(excepciones);

            if (excepciones.Count > 0)
            {
                base.AvisoDeErrores(errores, excepciones);
            }
            else
            {
                horneroAModificar.nombre = TxtNombre;
                horneroAModificar.esPeludo = VerificarEsPeludo();
                horneroAModificar.tieneAlas = ValidarTieneAlas();
                horneroAModificar.velocidadKmH = int.Parse(txtVelocidad.Text);

                await ModificarHorneroAsync(horneroAModificar);
                OperacionCompletada?.Invoke(true, "Modificación de datos exitoso");
                this.DialogResult = DialogResult.OK;
            }
        }
        private void BtnCancelar_Click(object? sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        public async Task ModificarHorneroAsync(Hornero h)
        {
            try
            {
                await Task.Run(() =>
                {
                    horneroAModificar.ActualizarHornero(h);
                    this.ado.ModificarHornero(h);
                });
            }
            catch (Exception ex)
            {
                OperacionCompletada?.Invoke(false, $"Error al modificar el hornero: {ex.Message}");
            }
        }

        public bool ValidarTieneAlas()
        {
            bool tieneAlas;
            if (txtAlas.Text == "si")
            {
                tieneAlas = true;
            }
            else if (txtAlas.Text == "no")
            {
                tieneAlas = false;
            }
            else
            {
                throw new ExcepcionTieneAlasErroneo();
            }
            return tieneAlas;
        }

        public void ValidarDatosHornero(List<Exception> excepciones)
        {
            if (string.IsNullOrWhiteSpace(txtAlas.Text))
            {
                excepciones.Add(new ExcepcionTieneAlasVacio());
            }
            else if (txtAlas.Text.ToLower() != "si" && txtAlas.Text.ToLower() != "no")
            {
                excepciones.Add(new ExcepcionTieneAlasErroneo());
            }
            if (string.IsNullOrWhiteSpace(txtVelocidad.Text))
            {
                excepciones.Add(new ExcepcionVelocidadVacio());
            }
            else if (!int.TryParse(txtVelocidad.Text, out int velocidad) || velocidad <= 0)
            {
                excepciones.Add(new ExcepcionVelocidadErroneo());
            }
        }
    }
}
```


4. `FormVisualizador`: Este formulario permite observar los usuarios que han ingresado al programa junto con sus datos y la fecha y la hora en la que han ingresado. Cuenta con una instancia en el formulario principal.

![image](https://github.com/fedecorbalan/Corbalan.Federico.PrimerParcial/assets/123754871/613248d9-dc00-494f-a51e-44bec43da1f2)

```c#
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsPrimerParcial
{
    public partial class FormVisualizador : Form
    {
        public FormVisualizador(string logContent)
        {
            InitializeComponent();
            rtxtUsuarios.Text = logContent;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

```
```c#
private void btnVisualizador_Click(object sender, EventArgs e)
{
    string rutaArchivoLog = @"..\..\..\usuarios.log";

    try
    {
        if (File.Exists(rutaArchivoLog))
        {
            string contenidoLog = File.ReadAllText(rutaArchivoLog);

            FormVisualizador formVisualizador = new FormVisualizador(contenidoLog);
            formVisualizador.StartPosition = FormStartPosition.CenterScreen;
            formVisualizador.ShowDialog();
        }
        else
        {
            MessageBox.Show("El archivo de registro no existe o no se ha generado aún.");
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show($"Error al abrir el archivo de registro: {ex.Message}");
    }
}
```

## Serialización y Deserialización

El proyecto utiliza tanto métodos de Serialización como de Deserialización, en este caso, la Serializacion puede darse tranquilamente, eligiendo y sobreescribiendo el archivo que se desee serializar, pero lo que conllevaria la deserializacion solo se da en el login, y los que serian los metodos de deserializacion, solamente traen los datos existentes en la base de datos.

```c#
        private void btnArchivoEntrada_Click(object sender, EventArgs e)
    {
        if (openFileDialogDeserializar.ShowDialog() == DialogResult.OK)
        {
            string rutaArchivoEntrada = openFileDialogDeserializar.FileName;

            string archivoSeleccionado = Path.GetFileName(rutaArchivoEntrada);

            if (archivoSeleccionado == "ranas.json")
            {
                ado.ObtenerListaRanas(listaRanasRefugiadas);
                ActualizarRanas();
            }
            else if (archivoSeleccionado == "ornitorrincos.json")
            { 
                ado.ObtenerListaOrnitorrincos(listaOrnitorrincosRefugiados);
                ActualizarOrnitorrincos();
            }
            else if (archivoSeleccionado == "horneros.json")
            {
                ado.ObtenerListaHorneros(listaHornerosRefugiados);
                ActualizarHorneros();
            }
        }
    }
    private void btnArchivoSalida_Click(object sender, EventArgs e)
    {
        if (perfilUsuario.ToLower() == "administrador" || perfilUsuario.ToLower() == "supervisor")
        {
            if (saveFileDialogSerializar.ShowDialog() == DialogResult.OK)
            {
                string rutaArchivoSalida = saveFileDialogSerializar.FileName;

                string archivoSeleccionado = Path.GetFileName(rutaArchivoSalida);
                if (archivoSeleccionado == "ranas.json")
                {
                    SerializarAArchivoRana(rutaArchivoSalida, listaRanasRefugiadas);
                }
                else if (archivoSeleccionado == "ornitorrincos.json")
                {
                    SerializarAArchivoOrnitorrinco(rutaArchivoSalida, listaOrnitorrincosRefugiados);
                }
                else if (archivoSeleccionado == "horneros.json")
                {
                    SerializarAArchivoHornero(rutaArchivoSalida, listaHornerosRefugiados);
                }
            }
        }
        else
        {
            MessageBox.Show("Usted es vendedor, por lo que solo puede leer los elementos, no guardarlos");
        }
    }
    public void SerializarAArchivoRana(string rutaArchivo, Refugio<Rana> lista)
    {
        string json = JsonConvert.SerializeObject(lista, Formatting.Indented);
        File.WriteAllText(rutaArchivo, json);
    }
    public void SerializarAArchivoOrnitorrinco(string rutaArchivo, Refugio<Ornitorrinco> lista)
    {
        string json = JsonConvert.SerializeObject(lista, Formatting.Indented);
        File.WriteAllText(rutaArchivo, json);
    }
    public void SerializarAArchivoHornero(string rutaArchivo, Refugio<Hornero> lista)
    {
        string json = JsonConvert.SerializeObject(lista, Formatting.Indented);
        File.WriteAllText(rutaArchivo, json);
    }
}
```

## Excepciones

El proyecto maneja excepciones tanto del sistema, como propias, en el caso de las propias, se ha creado una clase que contienen estas excepciones, que se ejecutan primordialmente en las validaciones de los animales.

```c#
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
```

## Pruebas Unitarias

En este proyecto se dan una serie de pruebas unitarias, las cuales estan basadas en metodos correspondientes al proyecto principal, para que se puedan comprobar, se debe establecer como proyecto de inicio el archivo ConsolaUnitTest y ejecutar su program, el cual contiene las pruebas a realizar, que son las siguientes :

```c#
using Entidades;
using PrimerParcial;
using System.Text;
using WinFormsPrimerParcial;

namespace PruebasUnitarias
{
    [TestClass]
    public class Pruebas
    {
        [TestMethod]
        public static void TestComparacion1()
        {
            //arrange
            Ornitorrinco ornitorrinco = new Ornitorrinco(true, true, true, Eespecies.Mamifero, "tomas");
            Rana rana = new Rana(true, true, false, Eespecies.Anfibio, "marcos");

            //act
            int resultado = OrdenarAnimalesPorCantidadDeExtremidades(ornitorrinco, rana);

            //assert
            Assert.AreEqual(0, resultado);

            Console.WriteLine(resultado);
        }

        [TestMethod]
        public static void TestComparacion2()
        {
            //arrange
            Ornitorrinco ornitorrinco = new Ornitorrinco(true, true, true, Eespecies.Mamifero, "tomas");
            Rana rana = new Rana(true, true, false, Eespecies.Anfibio, "marcos");
            
            //act
            int resultado = OrdenarAnimalesPorEspecie(ornitorrinco, rana);

            //assert
            Assert.AreEqual(-1, resultado);

            Console.WriteLine(resultado);
        }
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
        public static string EmitirSonido()
        {
            string sonidoHornero = "Pi-pi-pi!";
            return sonidoHornero;
        }
    }
}
```

## Uso del Programa

Puede ejecutar la aplicación y utilizar la ventana principal (`FormPrincipal`) para gestionar los animales en el refugio, obviamente luego del login. Se debe elegir el archivo al que quiere deserializar para poder ver los animales en el visor, al descargar este repositorio, usted puede encontrar en la carpeta ArchivosASeleccionar los archivos JSON que corresponden a los diferentes animales, pero en este caso, trae los datos existentes en la base de datos correspondiente al archivo seleccionado, sea ranas, ornitorrincos u horneros. Puede agregar nuevos animales, modificar sus atributos (no puede cambiar de especie) y eliminar animales. También, la aplicación permite ordenar la lista de animales por cantidad de extremidades y especie. Además cuenta con un visualizador de usuarios, el cual le brinda toda la informacion acerca de los usuarios que han ingresado con su cuenta al programa y la fecha y hora en la que lo hicieron.

# Diagrama de clases
![image](https://github.com/fedecorbalan/Corbalan.Federico.SegundoParcial/assets/123754871/7732a4d3-a236-4aa4-9dd5-efef80790d5b)

# Fin del Repositorio
Muchas gracias por leer ;D!
