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
using System.Windows.Forms.VisualStyles;

namespace WinFormsSegundoParcial
{
    public partial class FormSeleccionAnimal : Form
    {
        public FormSeleccionAnimal()
        {
            InitializeComponent();
            SeleccionRadioButtons();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            SeleccionAnimal();
            this.Close();
        }

        public void SeleccionAnimal()
        {

            if (rbtnHornero.Checked)
            {
                FormAgregarHornero frmHornero = new FormAgregarHornero();
                frmHornero.ShowDialog();
                this.Close();
            }
            else if (rbtnOrnitorrinco.Checked)
            {
                FormAgregarOrnitorrinco frmOrnitorrinco = new FormAgregarOrnitorrinco();
                frmOrnitorrinco.ShowDialog();
                this.Close();
            }
            else if (rbtnRana.Checked)
            {
                FormAgregarRana frmRana = new FormAgregarRana();
                frmRana.ShowDialog();
                this.Close();
            }   
            else
            {
                MessageBox.Show("Seleccione un Animal.");
            }
        }
        public Type GetSelectedAnimalType()
        {
            if (rbtnHornero.Checked)
            {
                return typeof(Hornero);
            }
            else if (rbtnOrnitorrinco.Checked)
            {
                return typeof(Ornitorrinco);
            }
            else if (rbtnRana.Checked)
            {
                return typeof(Rana);
            }
            else
            {
                throw new ExcepcionAnimalNoSeleccionado();
            }
        }

        public void SeleccionRadioButtons()
        {
            if (rbtnHornero.Checked)
            {
                rbtnOrnitorrinco.Checked = false;
                rbtnRana.Checked = false;
            }
            else if (rbtnOrnitorrinco.Checked)
            {
                rbtnRana.Checked = false;
                rbtnHornero.Checked = false;
            }
            else if (rbtnRana.Checked)
            {
                rbtnOrnitorrinco.Checked = false;
                rbtnHornero.Checked = false;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

            this.Close();
        }
    }
}
