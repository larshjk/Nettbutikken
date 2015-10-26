using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Nettbutikken.Models
{
    public class Ordre
    {
        [ScaffoldColumn(false)]
        public int OrdreId { get; set; }

        [ScaffoldColumn(false)]
        public System.DateTime OrdreDato { get; set; }

        [ScaffoldColumn(false)]
        public string Brukernavn { get; set; }

        [Required(ErrorMessage = "Fornavn må oppgis")]
        [Display(Name="First Name")]
        [StringLength(160)]
        public string Fornavn { get; set; }

        [Required(ErrorMessage = "Etternavn må oppgis")]
        [Display(Name="Last Name")]
        [StringLength(160)]
        public string Etternavn { get; set; }

        [Required(ErrorMessage = "Adresse må oppgis")]
        [StringLength(70)]
        public string Adresse { get; set; }

        [Required(ErrorMessage = "By må oppgis")]
        [StringLength(40)]
        public string By { get; set; }

        [Required(ErrorMessage = "Postnr må oppgis")]
        [Display(Name="Postal Code")]
        [StringLength(4)]
        public string Postnr { get; set; }

        [Required(ErrorMessage = "Telefonnr")]
        [StringLength(8)]
        public string Telefon { get; set; }

        [Required(ErrorMessage = "Du må oppgi email")]
        [Display(Name="Email Address")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",
            ErrorMessage = "Email er ikke gyldig.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [ScaffoldColumn(false)]
        public decimal Total { get; set; }

        public List<OrdreDetaljer> OrdreDetaljer { get; set; }
    }
}