using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nettbutikken.Models;
using System.Web.Security;

namespace Nettbutikken.Controllers
{
    public class KontoController : Controller
    {
        private void MigrateShoppingCart(string Brukernavn)
        {
            // Kobler handlevogn til bruker
            var cart = Handlekurv.GetCart(this.HttpContext);

            cart.MigrateCart(Brukernavn);
            Session[Handlekurv.CartSessionKey] = Brukernavn;
        }
        public ActionResult Logginn()
        {
            if (Session["LoggetInn"] == null)
            {
                Session["LoggetInn"] = false;
                ViewBag.Innlogget = false;
            }
            else
            {
                ViewBag.Innlogget = (bool)Session["LoggetInn"];
            }
            return View();
        }

        [HttpPost]
        public ActionResult Logginn(Konto.LogInn innBruker)
        {
            if (BrukerIdb(innBruker))
            {
                Session["LoggetInn"] = true;
                Session["Brukernavn"] = innBruker.Brukernavn; // legger brukernavnet i session
                ViewBag.Innlogget = true;
                return View();
            }
            else
            {
                Session["LoggetInn"] = false;
                ViewBag.Innlogget = false;
                return View();
            }
        }

        public ActionResult MinProfil()
        {
            if (Session["LoggetInn"] != null)
            {
                bool loggetInn = (bool)Session["LoggetInn"];
                if (loggetInn)
                {var db = new Dbkontekst();
                 var brukernavn = Session["Brukernavn"];
                 string b = Convert.ToString(brukernavn);
                    //Skriver ut alle bestillinger for brukeren som er innlogget. Bruker brukernavnet fra session ved innlogging
                 var bestillinger = (from odetaljer in db.OrdreDetaljers
                                from ord in db.Ordrer
                                from v in db.Varer
                                where odetaljer.OrdreId == ord.OrdreId && odetaljer.VareId == v.VareId && odetaljer.Ordre.Brukernavn == b
                                select new Kvitto
                                {
                                    Brukernavn = b,
                                    OrdreId = odetaljer.Ordre.OrdreId,
                                    Antall = odetaljer.Antall,
                                    OrdreDato = odetaljer.Ordre.OrdreDato,
                                    Varenavn = odetaljer.Vare.Varenavn,
                                    Total = odetaljer.Ordre.Total

                                }).ToList();
                    return View(bestillinger);
                }
            }
            return RedirectToAction("Logginn", "Konto");
          
        }
        [ChildActionOnly]
        public ActionResult brukerPå() // skal fikses senere, men skal vise i navbar hvilken bruker som er på og ikke innlogget dersom ingen er det.
        {
            var brukernavn = Session["Brukernavn"];
            string b = Convert.ToString(brukernavn);

            ViewData["brukerpå"] = b;

            return PartialView("brukerPå");
        }
        public ActionResult Registrer()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        public ActionResult Registrer(Konto.Registrer innBruker)
        {
            //Registrer bruker
            if (!ModelState.IsValid)
            {
                return View();
            }
            using (var db = new Dbkontekst())
            {
                try
                {
                    var nyBruker = new Konto.dbBruker();
                    byte[] passordDb = lagHash(innBruker.Passord);
                    nyBruker.Passord = passordDb;
                    nyBruker.Brukernavn = innBruker.Brukernavn;
                    nyBruker.Email = innBruker.Email;
                    db.Bruker.Add(nyBruker);
                    db.SaveChanges();
                    return RedirectToAction("Logginn");
                }
                catch (Exception feil)
                {
                    return View();
                }
            }
        }

        private static byte[] lagHash(string innPassord)
        {
            //Hash passord
            byte[] innData, utData;
            var algoritme = System.Security.Cryptography.SHA256.Create();
            if (innPassord != null)
            { innData = System.Text.Encoding.ASCII.GetBytes(innPassord);
            utData = algoritme.ComputeHash(innData);
            return utData;

            }
            return null;
        }
        private static bool BrukerIdb(Konto.LogInn innBruker)
        {   //Sjekker om bruker er i db
            using (var db = new Dbkontekst())
            {
                byte[] passordDb = lagHash(innBruker.Passord);
                Konto.dbBruker funnetBruker = db.Bruker.FirstOrDefault
                    (b => b.Passord == passordDb && b.Brukernavn == innBruker.Brukernavn);
                if (funnetBruker == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

    }
}