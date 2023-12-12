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

                MessageBox.Show("Modificando Ornitorrinco, presione aceptar para continuar");
                await Task.Delay(1000);

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