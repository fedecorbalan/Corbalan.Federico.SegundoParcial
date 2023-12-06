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
    public partial class FormModificarOrnitorrinco : FormModificar
    {
        public Ornitorrinco ornitorrincoAModificar;

        public delegate void OperacionCompletaEventHandler(bool exito, string mensaje);

        public event OperacionCompletaEventHandler OperacionCompletada;

        AccesoDatos ado = new AccesoDatos();

        public FormModificarOrnitorrinco()
        {
            InitializeComponent();
            BtnCancelar.Enabled = true;
            BtnAceptar.Click += BtnAceptar_Click;
            BtnCancelar.Click += BtnCancelar_Click;
            this.FormPrincipalRef = (FormPrincipal)Application.OpenForms["FormPrincipal"];
        }
        public FormModificarOrnitorrinco(Ornitorrinco o) : this()
        {
            LblTitulo = "Modificar Ornitorrinco";

            TxtNombre = o.nombre;

            if (o.esPeludo == true)
            {
                TxtPeludo = "si";
            }
            else
            {
                TxtPeludo = "no";
            }
            if (o.oviparo)
            {
                txtOviparo.Text = "si";
            }
            else
            {
                txtOviparo.Text = "no";
            }

            if (o.tieneCola)
            {
                txtTieneCola.Text = "si";
            }
            else
            {
                txtTieneCola.Text = "no";
            }

            ornitorrincoAModificar = o;
        }
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

                await ModificarOrnitorrincoAsync(ornitorrincoAModificar);
                OperacionCompletada?.Invoke(true, "Modificacion de datos exitoso");
                this.DialogResult = DialogResult.OK;
            }
        }
        private void BtnCancelar_Click(object? sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
        public bool VerificarOviparo()
        {
            bool esOviparo;

            if (txtOviparo.Text == "si")
            {
                esOviparo = true;
            }
            else if (txtOviparo.Text == "no")
            {
                esOviparo = false;
            }
            else
            {
                throw new ExcepcionEsOviparoErroneo();
            }
            return esOviparo;
        }
        public bool VerificarTieneCola()
        {
            bool tieneCola;

            if (txtTieneCola.Text == "si")
            {
                tieneCola = true;
            }
            else if (txtTieneCola.Text == "no")
            {
                tieneCola = false;
            }
            else
            {
                throw new ExcepcionTieneColaVacio();
            }
            return tieneCola;
        }
        public void ValidarDatosOrnitorrinco(List<Exception> excepciones)
        {
            if (string.IsNullOrWhiteSpace(txtTieneCola.Text))
            {
                excepciones.Add(new ExcepcionEsArboricolaVacio());
            }
            else if (txtTieneCola.Text.ToLower() != "si" && txtTieneCola.Text.ToLower() != "no")
            {
                excepciones.Add(new ExcepcionEsArboricolaErroneo());
            }
            if (string.IsNullOrWhiteSpace(txtOviparo.Text))
            {
                excepciones.Add(new ExcepcionEsVenenosaVacio());
            }
            else if (txtOviparo.Text.ToLower() != "si" && txtOviparo.Text.ToLower() != "no")
            {
                excepciones.Add(new ExcepcionEsVenenosaErroneo());
            }
        }
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