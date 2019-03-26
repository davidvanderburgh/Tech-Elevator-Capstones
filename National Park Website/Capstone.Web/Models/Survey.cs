using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.Models
{
    public class Survey
    {
        [Display(Name ="What is your favorite National Park?")]
        public string FavoriteNationalPark { get; set; }

        [Required(ErrorMessage = "Please enter an email address")]
        [EmailAddress(ErrorMessage = "This is not a valid email address")]
        [Display(Name ="Your email")]
        public string EmailAddress { get; set; }

        [Display(Name = "State of residence")]
        public string StateOfResidence { get; set; }

        [Display(Name = "Activity level")]
        public string ActivityLevel { get; set; }        
    }
}
