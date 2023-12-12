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
