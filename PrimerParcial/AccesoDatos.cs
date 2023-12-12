using Microsoft.Data.SqlClient;
using PrimerParcial;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    /// <summary>
    /// Clase que proporciona acceso a datos y realiza operaciones CRUD en la base de datos para las entidades Rana, Hornero y Ornitorrinco.
    /// Implementa las interfaces ICrudRana, ICrudHornero e ICrudOrnitorrinco.
    /// </summary>
    public class AccesoDatos: ICrudRana,ICrudHornero,ICrudOrnitorrinco
    {
        private SqlConnection conexion;
        private static string cadena_conexion;
        private SqlCommand comando;
        private SqlDataReader lector;

        /// <summary>
        /// Inicializa la cadena de conexión estática con el valor proporcionado en los recursos.
        /// </summary>
        static AccesoDatos()
        {
            AccesoDatos.cadena_conexion = Properties.Resources.miConexion;
        }
        /// <summary>
        /// Inicializa una nueva instancia de la clase AccesoDatos con una conexión a la base de datos.
        /// </summary>
        public AccesoDatos()
        {
            this.conexion = new SqlConnection(AccesoDatos.cadena_conexion);
        }
        /// <summary>
        /// Realiza una prueba de conexión a la base de datos.
        /// </summary>
        /// <returns>True si la conexión es exitosa, false en caso contrario.</returns>
        public bool PruebaConexion()
        {
            bool retorno = false;

            try
            {
                this.conexion.Open();
                retorno = true;
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (this.conexion.State == System.Data.ConnectionState.Open)
                {
                    this.conexion.Close();
                }
            }
            return retorno;
        }
        /// <summary>
        /// Obtiene la lista de ranas desde la base de datos y la asigna a la lista proporcionada.
        /// </summary>
        /// <param name="lista">Lista de ranas existente.</param>
        /// <returns>La lista de ranas actualizada.</returns>
        public Refugio<Rana> ObtenerListaRanas(Refugio<Rana> lista)
        {
            try
            {
                this.comando = new SqlCommand();
                this.comando.CommandType = System.Data.CommandType.Text;
                this.comando.CommandText = "select id,nombre,especie,esPeludo,esVenenosa,esArboricola from ranas";
                this.comando.Connection = this.conexion;
                this.conexion.Open();

                this.lector = this.comando.ExecuteReader();
                while (lector.Read())
                {
                    Rana rana = new Rana();
                    rana.Id = (int)this.lector["id"];
                    rana.nombre = this.lector[1].ToString();
                    rana.especie = (Eespecies)this.lector["especie"];
                    rana.esPeludo = (bool)this.lector["esPeludo"];
                    rana.esVenenosa = (bool)this.lector["esVenenosa"];
                    rana.esArboricola = (bool)this.lector["esArboricola"];

                    lista.animalesRefugiados.Add(rana);
                }
                this.lector.Close();
            }
            catch (Exception e){ }
            finally
            {
                if (this.conexion.State == System.Data.ConnectionState.Open)
                {
                    this.conexion.Close();
                }
            }
            return lista;
        }
        /// <summary>
        /// Obtiene la lista de horneros desde la base de datos y la asigna a la lista proporcionada.
        /// </summary>
        /// <param name="lista">Lista de horneros existente.</param>
        /// <returns>La lista de horneros actualizada.</returns>
        public Refugio<Hornero> ObtenerListaHorneros(Refugio<Hornero> lista)
        {
            try
            {
                this.comando = new SqlCommand();
                this.comando.CommandType = System.Data.CommandType.Text;
                this.comando.CommandText = "select id,nombre,especie,esPeludo,tieneAlas,velocidadKmH from horneros";
                this.comando.Connection = this.conexion;
                this.conexion.Open();

                this.lector = this.comando.ExecuteReader(); 
                while (lector.Read())
                {
                    Hornero hornero = new Hornero();
                    hornero.Id = (int)this.lector["id"];
                    hornero.nombre = this.lector[1].ToString();
                    hornero.especie = (Eespecies)this.lector["especie"];
                    hornero.esPeludo = (bool)this.lector["esPeludo"];
                    hornero.tieneAlas = (bool)this.lector["tieneAlas"];
                    hornero.velocidadKmH = (int)this.lector["velocidadKmH"];

                    lista.animalesRefugiados.Add(hornero);
                }
                this.lector.Close();
            }
            catch (Exception e) {}
            finally
            {
                if (this.conexion.State == System.Data.ConnectionState.Open)
                {
                    this.conexion.Close();
                }
            }
            return lista;
        }
        /// <summary>
        /// Obtiene la lista de ornitorrincos desde la base de datos y la asigna a la lista proporcionada.
        /// </summary>
        /// <param name="lista">Lista de ornitorrincos existente.</param>
        /// <returns>La lista de ornitorrincos actualizada.</returns>
        public Refugio<Ornitorrinco> ObtenerListaOrnitorrincos(Refugio<Ornitorrinco> lista)
        {
            try
            {
                this.comando = new SqlCommand();
                this.comando.CommandType = System.Data.CommandType.Text;
                this.comando.CommandText = "select id,nombre,especie,esPeludo,esOviparo,tieneCola from ornitorrincos";
                this.comando.Connection = this.conexion;
                this.conexion.Open();

                this.lector = this.comando.ExecuteReader(); 
                while (lector.Read())
                {
                    Ornitorrinco ornitorrinco = new Ornitorrinco();
                    ornitorrinco.Id = (int)this.lector["id"];
                    ornitorrinco.nombre = this.lector[1].ToString();
                    ornitorrinco.especie = (Eespecies)this.lector["especie"];
                    ornitorrinco.esPeludo = (bool)this.lector["esPeludo"];
                    ornitorrinco.oviparo = (bool)this.lector["esOviparo"];
                    ornitorrinco.tieneCola = (bool)this.lector["tieneCola"];

                    lista.animalesRefugiados.Add(ornitorrinco);
                }
                this.lector.Close();
            }
            catch (Exception e) {}
            finally
            {
                if (this.conexion.State == System.Data.ConnectionState.Open)
                {
                    this.conexion.Close();
                }
            }
            return lista;
        }
        /// <summary>
        /// Agrega una rana a la base de datos.
        /// </summary>
        /// <param name="r">La rana a agregar.</param>
        public bool AgregarRana(Rana r)
        {
            bool retorno = false;

            try
            {
                this.comando = new SqlCommand();
                this.comando.CommandType = System.Data.CommandType.Text;

                this.comando.CommandText = "INSERT INTO ranas(nombre, especie, esPeludo, esVenenosa, esArboricola) " +
                    "VALUES(@nombre, @especie, @esPeludo, @esVenenosa, @esArboricola)";

                this.comando.Parameters.AddWithValue("@nombre", r.nombre);
                this.comando.Parameters.AddWithValue("@especie", (int)r.especie);
                this.comando.Parameters.AddWithValue("@esPeludo", r.esPeludo);
                this.comando.Parameters.AddWithValue("@esVenenosa", r.esVenenosa);
                this.comando.Parameters.AddWithValue("@esArboricola", r.esArboricola);

                this.comando.Connection = this.conexion;

                this.conexion.Open();

                int filasAfectadas = this.comando.ExecuteNonQuery();

                if (filasAfectadas > 0)
                {
                    // Recuperar el último ID insertado
                    this.comando.CommandText = "SELECT IDENT_CURRENT('ranas')";
                    int nuevoId = Convert.ToInt32(this.comando.ExecuteScalar());

                    r.Id = nuevoId;
                    retorno = true;
                }
            }
            catch (Exception w)
            {
                // Manejar la excepción
            }
            finally
            {
                if (this.conexion.State == System.Data.ConnectionState.Open)
                {
                    this.conexion.Close();
                }
            }
            return retorno;
        }
        /// <summary>
        /// Agrega un hornero a la base de datos.
        /// </summary>
        /// <param name="h">El hornero a agregar.</param>
        public bool AgregarHornero(Hornero h)
        {
            bool retorno = false;

            try
            {
                this.comando = new SqlCommand();
                this.comando.CommandType = System.Data.CommandType.Text;

                // Utilizar parámetros para evitar la inyección de SQL
                this.comando.CommandText = "INSERT INTO horneros(nombre, especie, esPeludo, tieneAlas, velocidadKmH) " +
                    "VALUES(@nombre, @especie, @esPeludo, @tieneAlas, @velocidadKmH)"; 

                // Añadir parámetros
                this.comando.Parameters.AddWithValue("@nombre", h.nombre);
                this.comando.Parameters.AddWithValue("@especie", (int)h.especie);
                this.comando.Parameters.AddWithValue("@esPeludo", h.esPeludo);
                this.comando.Parameters.AddWithValue("@tieneAlas", h.tieneAlas);
                this.comando.Parameters.AddWithValue("@velocidadKmH", (int)h.velocidadKmH);

                this.comando.Connection = this.conexion;

                this.conexion.Open();

                int filasAfectadas = this.comando.ExecuteNonQuery();

                if (filasAfectadas > 0)
                {
                    this.comando.CommandText = "SELECT IDENT_CURRENT('horneros')";
                    int nuevoId = Convert.ToInt32(this.comando.ExecuteScalar());

                    h.Id = nuevoId;
                    retorno = true;
                }
            }
            catch (Exception w)
            {
                // Manejar la excepción
            }
            finally
            {
                if (this.conexion.State == System.Data.ConnectionState.Open)
                {
                    this.conexion.Close();
                }
            }
            return retorno;
        }
        /// <summary>
        /// Agrega un ornitorrinco a la base de datos.
        /// </summary>
        /// <param name="o">El ornitorrinco a agregar.</param>
        public bool AgregarOrnitorrinco(Ornitorrinco o)
        {
            bool retorno = false;

            try
            {
                this.comando = new SqlCommand();
                this.comando.CommandType = System.Data.CommandType.Text;

                // Utilizar parámetros para evitar la inyección de SQL
                this.comando.CommandText = "INSERT INTO ornitorrincos(nombre, especie, esPeludo, esOviparo, tieneCola) " +
                    "VALUES(@nombre, @especie, @esPeludo, @esOviparo, @tieneCola)";

                // Añadir parámetros
                this.comando.Parameters.AddWithValue("@nombre", o.nombre);
                this.comando.Parameters.AddWithValue("@especie", (int)o.especie);
                this.comando.Parameters.AddWithValue("@esPeludo", o.esPeludo);
                this.comando.Parameters.AddWithValue("@esOviparo", o.oviparo);
                this.comando.Parameters.AddWithValue("@tieneCola", o.tieneCola);

                this.comando.Connection = this.conexion;

                this.conexion.Open();

                int filasAfectadas = this.comando.ExecuteNonQuery();

                if (filasAfectadas > 0)
                {
                    this.comando.CommandText = "SELECT IDENT_CURRENT('ornitorrincos')";
                    int nuevoId = Convert.ToInt32(this.comando.ExecuteScalar());

                    o.Id = nuevoId;
                    retorno = true;
                }

            }
            catch (Exception w)
            {
                // Manejar la excepción
            }
            finally
            {
                if (this.conexion.State == System.Data.ConnectionState.Open)
                {
                    this.conexion.Close();
                }
            }
            return retorno;
        }
        /// <summary>
        /// Modifica una rana en la base de datos.
        /// </summary>
        /// <param name="r">La rana a modificar.</param>
        /// <returns>True si se modificó correctamente, false en caso contrario.</returns>
        public bool ModificarRana(Rana r)
        {
            bool retorno = false;
            try
            {
                this.comando = new SqlCommand();

                this.comando.Parameters.AddWithValue("@id", r.Id);
                this.comando.Parameters.AddWithValue("@nombre", r.nombre);
                this.comando.Parameters.AddWithValue("@especie", r.especie);
                this.comando.Parameters.AddWithValue("@esPeludo", r.esPeludo);
                this.comando.Parameters.AddWithValue("@esVenenosa", r.esVenenosa);
                this.comando.Parameters.AddWithValue("@esArboricola", r.esArboricola);

                this.comando.CommandType = System.Data.CommandType.Text;

                this.comando.CommandText = "update ranas set nombre = @nombre, especie = @especie, esPeludo = @esPeludo, esVenenosa = @esVenenosa, esArboricola = @esArboricola WHERE id = @id";
                this.comando.Connection = this.conexion;

                this.conexion.Open();

                int filasAfectadas = this.comando.ExecuteNonQuery();

                if (filasAfectadas == 1)
                {
                    retorno = true;
                }

            }
            catch (Exception w) {}
            finally
            {
                if (this.conexion.State == System.Data.ConnectionState.Open)
                {
                    this.conexion.Close();
                }
            }
            return retorno;
        }
        /// <summary>
        /// Modifica un hornero en la base de datos.
        /// </summary>
        /// <param name="h">El hornero a modificar.</param>
        /// <returns>True si se modificó correctamente, false en caso contrario.</returns>
        public bool ModificarHornero(Hornero h)
        {
            bool retorno = false;
            try
            {
                this.comando = new SqlCommand();

                this.comando.Parameters.AddWithValue("@id", h.Id);
                this.comando.Parameters.AddWithValue("@nombre", h.nombre);
                this.comando.Parameters.AddWithValue("@especie", h.especie);
                this.comando.Parameters.AddWithValue("@esPeludo", h.esPeludo);
                this.comando.Parameters.AddWithValue("@tieneAlas", h.tieneAlas);
                this.comando.Parameters.AddWithValue("@velocidadKmH", h.velocidadKmH);

                this.comando.CommandType = System.Data.CommandType.Text;

                this.comando.CommandText = "update horneros set nombre = @nombre, especie = @especie, esPeludo = @esPeludo, tieneAlas = @tieneAlas, velocidadKmH = @velocidadKmH WHERE id = @id";
                this.comando.Connection = this.conexion;

                this.conexion.Open();

                int filasAfectadas = this.comando.ExecuteNonQuery();

                if (filasAfectadas == 1)
                {
                    retorno = true;
                }

            }
            catch (Exception w) 
            { 
            }
            finally
            {
                if (this.conexion.State == System.Data.ConnectionState.Open)
                {
                    this.conexion.Close();
                }
            }
            return retorno;
        }


        /// <summary>
        /// Modifica un ornitorrinco en la base de datos.
        /// </summary>
        /// <param name="o">El ornitorrinco a modificar.</param>
        /// <returns>True si se modificó correctamente, false en caso contrario.</returns>
        public bool ModificarOrnitorrinco(Ornitorrinco o)
        {
            bool retorno = false;
            try
            {
                this.comando = new SqlCommand();

                this.comando.Parameters.AddWithValue("@id", o.Id);
                this.comando.Parameters.AddWithValue("@nombre", o.nombre);
                this.comando.Parameters.AddWithValue("@especie", o.especie);
                this.comando.Parameters.AddWithValue("@esPeludo", o.esPeludo);
                this.comando.Parameters.AddWithValue("@esOviparo", o.oviparo);
                this.comando.Parameters.AddWithValue("@tieneCola", o.tieneCola);

                this.comando.CommandType = System.Data.CommandType.Text;

                this.comando.CommandText = "update ornitorrincos set nombre = @nombre, especie = @especie, esPeludo = @esPeludo, esOviparo = @esOviparo, tieneCola = @tieneCola WHERE id = @id";
                this.comando.Connection = this.conexion;

                this.conexion.Open();

                int filasAfectadas = this.comando.ExecuteNonQuery();

                if (filasAfectadas == 1)
                {
                    retorno = true;
                }

            }
            catch (Exception w) { }
            finally
            {
                if (this.conexion.State == System.Data.ConnectionState.Open)
                {
                    this.conexion.Close();
                }
            }
            return retorno;
        }
        /// <summary>
        /// Elimina una rana de la base de datos.
        /// </summary>
        /// <param name="r">La rana a eliminar.</param>
        /// <returns>True si se eliminó correctamente, false en caso contrario.</returns>
        public bool EliminarRana(Rana r)
        {
            bool retorno = false;
            try
            {
                this.comando = new SqlCommand();
                this.comando.Parameters.AddWithValue("@id", r.Id);
                this.comando.CommandType = System.Data.CommandType.Text;
                this.comando.CommandText = "DELETE FROM ranas WHERE id = @id";
                this.comando.Connection = this.conexion;

                this.conexion.Open();

                int filasAfectadas = this.comando.ExecuteNonQuery();

                if (filasAfectadas == 1)
                {
                    retorno = true;

                }
            }
            catch (Exception ex) { }
            finally
            {
                if (this.conexion.State == System.Data.ConnectionState.Open)
                {
                    this.conexion.Close();
                }
            }
            return retorno;
        }
        /// <summary>
        /// Elimina un hornero de la base de datos.
        /// </summary>
        /// <param name="h">El hornero a eliminar.</param>
        /// <returns>True si se eliminó correctamente, false en caso contrario.</returns>
        public bool EliminarHornero(Hornero h)
        {
            bool retorno = false;
            try
            {
                this.comando = new SqlCommand();
                this.comando.Parameters.AddWithValue("@id", h.Id);
                this.comando.CommandType = System.Data.CommandType.Text;
                this.comando.CommandText = "DELETE FROM horneros WHERE id = @id";
                this.comando.Connection = this.conexion;

                this.conexion.Open();

                int filasAfectadas = this.comando.ExecuteNonQuery();

                if (filasAfectadas == 1)
                {
                    retorno = true;
                }
            }
            catch (Exception ex) { }
            finally
            {
                if (this.conexion.State == System.Data.ConnectionState.Open)
                {
                    this.conexion.Close();
                }
            }
            return retorno;
        }
        /// <summary>
        /// Elimina un ornitorrinco de la base de datos.
        /// </summary>
        /// <param name="o">El ornitorrinco a eliminar.</param>
        /// <returns>True si se eliminó correctamente, false en caso contrario.</returns>
        public bool EliminarOrnitorrinco(Ornitorrinco o)
        {
            bool retorno = false;
            try
            {
                this.comando = new SqlCommand();
                this.comando.Parameters.AddWithValue("@id", o.Id);
                this.comando.CommandType = System.Data.CommandType.Text;
                this.comando.CommandText = "DELETE FROM ornitorrincos WHERE id = @id";
                this.comando.Connection = this.conexion;

                this.conexion.Open();

                int filasAfectadas = this.comando.ExecuteNonQuery();

                if (filasAfectadas == 1)
                {
                    retorno = true;
                }
            }
            catch (Exception ex){}
            finally
            {
                if (this.conexion.State == System.Data.ConnectionState.Open)
                {
                    this.conexion.Close();
                }
            }
            return retorno;
        }
    }
}

