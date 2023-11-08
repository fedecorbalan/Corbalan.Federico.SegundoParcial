using Entidades;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsPrimerParcial
{
    public partial class FormLogin : Form
    {
        public Usuario usuario;

        public Usuario UsuarioForm { get { return this.usuario; } }
        public FormLogin()
        {
            InitializeComponent();
        }

        public FormLogin(Usuario usuario) : this()
        {
            this.usuario = usuario;
            this.txtMailUser.Focus();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            this.usuario = this.Verificar();

            if (this.usuario != null)
            {
                RegistrarAcceso(this.usuario);
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Error al iniciar sesion");
            }
            this.DialogResult = DialogResult.OK;
        }

        private Usuario Verificar()
        {
            Usuario? rta = null;

            using (System.IO.StreamReader sr = new System.IO.StreamReader(@"..\..\..\MOCK_DATA.json"))
            {
                System.Text.Json.JsonSerializerOptions opciones = new System.Text.Json.JsonSerializerOptions();
                opciones.WriteIndented = true;

                string json_str = sr.ReadToEnd();

                List<Usuario> users = System.Text.Json.JsonSerializer.Deserialize<List<Usuario>>(json_str, opciones);

                foreach (Usuario item in users)
                {
                    if (item.correo == this.txtMailUser.Text && item.clave == this.txtPassword.Text)
                    {
                        rta = item;
                        break;
                    }
                }
            }
            return rta;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {

            DialogResult resultado = MessageBox.Show("¿Estas seguro que quieres salir?", "SALIR", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                MessageBox.Show("Aplicacion Finalizada");
                Application.Exit();
            }
            else
            {
                MessageBox.Show("App no cerrada");
            }


        }
        public static void RegistrarAcceso(Usuario usuario)
        {
            string registro = $"{DateTime.Now}: Usuario {usuario.nombre} {usuario.apellido} ({usuario.correo}) ha ingresado.";

            // Especifica la ruta del archivo de registro (usuarios.log)
            string rutaArchivoLog = @"..\..\..\usuarios.log";

            try
            {
                using (StreamWriter sw = File.AppendText(rutaArchivoLog))
                {
                    sw.WriteLine(registro);
                }
            }
            catch (Exception ex)
            {
                // Maneja cualquier error de registro
                Console.WriteLine($"Error al registrar acceso: {ex.Message}");
            }
        }
    }
}
