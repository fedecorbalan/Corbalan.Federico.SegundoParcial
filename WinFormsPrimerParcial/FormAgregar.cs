﻿using Entidades;
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


        public FormAgregar()
        {
            InitializeComponent();

        }
        public Button BtnAceptar
        {
            get { return btnAceptar; }
            set { btnAceptar = value; }
        }
        public TextBox TxtNombre { get; set; }
        public TextBox TxtPeludo { get; set; }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            List<string> errores = new List<string>();
            List<Exception> excepciones = new List<Exception>();

            ValidarDatosAnimal(excepciones);

            if (excepciones.Count > 0)
            {
                AvisoDeErrores(errores, excepciones);
            }
            else
            {
               
            }


            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        

        public void CrearOrnitorrinco()
        {
            string nombre = txtNombre.Text;
            bool esPeludo = VerificarEsPeludo();

            nuevoOrnitorrinco = new Ornitorrinco(esPeludo, true, true, Eespecies.Mamifero, nombre);
            _ = FormPrincipalRef.listaOrnitorrincosRefugiados + nuevoOrnitorrinco;
            NuevoAnimal = nuevoOrnitorrinco;
            ado.AgregarOrnitorrinco(nuevoOrnitorrinco);

        }

        public void CrearHornero()
        {
            string nombre = txtNombre.Text;
            bool esPeludo = VerificarEsPeludo();

            nuevoHornero = new Hornero(40, true, esPeludo, Eespecies.Ave, nombre);
            _ = FormPrincipalRef.listaHornerosRefugiados + nuevoHornero;
            NuevoAnimal = nuevoHornero;
            ado.AgregarHornero(nuevoHornero);
        }


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
        {   //al negar el TryParse cuando el parseo no es exitoso retorna true
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                excepciones.Add(new ExcepcionNombreVacio());
            }
            if (string.IsNullOrWhiteSpace(txtEsPeludo.Text))
            {
                excepciones.Add(new ExcepcionPeludoVacio());
            }
        }

    }
}
