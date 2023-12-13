# CRUD - Refugio de Animales
Este repositorio contiene un proyecto de C# que simula un refugio de animales. En el refugio se pueden alojar tres tipos de animales: Ornitorrincos, Ranas y Horneros.
## Sobre mi
- Mi nombre es Federico Corbalán, tengo 18 años y actualmente me encuentro cursando la Tecnicatura en Programación en UTN FRA, con el objetivo de poder expandir mis horizontes para poder mejorar mis habilidades como programador.
# Resumen
- En este programa, lo que se busca es poder listar a los diferentes animales que se encuentran en el refugio en un formulario, al que se ingresa mediante un Login de Usuarios, el cual segun el correo y contraseña ingresados, se determinaran los permisos del formulario segun el perfíl del usuario, en este caso se presentan Vendedor, Supervisor y Administrador. En base a esto cabe resaltar que el Administrador puede administrar las todas las funciones del CRUD (Create, Read, Update, Delete), en el caso del Supervisor, puede ejecutar las funciones CRU (Create, Read, Update) y no puede realizar el Delete. Y por último, el Vendedor solo puede hacer el Read de los archivos.
- Tambien me gustaria aclarar que hay bloques de codigo que no estan documentados en este repositorio ya que no se han realizado cambios en ellos dentro del proyecto, aquellos que si presentan la documentacion si fueron modificados.
  
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
    /// <summary>
    /// Clase del formulario principal de la aplicación.
    /// </summary>
    public partial class FormPrincipal : Form
    {
        /// <summary>
        /// Listas de Ornitorrincos, Horneros y Ranas refugiadas.
        /// </summary>
        public Refugio<Rana> listaRanasRefugiadas;
        public Refugio<Hornero> listaHornerosRefugiados;
        public Refugio<Ornitorrinco> listaOrnitorrincosRefugiados;

        /// <summary>
        /// Perfil del usuario actual.
        /// </summary>
        public string perfilUsuario;

        /// <summary>
        /// Instancia de la clase de acceso a datos.
        /// </summary>
        AccesoDatos ado = new AccesoDatos();
       
        /// <summary>
        /// ListBox utilizado para mostrar información en el formulario.
        /// </summary>
        public ListBox LstVisor { get; set; }

        /// <summary>
        /// Constructor de la clase FormPrincipal.
        /// </summary>
        public FormPrincipal()
        {
            InitializeComponent();
            MostrarFechaActual();

            this.listaHornerosRefugiados = new Refugio<Hornero>();
            this.listaOrnitorrincosRefugiados = new Refugio<Ornitorrinco>();
            this.listaRanasRefugiadas = new Refugio<Rana>();
        }
        /// <summary>
        /// Constructor sobrecargado de la clase FormPrincipal.
        /// </summary>
        /// <param name="usuario">Usuario que ha iniciado sesión.</param>
        public FormPrincipal(Usuario usuario) : this()
        {
            MessageBox.Show($"Bienvenido, {usuario.nombre}");
            this.lblInfo.Text = usuario.nombre;
            this.perfilUsuario = usuario.perfil;

        }
        /// <summary>
        /// Muestra la fecha actual en el formulario.
        /// </summary>
        private void MostrarFechaActual()
        {
            DateTime fechaActual = DateTime.Now;
            lblDateTime.Text = fechaActual.ToString("dd/MM/yyyy");
        }
        /// <summary>
        /// Maneja el evento de clic en el botón "Agregar".
        /// </summary>
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
        /// <summary>
        /// Maneja el evento de clic en el botón "Modificar".
        /// </summary>
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
        /// <summary>
        /// Maneja el evento de clic en el botón "Eliminar".
        /// </summary>
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            
            int selectedIndex = lstVisor.SelectedIndex;

            if (selectedIndex >= 0)
            {
                if (perfilUsuario.ToLower() == "administrador")
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
                                MessageBox.Show("Rana eliminada");
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
                                MessageBox.Show("Hornero eliminado");
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
                                MessageBox.Show("Ornitorrinco Eliminado");
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Usted no es administrador por lo tanto, no posee permisos para eliminar elementos");
                }
            }
            else
            {
                MessageBox.Show("Selecciona un elemento para eliminar.");
            }
        }
        /// <summary>
        /// Maneja el evento Load.
        /// </summary>
        private void FormPrincipal_Load(object sender, EventArgs e) 
        {
            
        }
        /// <summary>
        /// Maneja el evento de clic en el botón "Salir".
        /// </summary>
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
        /// <summary>
        /// Ordena la lista de animales por cantidad de extremidades y actualiza el visor.
        /// </summary>
        private void btnOrdenar1_Click(object sender, EventArgs e)
        {
            listaOrnitorrincosRefugiados.animalesRefugiados.Sort((a1, a2) => listaOrnitorrincosRefugiados.OrdenarAnimalesPorCantidadDeExtremidades(a1, a2));
            listaRanasRefugiadas.animalesRefugiados.Sort((a1, a2) => listaRanasRefugiadas.OrdenarAnimalesPorCantidadDeExtremidades(a1, a2));
            listaHornerosRefugiados.animalesRefugiados.Sort((a1, a2) => listaOrnitorrincosRefugiados.OrdenarAnimalesPorCantidadDeExtremidades(a1, a2));

            ActualizarVisor();
        }
        /// <summary>
        /// Ordena la lista de animales por Especie y actualiza el visor.
        /// </summary>
        private void btnOrdenar2_Click(object sender, EventArgs e)
        {
            listaOrnitorrincosRefugiados.animalesRefugiados.Sort((a1, a2) => listaOrnitorrincosRefugiados.OrdenarAnimalesPorEspecie(a1, a2));
            listaRanasRefugiadas.animalesRefugiados.Sort((a1, a2) => listaRanasRefugiadas.OrdenarAnimalesPorEspecie(a1, a2));
            listaHornerosRefugiados.animalesRefugiados.Sort((a1, a2) => listaOrnitorrincosRefugiados.OrdenarAnimalesPorEspecie(a1, a2));

            ActualizarVisor();
        }
        /// <summary>
        /// Actualiza el contenido del visor con la información de los animales.
        /// </summary>
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
        /// <summary>
        /// Actualiza el contenido del visor con la información de las ranas.
        /// </summary>
        private void ActualizarRanas()
        {
            lstVisor.Items.Clear();
            foreach (Rana rana in listaRanasRefugiadas.animalesRefugiados)
            {
                lstVisor.Items.Add(rana.ToString());
            }
        }
        /// <summary>
        /// Actualiza el contenido del visor con la información de los horneros.
        /// </summary>
        private void ActualizarHorneros()
        {
            lstVisor.Items.Clear();
            foreach (Hornero h in listaHornerosRefugiados.animalesRefugiados)
            {
                lstVisor.Items.Add(h.ToString());
            }
        }
        /// <summary>
        /// Actualiza el contenido del visor con la información de los ornitorrincos.
        /// </summary>
        private void ActualizarOrnitorrincos()
        {
            lstVisor.Items.Clear();
            foreach (Ornitorrinco o in listaOrnitorrincosRefugiados.animalesRefugiados)
            {
                lstVisor.Items.Add(o.ToString());
            }
        }
        /// <summary>
        /// Maneja el resultado de una operación completa.
        /// </summary>
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
        /// <summary>
        /// Abre un formulario para visualizar el contenido del archivo de registro llamado usuarios.log.
        /// </summary>
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
        /// <summary>
        /// Abre el diálogo para seleccionar un archivo de entrada y carga la información.
        /// </summary>
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
        /// <summary>
        /// Abre el diálogo para seleccionar un archivo de salida y guarda la información.
        /// </summary>
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
        /// <summary>
        /// Serializa la lista de ranas y guarda el contenido en un archivo, en este caso un JSON.
        /// </summary>
        public void SerializarAArchivoRana(string rutaArchivo, Refugio<Rana> lista)
        {
            string json = JsonConvert.SerializeObject(lista, Formatting.Indented);
            File.WriteAllText(rutaArchivo, json);
        }
        /// <summary>
        /// Serializa la lista de ornitorrincos y guarda el contenido en un archivo, en este caso un JSON.
        /// </summary>
        public void SerializarAArchivoOrnitorrinco(string rutaArchivo, Refugio<Ornitorrinco> lista)
        {
            string json = JsonConvert.SerializeObject(lista, Formatting.Indented);
            File.WriteAllText(rutaArchivo, json);
        }
        /// <summary>
        /// Serializa la lista de horneros y guarda el contenido en un archivo, en este caso un JSON.
        /// </summary>
        public void SerializarAArchivoHornero(string rutaArchivo, Refugio<Hornero> lista)
        {
            string json = JsonConvert.SerializeObject(lista, Formatting.Indented);
            File.WriteAllText(rutaArchivo, json);
        }
    }
}
```
2. `FormSeleccionAnimal`: Este formulario sirve para poder elegir el tipo de animal que deseemos crear entre los tipos de animales posibles, luego de seleccionar uno, se pasa al FormAgregar para poder darle los atributos correspondientes a la clase seleccionada.

![image](https://github.com/fedecorbalan/Corbalan.Federico.SegundoParcial/assets/123754871/82f0a92b-a5b3-45b7-9dcb-3d7dfa370dc3)


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
3. `FormEspera`: Este formulario solamente contiene un label que sirve a modo de pantalla de espera.

![image](https://github.com/fedecorbalan/Corbalan.Federico.SegundoParcial/assets/123754871/40486678-6791-430e-b636-102bdafdaa89)

   
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

namespace WinFormsSegundoParcial
{
    public partial class FormEspera : Form
    {
        public FormEspera()
        {
            InitializeComponent();
        }
    }
}

```

