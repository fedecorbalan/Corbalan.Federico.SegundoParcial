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
