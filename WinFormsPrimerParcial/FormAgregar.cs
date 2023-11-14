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

namespace WinFormsPrimerParcial
{
    public partial class FormAgregar : Form
    {
        public Ornitorrinco nuevoOrnitorrinco;
        public Rana nuevaRana;
        public Hornero nuevoHornero;

        public Ornitorrinco NuevoOrnitorrinco { get; private set; }
        public Hornero NuevoHornero { get; private set; }
        public Rana NuevaRana { get; private set; }

        public Animal NuevoAnimal { get; private set; }
        public FormPrincipal FormPrincipalRef { get; set; }

        AccesoDatos ad = new AccesoDatos();


        public FormAgregar()
        {
            InitializeComponent();
        }
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text;
            string textoPeludo = txtEsPeludo.Text.ToLower();
            bool esPeludo;

            if (textoPeludo == "si")
            {
                esPeludo = true;
            }
            else
            {
                esPeludo = false;
            }
            string especie = txtAnimal.Text.ToLower();

            if (especie == "ornitorrinco")
            {
                nuevoOrnitorrinco = new Ornitorrinco(esPeludo, true, true, Eespecies.Mamifero, nombre);
                _ = FormPrincipalRef.listaOrnitorrincosRefugiados + nuevoOrnitorrinco;
                NuevoAnimal = nuevoOrnitorrinco;
                ad.AgregarOrnitorrinco(nuevoOrnitorrinco);
                
            }
            else if (especie == "rana")
            {
                nuevaRana = new Rana(true, true, esPeludo, Eespecies.Anfibio, nombre);
                _ = FormPrincipalRef.listaRanasRefugiadas + nuevaRana;
                NuevoAnimal = nuevaRana;
                ad.AgregarRana(nuevaRana);
            }
            else if (especie == "hornero")
            { 
                nuevoHornero = new Hornero(40, true, esPeludo, Eespecies.Ave, nombre);
                _ = FormPrincipalRef.listaHornerosRefugiados + nuevoHornero;
                NuevoAnimal = nuevoHornero;
                ad.AgregarHornero(nuevoHornero);
            }
            else
            {
                MessageBox.Show("Especie no válida. Solo se permite Ornitorrinco, Rana o Hornero.");
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