4. `FormAgregar`: Este formulario permite agregar un nuevo animal al refugio. El usuario puede modificar el nombre, si es peludo, y atributos específicos del animal que ha sido seleccionado en el visor, en este caso, este sirve como fomulario base, ya que FormAgregarRana, Ornitorrinco y Hornero heredan de este.
   
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
    /// <summary>
    /// Formulario para agregar animales.
    /// </summary>
    public partial class FormAgregar : Form
    {

        /// <summary>
        /// Propiedades que obtienen o establecen el nuevo animal creado y una referencia al form principal.
        /// </summary>
        public Animal NuevoAnimal { get; private set; }

        public FormPrincipal FormPrincipalRef { get; set; }

        /// <summary>
        /// Constructor por defecto del form
        /// </summary>
        public FormAgregar()
        {
            InitializeComponent();
            setRadioButtons();
        }
        /// <summary>
        /// Propiedades que obtiene o establecen el título de un Label, Un boton aceptar, un boton cancelar y el texto de la Textbox Nombre.
        /// </summary>
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

        /// <summary>
        /// Maneja el evento aceptar que en este caso no se usa, ya que este seria un form padre.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAceptar_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Obtiene el valor de la opción seleccionada en el grupo "Es peludo".
        /// </summary>
        /// <returns>True si es peludo, false si no lo es.</returns>
        public bool VerificarEsPeludo()
        {
            bool esPeludo;

            if (rbtnPeludoSi.Checked)
            {
                esPeludo = true;
            }
            else if (rbtnPeludoNo.Checked)
            {
                esPeludo = false;
            }
            else
            {
                throw new ExcepcionPeludoVacio();
            }
            return esPeludo;
        }
        /// <summary>
        /// Maneja el evento de hacer clic en el botón Cancelar.
        /// </summary>
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// Muestra mensajes de error en caso de excepciones.
        /// </summary>
        /// <param name="errores">Lista de mensajes de error.</param>
        /// <param name="excepciones">Lista de excepciones.</param>
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
        /// <summary>
        /// Valida los datos del animal y agrega excepciones a la lista.
        /// </summary>
        /// <param name="excepciones">Lista de excepciones.</param>
        public void ValidarDatosAnimal(List<Exception> excepciones)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                excepciones.Add(new ExcepcionNombreVacio());
            }
            if (!(rbtnPeludoSi.Checked) && !(rbtnPeludoNo.Checked))
            {
                excepciones.Add(new ExcepcionPeludoVacio());
            }
        }
        /// <summary>
        /// Establece el estado de los botones de opción "Es peludo".
        /// </summary>
        public void setRadioButtons()
        {
            if (rbtnPeludoSi.Checked)
            {
                rbtnPeludoNo.Checked = false;
            }
            else if (rbtnPeludoNo.Checked)
            {
                rbtnPeludoSi.Checked = false;
            }
        }

    }
}

