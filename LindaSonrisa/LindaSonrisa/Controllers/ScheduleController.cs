using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LindaSonrisa.Models.Schedule;
using LindaSonrisa.Models.Account;

namespace LindaSonrisa.Controllers
{
    public class ScheduleController : Controller
    {
        // GET: schedule
        public ActionResult Sched()
        {
            

            if (Session["Login"]=="NO LOGEADO")
            {
                return RedirectToAction("IniciarSesion", "Account", new { id = 1 });
            }
               

                Servicio ser = new Servicio();
                List<Servicio> serv = ser.BuscarServicio();

            return View(serv);
        }

        // GET: schedule/Reserve
        public ActionResult Reserve(string id,string day)
        {
            List<Horas> hora = new List<Horas>();
            ViewBag.id_servicio = id;

            DateTime thisDay = DateTime.Today;
            string dia_hoy = thisDay.ToString("d");
            //15-06-2019
            string yyyy = dia_hoy.Substring(6, 4);
            string mm= dia_hoy.Substring(3, 2);
            string dd= dia_hoy.Substring(0, 2);
            ViewBag.dia = yyyy +"-"+ mm +"-"+ dd;

            //2019-06-13
            if (Session["Login"] == "NO LOGEADO")
            {
                return RedirectToAction("IniciarSesion", "Account", new { id = 1 });
            }
            if (day!=null)
            {
                string year = day.Substring(0, 4);
                string mes = day.Substring(5, 2);
                string dia = day.Substring(8, 2);

                string eldia = dia + "/" + mes + "/" + year;

                Horas ho = new Horas();
                List<Horas> hor = ho.BuscarHorasDisponibles(eldia, id);
                ViewBag.buscar = "Mostrar";
                return View(hor);

            }

            return View(hora);
        }

        public ActionResult Register(string nom_prof="",string rut= "",string id_servicio = "" , string hora = "", string descripcion = "", string dia="")
        {
                if (Session["Login"] == "NO LOGEADO")
                {
                     return RedirectToAction("IniciarSesion", "Account", new { id = 1 });
                 }

                Apoderado apo = new Apoderado();
                string user = Session["Login"].ToString();
                Apoderado apod = apo.BuscarApoderado(user);
                if (apod != null)
                {
                    //=========Apoderado==========
                    ViewBag.email = apod.Email;
                    ViewBag.celular = apod.Celular;
                    ViewBag.s_apellido = apod.S_apellido;
                    ViewBag.p_apellido = apod.P_apellido;
                    ViewBag.nombre = apod.Nombre;
                    ViewBag.rutApoderado = apod.Rut;
                    //=========Apoderado==========

                    //=========Datos==============
                    ViewBag.nombreProf = nom_prof;
                    ViewBag.rutProf = rut;
                    ViewBag.id_servicio = id_servicio;
                    ViewBag.hora = hora;
                    ViewBag.descripcion = descripcion;
                    ViewBag.dia = dia;

                    return View();
                }

            return RedirectToAction("Reserve", "Schedule");
        }

        public ActionResult Save(string rut_apo = "", string nombre_paciente="", string rut_profesional = "", string id_servicio = "",
                                string dia = "", string hora = "",string descripcion="")
        {
            if (Session["Login"] == "NO LOGEADO")
            {
                return RedirectToAction("IniciarSesion", "Account", new { id = 1 });
            }

            if (rut_apo!="")
            {
                //"14-06-2019 0:00:00"
                string day = dia.Substring(0, 10);

                string dd = dia.Substring(0, 2);
                string mm = dia.Substring(3, 2);
                string yy = dia.Substring(8, 2);
                string hh = hora.Substring(0, 2);
                string min = hora.Substring(3, 2);

                string id_reserva = dd + mm + yy + hh + min;

                Reserve res = new Reserve();
                res = new Reserve(nombre_paciente, rut_profesional, id_servicio, rut_apo, Convert.ToDateTime(day), descripcion, Int64.Parse(id_reserva));
                if (res.Create())
                {
                    Horas ho = new Horas();
                    int truee = 1;
                    if(ho.modificarEstado(truee,day,hora))
                    {
                      
                        return RedirectToAction("Index", "Home", new { exito= "Exito"});
                    }
                    else
                    {
                        //realizar un delete de reserve
                    }
                }
                else
                {

                }
            }
            

            return View();
        }


      
    }
}