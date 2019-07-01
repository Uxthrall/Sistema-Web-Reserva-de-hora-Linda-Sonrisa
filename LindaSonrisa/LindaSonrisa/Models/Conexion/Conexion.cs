using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.DataAccess.Client;

namespace LindaSonrisa.Models.Conexion
{
    public class Conexion
    {
       //private  static OracleConnection conn = null;
        public static OracleConnection GetConnection()
        {
            OracleConnection conn = null;

            if (conn==null)
            {
                conn = new OracleConnection("DATA SOURCE =XE; PASSWORD =ux2734a; USER ID =alej_ux");

            }
            return conn;
        }
    }
}