```
- FormAgregarRana

![image](https://github.com/fedecorbalan/Corbalan.Federico.SegundoParcial/assets/123754871/79702608-6941-4b3f-9b24-430c603e9398)

  
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
    /// <summary>
    /// Formulario para agregar una Rana.
    /// </summary>
    public partial class FormAgregarRana : FormAgregar
    {
        /// <summary>
        /// Nueva Rana creada.
        /// </summary>
        public Rana nuevaRana;

        /// <summary>
        /// Delegado para manejar la operación completada.
        /// </summary>
        public delegate void OperacionCompletaEventHandler(bool exito, string mensaje);
        /// <summary>
        /// Evento que se dispara cuando la operación se completa.
        /// </summary>
        public event OperacionCompletaEventHandler OperacionCompletada;
        /// <summary>
        /// Acceso a datos para interactuar con la base de datos.
        /// </summary>
        AccesoDatos ado = new AccesoDatos();

        /// <summary>
        /// Constructor de la clase FormAgregarRana.
        /// </summary>
        public FormAgregarRana()
        {
            InitializeComponent();
            setRadioButtonsRana();
            BtnAceptar.Enabled = true;
            BtnCancelar.Enabled = true;
            BtnAceptar.Click += BtnAceptar_Click;
            BtnCancelar.Click += BtnCancelar_Click;
            this.FormPrincipalRef = (FormPrincipal)Application.OpenForms["FormPrincipal"];
        }
        /// <summary>
        /// Maneja el evento de hacer clic en el botón Aceptar.
        /// </summary>
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

                FormEspera frmEspera = new FormEspera();
                frmEspera.Show();

                await AgregarRanaAsync(nuevaRana);

                frmEspera.Close();
                MessageBox.Show("Agregado de datos exitoso");
                
                OperacionCompletada?.Invoke(true, "Agregado de datos exitoso");
                this.DialogResult = DialogResult.OK;
            }
        }
        /// <summary>
        /// Maneja el evento de hacer clic en el botón Cancelar.
        /// </summary>
        private void BtnCancelar_Click(object? sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
        /// <summary>
        /// Valida el valor de la opción seleccionada en el grupo "Es venenosa".
        /// </summary>
        /// <returns>True si es venenosa, false si no lo es.</returns>
        public bool ValidarVenenosa()
        {
            bool esVenenosa;
            if (rbtnVenenosaSi.Checked)
            {
                esVenenosa = true;
            }
            else if (rbtnVenenosaNo.Checked)
            {
                esVenenosa = false;
            }
            else
            {
                throw new ExcepcionEsVenenosaVacio();
            }
            return esVenenosa;

        }
        /// <summary>
        /// Crea un nuevo objeto Rana con los datos proporcionados en el formulario.
        /// </summary>
        /// <returns>Nuevo objeto Rana.</returns>
        public Rana CrearRana()
        {
            bool esPeludo = VerificarEsPeludo();
            bool esVenenosa = ValidarVenenosa();
            bool esArboricola = ValidarArboricola();
            string nombre = TxtNombre.ToString();

            nuevaRana = new Rana(esArboricola, esVenenosa, nombre, esPeludo, Eespecies.Anfibio);

            return nuevaRana;
        }
        /// <summary>
        /// Valida el valor de la opción seleccionada en el grupo "Es arborícola".
        /// </summary>
        /// <returns>True si es arborícola, false si no lo es.</returns>
        public bool ValidarArboricola()
        {
            bool esArboricola;

            if (rbtnArboricolaSi.Checked)
            {
                esArboricola = true;
            }
            else if (rbtnArboricolaNo.Checked)
            {
                esArboricola = false;
            }
            else
            {
                throw new ExcepcionEsArboricolaVacio();
            }
            return esArboricola;
        }
        /// <summary>
        /// Valida los datos específicos de una Rana y agrega excepciones a la lista.
        /// </summary>
        /// <param name="excepciones">Lista de excepciones.</param>
        public void ValidarDatosRana(List<Exception> excepciones)
        {
            if (!(rbtnArboricolaSi.Checked) && !(rbtnArboricolaNo.Checked))
            {
                excepciones.Add(new ExcepcionEsArboricolaVacio());
            }
            if (!(rbtnVenenosaSi.Checked) && !(rbtnVenenosaNo.Checked))
            {
                excepciones.Add(new ExcepcionEsVenenosaVacio());
            }
        }
        /// <summary>
        /// Agrega la nueva Rana a la base de datos y a la lista de Ranas en el formulario principal.
        /// </summary>
        /// <param name="r">Nueva Rana a agregar.</param>
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
            catch (Exception ex)
            {
                OperacionCompletada?.Invoke(false, $"Error al agregar la rana: {ex.Message}");
            }
        }
        /// <summary>
        /// Obtiene un ID adecuado para la nueva Rana.
        /// </summary>
        /// <returns>ID adecuado.</returns>
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
        /// <summary>
        /// Establece el estado de los botones de opción "Es venenosa" y "Es arborícola".
        /// </summary>
        public void setRadioButtonsRana()
        {
            if (rbtnVenenosaSi.Checked)
            {
                rbtnVenenosaNo.Checked = false;
            }
            else if(rbtnVenenosaNo.Checked)
            {
                rbtnVenenosaSi.Checked = false;
            }
            if (rbtnArboricolaSi.Checked)
            {
                rbtnArboricolaNo.Checked = false;
            }
            else if (rbtnArboricolaNo.Checked)
            {
                rbtnArboricolaSi.Checked = false;
            }
        }
    }
}


```
- FormAgregarOrnitorrinco

