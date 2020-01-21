using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.ComponentModel;

namespace niccrud.Models
{
    public class customer
    {
        [Display(Name = "customerID")]
        public int customerID { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "country is required.")]
        public int country { get; set; }

        [Required(ErrorMessage = "sex is required.")]
        public string sex { get; set; }

        [DisplayName("Upload File")]
        public string photo { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }

      
       

        [Required(ErrorMessage = "Field can't be empty")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string EmailID { get; set; }
        public List<country> countryList { get; set; }

    }
    public class country
    {
        public int CountryID { get; set; }

      
        public string Country { get; set; }
      //  public SelectList MobileList { get; set; }
        public List <country> MobileList { get; set; }
    }
}