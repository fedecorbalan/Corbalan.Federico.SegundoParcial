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
    /// <summary>
    /// Formulario para el inicio de sesión de usuarios.
    /// </summary>
    public partial class FormLogin : Form
    {
        /// <summary>
        /// Usuario actualmente autenticado.
        /// </summary>
        public Usuario usuario;
        /// <summary>
        /// Propiedad de solo lectura para obtener el usuario del formulario.
        /// </summary>
        public Usuario UsuarioForm { get { return this.usuario; } }

        public object VerificarUsuario { get; private set; }

        /// <summary>
        /// Delegado para manejar la actualización de permisos en el formulario principal.
        /// </summary>
        public delegate void ActualizarPermisosHandler(bool permiteAgregar, bool permiteVer, bool permiteModificar, bool permiteEliminar);
        /// <summary>
        /// Evento que se dispara al actualizar los permisos.
        /// </summary>
        public event ActualizarPermisosHandler ActualizarPermisos;
       
        /// <summary>
        /// Constructor de la clase FormLogin.
        /// </summary>
        public FormLogin()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Constructor sobrecargado de la clase FormLogin.
        /// </summary>
        /// <param name="usuario">Usuario para el inicio de sesión.</param>
        public FormLogin(Usuario usuario) : this()
        {
            this.usuario = usuario;
            this.txtMailUser.Focus();
        }
        /// <summary>
        /// Maneja el evento de hacer clic en el botón Aceptar.
        /// </summary>
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            this.usuario = this.Verificar();

            if (this.usuario != null)
            {
                RegistrarAcceso(this.usuario);

                string perfil = this.usuario.perfil.ToLower();

                // Disparar el evento para ajustar los permisos en FormPrincipal
                ActualizarPermisos?.Invoke(perfil == "administrador", perfil == "vendedor" || perfil == "supervisor", perfil == "administrador" || perfil == "supervisor", perfil == "administrador");
                    

                this.DialogResult = DialogResult.OK;

            }
            else
            {
                MessageBox.Show("Error al iniciar sesion");
            }
            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// Verifica la autenticación del usuario.
        /// </summary>
        /// <returns>Usuario autenticado o null si no se encuentra.</returns>
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
        /// <summary>
        /// Maneja el evento de hacer clic en el botón Salir.
        /// </summary>
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
        // <summary>
        /// Registra el acceso del usuario en un archivo de registro.
        /// </summary>
        /// <param name="usuario">Usuario que accede.</param>
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
