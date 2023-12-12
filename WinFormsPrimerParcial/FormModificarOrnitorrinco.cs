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