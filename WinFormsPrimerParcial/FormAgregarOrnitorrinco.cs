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
