using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LindaSonrisa.Models.Account;

namespace LindaSonrisa.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            ViewBag.id = "hola";
            return View();
        }
        // 
        // GET: /Account/CrearCuenta/ 
        public ActionResult CrearCuenta(string Email1="", string Password="", string Password2="", string rut="", string dv_rut="", 
                                        string Name="",string p_apellido="", string s_apellido="", int Celular=0, string Direccion="",int n_direccion=0,string Ciudad="",string Comuna="")
        {

            if (Email1!="")
            {
                Apoderado apo = new Apoderado();


                apo = new Apoderado( rut,  dv_rut,  Password,  Name,  p_apellido,  s_apellido,  Celular,  Direccion,  n_direccion,  Comuna,  Ciudad, Email1);

                if(apo.Create())
                {
                    ViewBag.Existo = "Cuenta Creada con Exito";
                }
                else
                {

                }
            }
            return View();
        }

        // GET: /Account/IniciarSesionR/
        public ActionResult IniciarSesion(string Email="", string password="",string id="0")
        {
            
            if (Email != "")
            {
                
                Apoderado apo = new Apoderado();
                Apoderado apod = apo.inicioSesion(Email, password);

                if (apod!=null && id=="0")
                {
                    Session["Login"] = apod.Nombre+apod.Rut;
                                       
                    return RedirectToAction("Index", "Home");

                }
                else if (apod != null && id == "1")
                {
                    Session["Login"] = apod.Nombre + apod.Rut;

                    return RedirectToAction("Sched", "Schedule");

                }
                else if (apod != null && id == "2")
                {
                    Session["Login"] = apod.Nombre + apod.Rut;

                    return RedirectToAction("Search", "MyReserve");

                }
                else
                {
                    ViewBag.ISFallo = "Falló";
                }

            }
            
            ViewBag.ID = id;
            return View();
        }
        public ActionResult CerrarrSesion()
        {
            Session["Login"] = "NO LOGEADO";

            return RedirectToAction("Index", "Home");
        }

    }
}