using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.DataAccess.Client;

namespace LindaSonrisa.Models.MyReserve
{
    public class Ticket
    {
        private string _apoderado_rut;
        private int _id_reserva;
        private string _fechaRegistro;

        public string FechaRegistro
        {
            get { return _fechaRegistro; }
            set { _fechaRegistro = value; }
        }

        public int Id_reserva
        {
            get { return _id_reserva; }
            set { _id_reserva = value; }
        }

        public string ApoderadoRut
        {
            get { return _apoderado_rut; }
            set { _apoderado_rut = value; }
        }

        public Ticket(string fechaRegistro, int id_reserva, string apoderadoRut)
        {
            FechaRegistro = fechaRegistro;
            Id_reserva = id_reserva;
            ApoderadoRut = apoderadoRut;
        }

        public Ticket()
        {
        }

        public bool Create()
        {
            try
            {
                OracleConnection connection = Conexion.Conexion.GetConnection();

                connection.Open();
                OracleCommand comando = new OracleCommand("insert into boleta_servicio (reserva_apoderado_rut,reserva_id_reserva,fecha_registro) " +
                                                        "values (:APODERADO_RUT, :ID_RESERVA,:FECHA_REGISTRO) ", connection);

                comando.Parameters.Add(":APODERADO_RUT", this.ApoderadoRut);
                comando.Parameters.Add(":ID_RESERVA", this.Id_reserva);
                comando.Parameters.Add(":FECHA_REGISTRO", this.FechaRegistro);

                OracleDataReader lector = comando.ExecuteReader();

                return true;
            }
            catch
            {

                return false;
            }

        }
    }
}