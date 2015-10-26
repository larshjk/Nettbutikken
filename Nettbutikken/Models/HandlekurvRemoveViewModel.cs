using System.Collections.Generic;
using Nettbutikken.Models;

namespace Nettbutikken.Models
{
    public class HandlekurvRemoveViewModel
    {
        public string Melding { get; set; }
        public decimal TotalSum { get; set; }
        public int AntallVogner { get; set; }
        public int AntallVarer { get; set; }
        public int SlettId { get; set; }
    }
}