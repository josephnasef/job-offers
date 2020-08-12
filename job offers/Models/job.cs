using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace job_offers.Models
{
    public class job
    {
        public int ID { get; set; }

        [DisplayName("job Title")]
        public string Title { get; set; }

        [DisplayName("job description")]
        [AllowHtml]
        public string JobContent { get; set; }

        [DisplayName("job images")]
        public string JobImage { get; set; }

        [DisplayName("job type")]
        public int CategoryID { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public virtual JobsCategory category { get; set; }
    }
}