using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace job_offers.Models
{
    public class contactmodel
    {
        [Required]
        public string Name { get; set; }        
        [Required]
        public string Emil { get; set; }        
        public string Subject { get; set; }
        [Required]
        public string Message { get; set; }
    }
}