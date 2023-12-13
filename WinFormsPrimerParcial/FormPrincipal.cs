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
        private bool ordenAscendente = true;

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
            if (ordenAscendente)
            {
                listaOrnitorrincosRefugiados.animalesRefugiados.Sort((a1, a2) => listaOrnitorrincosRefugiados.OrdenarAnimalesPorCantidadDeExtremidades(a1, a2));
                listaRanasRefugiadas.animalesRefugiados.Sort((a1, a2) => listaRanasRefugiadas.OrdenarAnimalesPorCantidadDeExtremidades(a1, a2));
                listaHornerosRefugiados.animalesRefugiados.Sort((a1, a2) => listaHornerosRefugiados.OrdenarAnimalesPorCantidadDeExtremidades(a1, a2));
                ActualizarVisor();
            }
            else
            {
                listaOrnitorrincosRefugiados.animalesRefugiados.Sort((a2, a1) => listaOrnitorrincosRefugiados.OrdenarAnimalesPorCantidadDeExtremidadesDesc(a2, a1));
                listaRanasRefugiadas.animalesRefugiados.Sort((a2, a1) => listaRanasRefugiadas.OrdenarAnimalesPorCantidadDeExtremidadesDesc(a2, a1));
                listaHornerosRefugiados.animalesRefugiados.Sort((a2, a1) => listaHornerosRefugiados.OrdenarAnimalesPorCantidadDeExtremidadesDesc(a2, a1));
                ActualizarVisor();
            }
            
            ordenAscendente = !ordenAscendente;
        }
        /// <summary>
        /// Ordena la lista de animales por Nombre y actualiza el visor.
        /// </summary>
        private void btnOrdenar2_Click(object sender, EventArgs e)
        {
            if (ordenAscendente)
            {
                listaHornerosRefugiados.OrdenarAnimalesPorNombre();
                listaOrnitorrincosRefugiados.OrdenarAnimalesPorNombre();
                listaRanasRefugiadas.OrdenarAnimalesPorNombre();
                ActualizarVisor();
            }
            else
            {
                listaHornerosRefugiados.OrdenarAnimalesPorNombreDescendente();
                listaOrnitorrincosRefugiados.OrdenarAnimalesPorNombreDescendente();
                listaRanasRefugiadas.OrdenarAnimalesPorNombreDescendente();
                ActualizarVisor();
            }
            ordenAscendente = !ordenAscendente;
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