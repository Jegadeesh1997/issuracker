using isuuetracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace isuuetracker.Controllers
{
    public class devController : Controller
    {
        // GET: dev
        public ActionResult resolve()
        {
            try
            {
                datamodel data = new datamodel();
                int j = (int)Session["id"];
                string job = data.roles.Where(i => i.userid == j).Select(i => i.work).FirstOrDefault();
                if (job == "DEV")
                {

                }
                else
                {
                    return RedirectToAction("login", "login");
                }
            }
            catch
            {
                return RedirectToAction("login", "login");
            }
            return View();
        }
        public ActionResult devresult(int bid)
        {
            try
            {
                datamodel data = new datamodel();
                int j = (int)Session["id"];
                string job = data.roles.Where(i => i.userid == j).Select(i => i.work).FirstOrDefault();
                if (job == "DEV")
                {
                    ViewBag.bid = bid;
                }
                else
                {
                    return RedirectToAction("login", "login");
                }
            }
            catch
            {
                return RedirectToAction("login", "login");
            }
            return View();
        }
        [HttpPost]
        public ActionResult devresult(int bid,Modelclass model)
        {
            datamodel data = new datamodel();
            bugpool bugs = data.bugpools.Where(i => i.bugid == bid).Select(i => i).FirstOrDefault();
            history hist = new history();
            bugs.status = "Resolved";

            data.SaveChanges();
            hist.bugid = bugs.bugid;
            hist.ModifieduserId = (int)Session["id"];
            hist.comment = model.comments;
            hist.status = bugs.status;
            hist.time = DateTime.Now;
            data.historys.Add(hist);
            data.SaveChanges();
            return Redirect("/testing/dashboard");
           
        }
    }
}