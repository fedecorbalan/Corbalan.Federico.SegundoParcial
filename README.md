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

La clase `Animal` es una clase abstracta que representa a todos los animales en el refugio. Tiene propiedades como nombre, especie, si es peludo, y métodos para mostrar información, emitir un sonido y contar la cantidad de extremidades.

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

        public abstract string EmitirSonido();

        public Animal()
        {
            this.esPeludo = false;
            this.nombre = "SIN NOMBRE";
            this.especie = Eespecies.Anfibio;
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

            sb.Append($" Nombre:{this.nombre} - Es Peludo:{this.esPeludo} - Especie:{this.especie.ToString()} ");

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

        public Rana() 
        { 
        }
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
            sb.AppendLine($"- Es venenosa: {this.esVenenosa} - Es arboricola {this.esArboricola}");

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
            sb.AppendLine($"- Es oviparo: {this.oviparo} - Tiene cola: {this.tieneCola}");

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
        public int velocidadVueloKMporH;

        public Hornero() 
        {
            this.tieneAlas = true;
            this.velocidadVueloKMporH = 30;
        }
        public Hornero(bool tieneAlas, string nombre, bool esPeludo, Eespecies especie) : base(nombre, esPeludo, especie)
        {
            this.tieneAlas = tieneAlas;
        }
        public Hornero(int velocidadVueloKMporH,bool tieneAlas, bool esPeludo, Eespecies especie, string nombre) : this(tieneAlas, nombre, esPeludo, especie)
        {
            this.tieneAlas = tieneAlas;
            this.velocidadVueloKMporH = velocidadVueloKMporH;
        }

        public override string Mostrar()
        {
            StringBuilder sb = new StringBuilder();

            string rta = base.Mostrar();

            sb.Append(rta);
            sb.Append($"- Tiene alas: {this.tieneAlas} - Velocidad de vuelo en KM/H{this.velocidadVueloKMporH}");

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
    }
}
```

## Formularios

El proyecto contiene cinco formularios en total, incluyendo el Formulario de Login que se ha mencionado previamente:

1. `FormPrincipal`: Este formulario es la ventana principal de la aplicación. Permite agregar, modificar y eliminar animales en el refugio. Ademas, se pueden seleccionar los archivos que se quieren tanto serializar como deserializar y mostrarse en el visor. También se cuenta con botones para poder ordenar los animales que han sido deserializados en el visor en base a su cantidad de extremidades y su especie (Número de enumerado) y un visualizador para poder observar quien ha iniciado sesión y en que fecha y horario.

![image](https://github.com/fedecorbalan/Corbalan.Federico.PrimerParcial/assets/123754871/c23c200b-88b5-4540-95e3-72ce5299e505)


```c#

using Entidades;
using Newtonsoft.Json;
using PrimerParcial;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.IO;

