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

        AccesoDatos ado = new AccesoDatos();

        public bool modificar = false;


        public FormAgregar()
        {
            InitializeComponent();
        }

        public FormAgregar(Animal a) : this()
        {
            txtNombre.Text = a.nombre;
            if (a.esPeludo)
            {
                txtEsPeludo.Text = "si";
            }
            else
            {
                txtEsPeludo.Text = "no";
            }
            this.modificar = true;
        }

        public Label LblTitulo { get; set; }

        public Button BtnAceptar
        {
            get { return btnAceptar; }
            set { btnAceptar = value; }
        }
        public Button BtnCancelar
        {
            get { return btnCancelar;}
            set { btnCancelar = value; }
        }
        public string TxtNombre { get { return txtNombre.Text; } }
        public TextBox TxtPeludo { get; set; }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
           
        }



        //public void CrearOrnitorrinco()
        //{
        //    string nombre = txtNombre.Text;
        //    bool esPeludo = VerificarEsPeludo();

        //    nuevoOrnitorrinco = new Ornitorrinco(esPeludo, true, true, Eespecies.Mamifero, nombre);
        //    _ = FormPrincipalRef.listaOrnitorrincosRefugiados + nuevoOrnitorrinco;
        //    NuevoAnimal = nuevoOrnitorrinco;
        //    ado.AgregarOrnitorrinco(nuevoOrnitorrinco);

        //}

        //public void CrearHornero()
        //{
        //    string nombre = txtNombre.Text;
        //    bool esPeludo = VerificarEsPeludo();

        //    nuevoHornero = new Hornero(40, true, esPeludo, Eespecies.Ave, nombre);
        //    _ = FormPrincipalRef.listaHornerosRefugiados + nuevoHornero;
        //    NuevoAnimal = nuevoHornero;
        //    ado.AgregarHornero(nuevoHornero);
        //}


        public bool VerificarEsPeludo()
        {
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
            return esPeludo;
        }
        
        

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        public void AvisoDeErrores(List<string> errores, List<Exception> excepciones)
        {
            foreach (Exception excepcion in excepciones)
            {
                errores.Add(excepcion.Message);
            }
            if (errores.Count > 0)
            {
                string mensajeError = string.Join("\n", errores);
                MessageBox.Show(mensajeError, "Errores al validar datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void ValidarDatosAnimal(List<Exception> excepciones)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                excepciones.Add(new ExcepcionNombreVacio());
            }
            if (string.IsNullOrWhiteSpace(txtEsPeludo.Text))
            {
                excepciones.Add(new ExcepcionPeludoVacio());
            }
            else if (txtEsPeludo.Text.ToLower() != "si" && txtEsPeludo.Text.ToLower() != "no")
            {
                excepciones.Add(new ExcepcionPeludoErroneo());
            }
        }

    }
}
