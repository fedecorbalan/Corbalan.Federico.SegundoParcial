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
    public partial class FormAgregarRana : FormAgregar
    {
        public Animal NuevoAnimal { get; private set; }

        public Rana nuevaRana;
        public FormPrincipal FormPrincipalRef { get; set; }

        public delegate void OperacionCompletaEventHandler(bool exito, string mensaje);
       
        public event OperacionCompletaEventHandler OperacionCompletada;

        AccesoDatos ado = new AccesoDatos();
        public FormAgregarRana()
        {
            InitializeComponent();
            BtnAceptar.Enabled = true;

            BtnAceptar.Click += BtnAceptar_Click;

        }

        private void BtnAceptar_Click(object? sender, EventArgs e)
        {
            string nombre = TxtNombre.Text;
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
                CrearRana(nombre);
            }
        }

        public bool ValidarVenenosa()
        {
            bool esVenenosa;
            if (txtVenenosa.Text.ToLower() == "si" )
            {
                esVenenosa = true;
            }
            else if (txtVenenosa.Text.ToLower() == "no")
            {
                esVenenosa = false;
            }
            else 
            { 
                throw new ExcepcionEsArboricolaErroneo(); 
            }
            return esVenenosa;

        }
        public void CrearRana(string nombre)
        {
            bool esPeludo = VerificarEsPeludo();
            bool esVenenosa = ValidarVenenosa();
            bool esArboricola = ValidarArboricola();

            nuevaRana = new Rana(esArboricola, esVenenosa, esPeludo, Eespecies.Anfibio, nombre);
            _ = FormPrincipalRef.listaRanasRefugiadas + nuevaRana;
            NuevoAnimal = nuevaRana;
            ado.AgregarRana(nuevaRana);
        }

        public bool ValidarArboricola()
        {
            bool esArboricola;

            if (txtArboricola.Text.ToLower() == "si")
            {
                esArboricola = true;
            }
            else
            {
                esArboricola = false;
            }
            return esArboricola;
        }

        public void ValidarDatosRana(List<Exception> excepciones)
        {
            if (string.IsNullOrWhiteSpace(txtArboricola.Text))
            {
                excepciones.Add(new ExcepcionEsArboricolaVacio());
            }
            else if (txtArboricola.Text.ToLower() != "si" || txtArboricola.Text.ToLower() != "no")
            {
                excepciones.Add(new ExcepcionEsArboricolaErroneo());
            }
            if (string.IsNullOrWhiteSpace(txtVenenosa.Text))
            {
                excepciones.Add(new ExcepcionEsVenenosaVacio());
            }
            else if (txtVenenosa.Text.ToLower() != "si" || txtVenenosa.Text.ToLower() != "no")
            {
                excepciones.Add(new ExcepcionEsVenenosaErroneo());
            }
        }

    }
}
