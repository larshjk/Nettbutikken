using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nettbutikken.Models;

namespace Nettbutikken.Controllers
{
    public class BestillController : Controller
    {
         Dbkontekst db = new Dbkontekst();
  
        public ActionResult Betaling() // Om bruker ikke er pålogget, send til Loggin
        {
            if (Session["LoggetInn"] != null)
            {
                bool loggetInn = (bool)Session["LoggetInn"];
                if (loggetInn)
                {
                    return View();
                }
            }
            return RedirectToAction("Logginn","Konto");
            
        }

        [HttpPost]
        public ActionResult Betaling(Ordre ordre)
        {
            try
            {
                    var cart = Handlekurv.GetCart(this.HttpContext);
                    var bruker = Session["Brukernavn"];
                    var brukernavn = Convert.ToString(bruker);
                    ordre.Brukernavn = brukernavn;
                    ordre.OrdreDato = DateTime.Now;
                    ordre.Total = cart.GetTotal();
                 
                    db.Ordrer.Add(ordre);
                    db.SaveChanges();

                   
                    
                    cart.CreateOrder(ordre);
 
                    return RedirectToAction("Ferdig",
                        new { id = ordre.OrdreId });

            }
            catch
            {
              
                return View(ordre);
            }
        }

        public ActionResult Ferdig(int id)
        {
            var bruker = Session["Brukernavn"];
            var brukernavn = Convert.ToString(bruker);
            //Sjekk om bruker er rett
            bool isValid = db.Ordrer.Any(
                o => o.OrdreId == id &&
                o.Brukernavn == brukernavn );

            if (isValid)
            {
                return View(id);
            }
            else
            {
                return View("Error");
            }
        }

    }
}