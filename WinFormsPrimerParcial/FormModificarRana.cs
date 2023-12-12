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
    public partial class FormModificarRana : FormModificar
    {
        public Rana ranaAModificar;

        public delegate void OperacionCompletaEventHandler(bool exito, string mensaje);

        public event OperacionCompletaEventHandler OperacionCompletada;

        AccesoDatos ado = new AccesoDatos();
        public FormModificarRana()
        {
            InitializeComponent();
            BtnAceptar.Enabled = true;
            BtnCancelar.Enabled = true;
            BtnAceptar.Click += BtnAceptar_Click;
            BtnCancelar.Click += BtnCancelar_Click;
            this.FormPrincipalRef = (FormPrincipal)Application.OpenForms["FormPrincipal"];
        }

        public FormModificarRana(Rana r) : this()
        {
            LblTitulo = "Modificar Rana";

            TxtNombre = r.nombre;

            if (r.esPeludo)
            {
                RbtnPeludoSi.Checked = true;
            }
            else
            {
                RbtnPeludoNo.Checked = true;
            }

            if (r.esVenenosa)
            {
                rbtnVenenosaSi.Checked = true;
            }
            else
            {
                rbtnVenenosaSi.Checked = false;
            }

            if (r.esArboricola)
            {
                rbtnArboricolaSi.Checked = true;
            }
            else
            {
                rbtnArboricolaNo.Checked = true;
            }

            ranaAModificar = r;
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
                ranaAModificar.nombre = TxtNombre;
                ranaAModificar.esPeludo = VerificarEsPeludo();
                ranaAModificar.esVenenosa = ValidarVenenosa();
                ranaAModificar.esArboricola = ValidarArboricola();

                MessageBox.Show("Modificando Rana, presione aceptar para continuar");
                await Task.Delay(1000);

                await ModificarRanaAsync(ranaAModificar);

                OperacionCompletada?.Invoke(true, "Modificación de datos exitoso");
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
                throw new ExcepcionEsArboricolaVacio();
            }
            return esVenenosa;

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
                throw new ExcepcionEsArboricolaErroneo();
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
        public async Task ModificarRanaAsync(Rana r)
        {
            try
            {
                await Task.Run(() =>
                {
                    ranaAModificar.ActualizarRana(r);
                    this.ado.ModificarRana(r);
                });
            }
            catch (Exception ex)
            {
                OperacionCompletada?.Invoke(false, $"Error al modificar la rana: {ex.Message}");
            }
        }
    }
}