namespace WinFormsPrimerParcial
{
    public partial class FormPrincipal : Form
    {
        public Refugio<Rana> listaRanasRefugiadas;
        public Refugio<Hornero> listaHornerosRefugiados;
        public Refugio<Ornitorrinco> listaOrnitorrincosRefugiados;

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
        }
        private void MostrarFechaActual()
        {
            DateTime fechaActual = DateTime.Now;
            lblDateTime.Text = fechaActual.ToString("dd/MM/yyyy");
        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            FormAgregar frmAgregar = new FormAgregar();
            frmAgregar.FormPrincipalRef = this;
            frmAgregar.StartPosition = FormStartPosition.CenterScreen;

            if (frmAgregar.ShowDialog() == DialogResult.OK)
            {
                Animal nuevoAnimal = frmAgregar.NuevoAnimal;

                if (lstVisor.Items.Count >= 0)
                {
                    if (nuevoAnimal is Ornitorrinco)
                    {
                        lstVisor.Items.Add(nuevoAnimal.ToString());
                    }
                    if (nuevoAnimal is Rana)
                    {
                        lstVisor.Items.Add(nuevoAnimal.ToString());
                    }
                    if (nuevoAnimal is Hornero)
                    {
                        lstVisor.Items.Add(nuevoAnimal.ToString());
                    }
                }
            }
        }
        private void btnModificar_Click(object sender, EventArgs e)
        {
            int selectedIndex = lstVisor.SelectedIndex;
            if (selectedIndex >= 0)
            {
                Animal animalAModificar = null;
                Eespecies especie = Eespecies.Mamifero;

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

                FormModificar frmModificar = new FormModificar(animalAModificar);
                frmModificar.StartPosition = FormStartPosition.CenterScreen;
                frmModificar.ShowDialog();

                // Dependiendo de cómo manejes las modificaciones en FormModificar, actualiza las listas correspondientes
                // Puede requerir reemplazar o actualizar el elemento en la lista
                if (especie == Eespecies.Anfibio)
                {
                    listaRanasRefugiadas.animalesRefugiados[selectedIndex] = (Rana)animalAModificar;
                }
                else if (especie == Eespecies.Ave)
                {
                    listaHornerosRefugiados.animalesRefugiados[selectedIndex - listaRanasRefugiadas.animalesRefugiados.Count] = (Hornero)animalAModificar;
                }
                else if (especie == Eespecies.Mamifero)
                {
                    listaOrnitorrincosRefugiados.animalesRefugiados[selectedIndex - listaRanasRefugiadas.animalesRefugiados.Count - listaHornerosRefugiados.animalesRefugiados.Count] = (Ornitorrinco)animalAModificar;
                }

                lstVisor.Items[selectedIndex] = animalAModificar.ToString();
            }
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {

            int selectedIndex = lstVisor.SelectedIndex;

            if (selectedIndex >= 0)
            {
                if (selectedIndex < listaRanasRefugiadas.animalesRefugiados.Count)
                {
                    listaRanasRefugiadas.animalesRefugiados.RemoveAt(selectedIndex);
                }
                else if (selectedIndex < listaRanasRefugiadas.animalesRefugiados.Count + listaHornerosRefugiados.animalesRefugiados.Count)
                {
                    int index = selectedIndex - listaRanasRefugiadas.animalesRefugiados.Count;
                    listaHornerosRefugiados.animalesRefugiados.RemoveAt(index);
                }
                else
                {
                    int index = selectedIndex - listaRanasRefugiadas.animalesRefugiados.Count - listaHornerosRefugiados.animalesRefugiados.Count;
                    listaOrnitorrincosRefugiados.animalesRefugiados.RemoveAt(index);
                }

                lstVisor.Items.RemoveAt(selectedIndex);
            }
            else
            {
                MessageBox.Show("Selecciona un elemento para eliminar.");
            }
        }
        private void FormPrincipal_Load(object sender, EventArgs e) { }
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
                    DeserializarDesdeArchivoRanas(rutaArchivoEntrada, listaRanasRefugiadas);
                }
                else if (archivoSeleccionado == "ornitorrincos.json")
                {
                    DeserializarDesdeArchivoOrnitorrincos(rutaArchivoEntrada, listaOrnitorrincosRefugiados);
                }
                else if (archivoSeleccionado == "horneros.json")
                {
                    DeserializarDesdeArchivoHorneros(rutaArchivoEntrada, listaHornerosRefugiados);
                }
            }
        }

        private void btnArchivoSalida_Click(object sender, EventArgs e)
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

        public void DeserializarDesdeArchivoOrnitorrincos(string rutaArchivo, Refugio<Ornitorrinco> lista)
        {
            if (File.Exists(rutaArchivo))
            {
                using (StreamReader sr = new StreamReader(rutaArchivo))
                {
                    string json_str = sr.ReadToEnd();
                    Refugio<Ornitorrinco> refugioDeserializado = JsonConvert.DeserializeObject<Refugio<Ornitorrinco>>(json_str);

                    lista.animalesRefugiados = refugioDeserializado.animalesRefugiados;

                    lstVisor.Items.Clear();
                    foreach (Ornitorrinco elemento in lista.animalesRefugiados)
                    {
                        lstVisor.Items.Add(elemento.ToString());
                    }
                }
            }
        }
        public void DeserializarDesdeArchivoRanas(string rutaArchivo, Refugio<Rana> lista)
        {
            if (File.Exists(rutaArchivo))
            {
                using (StreamReader sr = new StreamReader(rutaArchivo))
                {
                    string json_str = sr.ReadToEnd();
                    Refugio<Rana> refugioDeserializado = JsonConvert.DeserializeObject<Refugio<Rana>>(json_str);

                    lista.animalesRefugiados = refugioDeserializado.animalesRefugiados;

                    lstVisor.Items.Clear();
                    foreach (Rana elemento in lista.animalesRefugiados)
                    {
                        lstVisor.Items.Add(elemento.ToString());
                    }
                }
            }
        }
        public void DeserializarDesdeArchivoHorneros(string rutaArchivo, Refugio<Hornero> lista)
        {
            if (File.Exists(rutaArchivo))
            {
                using (StreamReader sr = new StreamReader(rutaArchivo))
                {
                    string json_str = sr.ReadToEnd();
                    Refugio<Hornero> refugioDeserializado = JsonConvert.DeserializeObject<Refugio<Hornero>>(json_str);

                    lista.animalesRefugiados = refugioDeserializado.animalesRefugiados;

                    lstVisor.Items.Clear();
                    foreach (Hornero elemento in lista.animalesRefugiados)
                    {
                        lstVisor.Items.Add(elemento.ToString());
                    }
                }
            }
        }
    }
}
```


2. `FormAgregar`: Este formulario permite agregar un nuevo animal al refugio. El usuario puede modificar el nombre, si es peludo, y atributos específicos del animal que ha sido seleccionado en el visor.

![image](https://github.com/fedecorbalan/Corbalan.Federico.PrimerParcial/assets/123754871/fe109fb8-e409-4726-8542-bd9d9c176581)

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
        public Ornitorrinco nuevoOrnitorrinco;
        public Rana nuevaRana;
        public Hornero nuevoHornero;

        public Ornitorrinco NuevoOrnitorrinco { get; private set; }
        public Hornero NuevoHornero { get; private set; }
        public Rana NuevaRana { get; private set; }

        public Animal NuevoAnimal { get; private set; }
        public FormPrincipal FormPrincipalRef { get; set; }


        public FormAgregar()
        {
            InitializeComponent();
        }
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text;
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
            string especie = txtAnimal.Text.ToLower();

            if (especie == "ornitorrinco")
            {
                nuevoOrnitorrinco = new Ornitorrinco(esPeludo, true, true, Eespecies.Mamifero, nombre);
                _ = FormPrincipalRef.listaOrnitorrincosRefugiados + nuevoOrnitorrinco;
                NuevoAnimal = nuevoOrnitorrinco;
            }
            else if (especie == "rana")
            {
                nuevaRana = new Rana(true, true, esPeludo, Eespecies.Anfibio, nombre);
                _ = FormPrincipalRef.listaRanasRefugiadas + nuevaRana;
                NuevoAnimal = nuevaRana;
            }
            else if (especie == "hornero")
            { 
                nuevoHornero = new Hornero(40, true, esPeludo, Eespecies.Ave, nombre);
                _ = FormPrincipalRef.listaHornerosRefugiados + nuevoHornero;
                NuevoAnimal = nuevoHornero;
            }
            else
            {
                MessageBox.Show("Especie no válida. Solo se permite Ornitorrinco, Rana o Hornero.");
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

```

