using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.DataAccess.Client;

namespace LindaSonrisa.Models.Schedule
{
    public class Servicio
    {
        private string _servicioId;
        private string  _descripcion;
        private string _profesionalRut;

        public string ProfesionalRut
        {
            get { return _profesionalRut; }
            set { _profesionalRut = value; }
        }


        public string  Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }


        public string ServicioId
        {
            get { return _servicioId; }
            set { _servicioId = value; }
        }

        public Servicio()
        {
        }

        public Servicio(string servicioId, string descripcion)
        {
           
            Descripcion = descripcion;
            ServicioId = servicioId;
        }

        public List<Servicio> BuscarServicio()
        {            
            List<Servicio> serv = new List<Servicio>();

            try
            {
                OracleConnection connection = Conexion.Conexion.GetConnection();

                connection.Open();
                OracleCommand comando = new OracleCommand("select DISTINCT(descripcion),id_servicio from servicio", connection);

                OracleDataReader lector = comando.ExecuteReader();
                Servicio ser;
                while (lector.Read())
                {
                    ser = new Servicio(lector.GetString(1), lector.GetString(0));
                    serv.Add(ser);
                }              

            }
            catch
            {

                return serv;
            }
            return serv;
        }
    }
}