using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nettbutikken.Models
{
    public class Handlekurv
    {
        Dbkontekst lagreDb = new Dbkontekst();

        string HandlekurvId { get; set; }

        public const string CartSessionKey = "VognId";

        public static Handlekurv GetCart(HttpContextBase context)
        {
            var cart = new Handlekurv();
            cart.HandlekurvId = cart.GetCartId(context);
            return cart;
        }

        public static Handlekurv GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);
        }

        public void AddToCart(Vare vare)
        {
           
            var valgtVare = lagreDb.Vogner.SingleOrDefault(
            c => c.VognId == HandlekurvId
             && c.VareId == vare.VareId);

            if (valgtVare == null)
            {
                //Oprett en ny vogn om det ikke finnes
                valgtVare = new Vogn
                {
                    VareId = vare.VareId,
                    VognId = HandlekurvId,
                    Count = 1,
                    DateCreated = DateTime.Now
                };

                lagreDb.Vogner.Add(valgtVare);
            }
            else
            {
                // Øk antall dersom varen finnes
                valgtVare.Count++;
            }
            lagreDb.SaveChanges();
        }

        public int RemoveFromCart(int id)
        {
     
            var valgtVare = lagreDb.Vogner.Single(
             cart => cart.VognId == HandlekurvId
             && cart.HandleId == id); 

            int itemCount = 0;

            if (valgtVare != null)
            {
                if (valgtVare.Count > 1)
                {
                    valgtVare.Count--;
                    itemCount = valgtVare.Count;
                }
                else
                {
                    lagreDb.Vogner.Remove(valgtVare);
                }
                lagreDb.SaveChanges();
            }

            return itemCount;
        }

        public void EmptyCart()
        {
            var valgtVare = lagreDb.Vogner.Where(vogn => vogn.VognId == HandlekurvId);

            foreach (var valgt in valgtVare)
            {
                lagreDb.Vogner.Remove(valgt);
            }
            lagreDb.SaveChanges();
        }

        public List<Vogn> GetValgtVare()
        {
            return lagreDb.Vogner.Where(cart => cart.VognId == HandlekurvId).ToList();
        }

        public int GetCount()
        {
            int? count = (from valgtVare in lagreDb.Vogner
                          where valgtVare.VognId == HandlekurvId
                          select (int?)valgtVare.Count).Sum();
            return count ?? 0;
        }

        public decimal GetTotal()
        {
            // Totalsummen som blir vist og lagt til i bestillingen
            decimal? total = (from valgtVare in lagreDb.Vogner
                              where valgtVare.VognId == HandlekurvId
                              select (int?)valgtVare.Count * valgtVare.Vare.Pris).Sum();
            return total ?? decimal.Zero;
        }

        public int CreateOrder(Ordre ordre)
        {
            decimal total = 0;

            var valgtVare = GetValgtVare();

            // Legg bestillinger i ordredetaljer 
            foreach (var vare in valgtVare)
            {
                var ordreDetaljer = new OrdreDetaljer
                {
                    VareId = vare.VareId,
                    OrdreId = ordre.OrdreId,
                    Enhetspris = vare.Vare.Pris,
                    Antall = vare.Count
                };
                total += (vare.Count * vare.Vare.Pris);

                lagreDb.OrdreDetaljers.Add(ordreDetaljer);
            }
            ordre.Total = total;
 
            lagreDb.SaveChanges();

            // Tøm handlevogen
            EmptyCart();

            // Ordrenummer til kunde
            return ordre.OrdreId;
        }

        public string GetCartId(HttpContextBase context)
        {
            if (context.Session[CartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[CartSessionKey] = context.User.Identity.Name;
                }
                else
                {
                    Guid tempCartId = Guid.NewGuid();
                    
                    context.Session[CartSessionKey] = tempCartId.ToString();
                }
            }

            return context.Session[CartSessionKey].ToString();
        }

        // Knytter handlekurven til bruker
        public void MigrateCart(string userName)
        {
            var shoppingCart = lagreDb.Vogner.Where(c => c.VognId == HandlekurvId);

            foreach (Vogn item in shoppingCart)
            {
                item.VognId = userName;
            }
            lagreDb.SaveChanges();
        }
    }
}