3. `FormModificar`: Este formulario permite modificar los atributos disponibles, que dependen del tipo de animal seleccionado.


```c#
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
    public partial class FormModificar : Form
    {
        private Animal animalAModificar;
        public FormPrincipal FormPrincipalRef { get; set; }

        public Animal AnimalAModificar { get; set; }

        public Label LblClase { get; set; }
        public Label LblAtributo1 { get; set; }
        public Label LblAtributo2 { get; set; }

        public Eespecies Especie { get; set; }

        public FormModificar(Animal animal)
        {
            InitializeComponent();
            animalAModificar = animal;
            if (animalAModificar.especie == Eespecies.Mamifero)
            {
                lblClase.Text = "Ornitorrinco";
                lblAtributo1.Text = "Es Oviparo?";
                lblAtributo2.Text = "Tiene Cola?";

            }
            else if (animalAModificar.especie == Eespecies.Ave)
            {
                lblClase.Text = "Hornero";
                lblAtributo1.Text = "Tiene alas?";
                lblAtributo2.Text = "Velocidad en KM/H";

            }
            else if (animalAModificar.especie == Eespecies.Anfibio)
            {
                lblClase.Text = "Rana";
                lblAtributo1.Text = "Es Venenosa?";
                lblAtributo2.Text = "Es Arboricola?";
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            string textoNombre = txtNombre.Text;
            string textoPeludo = txtPeludo.Text;

            animalAModificar.nombre = textoNombre;

            if (textoPeludo == "si")
            {
                animalAModificar.esPeludo = true;
            }
            else
            {
                animalAModificar.esPeludo = false;
            }

            if (animalAModificar.especie == Eespecies.Mamifero)
            {
                if (txtAtributo1.Text.ToLower() == "si" && txtAtributo2.Text.ToLower() == "si")
                {
                    ((Ornitorrinco)animalAModificar).oviparo = true;
                    ((Ornitorrinco)animalAModificar).tieneCola = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Datos erroneos, vuelva a ingresarlos");
                }
            }
            else if (animalAModificar.especie == Eespecies.Ave)
            {
                int velocidad = int.Parse(txtAtributo2.Text);
                if (txtAtributo1.Text == "si" && (velocidad > 0 || velocidad < 100))
                {
                    ((Hornero)animalAModificar).tieneAlas = (txtAtributo1.Text == "si");
                    ((Hornero)animalAModificar).velocidadVueloKMporH = velocidad;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Datos erroneos, vuelva a ingresarlos");
                }

            }
            else if (animalAModificar.especie == Eespecies.Anfibio)
            {
                if (txtAtributo1.Text.ToLower() == "si" && txtAtributo2.Text.ToLower() == "si")
                {
                    ((Rana)animalAModificar).esVenenosa = true;
                    ((Rana)animalAModificar).esArboricola = true;
                    this.Close();
                }
                else if (txtAtributo1.Text.ToLower() == "no" && txtAtributo2.Text.ToLower() == "si")
                {
                    ((Rana)animalAModificar).esVenenosa = false;
                    ((Rana)animalAModificar).esArboricola = true;
                    this.Close();
                }
                else if (txtAtributo1.Text.ToLower() == "si" && txtAtributo2.Text.ToLower() == "no")
                {
                    ((Rana)animalAModificar).esVenenosa = true;
                    ((Rana)animalAModificar).esArboricola = false;
                    this.Close();
                }
                else
                {
                    ((Rana)animalAModificar).esVenenosa = false;
                    ((Rana)animalAModificar).esArboricola = false;
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Especie no válida. Solo se permite Ornitorrinco, Rana o Hornero.");
                return;
            }

            
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
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

El proyecto utiliza tanto métodos de Serialización como de Deserialización para cada lista creada en base a las clases hijas de Animal. Estos se encuentran diseñados de forma tal que adapten a la utilización de openFileDialog y saveFileDialog, para asi, poder realizar los procesos anteriormente mencionados con los archivos que el usuario final desee. Estos bloques de código se encuentran dentro del Formulario Principal.

```c#
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

    public void DeserializarDesdeArchivoOrnitorrincos(string rutaArchivo, Refugio<Ornitorrinco> lista)
    {
        if (File.Exists(rutaArchivo))
        {
            using (StreamReader sr = new StreamReader(rutaArchivo))
            {
                string json_str = sr.ReadToEnd();
                Refugio<Ornitorrinco> refugioDeserializado = JsonConvert.DeserializeObject<Refugio<Ornitorrinco>>(json_str);

                lista.animalesRefugiados = refugioDeserializado.animalesRefugiados;

                lstVisor.Items.Clear();
                foreach (Ornitorrinco elemento in lista.animalesRefugiados)
                {
                    lstVisor.Items.Add(elemento.ToString());
                }
            }
        }
    }
    public void DeserializarDesdeArchivoRanas(string rutaArchivo, Refugio<Rana> lista)
    {
        if (File.Exists(rutaArchivo))
        {
            using (StreamReader sr = new StreamReader(rutaArchivo))
            {
                string json_str = sr.ReadToEnd();
                Refugio<Rana> refugioDeserializado = JsonConvert.DeserializeObject<Refugio<Rana>>(json_str);

                lista.animalesRefugiados = refugioDeserializado.animalesRefugiados;

                lstVisor.Items.Clear();
                foreach (Rana elemento in lista.animalesRefugiados)
                {
                    lstVisor.Items.Add(elemento.ToString());
                }
            }
        }
    }
    public void DeserializarDesdeArchivoHorneros(string rutaArchivo, Refugio<Hornero> lista)
    {
        if (File.Exists(rutaArchivo))
        {
            using (StreamReader sr = new StreamReader(rutaArchivo))
            {
                string json_str = sr.ReadToEnd();
                Refugio<Hornero> refugioDeserializado = JsonConvert.DeserializeObject<Refugio<Hornero>>(json_str);

                lista.animalesRefugiados = refugioDeserializado.animalesRefugiados;

                lstVisor.Items.Clear();
                foreach (Hornero elemento in lista.animalesRefugiados)
                {
                    lstVisor.Items.Add(elemento.ToString());
                }
            }
        }
    }
}

```

## Uso del Programa

Puede ejecutar la aplicación y utilizar la ventana principal (`FormPrincipal`) para gestionar los animales en el refugio, obviamente luego del login. Se debe elegir el archivo al que quiere deserializar para poder ver los animales en el visor, al descargar este repositorio, usted puede encontrar en la carpeta WinFormsPrimerParcial los archivos JSON que corresponden a los diferentes animales. Puede agregar nuevos animales, modificar sus atributos (no puede cambiar de especie) y eliminar animales. También, la aplicación permite ordenar la lista de animales por cantidad de extremidades y especie. Además cuenta con un visualizador de usuarios, el cual le brinda toda la informacion acerca de los usuarios que han ingresado con su cuenta al programa y la fecha y hora en la que lo hicieron.

# Diagrama de clases
![image](https://github.com/fedecorbalan/Corbalan.Federico.PrimerParcial/assets/123754871/c7c14ede-cd81-44f3-90e2-011a5545c549)



# Fin del Repositorio
Muchas gracias por leer ;D!
