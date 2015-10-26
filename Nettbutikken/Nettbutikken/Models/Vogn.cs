using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Nettbutikken.Models
{
    public class Vogn
    {
        [Key]
        public int HandleId { get; set; }
        public string VognId { get; set; }
        public int VareId { get; set; }
        public int Count { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual Vare Vare { get; set; }
    }
}