using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nettbutikken.Models
{
    public class OrdreDetaljer
    {
        public int OrdreDetaljerId { get; set; }
        public int OrdreId { get; set; }
        public int VareId { get; set; }
        public int Antall { get; set; }
        public decimal Enhetspris { get; set; }

        public virtual Vare Vare { get; set; }
        public virtual Ordre Ordre { get; set; }
    }
}