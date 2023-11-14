﻿using Microsoft.Data.SqlClient;
using PrimerParcial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class AccesoDatos
    {
        private SqlConnection conexion;
        private static string cadena_conexion;
        private SqlCommand comando;
        private SqlDataReader lector;
        static AccesoDatos()
        {
            AccesoDatos.cadena_conexion = Properties.Resources.miConexion;
        }
        public AccesoDatos()
        {
            this.conexion = new SqlConnection(AccesoDatos.cadena_conexion);
        }
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

        public Refugio<Rana> ObtenerListaRanas(Refugio<Rana> lista)
        {
            try
            {
                this.comando = new SqlCommand();
                this.comando.CommandType = System.Data.CommandType.Text;
                this.comando.CommandText = "select id,nombre,especie,esPeludo,esVenenosa,esArboricola from ranas";
                this.comando.Connection = this.conexion;
                this.conexion.Open();


                //this.comando.ExecuteNonQuery(); para comandos que no devuelven nada, lease insert, update y delete
                this.lector = this.comando.ExecuteReader(); //-> Consulta tipo select, devuelve un obj SqlReader con consulta
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
            catch (Exception e)
            {

            }
            finally
            {
                if (this.conexion.State == System.Data.ConnectionState.Open)
                {
                    this.conexion.Close();
                }
            }
            return lista;
        }
        public Refugio<Hornero> ObtenerListaHorneros(Refugio<Hornero> lista)
        {
            try
            {
                this.comando = new SqlCommand();
                this.comando.CommandType = System.Data.CommandType.Text;
                this.comando.CommandText = "select id,nombre,especie,esPeludo,tieneAlas,velocidadKmH from horneros";
                this.comando.Connection = this.conexion;
                this.conexion.Open();


                //this.comando.ExecuteNonQuery(); para comandos que no devuelven nada, lease insert, update y delete
                this.lector = this.comando.ExecuteReader(); //-> Consulta tipo select, devuelve un obj SqlReader con consulta
                while (lector.Read())
                {
                    Hornero hornero = new Hornero();
                    hornero.Id = (int)this.lector["id"];
                    hornero.nombre = this.lector[1].ToString();
                    hornero.especie = (Eespecies)this.lector["especie"];
                    hornero.esPeludo = (bool)this.lector["esPeludo"];
                    hornero.tieneAlas = (bool)this.lector["tieneAlas"];
                    hornero.velocidadVueloKMporH = (int)this.lector["velocidadKmH"];

                    lista.animalesRefugiados.Add(hornero);
                }

                this.lector.Close();
            }
            catch (Exception e)
            {

            }
            finally
            {
                if (this.conexion.State == System.Data.ConnectionState.Open)
                {
                    this.conexion.Close();
                }
            }
            return lista;
        }
        public Refugio<Ornitorrinco> ObtenerListaOrnitorrincos(Refugio<Ornitorrinco> lista)
        {
            try
            {
                this.comando = new SqlCommand();
                this.comando.CommandType = System.Data.CommandType.Text;
                this.comando.CommandText = "select id,nombre,especie,esPeludo,esOviparo,tieneCola from ornitorrincos";
                this.comando.Connection = this.conexion;
                this.conexion.Open();


                //this.comando.ExecuteNonQuery(); para comandos que no devuelven nada, lease insert, update y delete
                this.lector = this.comando.ExecuteReader(); //-> Consulta tipo select, devuelve un obj SqlReader con consulta
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
            catch (Exception e)
            {

            }
            finally
            {
                if (this.conexion.State == System.Data.ConnectionState.Open)
                {
                    this.conexion.Close();
                }
            }
            return lista;
        }
        public bool AgregarDatoRanas(Rana rana)
        {
            bool retorno = false;
            try
            {
                this.comando = new SqlCommand();
                this.comando.CommandType = System.Data.CommandType.Text;
                this.comando.CommandText = "insert into ranas(nombre,especie,esPeludo,esVenenosa,esArboricola) values('" + rana.nombre + "'," + rana.especie + "," + rana.esPeludo + ", " + rana.esVenenosa + "," + rana.esArboricola + ") "; //para agregar datos
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
        public bool AgregarDatoHorneros(Hornero hornero)
        {
            bool retorno = false;
            try
            {
                this.comando = new SqlCommand();
                this.comando.CommandType = System.Data.CommandType.Text;
                this.comando.CommandText = "insert into horneros(nombre,especie,esPeludo,tieneAlas,velocidadKmH) values('" + hornero.nombre + "'," + hornero.especie + "," + hornero.esPeludo + ", " + hornero.tieneAlas + "," + hornero.velocidadVueloKMporH + ") "; //para agregar datos
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
        public bool AgregarDatoOrnitorrincos(Ornitorrinco o)
        {
            bool retorno = false;
            try
            {
                this.comando = new SqlCommand();
                this.comando.CommandType = System.Data.CommandType.Text;
                this.comando.CommandText = "insert into ornitorrincos(nombre,especie,esPeludo,esOviparo,tieneCola) values('" + o.nombre + "'," + o.especie + "," + o.esPeludo + ", " +  o.oviparo + "," + o.tieneCola + ") "; //para agregar datos
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

        public bool ModificarDatoRana(Rana r)
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
        public bool ModificarDatoHornero(Hornero h)
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
                this.comando.Parameters.AddWithValue("@velocidadKmH", h.velocidadVueloKMporH);

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
        public bool ModificarDatoOrnitorrinco(Ornitorrinco o)
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







    }





}

