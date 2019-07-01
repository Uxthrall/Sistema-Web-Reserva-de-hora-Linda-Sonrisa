using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Oracle.DataAccess.Client;

namespace LindaSonrisa.Models.Account
{
    public class Apoderado
    {
        private string _rut;
        private string _dv_rut;
        private string _password;
        private string _nombre;
        private string _p_apellido;
        private string _s_apellido;
        private int _celular;
        private string _direccion;
        private int _n_direccion;
        private string _comuna;
        private string _ciudad;
        private string _email;

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }


        public string Ciudad
        {
            get { return _ciudad; }
            set { _ciudad = value; }
        }

        public Apoderado(string email, int celular, string s_apellido, string p_apellido, string nombre, string rut) 
        {
            this.Email = email;
            this.Celular = celular;
            this.S_apellido = s_apellido;
            this.P_apellido = p_apellido;
            this.Nombre = nombre;
            this.Rut = rut;
        }

        public string Comuna
        {
            get { return _comuna; }
            set { _comuna = value; }
        }

        public int N_direccion
        {
            get { return _n_direccion; }
            set { _n_direccion = value; }
        }

        public string Direccion
        {
            get { return _direccion; }
            set { _direccion = value; }
        }

        public int Celular
        {
            get { return _celular; }
            set { _celular = value; }
        }

        public string S_apellido
        {
            get { return _s_apellido; }
            set { _s_apellido = value; }
        }

        public string P_apellido
        {
            get { return _p_apellido; }
            set { _p_apellido = value; }
        }


        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public string Dv_rut
        {
            get { return _dv_rut; }
            set { _dv_rut = value; }
        }

        public string Rut
        {
            get { return _rut; }
            set { _rut = value; }
        }

        public Apoderado(string rut, string dv_rut, string password, string nombre, string p_apellido, string s_apellido, int celular, string direccion, int n_direccion, string comuna, string ciudad, string email)
        {
            this.Rut = rut;
            this.Dv_rut = dv_rut;
            this.Password = password;
            this.Nombre = nombre;
            this.P_apellido = p_apellido;
            this.S_apellido = s_apellido;
            this.Celular = celular;
            this.Direccion = direccion;
            this.N_direccion = n_direccion;
            this.Comuna = comuna;
            this.Ciudad = ciudad;
            this.Email = email;
        }

        public Apoderado(string password, string email)
        {
            this.Password = password;
            this.Email = email;
        }

        public Apoderado(string nombre, string rut,string dv_rut)
        {
            this.Dv_rut = dv_rut;
            this.Nombre = nombre;
            this.Rut = rut;
        }

        public Apoderado()
        {
            _rut = string.Empty;
            _dv_rut=string.Empty;
            _password=string.Empty;
            _nombre=string.Empty;
            _p_apellido=string.Empty;
            _s_apellido=string.Empty;
            _celular=0;
            _direccion=string.Empty;
            _n_direccion=0;
            _comuna=string.Empty;
            _ciudad=string.Empty;
            _email = string.Empty;
         }

        public bool Create()
        {
            try
            {
                OracleConnection connection = Conexion.Conexion.GetConnection();

                connection.Open();                
                OracleCommand comando = new OracleCommand("insert into apoderado (RUT,DV_RUT,CONTRASENA,NOMBRE,P_APELLIDO,S_APELLIDO,CELULAR,EMAIL,DIRECCION,TIPO_USUARIO,N_DIRECCION,COMUNA,CIUDAD) values (:RUT,:DV_RUT,:CONTRASENA,:NOMBRE,:P_APELLIDO,:S_APELLIDO,:CELULAR,:EMAIL,:DIRECCION,:TIPO_USUARIO,:N_DIRECCION,:COMUNA,:CIUDAD)", connection);

                int tipo_usuario = 1;
                comando.Parameters.Add(":RUT", this.Rut);
                comando.Parameters.Add(":DV_RUT", this.Dv_rut);
                comando.Parameters.Add(":CONTRASENA", this.Password);
                comando.Parameters.Add(":NOMBRE", this.Nombre);
                comando.Parameters.Add(":P_APELLIDO", this.P_apellido);
                comando.Parameters.Add(":S_APELLIDO", this.S_apellido);
                comando.Parameters.Add(":CELULAR", this.Celular);
                comando.Parameters.Add(":EMAIL", this.Email);
                comando.Parameters.Add(":DIRECCION", this.Direccion);
                comando.Parameters.Add(":TIPO_USUARIO", tipo_usuario);
                comando.Parameters.Add(":N_DIRECCION", this.N_direccion);
                comando.Parameters.Add(":COMUNA", this.Comuna);
                comando.Parameters.Add(":CIUDAD", this.Ciudad);

                OracleDataReader lector = comando.ExecuteReader();

                return true;

            }
            catch
            {

                return false;
            }

        }

        public Apoderado inicioSesion(string email,string password)
        {
            Apoderado apod = null;
            try
            {
                OracleConnection connection = Conexion.Conexion.GetConnection();

                connection.Open();
                OracleCommand comando = new OracleCommand("select nombre||' '||p_apellido||' '||s_apellido as nombre, rut, dv_rut from Apoderado Where email=:EMAIL and CONTRASENA=:CONTRASENA", connection);

                comando.Parameters.Add(":EMAIL", email);
                comando.Parameters.Add(":CONTRASENA", password);              
               
                OracleDataReader lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    apod = new Apoderado(lector.GetString(0), lector.GetString(1),lector.GetString(2)) ;
                    
                }

                return apod;

            }
            catch
            {

                return apod;
            }
        }

        public Apoderado BuscarApoderado(string nombre)
        {
            Apoderado apod = null;
            try
            {
                OracleConnection connection = Conexion.Conexion.GetConnection();

                connection.Open();
                OracleCommand comando = new OracleCommand("select  email,celular,s_apellido,p_apellido,nombre,rut from Apoderado Where nombre||' '||p_apellido||' '||s_apellido||''||rut=:NOMBRE ", connection);

                comando.Parameters.Add(":NOMBRE", nombre);

                OracleDataReader lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    apod = new Apoderado(lector.GetString(0),lector.GetInt32(1),lector.GetString(2),lector.GetString(3),lector.GetString(4),lector.GetString(5));

                }

                return apod;

            }
            catch
            {

                return apod;
            }
        }
    }
}