using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Nettbutikken.Models
{
    public class Vare
    {
        [ScaffoldColumn(false)]
        public int VareId { get; set; }

        [Display(Name="KategorId")]
        public int KategoriId { get; set; }

        [Required(ErrorMessage = "Varenavn må oppgis")]
        [StringLength(160)]
        public string Varenavn { get; set; }

        [Required(ErrorMessage = "Pris må oppgis")]
        public decimal Pris { get; set; }

        [Display(Name="Url Bilde")]
        [StringLength(1024)]
        public string Varebilde { get; set; }

        public virtual Kategori Kategori { get; set; }
        public virtual List<OrdreDetaljer> OrderDetails { get; set; }
    }
}