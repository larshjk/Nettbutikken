using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nettbutikken.Models;

namespace Nettbutikken.Controllers
{
    public class HjemController : Controller
    {
        // GET: Hjem
        Dbkontekst db = new Dbkontekst(); 
        public ActionResult Index()
        {
            var varer = ListVarer();
            return View(varer);
        }
        private List<Vare> ListVarer()
        {
            //Liste med varer
            return db.Varer.ToList();
        }
    }
}