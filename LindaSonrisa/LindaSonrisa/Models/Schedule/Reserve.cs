using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.DataAccess.Client;

namespace LindaSonrisa.Models.Schedule
{
    public class Reserve
    {
        private Int64 _id_reserva;
        private string _servicio;
        private DateTime _fecha;
        private string  _apoderado_rut;
        private string _id_servicio;
        private string _profesional_rut;
        private string _nom_paciente;

        private string _nom_profesional;
        private string _hora;

        public string Hora
        {
            get { return _hora; }
            set { _hora = value; }
        }


        public string NomProfesional
        {
            get { return _nom_profesional; }
            set { _nom_profesional = value; }
        }


        public string Nom_paciente
        {
            get { return _nom_paciente; }
            set { _nom_paciente = value; }
        }


        public string ProfesionalRut
        {
            get { return _profesional_rut; }
            set { _profesional_rut = value; }
        }

        public string Id_servicio   
        {
            get { return _id_servicio; }
            set { _id_servicio = value; }
        }


        public string  ApoderadoRut
        {
            get { return _apoderado_rut; }
            set { _apoderado_rut = value; }
        }


        public DateTime Fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }


        public string Servicio
        {
            get { return _servicio; }
            set { _servicio = value; }
        }


        public Int64 Id_reserva
        {
            get { return _id_reserva; }
            set { _id_reserva = value; }
        }

        public Reserve(string nom_paciente, string profesionalRut, string id_servicio, string apoderadoRut, DateTime fecha, string servicio, Int64 id_reserva)
        {
            Nom_paciente = nom_paciente;
            ProfesionalRut = profesionalRut;
            Id_servicio = id_servicio;
            ApoderadoRut = apoderadoRut;
            Fecha = fecha;
            Servicio = servicio;
            Id_reserva = id_reserva;
        }

        public Reserve()
        {
        }

        public Reserve(string hora, string nomProfesional, string nom_paciente, string profesionalRut, string apoderadoRut, DateTime fecha, string servicio, Int64 id_reserva)
        {
            Hora = hora;
            NomProfesional = nomProfesional;
            Nom_paciente = nom_paciente;
            ProfesionalRut = profesionalRut;
            ApoderadoRut = apoderadoRut;
            Fecha = fecha;
            Servicio = servicio;
            Id_reserva = id_reserva;
        }

        public bool Create()
        {
            try
            {
                OracleConnection connection = Conexion.Conexion.GetConnection();

                connection.Open();
                OracleCommand comando = new OracleCommand("insert into reserva_servicio (ID_RESERVA,SERVICIO,FECHA,APODERADO_RUT,SERVICIO_ID_SERVICIO,SERVICIO_PROFESIONAL_RUT,NOM_PACIENTE) " +
                                                            "values (:ID_RESERVA,:SERVICIO,:FECHA,:APODERADO_RUT,:ID_SERVICIO,:PROFESIONAL_RUT,:NOM_PACIENTE)", connection);

                comando.Parameters.Add(":ID_RESERVA", this.Id_reserva);
                comando.Parameters.Add(":SERVICIO", this.Servicio);
                comando.Parameters.Add(":FECHA", this.Fecha);
                comando.Parameters.Add(":APODERADO_RUT", this.ApoderadoRut);
                comando.Parameters.Add(":ID_SERVICIO", this.Id_servicio);
                comando.Parameters.Add(":PROFESIONAL_RUT", this.ProfesionalRut);
                comando.Parameters.Add(":NOM_PACIENTE", this.Nom_paciente);              

                OracleDataReader lector = comando.ExecuteReader();

                return true;
            }
            catch
            {

                return false;
            }

        }
        public List<Reserve> ListarReserva(string rut)
        {
            List<Reserve> res= new List<Reserve>();

            try
            {
                OracleConnection connection = Conexion.Conexion.GetConnection();

                connection.Open();
                OracleCommand comando = new OracleCommand("select        " +
                                                                "r.nom_paciente,      " +
                                                                " p.rut,      " +
                                                                " r.servicio,       " +
                                                                " p.nombre||' '||p.p_apellido||' '||p.s_apellido as profesional,       " +
                                                                " r.id_reserva,        " +
                                                                "r.fecha,        " +
                                                                "r.apoderado_rut,       " +
                                                                "case        " +
                                                                " when length(r.id_reserva) = 9 then SUBSTR(r.id_reserva,6,2)||':'||SUBSTR(r.id_reserva,8,2) " +
                                                                " when length(r.id_reserva) = 10 then SUBSTR(r.id_reserva,7,2)||':'||SUBSTR(r.id_reserva,9,2) " +
                                                                "end  as hora         " +
                                                                "from reserva_servicio r left join profesional p " +
                                                                " on(r.servicio_profesional_rut = p.rut)                                        " +
                                                                                                         "where r.apoderado_rut=:RUT                                          " +
                                                                                                         "order by r.fecha asc  ", connection);
                comando.Parameters.Add(":RUT", rut);

                OracleDataReader lector = comando.ExecuteReader();

                Reserve re;
                while(lector.Read())
                {
                    re = new Reserve(lector.GetString(7), lector.GetString(3), lector.GetString(0), lector.GetString(1),  lector.GetString(6), lector.GetDateTime(5), lector.GetString(2), lector.GetInt64(4));
                    res.Add(re);
                }
            }
            catch 
            {

                return  res;
            }

            return res;
        }

        public bool EliminarReserva(string id_reserva)
        {
            try
            {
                OracleConnection connection = Conexion.Conexion.GetConnection();

                connection.Open();

                OracleCommand comando = new OracleCommand("Delete From reserva_servicio Where id_reserva=:ID_RESERVA",connection);

                comando.Parameters.Add(":ID_RESERVA", id_reserva);

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