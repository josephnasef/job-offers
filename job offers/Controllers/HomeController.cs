using job_offers.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var list = db.JobsCategories.ToList();
            return View(list);
        }
        public ActionResult Details (int jobid)
        {
            var job = db.jobs.Find(jobid);
            if (job==null)
            {
                return HttpNotFound();
            }
            Session["JobId"] = jobid;
            return View(job);
        }

        [Authorize]
        public ActionResult apply()
        {
            return View();
        }

        [HttpPost]
        public ActionResult apply(string message)
        {
            var userId = User.Identity.GetUserId();
            var jobId = (int)Session["JobId"];

            var check = db.ApplyForJobs.Where(a => a.JobId == jobId && a.UserId == userId).ToList(); 
            if (check.Count<1)
            {
                var job = new ApplyForJob();
                job.JobId = jobId;
                job.UserId = userId;
                job.Message = message;
                job.ApplyDate = DateTime.Now;

                db.ApplyForJobs.Add(job);
                db.SaveChanges();
                ViewBag.result = "thanks to your apply";
            }
            else
            {
                ViewBag.result = "thanks to your apply befor";

            }
            return View();
        }

        [Authorize]
        public ActionResult GetJobsByUser()
        {
            var userId = User.Identity.GetUserId();
            var jobs = db.ApplyForJobs.Where(a => a.UserId == userId);
            return View(jobs.ToList());
        }

        [Authorize]
        public ActionResult DetailsOfJob( int id)
        {
            var job = db.ApplyForJobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }
        public ActionResult editeOfMyJobs(int id )
        {
            var job = db.ApplyForJobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }
        // GET: ApplyForJob/Edit/5
        public ActionResult EdediteOfMyJobsit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplyForJob ApplyForJob = db.ApplyForJobs.Find(id);
            if (ApplyForJob == null)
            {
                return HttpNotFound();
            }
            return View(ApplyForJob);
        }

        // POST: ApplyForJob/EdediteOfMyJobsit/5
        [HttpPost]
        public ActionResult EdediteOfMyJobsit(ApplyForJob ApplyForJob)
        {
            if (ModelState.IsValid)
            {
                ApplyForJob.ApplyDate = DateTime.Now;
                db.Entry(ApplyForJob).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("GetJobsByUser");
            }
            return View(ApplyForJob);
        }
        // GET: Roles/Deletemyapply/5
        public ActionResult Deletemyapply(int? id)
        {
            var ApplyForJobs = db.ApplyForJobs.Find(id);
            if (ApplyForJobs == null)
            {
                return HttpNotFound();
            }
            return View(ApplyForJobs);

        }

        // POST: Roles/Deletemyapply/5
        [HttpPost]
        public ActionResult Deletemyapply(ApplyForJob ApplyForJob)
        {
            if (ModelState.IsValid)
            {
                var deleteApplyForJob = db.ApplyForJobs.Find(ApplyForJob.Id);
                db.ApplyForJobs.Remove(deleteApplyForJob);

                db.SaveChanges();
                return RedirectToAction("GetJobsByUser");
            }
            return View(ApplyForJob);

        }
        [Authorize]
        public ActionResult GetJobsByPublisher()
        {
            var userId = User.Identity.GetUserId();
            var jobs = from app in db.ApplyForJobs
                       join job in db.jobs
                       on app.JobId equals job.ID
                       where job.User.Id == userId
                       select app;
            var groubed = from j in jobs
                          group j by j.job.Title
                          into gr
                          select new JobsViewModel
                          {
                              Jobtitle = gr.Key,
                              Items = gr
                          };
            return View(groubed.ToList());
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Contact(contactmodel contact)
        {
            var mail = new MailMessage();
            var loginInfo = new NetworkCredential("gosephnasef@gmail.com", "01287811957godloveforever");
            mail.From = new MailAddress(contact.Emil);
            mail.To.Add(new MailAddress("gosephnasef@gmail.com"));
            mail.Subject = contact.Subject;
            mail.IsBodyHtml = true;
            string body = "userName :  " + contact.Name + "<br>" +
                          "Email : " + contact.Emil + "<br>" +
                          " subject : " + contact.Subject + "<br>" +
                          " Message :<br> <b> " + contact.Message+"</b>" ;
            mail.Body = body;
            var smtpclient = new SmtpClient("smtp.gmail.com",587);
            smtpclient.EnableSsl = true;
            smtpclient.Credentials = loginInfo;
            smtpclient.Send(mail);


            return RedirectToAction("index");
        }
        public ActionResult search()
        {
            return View();
        }
        [HttpPost]
        public ActionResult search(string searchname)
        {
            var result = db.jobs.Where(a => a.Title.Contains(searchname)
            || a.JobContent.Contains(searchname)
            ||a.category.CategoryName.Contains(searchname)
            ||a.category.CategoryDescription.Contains(searchname)).ToList();
            return View(result);
        }
    }
}