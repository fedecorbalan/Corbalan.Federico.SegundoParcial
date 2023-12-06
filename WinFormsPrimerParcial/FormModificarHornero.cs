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
    public partial class FormModificarHornero : FormModificar
    {
        public Hornero horneroAModificar;

        public delegate void OperacionCompletaEventHandler(bool exito, string mensaje);

        public event OperacionCompletaEventHandler OperacionCompletada;

        AccesoDatos ado = new AccesoDatos();
        public FormModificarHornero()
        {
            InitializeComponent();
            BtnAceptar.Enabled = true;
            BtnCancelar.Enabled = true;
            BtnAceptar.Click += BtnAceptar_Click;
            BtnCancelar.Click += BtnCancelar_Click;
            this.FormPrincipalRef = (FormPrincipal)Application.OpenForms["FormPrincipal"];
        }
        public FormModificarHornero(Hornero h) : this()
        {
            LblTitulo = "Modificar Hornero";

            TxtNombre = h.nombre;

            if (h.esPeludo == true)
            {
                TxtPeludo = "si";
            }
            else
            {
                TxtPeludo = "no";
            }

            if (h.tieneAlas)
            {
                txtAlas.Text = "si";
            }
            else
            {
                txtAlas.Text = "no";
            }

            txtVelocidad.Text = h.velocidadKmH.ToString();

            horneroAModificar = h;
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
                horneroAModificar.nombre = TxtNombre;
                horneroAModificar.esPeludo = VerificarEsPeludo();
                horneroAModificar.tieneAlas = ValidarTieneAlas();
                horneroAModificar.velocidadKmH = int.Parse(txtVelocidad.Text);

                await ModificarHorneroAsync(horneroAModificar);
                OperacionCompletada?.Invoke(true, "Modificación de datos exitoso");
                this.DialogResult = DialogResult.OK;
            }
        }
        private void BtnCancelar_Click(object? sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

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

        public bool ValidarTieneAlas()
        {
            bool tieneAlas;
            if (txtAlas.Text == "si")
            {
                tieneAlas = true;
            }
            else if (txtAlas.Text == "no")
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
            if (string.IsNullOrWhiteSpace(txtAlas.Text))
            {
                excepciones.Add(new ExcepcionTieneAlasVacio());
            }
            else if (txtAlas.Text.ToLower() != "si" && txtAlas.Text.ToLower() != "no")
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
    }
}
