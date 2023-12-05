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
            BtnAceptar.Enabled = true;
            BtnCancelar.Enabled = true;
            BtnAceptar.Click += BtnAceptar_Click;
            BtnCancelar.Click += BtnCancelar_Click;
            this.FormPrincipalRef = (FormPrincipal)Application.OpenForms["FormPrincipal"];
        }

        public FormAgregarRana(Rana r) : this()
        {
            LblTitulo.Text = "Modificar Rana";

            TxtNombre = r.nombre;

            if (r.esPeludo == true)
            {
                TxtPeludo.Text = "si";
            }
            else
            {
                TxtPeludo.Text = "no";
            }


            if (r.esVenenosa)
            {
                txtVenenosa.Text = "si";
            }
            else
            {
                txtVenenosa.Text = "no";
            }

            if (r.esArboricola)
            {
                txtArboricola.Text = "si";
            }
            else
            {
                txtArboricola.Text = "no";
            }

            nuevaRana = r;
            this.modificar = true;
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
                if (modificar)
                {
                    await ModificarRanaAsync(nuevaRana);
                    OperacionCompletada?.Invoke(true, "Modificación de datos exitoso");
                }
                else
                {
                    await AgregarRanaAsync(nuevaRana);
                    OperacionCompletada?.Invoke(true, "Agregado de datos exitoso");
                }
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
        public Rana CrearRana()
        {
            bool esPeludo = VerificarEsPeludo();
            bool esVenenosa = ValidarVenenosa();
            bool esArboricola = ValidarArboricola();
            string nombre = TxtNombre.ToString();

            nuevaRana = new Rana(esArboricola, esVenenosa, esPeludo, Eespecies.Anfibio, nombre);

            return nuevaRana;
        }

        public bool ValidarArboricola()
        {
            bool esArboricola;

            if (txtArboricola.Text.ToLower() == "si")
            {
                esArboricola = true;
            }
            else if  (txtArboricola.Text.ToLower() == "no")
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
            if (string.IsNullOrWhiteSpace(txtArboricola.Text))
            {
                excepciones.Add(new ExcepcionEsArboricolaVacio());
            }
            else if (txtArboricola.Text.ToLower() != "si" && txtArboricola.Text.ToLower() != "no")
            {
                excepciones.Add(new ExcepcionEsArboricolaErroneo());
            }
            if (string.IsNullOrWhiteSpace(txtVenenosa.Text))
            {
                excepciones.Add(new ExcepcionEsVenenosaVacio());
            }
            else if (txtVenenosa.Text.ToLower() != "si" && txtVenenosa.Text.ToLower() != "no")
            {
                excepciones.Add(new ExcepcionEsVenenosaErroneo());
            }
        }

        public async Task AgregarRanaAsync(Rana r)
        {
            try
            {
                await Task.Run(() =>
                {
                    this.ado.AgregarRana(r);
                    FormPrincipalRef.listaRanasRefugiadas.AgregarAnimal(nuevaRana);
                });
            }
            catch(Exception ex)
            {
                OperacionCompletada?.Invoke(false, $"Error al agregar la rana: {ex.Message}");
            }

        }

        public async Task ModificarRanaAsync(Rana r)
        {
            try
            {
                await Task.Run(() =>
                {
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
