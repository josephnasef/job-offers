using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace job_offers.Models
{
    public class ApplyForJob
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime ApplyDate { get; set; }
        //public DateTime lastedite { get; set; }

        public int JobId { get; set; }
        public string UserId { get; set; }
        public virtual job job { get; set; }
        public virtual ApplicationUser user { get; set; }

    }
}