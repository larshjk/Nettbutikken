using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nettbutikken.Models
{
    public class Kategori
    {
        public int KategoriId { get; set; }
        public string Navn { get; set; }
        public List<Vare> Vare { get; set; }
    }
}