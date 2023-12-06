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
    public partial class FormAgregarHornero : FormAgregar
    {
        public Hornero nuevoHornero;

        public delegate void OperacionCompletaEventHandler(bool exito, string mensaje);

        public event OperacionCompletaEventHandler OperacionCompletada;

        AccesoDatos ado = new AccesoDatos();

        public FormAgregarHornero()
        {
            InitializeComponent();
            BtnAceptar.Enabled = true;
            BtnCancelar.Enabled = true;
            BtnAceptar.Click += BtnAceptar_Click;
            BtnCancelar.Click += BtnCancelar_Click;
            this.FormPrincipalRef = (FormPrincipal)Application.OpenForms["FormPrincipal"];
        }
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
                await AgregarHorneroAsync(nuevoHornero);
                OperacionCompletada?.Invoke(true, "Agregado de datos exitoso");
                this.DialogResult = DialogResult.OK;
            }
        }
        private void BtnCancelar_Click(object? sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        public Hornero CrearHornero()
        {
            string nombre = TxtNombre.ToString();
            bool esPeludo = VerificarEsPeludo();
            int velocidad = int.Parse(txtVelocidad.Text);
            bool tieneAlas = ValidarTieneAlas();

            nuevoHornero = new Hornero(velocidad, tieneAlas, esPeludo, Eespecies.Ave, nombre);

            return nuevoHornero;
        }

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
        public bool ValidarTieneAlas()
        {
            bool tieneAlas;
            if (txtTieneAlas.Text == "si")
            {
                tieneAlas = true;
            }
            else if (txtTieneAlas.Text == "no")
            {
                tieneAlas = false;
            }
            else
            {
                throw new ExcepcionTieneAlasErroneo();
            }
            return tieneAlas;
        }
        public void ValidarDatosHornero(List<Exception> excepciones)
        {
            if (string.IsNullOrWhiteSpace(txtTieneAlas.Text))
            {
                excepciones.Add(new ExcepcionTieneAlasVacio());
            }
            else if (txtTieneAlas.Text.ToLower() != "si" && txtTieneAlas.Text.ToLower() != "no")
            {
                excepciones.Add(new ExcepcionTieneAlasErroneo());
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
    }
}
