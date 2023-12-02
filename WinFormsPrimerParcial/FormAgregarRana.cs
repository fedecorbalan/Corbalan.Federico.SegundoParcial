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
            CrearRana(nombre);
        }

        public bool ValidarVenenosa()
        {
            bool esVenenosa;
            if(txtVenenosa.Text.ToLower() == "si")
            {
                esVenenosa = true;
            }
            else
            {
                esVenenosa = false;
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

    }
}
