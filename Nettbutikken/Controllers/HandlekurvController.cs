using System.Linq;
using System.Web.Mvc;
using Nettbutikken.Models;

namespace Nettbutikken.Controllers
{
    public class HandlekurvController : Controller
    {
        Dbkontekst db = new Dbkontekst();

        public ActionResult Index()
        {
            var vogn = Handlekurv.GetCart(this.HttpContext);
            var viewModel = new HandlekurvViewModel
            {
                VognVarer = vogn.GetValgtVare(),
                TotalSum = vogn.GetTotal()
            };
            return View(viewModel);
        }

        public ActionResult AddToCart(int id)
        {

            // henter vare fra db
            var vareLagtTil = db.Varer
                .Single(v => v.VareId == id);

            // legg i handlevogn
            var cart = Handlekurv.GetCart(this.HttpContext);

            cart.AddToCart(vareLagtTil);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
        
            var vogn = Handlekurv.GetCart(this.HttpContext);

            string vareNavn = db.Vogner
                .Single(v => v.HandleId == id).Vare.Varenavn;

            int vareCount = vogn.RemoveFromCart(id);
            
            var results = new HandlekurvRemoveViewModel
            {
                Melding = Server.HtmlEncode(vareNavn) +
                    " har blitt fjernet.",
                TotalSum = vogn.GetTotal(),
                AntallVogner = vogn.GetCount(),
                AntallVarer = vareCount,
                SlettId = id
            };

            return Json(results);
        }

        
        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            var vogn = Handlekurv.GetCart(this.HttpContext);

            ViewData["AntallVogner"] = vogn.GetCount();

            return PartialView("CartSummary");
        }
    }
}