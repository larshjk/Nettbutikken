using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Nettbutikken.Models
{  
    public class Konto
    {
        public class LogInn
        {
            [Required(ErrorMessage = "Du må skrive brukernavn")]
            [Display(Name = "Brukernavn")]
            public string Brukernavn { get; set; }

            [Required(ErrorMessage = "Du må skrive inn passord!")]
            [DataType(DataType.Password)]
            [Display(Name = "Passord")]
            public string Passord { get; set; }

        }

        public class Registrer
        {
            public int id { get; set; }
            [Required]
            [Display(Name = "Brukernavn")]
            public string Brukernavn { get; set; }

            [Required]
            [DataType(DataType.EmailAddress)]
            [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",
            ErrorMessage = "Email er ikke gyldig")]
            [Display(Name = "Email address")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "Passordet må være minst seks tegn", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Passord")]
            public string Passord { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Bekreft Passord")]
            [Compare("Passord", ErrorMessage = "Passordene er ikke like.")]
            public string BekreftPassord { get; set; }
        }
        public class dbBruker
        {
            
            public int Id { get; set; }
            public string Brukernavn { get; set; }
            public string Email { get; set; }
            public byte[] Passord { get; set; }
        }
    }
}