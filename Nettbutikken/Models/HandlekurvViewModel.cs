using System.Collections.Generic;
using Nettbutikken.Models;

namespace Nettbutikken.Models
{
    public class HandlekurvViewModel
    {
        public List<Vogn> VognVarer { get; set; }
        public decimal TotalSum { get; set; }
    }
}