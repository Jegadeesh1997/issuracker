using isuuetracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace isuuetracker.Controllers
{
    public class pmController : Controller
    {
        // GET: pm
        public ActionResult assign()
        {
            Modelclass model = new Modelclass();
            try
            {
                datamodel data = new datamodel();
                int j = (int)Session["id"];
                string job = data.roles.Where(i => i.userid == j).Select(i => i.work).FirstOrDefault();
                if (job == "PM")
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
       
        public ActionResult edit(string pid , int bid)
        {
            Modelclass model = new Modelclass();
            try
            {
                datamodel data = new datamodel();
                int j = (int)Session["id"];
                ViewBag.bid = bid;
                string job = data.roles.Where(i => i.userid == j).Select(i => i.work).FirstOrDefault();
                if (job == "PM")
                {
                    int projm = data.projects.Where(i => i.projectname == pid).Select(i => i.projectid).FirstOrDefault();
                    var devs = data.roles.Where(i => i.work == "DEV" && i.projectid == projm).Select(i => i.userid);
                    foreach (var n in devs)
                    {
                        var p = data.logins.Where(i => i.loginId == n).FirstOrDefault();
                        model.dev.Add(new SelectListItem() { Text = p.username, Value = p.loginId.ToString() });
                    }


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
            return View(model);

        }
        [HttpPost]
        public ActionResult edit(Modelclass model, int bid)
        {
            datamodel data = new datamodel();
            bugpool bugs = data.bugpools.Where(i => i.bugid == bid).Select(i => i).FirstOrDefault();
            history hist = new history();
            bugs.assigntoId = model.devid;
            bugs.status = "Assigned";
            
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
        public ActionResult status(int bid)
        {
            Modelclass model = new Modelclass();
            try
            {
                datamodel data = new datamodel();
                int j = (int)Session["id"];
                string job = data.roles.Where(i => i.userid == j).Select(i => i.work).FirstOrDefault();
                if (job !=null)
                {
                    List<Modelclass> modellist = new List<Modelclass>();
                    var hist = data.historys.Where(i => i.bugid == bid);
                    foreach (var val in hist)
                    {

                       string user = data.logins.Where(i => i.loginId == val.ModifieduserId).Select(i => i.username).FirstOrDefault();
                        string role = data.roles.Where(i => i.userid == val.ModifieduserId).Select(i => i.work).FirstOrDefault();
                        modellist.Add(new Modelclass() { username=user , status=val.status, comments=val.comment,time=val.time,job=role });
                    }
                    return View(modellist);
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
            
        }
    }
}