![image](https://github.com/fedecorbalan/Corbalan.Federico.SegundoParcial/assets/123754871/ea9d7669-cb9e-4842-8c26-7034b0e8ac23)


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
    /// <summary>
    /// Formulario para agregar un Ornitorrinco.
    /// </summary>
    public partial class FormAgregarOrnitorrinco : FormAgregar
    {
        /// <summary>
        /// Nuevo Ornitorrinco creado.
        /// </summary>
        public Ornitorrinco nuevoOrnitorrinco;
        /// <summary>
        /// Delegado para manejar la operación completada.
        /// </summary>
        public delegate void OperacionCompletaEventHandler(bool exito, string mensaje);
        /// <summary>
        /// Evento que se dispara cuando la operación se completa.
        /// </summary>
        public event OperacionCompletaEventHandler OperacionCompletada;
        /// <summary>
        /// Acceso a datos para interactuar con la base de datos.
        /// </summary>;
        AccesoDatos ado = new AccesoDatos();
        /// <summary>
        /// Constructor de la clase FormAgregarOrnitorrinco.
        /// </summary>
        public FormAgregarOrnitorrinco()
        {
            InitializeComponent();
            setRadioButtonsOrnitorrinco();
            BtnAceptar.Enabled = true;
            BtnCancelar.Enabled = true;
            BtnAceptar.Click += BtnAceptar_Click;
            BtnCancelar.Click += BtnCancelar_Click;
            this.FormPrincipalRef = (FormPrincipal)Application.OpenForms["FormPrincipal"];
        }
        /// <summary>
        /// Maneja el evento de hacer clic en el botón Aceptar.
        /// </summary>
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

                FormEspera frmEspera = new FormEspera();
                frmEspera.Show();

                await AgregarOrnitorrincoAsync(nuevoOrnitorrinco);

                frmEspera.Close();
                MessageBox.Show("Agregado de datos exitoso");

                OperacionCompletada?.Invoke(true, "Agregado de datos exitoso");
                this.DialogResult = DialogResult.OK;
            }
        }
        /// <summary>
        /// Maneja el evento de hacer clic en el botón Cancelar.
        /// </summary>
        private void BtnCancelar_Click(object? sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
        /// <summary>
        /// Crea un nuevo objeto Ornitorrinco con los datos proporcionados en el formulario.
        /// </summary>
        /// <returns>Nuevo objeto Ornitorrinco.</returns>
        public Ornitorrinco CrearOrnitorrinco()
        {
            string nombre = TxtNombre.ToString();
            bool esPeludo = VerificarEsPeludo();
            bool esOviparo = VerificarOviparo();
            bool tieneCola = VerificarTieneCola();

            nuevoOrnitorrinco = new Ornitorrinco(tieneCola, esOviparo, esPeludo, Eespecies.Mamifero, nombre);

            return nuevoOrnitorrinco;
        }
        /// <summary>
        /// Valida el valor de la opción seleccionada en el grupo "Es ovíparo".
        /// </summary>
        /// <returns>True si es ovíparo, false si no lo es.</returns>
        public bool VerificarOviparo()
        {
            bool esOviparo;

            if (rbtnOviparoSi.Checked)
            {
                esOviparo = true;
            }
            else if (rbtnOviparoNo.Checked)
            {
                esOviparo = false;
            }
            else
            {
                throw new ExcepcionEsOviparoVacio();
            }
            return esOviparo;
        }
        /// <summary>
        /// Valida el valor de la opción seleccionada en el grupo "Tiene cola".
        /// </summary>
        /// <returns>True si tiene cola, false si no la tiene.</returns>
        public bool VerificarTieneCola()
        {
            bool tieneCola;

            if (rbtnColaSi.Checked)
            {
                tieneCola = true;
            }
            else if (rbtnColaNo.Checked)
            {
                tieneCola = false;
            }
            else
            {
                throw new ExcepcionTieneColaVacio();
            }
            return tieneCola;
        }
        /// <summary>
        /// Valida los datos específicos del Ornitorrinco.
        /// </summary>
        /// <param name="excepciones">Lista de excepciones para agregar posibles errores de validación.</param>
        public void ValidarDatosOrnitorrinco(List<Exception> excepciones)
        {
            if (!(rbtnColaSi.Checked) && !(rbtnColaNo.Checked))
            {
                excepciones.Add(new ExcepcionTieneColaVacio());
            }
            if (!(rbtnOviparoSi.Checked) && !(rbtnOviparoNo.Checked))
            {
                excepciones.Add(new ExcepcionEsOviparoVacio());
            }
        }
        /// <summary>
        /// Realiza la operación de agregar el Ornitorrinco de forma asíncrona.
        /// </summary>
        /// <param name="o">Ornitorrinco a agregar.</param>
        /// <returns>Task.</returns>
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
        /// <summary>
        /// Obtiene el ID correcto para el nuevo Ornitorrinco a partir de la lista existente.
        /// </summary>
        /// <returns>ID correcto.</returns>
        public int ObtenerIdCorrecto()
        {
            var ultimoOrnitorrinco = FormPrincipalRef.listaOrnitorrincosRefugiados.animalesRefugiados.LastOrDefault();

            if (ultimoOrnitorrinco is not null)
            {
                return ultimoOrnitorrinco.Id + 1;
            }
            // Si la lista está vacía, devuelve 1 como el primer ID
            else
            {
                return 1;
            }
        }
        /// <summary>
        /// Establece los radio buttons específicos para Ornitorrinco.
        /// </summary>
        public void setRadioButtonsOrnitorrinco()
        {
            if (rbtnOviparoSi.Checked)
            {
                rbtnOviparoNo.Checked = false;
            }
            else if (rbtnOviparoNo.Checked)
            {
                rbtnOviparoSi.Checked = false;
            }
            if (rbtnColaSi.Checked)
            {
                rbtnOviparoNo.Checked = false;
            }
            else if (rbtnColaNo.Checked)
            {
                rbtnOviparoSi.Checked = false;
            }
        }

    }
}


