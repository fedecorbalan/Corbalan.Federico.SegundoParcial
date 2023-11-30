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
    public partial class FormModificar : Form
    {
        private Animal animalAModificar;
        public FormPrincipal FormPrincipalRef { get; set; }

        public Animal AnimalAModificar { get; set; }

        public Label LblClase { get; set; }
        public Label LblAtributo1 { get; set; }
        public Label LblAtributo2 { get; set; }

        public Eespecies Especie { get; set; }

      

        public FormModificar(Animal animal)
        {
            InitializeComponent();
            animalAModificar = animal;
            if (animalAModificar.especie == Eespecies.Mamifero)
            {
                lblClase.Text = "Ornitorrinco";
                lblAtributo1.Text = "Es Oviparo?";
                lblAtributo2.Text = "Tiene Cola?";

            }
            else if (animalAModificar.especie == Eespecies.Ave)
            {
                lblClase.Text = "Hornero";
                lblAtributo1.Text = "Tiene alas?";
                lblAtributo2.Text = "Velocidad en KM/H";

            }
            else if (animalAModificar.especie == Eespecies.Anfibio)
            {
                lblClase.Text = "Rana";
                lblAtributo1.Text = "Es Venenosa?";
                lblAtributo2.Text = "Es Arboricola?";
            }
        }
        

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            string textoNombre = txtNombre.Text;
            string textoPeludo = txtPeludo.Text;

            animalAModificar.nombre = textoNombre;

            if (textoPeludo == "si")
            {
                animalAModificar.esPeludo = true;
            }
            else
            {
                animalAModificar.esPeludo = false;
            }

            if (animalAModificar.especie == Eespecies.Mamifero)
            {
                if (txtAtributo1.Text.ToLower() == "si" && txtAtributo2.Text.ToLower() == "si")
                {
                    ((Ornitorrinco)animalAModificar).oviparo = true;
                    ((Ornitorrinco)animalAModificar).tieneCola = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Datos erroneos, vuelva a ingresarlos");
                }
            }
            else if (animalAModificar.especie == Eespecies.Ave)
            {
                int velocidad = int.Parse(txtAtributo2.Text);
                if (txtAtributo1.Text == "si" && (velocidad > 0 || velocidad < 100))
                {
                    ((Hornero)animalAModificar).tieneAlas = (txtAtributo1.Text == "si");
                    ((Hornero)animalAModificar).velocidadVueloKMporH = velocidad;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Datos erroneos, vuelva a ingresarlos");
                }

            }
            else if (animalAModificar.especie == Eespecies.Anfibio)
            {
                if (txtAtributo1.Text.ToLower() == "si" && txtAtributo2.Text.ToLower() == "si")
                {
                    ((Rana)animalAModificar).esVenenosa = true;
                    ((Rana)animalAModificar).esArboricola = true;
                    this.Close();
                }
                else if (txtAtributo1.Text.ToLower() == "no" && txtAtributo2.Text.ToLower() == "si")
                {
                    ((Rana)animalAModificar).esVenenosa = false;
                    ((Rana)animalAModificar).esArboricola = true;
                    this.Close();
                }
                else if (txtAtributo1.Text.ToLower() == "si" && txtAtributo2.Text.ToLower() == "no")
                {
                    ((Rana)animalAModificar).esVenenosa = true;
                    ((Rana)animalAModificar).esArboricola = false;
                    this.Close();
                }
                else
                {
                    ((Rana)animalAModificar).esVenenosa = false;
                    ((Rana)animalAModificar).esArboricola = false;
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Especie no válida. Solo se permite Ornitorrinco, Rana o Hornero.");
                return;
            }
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
