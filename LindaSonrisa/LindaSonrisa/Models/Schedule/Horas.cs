using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.DataAccess.Client;

namespace LindaSonrisa.Models.Schedule
{
    public class Horas
    {
        private DateTime _dia;
        private string _hora;
        private int _reservada;
        private string _rut;
        private string _nom_prof;
        private string _descripcionServicio;

        public string Descripcion
        {
            get { return _descripcionServicio; }
            set { _descripcionServicio = value; }
        }


        public Horas(string descripcion,string nom_prof, string rut_profes, int reservada, string hora, DateTime dia)
        {
            this.Descripcion = descripcion;
            this.Nom_prof = nom_prof;
            this.Rut_profes = rut_profes;
            this.Reservada = reservada;
            this.Hora = hora;
            this.Dia = dia;
        }

        public Horas()
        {
        }

        public string Nom_prof
        {
            get { return _nom_prof; }
            set { _nom_prof = value; }
        }

        public string Rut_profes
        {
            get { return _rut; }
            set { _rut = value; }
        }


        public int Reservada
        {
            get { return _reservada; }
            set { _reservada = value; }
        }


        public string Hora
        {
            get { return _hora; }
            set { _hora = value; }
        }

        public DateTime Dia
        {
            get { return _dia; }
            set { _dia = value; }
        }

        public List<Horas> BuscarHorasDisponibles(string dia,string id)
        {
            List<Horas> hora = new List<Horas>();

            try
            {
                OracleConnection connection = Conexion.Conexion.GetConnection();

                connection.Open();
                OracleCommand comando = new OracleCommand("select   " +
                                                                   "t.dia ," +
                                                                   "t.hora, " +
                                                                   "t.reservada," +
                                                                   "t.profesional_rut, " +
                                                                   "p.nombre||' '||p.p_apellido||' '||p.s_apellido, " +
                                                                   "s.descripcion " +

                                                         " from turno t join profesional p " +
                                                         " on (t.profesional_rut = p.rut) " +
                                                         " join servicio s " +
                                                         " on (p.rut = s.profesional_rut) " +
                                                                                        " where t.dia = '"+dia+"' and s.id_servicio = '"+id+"' " +
                                                                                        " order by t.hora asc", connection);              

                OracleDataReader lector = comando.ExecuteReader();
                Horas ho;
                while (lector.Read())
                {
                    ho = new Horas(lector.GetString(5),lector.GetString(4), lector.GetString(3), lector.GetInt32(2), lector.GetString(1), lector.GetDateTime(0));
                    hora.Add(ho);
                }
            }
            catch
            {
                return hora;
            }
            return hora;
        }

        public bool modificarEstado(int truee,string dia, string hora)
        {
            try
            {
                OracleConnection connection = Conexion.Conexion.GetConnection();

                connection.Open();
                OracleCommand comando = new OracleCommand("update turno set reservada=:TRUE where  dia = :DIA and  hora=:HORA", connection);

                comando.Parameters.Add(":TRUE", truee);
                comando.Parameters.Add(":DIA", dia);
                comando.Parameters.Add(":HORA",hora);

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