
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
            FormSeleccionAnimal frmSeleccionAnimal = new FormSeleccionAnimal();

            if (frmSeleccionAnimal.ShowDialog() == DialogResult.OK)
            {
                Type selectedAnimalType = frmSeleccionAnimal.GetSelectedAnimalType();

                if (selectedAnimalType != null)
                {
                    // Abre el formulario correspondiente seg�n el tipo de animal seleccionado
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

                // Dependiendo de c�mo manejes las modificaciones en FormModificar, actualiza las listas correspondientes
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
        private void FormPrincipal_Load(object sender, EventArgs e) 
        {
            ado.ObtenerListaHorneros(listaHornerosRefugiados);
            ActualizarVisor();
        }
 

        private void btnSalir_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("�Estas seguro que quieres salir?", "SALIR", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

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

        private void ManejarOperacionCompleta(bool exito, string mensaje)
        {
            if (exito)
            {
                MessageBox.Show($"{mensaje}", "�xito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show($"{mensaje}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActualizarPermisosFormPrincipal(bool permiteAgregar, bool permiteVer, bool permiteModificar, bool permiteEliminar)
        {
            // Implementa la l�gica para ajustar la interfaz de usuario seg�n los permisos
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
                    MessageBox.Show("El archivo de registro no existe o no se ha generado a�n.");
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
                //string rutaArchivoEntrada = openFileDialogDeserializar.FileName;

                //string archivoSeleccionado = Path.GetFileName(rutaArchivoEntrada);
                //if (archivoSeleccionado == "ranas.json")
                //{
                //    DeserializarDesdeArchivoRanas(rutaArchivoEntrada, listaRanasRefugiadas);
                //}
                //else if (archivoSeleccionado == "ornitorrincos.json")
                //{
                //    DeserializarDesdeArchivoOrnitorrincos(rutaArchivoEntrada, listaOrnitorrincosRefugiados);
                //}
                //else if (archivoSeleccionado == "horneros.json")
                //{
                //    DeserializarDesdeArchivoHorneros(rutaArchivoEntrada, listaHornerosRefugiados);
                //}
            }
        }

        private void btnArchivoSalida_Click(object sender, EventArgs e)
        {

            if (saveFileDialogSerializar.ShowDialog() == DialogResult.OK)
            {
                //string rutaArchivoSalida = saveFileDialogSerializar.FileName;

                //string archivoSeleccionado = Path.GetFileName(rutaArchivoSalida);
                //if (archivoSeleccionado == "ranas.json")
                //{
                //    SerializarAArchivoRana(rutaArchivoSalida, listaRanasRefugiadas);
                //}
                //else if (archivoSeleccionado == "ornitorrincos.json")
                //{
                //    SerializarAArchivoOrnitorrinco(rutaArchivoSalida, listaOrnitorrincosRefugiados);
                //}
                //else if (archivoSeleccionado == "horneros.json")
                //{
                //    SerializarAArchivoHornero(rutaArchivoSalida, listaHornerosRefugiados);
                //}
            }
        }
        #region metodos serializadores
        //public void SerializarAArchivoRana(string rutaArchivo, Refugio<Rana> lista)
        //{
        //    string json = JsonConvert.SerializeObject(lista, Formatting.Indented);
        //    File.WriteAllText(rutaArchivo, json);
        //}
        //public void SerializarAArchivoOrnitorrinco(string rutaArchivo, Refugio<Ornitorrinco> lista)
        //{
        //    string json = JsonConvert.SerializeObject(lista, Formatting.Indented);
        //    File.WriteAllText(rutaArchivo, json);
        //}
        //public void SerializarAArchivoHornero(string rutaArchivo, Refugio<Hornero> lista)
        //{
        //    string json = JsonConvert.SerializeObject(lista, Formatting.Indented);
        //    File.WriteAllText(rutaArchivo, json);
        //}

        //public void DeserializarDesdeArchivoOrnitorrincos(string rutaArchivo, Refugio<Ornitorrinco> lista)
        //{
        //    if (File.Exists(rutaArchivo))
        //    {
        //        using (StreamReader sr = new StreamReader(rutaArchivo))
        //        {
        //            string json_str = sr.ReadToEnd();
        //            Refugio<Ornitorrinco> refugioDeserializado = JsonConvert.DeserializeObject<Refugio<Ornitorrinco>>(json_str);

        //            lista.animalesRefugiados = refugioDeserializado.animalesRefugiados;

        //            lstVisor.Items.Clear();
        //            foreach (Ornitorrinco elemento in lista.animalesRefugiados)
        //            {
        //                lstVisor.Items.Add(elemento.ToString());
        //            }
        //        }
        //    }
        //}
        //public void DeserializarDesdeArchivoRanas(string rutaArchivo, Refugio<Rana> lista)
        //{
        //    if (File.Exists(rutaArchivo))
        //    {
        //        using (StreamReader sr = new StreamReader(rutaArchivo))
        //        {
        //            string json_str = sr.ReadToEnd();
        //            Refugio<Rana> refugioDeserializado = JsonConvert.DeserializeObject<Refugio<Rana>>(json_str);

        //            lista.animalesRefugiados = refugioDeserializado.animalesRefugiados;

        //            lstVisor.Items.Clear();
        //            foreach (Rana elemento in lista.animalesRefugiados)
        //            {
        //                lstVisor.Items.Add(elemento.ToString());
        //            }
        //        }
        //    }
        //}
        //public void DeserializarDesdeArchivoHorneros(string rutaArchivo, Refugio<Hornero> lista)
        //{
        //    if (File.Exists(rutaArchivo))
        //    {
        //        using (StreamReader sr = new StreamReader(rutaArchivo))
        //        {
        //            string json_str = sr.ReadToEnd();
        //            Refugio<Hornero> refugioDeserializado = JsonConvert.DeserializeObject<Refugio<Hornero>>(json_str);

        //            lista.animalesRefugiados = refugioDeserializado.animalesRefugiados;

        //            lstVisor.Items.Clear();
        //            foreach (Hornero elemento in lista.animalesRefugiados)
        //            {
        //                lstVisor.Items.Add(elemento.ToString());
        //            }
        //        }
        //    }
        //}
        #endregion

        
    }
}