```
- FormAgregarHornero
  
![image](https://github.com/fedecorbalan/Corbalan.Federico.SegundoParcial/assets/123754871/dc7f5dbc-6279-4f7a-9c1f-61306f758ba0)

  
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
    /// <summary>
    /// Formulario para agregar un Hornero.
    /// </summary>
    public partial class FormAgregarHornero : FormAgregar
    {
        /// <summary>
        /// Nuevo Hornero creado.
        /// </summary>
        public Hornero nuevoHornero;
        /// <summary>
        /// Delegado para manejar la operación completada.
        /// </summary>
        public delegate void OperacionCompletaEventHandler(bool exito, string mensaje);
        /// <summary>
        /// Evento que se dispara cuando la operación se completa.
        /// </summary>
        public event OperacionCompletaEventHandler OperacionCompletada;
        /// <summary>
        /// Acceso a datos para interactuar con la base de datos.
        /// </summary>
        AccesoDatos ado = new AccesoDatos();

        /// <summary>
        /// Constructor de la clase FormAgregarHornero.
        /// </summary>
        public FormAgregarHornero()
        {
            InitializeComponent();
            setRadioButtonHornero();
            BtnAceptar.Enabled = true;
            BtnCancelar.Enabled = true;
            BtnAceptar.Click += BtnAceptar_Click;
            BtnCancelar.Click += BtnCancelar_Click;
            this.FormPrincipalRef = (FormPrincipal)Application.OpenForms["FormPrincipal"];
        }
        /// <summary>
        /// Maneja el evento de hacer clic en el botón Aceptar.
        /// </summary>
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

                FormEspera frmEspera = new FormEspera();
                frmEspera.Show();

                await AgregarHorneroAsync(nuevoHornero);

                frmEspera.Close();
                MessageBox.Show("Agregado de datos exitoso");

                OperacionCompletada?.Invoke(true, "Agregado de datos exitoso");
                this.DialogResult = DialogResult.OK;
            }
        }
        /// <summary>
        /// Maneja el evento de hacer clic en el botón Cancelar.
        /// </summary>
        private void BtnCancelar_Click(object? sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
        /// <summary>
        /// Crea un nuevo objeto Hornero con los datos proporcionados en el formulario.
        /// </summary>
        /// <returns>Nuevo objeto Hornero.</returns>
        public Hornero CrearHornero()
        {
            string nombre = TxtNombre.ToString();
            bool esPeludo = VerificarEsPeludo();
            int velocidad = int.Parse(txtVelocidad.Text);
            bool tieneAlas = ValidarTieneAlas();

            nuevoHornero = new Hornero(velocidad, tieneAlas, nombre, esPeludo, Eespecies.Ave);

            return nuevoHornero;
        }
        /// <summary>
        /// Task que agrega el nuevo Hornero a la base de datos y a la lista de Horneros en el formulario principal.
        /// </summary>
        /// <param name="h">Nuevo Hornero a agregar.</param>
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
        /// <summary>
        /// Valida el valor de la opción seleccionada en el grupo "Tiene alas".
        /// </summary>
        /// <returns>True si tiene alas, false si no las tiene.</returns>
        public bool ValidarTieneAlas()
        {
            bool tieneAlas;
            if (rbtnSi.Checked)
            {
                tieneAlas = true;
            }
            else if (rbtnNo.Checked)
            {
                tieneAlas = false;
            }
            else
            {
                throw new ExcepcionTieneAlasVacio();
            }
            return tieneAlas;
        }
        /// <summary>
        /// Valida los datos específicos de un Hornero y agrega excepciones a la lista.
        /// </summary>
        /// <param name="excepciones">Lista de excepciones.</param>
        public void ValidarDatosHornero(List<Exception> excepciones)
        {
            if (!(rbtnSi.Checked) && !(rbtnNo.Checked))
            {
                excepciones.Add(new ExcepcionTieneAlasVacio());
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
        /// <summary>
        /// Obtiene un ID adecuado para el nuevo Hornero.
        /// </summary>
        /// <returns>ID adecuado.</returns>
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
        /// <summary>
        /// Establece el estado de los botones de opción "Tiene alas".
        /// </summary>
        public void setRadioButtonHornero()
        {
            if (rbtnSi.Checked)
            {
                rbtnNo.Checked = false;
            }
            else if (rbtnNo.Checked)
            {
                rbtnSi.Checked = false;
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
    /// <summary>
    /// Formulario para la modificación de datos de un animal.
    /// </summary>
    public partial class FormModificar : Form
    {
        /// <summary>
        /// Animal que se va a modificar.
        /// </summary>
        public Animal animalAModificar { get; private set; }
        /// <summary>
        /// Referencia al formulario principal.
        /// </summary>
        public FormPrincipal FormPrincipalRef { get; set; }
        /// <summary>
        /// Constructor de la clase FormModificar.
        /// </summary>
        public FormModificar()
        {
            InitializeComponent();
            setRadioButtons();
        }

        /// <summary>
        /// Constructor sobrecargado de la clase FormModificar.
        /// </summary>
        /// <param name="a">Animal que se va a modificar.</param>
        public FormModificar(Animal a) : this()
        {
            txtNombre.Text = a.nombre;
            if (a.esPeludo)
            {
                rbtnPeludoSi.Checked = true;
            }
            else
            {
                rbtnPeludoNo.Checked = true;
            }
        }
        /// <summary>
        /// Título del formulario.
        /// </summary>
        public string LblTitulo { get { return lblTitulo.Text; } set { lblTitulo.Text = value; } }
        /// <summary>
        /// RadioButton para indicar si el animal es peludo.
        /// </summary>
        public RadioButton RbtnPeludoSi { get {return rbtnPeludoSi; } set { rbtnPeludoSi = value; } }
        /// <summary>
        /// RadioButton para indicar si el animal no es peludo.
        /// </summary>
        public RadioButton RbtnPeludoNo { get { return rbtnPeludoNo; } set { rbtnPeludoNo = value; } }
        /// <summary>
        /// Botón de aceptar.
        /// </summary>
        public Button BtnAceptar
        {
            get { return btnAceptar; }
            set { btnAceptar = value; }
        }
        /// <summary>
        /// Botón de cancelar.
        /// </summary>
        public Button BtnCancelar
        {
            get { return btnCancelar; }
            set { btnCancelar = value; }
        }
        /// <summary>
        /// Nombre del animal.
        /// </summary>
        public string TxtNombre { get { return txtNombre.Text; } set { txtNombre.Text = value; } }

        /// <summary>
        /// Maneja el evento aceptar, que no se usa ya que este es un formulario base.
        /// </summary>
        private void btnAceptar_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Verifica si el animal es peludo.
        /// </summary>
        /// <returns>True si el animal es peludo, False si no.</returns>
        public bool VerificarEsPeludo()
        {
            bool esPeludo;

            if (rbtnPeludoSi.Checked)
            {
                esPeludo = true;
            }
            else if(rbtnPeludoNo.Checked)
            {
                esPeludo = false;
            }
            else
            {
                throw new ExcepcionPeludoVacio();
            }
            return esPeludo;
        }
        /// <summary>
        /// Maneja el evento de hacer clic en el botón Cancelar.
        /// </summary>
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// Muestra mensajes de error en caso de excepciones.
        /// </summary>
        /// <param name="errores">Lista de mensajes de error.</param>
        /// <param name="excepciones">Lista de excepciones.</param>
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
        /// <summary>
        /// Valida los datos del animal.
        /// </summary>
        /// <param name="excepciones">Lista de excepciones.</param>
        public void ValidarDatosAnimal(List<Exception> excepciones)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                excepciones.Add(new ExcepcionNombreVacio());
            }
            if (!(rbtnPeludoSi.Checked) && !(rbtnPeludoNo.Checked))
            {
                excepciones.Add(new ExcepcionPeludoVacio());
            }
        }
        /// <summary>
        /// Maneja el evento load, que no es utilizado
        /// </summary>
        private void FormModificar_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Configura los RadioButtons del formulario.
        /// </summary>
        public void setRadioButtons()
        {
            if (rbtnPeludoSi.Checked)
            {
                rbtnPeludoNo.Checked = false;
            }
            else if (rbtnPeludoNo.Checked)
            {
                rbtnPeludoSi.Checked = false;
            }
        }
    }
}


```
- FormModificarRana

