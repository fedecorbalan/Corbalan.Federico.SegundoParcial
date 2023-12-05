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
            BtnAceptar.Enabled = true;
            BtnCancelar.Enabled = true;
            BtnAceptar.Click += BtnAceptar_Click;
            BtnCancelar.Click += BtnCancelar_Click;
            this.FormPrincipalRef = (FormPrincipal)Application.OpenForms["FormPrincipal"];
        }
        public FormAgregarOrnitorrinco(Ornitorrinco o) : this()
        {
            LblTitulo.Text = "Modificar Rana";

            TxtNombre = o.nombre;

            if (o.esPeludo == true)
            {
                TxtPeludo.Text = "si";
            }
            else
            {
                TxtPeludo.Text = "no";
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

            nuevoOrnitorrinco = o;
            this.modificar = true;
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
                if (modificar)
                {
                    await ModificarOrnitorrincoAsync(nuevoOrnitorrinco);
                    OperacionCompletada?.Invoke(true, "Modificación de datos exitoso");
                }
                else
                {
                    await AgregarOrnitorrincoAsync(nuevoOrnitorrinco);
                    OperacionCompletada?.Invoke(true, "Agregado de datos exitoso");
                }
                this.DialogResult = DialogResult.OK;
            }
        }

        private void BtnCancelar_Click(object? sender, EventArgs e)
        {

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

            if(txtOviparo.Text == "si")
            {
                esOviparo = true;
            }
            else if(txtOviparo.Text == "no")
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

        public async Task AgregarOrnitorrincoAsync(Ornitorrinco o)
        {
            try
            {
                await Task.Run(() =>
                {
                    this.ado.AgregarOrnitorrinco(o);
                    FormPrincipalRef.listaOrnitorrincosRefugiados.AgregarAnimal(nuevoOrnitorrinco);
                });
            }
            catch (Exception ex)
            {
                OperacionCompletada?.Invoke(false, $"Error al agregar el ornitorrinco: {ex.Message}");
            }

        }

        public async Task ModificarOrnitorrincoAsync(Ornitorrinco o)
        {
            try
            {
                await Task.Run(() =>
                {
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
