using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nettbutikken.Models;

namespace Nettbutikken.Controllers
{
    public class ButikkController : Controller
    {
        // GET: Butikk
        Dbkontekst lagreDb = new Dbkontekst();
        public ActionResult Index()
        {
            var kat = lagreDb.Kategorier.ToList();

            return View(kat);
        }
        public ActionResult Browse(string kategori)
        {
            //varer basert på kategori
            var kat = lagreDb.Kategorier.Include("Vare").Single(g => g.Navn == kategori);

            return View(kat);
        }
        public ActionResult Details(int id)
        {
            var vare = lagreDb.Varer.Find(id);
            return View(vare);
        }

        [ChildActionOnly]
        public ActionResult GenreMenu()//Kategori meny
        {
            var kategori = lagreDb.Kategorier.ToList();

            return PartialView(kategori);
        }

    }
}
