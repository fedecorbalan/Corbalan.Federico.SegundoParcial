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
    public partial class FormAgregarOrnitorrinco : FormAgregar
    {
        public Ornitorrinco nuevoOrnitorrinco;

        public delegate void OperacionCompletaEventHandler(bool exito, string mensaje);

        public event OperacionCompletaEventHandler OperacionCompletada;

        AccesoDatos ado = new AccesoDatos();

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

                MessageBox.Show("Agregando Ornitorrinco, presione aceptar para continuar");
                await Task.Delay(1000);

                await AgregarOrnitorrincoAsync(nuevoOrnitorrinco);

                OperacionCompletada?.Invoke(true, "Agregado de datos exitoso");
                this.DialogResult = DialogResult.OK;
            }
        }

        private void BtnCancelar_Click(object? sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
        public Ornitorrinco CrearOrnitorrinco()
        {
            string nombre = TxtNombre.ToString();
            bool esPeludo = VerificarEsPeludo();
            bool esOviparo = VerificarOviparo();
            bool tieneCola = VerificarTieneCola();

            nuevoOrnitorrinco = new Ornitorrinco(tieneCola, esOviparo, esPeludo, Eespecies.Mamifero, nombre);

            return nuevoOrnitorrinco;
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
        public int ObtenerIdCorrecto()
        {
            var ultimoOrnitorrinco = FormPrincipalRef.listaOrnitorrincosRefugiados.animalesRefugiados.LastOrDefault();

            if (!(modificar) && ultimoOrnitorrinco is not null)
            {
                return ultimoOrnitorrinco.Id + 1;
            }
            // Si la lista está vacía, devuelve 1 como el primer ID
            else
            {
                return 1;
            }
        }

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
