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
    public partial class FormAgregarRana : FormAgregar
    {

        public Rana nuevaRana;

        public delegate void OperacionCompletaEventHandler(bool exito, string mensaje);

        public event OperacionCompletaEventHandler OperacionCompletada;

        AccesoDatos ado = new AccesoDatos();
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
        private void BtnCancelar_Click(object? sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

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
        public Rana CrearRana()
        {
            bool esPeludo = VerificarEsPeludo();
            bool esVenenosa = ValidarVenenosa();
            bool esArboricola = ValidarArboricola();
            string nombre = TxtNombre.ToString();

            nuevaRana = new Rana(esArboricola, esVenenosa, nombre, esPeludo, Eespecies.Anfibio);

            return nuevaRana;
        }

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