![image](https://github.com/fedecorbalan/Corbalan.Federico.SegundoParcial/assets/123754871/5b9a40aa-8c53-445f-a773-d0312b9f547a)

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
    /// <summary>
    /// Formulario para la modificación de datos de una Rana.
    /// </summary>
    public partial class FormModificarRana : FormModificar
    {
        /// <summary>
        /// Rana que se va a modificar.
        /// </summary>
        public Rana ranaAModificar;
        /// <summary>
        /// Delegado para manejar eventos de operación completada.
        /// </summary>
        public delegate void OperacionCompletaEventHandler(bool exito, string mensaje);
        /// <summary>
        /// Evento que se dispara cuando se completa una operación.
        /// </summary>
        public event OperacionCompletaEventHandler OperacionCompletada;
        /// <summary>
        /// Acceso a datos para interactuar con la base de datos.
        /// </summary>
        AccesoDatos ado = new AccesoDatos();
        /// <summary>
        /// Constructor de la clase FormModificarRana.
        /// </summary>
        public FormModificarRana()
        {
            InitializeComponent();
            BtnAceptar.Enabled = true;
            BtnCancelar.Enabled = true;
            BtnAceptar.Click += BtnAceptar_Click;
            BtnCancelar.Click += BtnCancelar_Click;
            this.FormPrincipalRef = (FormPrincipal)Application.OpenForms["FormPrincipal"];
        }
        /// <summary>
        /// Constructor sobrecargado de la clase FormModificarRana.
        /// </summary>
        /// <param name="r">Rana que se va a modificar.</param>
        public FormModificarRana(Rana r) : this()
        {
            LblTitulo = "Modificar Rana";

            TxtNombre = r.nombre;

            if (r.esPeludo)
            {
                RbtnPeludoSi.Checked = true;
            }
            else
            {
                RbtnPeludoNo.Checked = true;
            }

            if (r.esVenenosa)
            {
                rbtnVenenosaSi.Checked = true;
            }
            else
            {
                rbtnVenenosaNo.Checked = true;
            }

            if (r.esArboricola)
            {
                rbtnArboricolaSi.Checked = true;
            }
            else
            {
                rbtnArboricolaNo.Checked = true;
            }

            ranaAModificar = r;
        }
        /// <summary>
        /// Maneja el evento de hacer clic en el botón Aceptar.
        /// </summary>
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

                FormEspera frmEspera = new FormEspera();
                frmEspera.Show();

                await ModificarRanaAsync(ranaAModificar);

                frmEspera.Close();
                OperacionCompletada?.Invoke(true, "Modificación de datos exitoso");
                this.DialogResult = DialogResult.OK;
            }
        }
        /// <summary>
        /// Maneja el evento de hacer clic en el botón Cancelar.
        /// </summary>
        private void BtnCancelar_Click(object? sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
        /// <summary>
        /// Valida si la Rana es Venenosa.
        /// </summary>
        /// <returns>True si es Venenosa, False si no.</returns>
        public bool ValidarVenenosa()
        {
            bool esVenenosa;
            if (rbtnVenenosaSi.Checked)
            {
                esVenenosa = true;
            }
            else if (rbtnVenenosaNo.Checked)
            {
                esVenenosa = false;
            }
            else
            {
                throw new ExcepcionEsArboricolaVacio();
            }
            return esVenenosa;

        }
        /// <summary>
        /// Valida si la Rana es Arboricola.
        /// </summary>
        /// <returns>True si es Arboricola, False si no.</returns>
        public bool ValidarArboricola()
        {
            bool esArboricola;

            if (rbtnArboricolaSi.Checked)
            {
                esArboricola = true;
            }
            else if (rbtnArboricolaNo.Checked)
            {
                esArboricola = false;
            }
            else
            {
                throw new ExcepcionEsArboricolaVacio();
            }
            return esArboricola;
        }
        /// <summary>
        /// Valida los datos específicos de la Rana.
        /// </summary>
        /// <param name="excepciones">Lista de excepciones.</param>
        public void ValidarDatosRana(List<Exception> excepciones)
        {
            if (!(rbtnArboricolaSi.Checked) && !(rbtnArboricolaNo.Checked))
            {
                excepciones.Add(new ExcepcionEsArboricolaVacio());
            }
            if (!(rbtnVenenosaSi.Checked) && !(rbtnVenenosaNo.Checked))
            {
                excepciones.Add(new ExcepcionEsVenenosaVacio());
            }
        }
        /// <summary>
        /// Método asincrónico para modificar una Rana en la base de datos.
        /// </summary>
        /// <param name="r">Rana a modificar.</param>
        /// <returns>Task.</returns>
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

![image](https://github.com/fedecorbalan/Corbalan.Federico.SegundoParcial/assets/123754871/5de1812e-28d8-4c23-9aac-b2258061a8ab)

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
    /// <summary>
    /// Formulario para la modificación de datos de un Ornitorrinco.
    /// </summary>
    public partial class FormModificarOrnitorrinco : FormModificar
    {
        /// <summary>
        /// Ornitorrinco que se va a modificar.
        /// </summary>
        public Ornitorrinco ornitorrincoAModificar;
        /// <summary>
        /// Delegado para manejar eventos de operación completada.
        /// </summary>
        public delegate void OperacionCompletaEventHandler(bool exito, string mensaje);
        /// <summary>
        /// Evento que se dispara cuando se completa una operación.
        /// </summary>
        public event OperacionCompletaEventHandler OperacionCompletada;
        /// <summary>
        /// Acceso a datos para interactuar con la base de datos.
        /// </summary>
        AccesoDatos ado = new AccesoDatos();
        /// <summary>
        /// Constructor de la clase FormModificarOrnitorrinco.
        /// </summary>
        public FormModificarOrnitorrinco()
        {
            InitializeComponent();
            BtnCancelar.Enabled = true;
            BtnAceptar.Click += BtnAceptar_Click;
            BtnCancelar.Click += BtnCancelar_Click;
            this.FormPrincipalRef = (FormPrincipal)Application.OpenForms["FormPrincipal"];
        }
        /// <summary>
        /// Constructor sobrecargado de la clase FormModificarOrnitorrinco.
        /// </summary>
        /// <param name="o">Ornitorrinco que se va a modificar.</param>
        public FormModificarOrnitorrinco(Ornitorrinco o) : this()
        {
            LblTitulo = "Modificar Ornitorrinco";

            TxtNombre = o.nombre;

            if (o.esPeludo)
            {
                RbtnPeludoSi.Checked = true;
            }
            else
            {
                RbtnPeludoNo.Checked = true;
            }
            if (o.oviparo)
            {
                rbtnOviparoSi.Checked = true;
            }
            else
            {
                rbtnOviparoNo.Checked = true;
            }

            if (o.tieneCola)
            {
                rbtnColaSi.Checked = true;
            }
            else
            {
                rbtnColaNo.Checked = true;
            }

            ornitorrincoAModificar = o;
        }
        /// <summary>
        /// Maneja el evento de hacer clic en el botón Aceptar.
        /// </summary>
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

                FormEspera frmEspera = new FormEspera();
                frmEspera.Show();

                await ModificarOrnitorrincoAsync(ornitorrincoAModificar);

                frmEspera.Close();
                OperacionCompletada?.Invoke(true, "Modificacion de datos exitoso");
                this.DialogResult = DialogResult.OK;
            }
        }
        /// <summary>
        /// Maneja el evento de hacer clic en el botón Cancelar.
        /// </summary>
        private void BtnCancelar_Click(object? sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
        /// <summary>
        /// Valida si el Ornitorrinco es Oviparo.
        /// </summary>
        /// <returns>True si es Oviparo, False si no.</returns>
        public bool VerificarOviparo()
        {
            bool esOviparo;

            if (rbtnOviparoSi.Checked)
            {
                esOviparo = true;
            }
            else if (rbtnOviparoNo.Checked)
            {
                esOviparo = false;
            }
            else
            {
                throw new ExcepcionEsOviparoVacio();
            }
            return esOviparo;
        }
        /// <summary>
        /// Valida si el Ornitorrinco tiene cola.
        /// </summary>
        /// <returns>True si tiene cola, False si no.</returns>
        public bool VerificarTieneCola()
        {
            bool tieneCola;

            if (rbtnColaSi.Checked)
            {
                tieneCola = true;
            }
            else if (rbtnColaNo.Checked)
            {
                tieneCola = false;
            }
            else
            {
                throw new ExcepcionTieneColaVacio();
            }
            return tieneCola;
        }
        /// <summary>
        /// Valida los datos específicos del Ornitorrinco.
        /// </summary>
        /// <param name="excepciones">Lista de excepciones.</param>
        public void ValidarDatosOrnitorrinco(List<Exception> excepciones)
        {
            if (!(rbtnColaSi.Checked) && !(rbtnColaNo.Checked))
            {
                excepciones.Add(new ExcepcionTieneColaVacio());
            }
            if (!(rbtnOviparoSi.Checked) && !(rbtnOviparoNo.Checked))
            {
                excepciones.Add(new ExcepcionEsOviparoVacio());
            }
        }
        /// <summary>
        /// Método asincrónico para modificar un Ornitorrinco en la base de datos.
        /// </summary>
        /// <param name="o">Ornitorrinco a modificar.</param>
        /// <returns>Task.</returns>
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

![image](https://github.com/fedecorbalan/Corbalan.Federico.SegundoParcial/assets/123754871/222fa350-480f-44f6-b3a9-7955474c25aa)



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
    /// <summary>
    /// Formulario para la modificación de datos de un Hornero.
    /// </summary>
    public partial class FormModificarHornero : FormModificar
    {
        /// <summary>
        /// Hornero que se va a modificar.
        /// </summary>
        public Hornero horneroAModificar;
        /// <summary>
        /// Delegado para manejar eventos de operación completada.
        /// </summary>
        public delegate void OperacionCompletaEventHandler(bool exito, string mensaje);
        /// <summary>
        /// Evento que se dispara cuando se completa una operación.
        /// </summary>
        public event OperacionCompletaEventHandler OperacionCompletada;
        /// <summary>
        /// Acceso a datos para interactuar con la base de datos.
        /// </summary>
        AccesoDatos ado = new AccesoDatos();
        /// <summary>
        /// Constructor de la clase FormModificarHornero.
        /// </summary>
        public FormModificarHornero()
        {
            InitializeComponent();
            BtnAceptar.Enabled = true;
            BtnCancelar.Enabled = true;
            BtnAceptar.Click += BtnAceptar_Click;
            BtnCancelar.Click += BtnCancelar_Click;
            this.FormPrincipalRef = (FormPrincipal)Application.OpenForms["FormPrincipal"];
        }
        /// <summary>
        /// Constructor sobrecargado de la clase FormModificarHornero.
        /// </summary>
        /// <param name="h">Hornero que se va a modificar.</param>
        public FormModificarHornero(Hornero h) : this()
        {
            LblTitulo = "Modificar Hornero";

            TxtNombre = h.nombre;

            if (h.esPeludo == true)
            {
                RbtnPeludoSi.Checked = true;
            }
            else
            {
                RbtnPeludoNo.Checked = true;
            }

            if (h.tieneAlas)
            {
                rbtnSi.Checked = true;
            }
            else
            {
                rbtnNo.Checked = true;
            }

            txtVelocidad.Text = h.velocidadKmH.ToString();

            horneroAModificar = h;
        }
        /// <summary>
        /// Maneja el evento de hacer clic en el botón Aceptar.
        /// </summary>
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

                FormEspera frmEspera = new FormEspera();
                frmEspera.Show();

                await ModificarHorneroAsync(horneroAModificar);

                frmEspera.Close();
                OperacionCompletada?.Invoke(true, "Modificación de datos exitoso");
                this.DialogResult = DialogResult.OK;
            }
        }
        /// <summary>
        /// Maneja el evento de hacer clic en el botón Cancelar.
        /// </summary>
        private void BtnCancelar_Click(object? sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
        /// <summary>
        /// Método asincrónico para modificar un Hornero en la base de datos.
        /// </summary>
        /// <param name="h">Hornero a modificar.</param>
        /// <returns>Task.</returns>
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
        /// <summary>
        /// Valida si el Hornero tiene alas.
        /// </summary>
        /// <returns>True si tiene alas, False si no.</returns>
        public bool ValidarTieneAlas()
        {
            bool tieneAlas;
            if (rbtnSi.Checked)
            {
                tieneAlas = true;
            }
            else if (rbtnNo.Checked)
            {
                tieneAlas = false;
            }
            else
            {
                throw new ExcepcionTieneAlasVacio();
            }
            return tieneAlas;
        }
        /// <summary>
        /// Valida los datos específicos del Hornero.
        /// </summary>
        /// <param name="excepciones">Lista de excepciones.</param>
        public void ValidarDatosHornero(List<Exception> excepciones)
        {
            if (!(rbtnSi.Checked) && !(rbtnNo.Checked))
            {
                excepciones.Add(new ExcepcionTieneAlasVacio());
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


6. `FormVisualizador`: Este formulario permite observar los usuarios que han ingresado al programa junto con sus datos y la fecha y la hora en la que han ingresado. Cuenta con una instancia en el formulario principal.

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

Puede ejecutar la aplicación y utilizar la ventana principal (`FormPrincipal`) para gestionar los animales en el refugio, obviamente luego del login, en donde se determina el perfil del usuario (Administrador, Supervisor y Vendedor), que determina las funciones que uno puede realizar. En todos los casos, se debe elegir el archivo al que quiere deserializar para poder ver los animales en el visor, al descargar este repositorio, usted puede encontrar en la carpeta ArchivosASeleccionar los archivos JSON que corresponden a los diferentes animales, pero en este caso, trae los datos existentes en la base de datos correspondiente al archivo seleccionado, sea ranas, ornitorrincos u horneros. En el caso de que seas Administrador o Supervisor, puede agregar nuevos animales y modificar sus atributos (no puede cambiar de especie) y en el caso de que sea Administrador, usted puede eliminar animales. También, la aplicación permite ordenar la lista de animales por cantidad de extremidades y especie. Además cuenta con un visualizador de usuarios, el cual le brinda toda la informacion acerca de los usuarios que han ingresado con su cuenta al programa y la fecha y hora en la que lo hicieron.

## Uso Administrador
[Link al Video del Uso del Administrador](https://youtu.be/_c-UK64gK3Y)

## Uso Supervisor
[Link al Video del Uso del Supervisor](https://youtu.be/eJId8DHYQKM)

## Uso Vendedor
https://github.com/fedecorbalan/Corbalan.Federico.SegundoParcial/assets/123754871/515a31a6-5c97-439b-8a4f-fc3bb003478b

# Diagrama de clases
![image](https://github.com/fedecorbalan/Corbalan.Federico.SegundoParcial/assets/123754871/a4d330f2-fe5b-48a2-9715-ef43ab5a53b9)

# Fin del Repositorio
Muchas gracias por leer ;D!
