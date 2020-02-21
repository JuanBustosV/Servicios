﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Servicios//.system
{
    public class EnlaceSqlServer
    {
        private static SqlConnection conexion = null;

        public static SqlConnection Conexion
        {
            get { return EnlaceSqlServer.conexion; }
        }

        public static bool ConectarSqlServer()
        {
            bool estado = false;

            // CURSO: 43. Conexión a SQL Server - P3
            try
            {
                if (conexion == null)
                {
                    conexion = new SqlConnection();
                    /*
                     *  Cadena de conexión de SQL Server 2019 en Somee.com:
                     * 
                     *      workstation id=serviciosx.mssql.somee.com;packet size=4096;user id=JuanBustos_SQLLogin_1;pwd=pjcxkxnor3;
                     *      data source=serviciosx.mssql.somee.com;persist security info=False;initial catalog=serviciosx
                     * 
                     * 
                     */

                    // Cadena usada para el curso, distinto a mi prueba en somee.com
                    //conexion.ConnectionString = "Data Source=" + DatosEnlace.ipBaseDatos +
                    //            "; Initial Catalog=" + DatosEnlace.nombreBaseDatos +
                    //            "; User ID=" + DatosEnlace.usuarioBaseDatos +
                    //            "; Password=" + DatosEnlace.passwordBaseDatos +
                    //            "; MultipleActiveResultSets=True";
                    conexion.ConnectionString = "workstation id=" + DatosEnlace.ipBaseDatos +
                        ";packet size=4096;user id=" + DatosEnlace.usuarioBaseDatos +
                        ";pwd=" + DatosEnlace.passwordBaseDatos +
                        ";data source=" + DatosEnlace.ipBaseDatos +
                        ";persist security info=False;initial catalog=" + DatosEnlace.nombreBaseDatos;

                    //Funciones.Logs("cadena_conexión", conexion.ConnectionString);

                    System.Threading.Thread.Sleep(750);
                }

                if (conexion.State == System.Data.ConnectionState.Closed)
                {
                    conexion.Open();
                }

                if (conexion.State == System.Data.ConnectionState.Broken)
                {
                    conexion.Close();
                    conexion.Open();
                }

                if (conexion.State == System.Data.ConnectionState.Connecting)
                {
                    while (conexion.State == System.Data.ConnectionState.Connecting)
                        System.Threading.Thread.Sleep(500);
                }

                estado = true;
            }
            catch (Exception ex)
            {
                estado = false;
                Funciones.Logs("ENLACESQLSERVER", "Problemas al abrir la conexión; Captura error: " + ex.Message);
                Funciones.Logs("ENLACESQLSERVER_debug", ex.StackTrace);
            }

            return estado;
        }
    }
}