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
