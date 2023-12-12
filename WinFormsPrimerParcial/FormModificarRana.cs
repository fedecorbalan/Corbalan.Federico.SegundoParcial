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
