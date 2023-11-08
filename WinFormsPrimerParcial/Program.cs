using System.Windows.Forms;

namespace WinFormsPrimerParcial
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
        //    // To customize application configuration such as set high DPI settings or default font,
        //    // see https://aka.ms/applicationconfiguration.
        //    ApplicationConfiguration.Initialize();
        //    Application.Run(new FormPrincipal());
            FormLogin frm1 = new FormLogin();
            frm1.StartPosition = FormStartPosition.CenterScreen;

            int cantidadIntentos = 0;

            try
            {
                frm1.ShowDialog();
                do
                {
                    if (cantidadIntentos == 3 && frm1.UsuarioForm == null)
                    {
                        MessageBox.Show("Limite de intentos alcanzado, vuelva a intentarlo mas tarde", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else if (frm1.UsuarioForm == null)
                    {
                        MessageBox.Show("Error en usuario y/o clave!!!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        frm1.ShowDialog();
                    }
                    cantidadIntentos++;
                } while (cantidadIntentos < 3 && frm1.DialogResult != DialogResult.Cancel);

                if (frm1.UsuarioForm != null)
                {
                    FormPrincipal frmPrincipal = new FormPrincipal(frm1.UsuarioForm);
                    frmPrincipal.StartPosition = FormStartPosition.CenterScreen;

                    Application.Run(frmPrincipal);
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message, "Error!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                MessageBox.Show("La aplicación terminó.", "FIN DE LA APLICACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


        }
    }
}