using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LindaSonrisa.Models;

namespace LindaSonrisa.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string to="",string asunto="",string mensaje="", string CC="",string exito="")
        {
            if (to!="")
            {
                


            }
            ViewBag.Exito = exito;
            return View();
        }


        
        public ActionResult About(string Nombre="",string Apellido="")
        {

            ViewBag.Message = "Your application description page.";

           
                

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}