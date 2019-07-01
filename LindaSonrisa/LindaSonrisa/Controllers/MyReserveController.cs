using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LindaSonrisa.Models.Account;
using LindaSonrisa.Models.Schedule;
using LindaSonrisa.Models.MyReserve;

namespace LindaSonrisa.Controllers
{
    public class MyReserveController : Controller
    {
        // GET: MyReserve
        public ActionResult Search()
        {
            if (Session["Login"] == "NO LOGEADO")
            {
                return RedirectToAction("IniciarSesion", "Account", new { id = 2 });
            }

            string nombre = Session["Login"].ToString();

            Apoderado apo = new Apoderado();
            Apoderado apod = apo.BuscarApoderado(nombre);

            if (apod!=null)
            {
                ViewBag.RutApoderado = apod.Rut;
                ViewBag.Listar = "Listar";

                Reserve re = new Reserve();
                List<Reserve> res = re.ListarReserva(ViewBag.RutApoderado);

                DateTime thisDay = DateTime.Today;
                string dia_hoy = thisDay.ToString("d");
                //15-06-2019
                string yyyy = dia_hoy.Substring(6, 4);
                string mm = dia_hoy.Substring(3, 2);
                string dd = dia_hoy.Substring(0, 2);
                ViewBag.dia_hoy = yyyy + "-" + mm + "-" + dd;
                @ViewBag.No_Encontrado = "Encontrado";

                return View(res);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public ActionResult Cancel(string id_reserva, string dia ,string hora)
        {
            if (Session["Login"] == "NO LOGEADO")
            {
                return RedirectToAction("IniciarSesion", "Account", new { id = 2 });
            }

            Reserve re = new Reserve();

            if (re.EliminarReserva(id_reserva))
            {
                Horas ho = new Horas();
                int truee = 0;
                if (ho.modificarEstado(truee, dia, hora))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    //realizar un delete de reserve
                }
            }
            else
            {

            }

            return RedirectToAction("Search", "MyReserve");
        }

        public ActionResult Ticket(string id_reserva = "", string rut_apoderado = "", string dia = "",
                            string hora = "", string servicio = "", string NomProfesional = "")
        {
            DateTime thisDay = DateTime.Today;
            string dia_hoy = thisDay.ToString("d");
            //15-06-2019
            string yyyy = dia_hoy.Substring(6, 4);
            string mm = dia_hoy.Substring(3, 2);
            string dd = dia_hoy.Substring(0, 2);
            string fechaRegistro = dd + "/" + mm + "/" + yyyy;

            Ticket ti = new Ticket();
            int id_reservas = Int32.Parse(id_reserva);

            ti = new Ticket(fechaRegistro, id_reservas, rut_apoderado);
            if (ti.Create())
            {
                string nombre = Session["Login"].ToString();
                string asunto = "Linda Sonrisa: DETALLE SERVICIO N°: "+id_reserva;
                string CC = "lindasonrisaodontologia@gmail.com";
                string mensaje = "Estimado, el dia " + dia + " a las " + hora + " se realizó la atención para " + servicio + "\n" +
                    " en el cual lo realizó el profesional a cargo " + NomProfesional + " \n" +
                    "\nLinda Sonrisa \n" +
                    "Servicios de Odontología.";

                Apoderado apo = new Apoderado();
                Apoderado apod = apo.BuscarApoderado(nombre);

                System.Net.Mail.MailMessage mmsg = new System.Net.Mail.MailMessage();
                mmsg.To.Add(apod.Email);
                mmsg.Subject = asunto;
                mmsg.SubjectEncoding = System.Text.Encoding.UTF8;
                mmsg.Bcc.Add(CC);

                mmsg.Body = mensaje;
                mmsg.BodyEncoding = System.Text.Encoding.UTF8;
                mmsg.IsBodyHtml = true;
                mmsg.From = new System.Net.Mail.MailAddress("ux.mor.dez@gmail.com");

                System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();

                cliente.Credentials = new System.Net.NetworkCredential("lindasonrisaodontologia@gmail.com", "86763516jano");

                cliente.Port = 587;
                cliente.EnableSsl = true;
                cliente.Host = "smtp.gmail.com";

                try
                {
                    cliente.Send(mmsg);
                }
                catch
                {

                    Console.WriteLine("eerraa");
                }
            }
            else
            {

            }


            return View();
        }



    }
}