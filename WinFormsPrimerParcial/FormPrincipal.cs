
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

            var formLogin = new FormLogin();
            formLogin.ActualizarPermisos += ActualizarPermisosFormPrincipal;
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
            if (perfilUsuario.ToLower() == "administrador" || perfilUsuario.ToLower() == "supervisor") {
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
                        FormAgregarRana frmModificarRana = new FormAgregarRana(ranaAModificar);
                        frmModificarRana.OperacionCompletada += ManejarOperacionCompleta;
                        frmModificarRana.IndiceSeleccionado = selectedIndex;

                        frmModificarRana.StartPosition = FormStartPosition.CenterScreen;
                        frmModificarRana.ShowDialog();
                    }
                    if (especie == Eespecies.Ave && animalAModificar is Hornero horneroAModificar)
                    {
                        FormAgregarHornero frmModificarHornero = new FormAgregarHornero(horneroAModificar);
                        frmModificarHornero.OperacionCompletada += ManejarOperacionCompleta;

                        frmModificarHornero.StartPosition = FormStartPosition.CenterScreen;
                        frmModificarHornero.ShowDialog();
                    }
                    if (especie == Eespecies.Mamifero && animalAModificar is Ornitorrinco ornitorrincoAModificar)
                    {
                        FormAgregarOrnitorrinco frmModificarOrnitorrinco = new FormAgregarOrnitorrinco(ornitorrincoAModificar);
                        frmModificarOrnitorrinco.OperacionCompletada += ManejarOperacionCompleta;

                        frmModificarOrnitorrinco.StartPosition = FormStartPosition.CenterScreen;
                        frmModificarOrnitorrinco.ShowDialog();
                    }

                    // Agrega lógica similar para otros tipos de animales si es necesario
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
                            //listaRanasRefugiadas.animalesRefugiados.RemoveAt(selectedIndex);
                            Rana ranaAEliminar = listaRanasRefugiadas.animalesRefugiados[selectedIndex];

                            // Elimina la rana de la base de datos
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

        private void ActualizarPermisosFormPrincipal(bool permiteAgregar, bool permiteVer, bool permiteModificar, bool permiteEliminar)
        {
            // Implementa la lógica para ajustar la interfaz de usuario según los permisos
            btnAgregar.Enabled = permiteAgregar;
            btnModificar.Enabled = permiteModificar;
            btnEliminar.Enabled = permiteEliminar;
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