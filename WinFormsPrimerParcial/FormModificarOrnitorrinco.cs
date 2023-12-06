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

namespace WinFormsSegundoParcial
{
    public partial class FormModificarOrnitorrinco : FormModificar
    {
        public Ornitorrinco nuevoOrnitorrinco;

        public delegate void OperacionCompletaEventHandler(bool exito, string mensaje);

        public event OperacionCompletaEventHandler OperacionCompletada;

        AccesoDatos ado = new AccesoDatos();

        public int IndiceSeleccionado { get; set; }
        public FormModificarOrnitorrinco()
        {
            InitializeComponent();
        }
    }
}
