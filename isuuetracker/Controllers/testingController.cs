using isuuetracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace isuuetracker.Controllers
{
    public class testingController : Controller
    {
        // GET: testing
        public ActionResult bug()
        {
            ViewBag.msg = "BUG Report";
            Modelclass model = new Modelclass();
            try
            {
                datamodel data = new datamodel();
                int j = (int)Session["id"];
                var role= data.roles.Where(i => i.userid == j);
                string job = role.Select(i=>i.work).FirstOrDefault();
                if (job=="TESTER")
                {
                    var projnm = role.Select(i => i.projectid);
                    foreach (var n in projnm)
                    {
                        var p = data.projects.Where(i => i.projectid == n).FirstOrDefault();
                        model.projectlist.Add(new SelectListItem() { Text = p.projectname, Value = p.projectid.ToString() });
                    }
                    SelectListItem[] list = new SelectListItem[3];
                    list[0] = new SelectListItem() { Text = "Blocking", Value = "Blocking" };
                    list[1] = new SelectListItem() { Text = "High Priority", Value = "High" };
                    list[2] = new SelectListItem() { Text = "Low Priority", Value = "Low" };
                    ViewBag.list = list;
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
        public ActionResult bug(Modelclass model)
        {
            datamodel data = new datamodel();
            bugpool bugs = new bugpool();
            history hist = new history();
            int j= (int)Session["id"];
            bugs.testerid = j;
            bugs.projectid = int.Parse(model.projid);
            bugs.bugname = model.bugname;
            bugs.bugtype = model.bugtype;
            bugs.status = "Open";
            data.bugpools.Add(bugs);
            data.SaveChanges();
            hist.bugid = bugs.bugid;
            hist.ModifieduserId = bugs.testerid;
            hist.comment = model.comments;
            hist.status = bugs.status;
            hist.time = DateTime.Now;
            data.historys.Add(hist);
            data.SaveChanges();
            return Redirect("/testing/dashboard");
        }
        public ActionResult dashboard()
        {
            Modelclass model = new Modelclass();
            try
            {
                datamodel data = new datamodel();
                int j = (int)Session["id"];
                var role = data.roles.Where(i => i.userid == j);
                string job = role.Select(i => i.work).FirstOrDefault();
                List<Modelclass> modellist = new List<Modelclass>();
                if (job!=null)
                {

                     if (job == "TESTER")
                    {
                        var info = data.bugpools.Where(i => i.testerid == j).Select(i => i);
                        foreach (var val in info)
                        {
                            var proj = data.projects.Where(i => i.projectid == val.projectid).Select(i=> i.projectname).FirstOrDefault();
                            modellist.Add(new Modelclass() { status=val.status, bugname=val.bugname, projid=proj, bugtype=val.bugtype,bugid=val.bugid,job=job  });
                        }
                    }
                    else if (job == "DEV")
                    {

                        var info = data.bugpools.Where(i => i.assigntoId == j).Select(i => i);
                        foreach (var val in info)
                        {
                            var proj = data.projects.Where(i => i.projectid == val.projectid).Select(i => i.projectname).FirstOrDefault();
                            modellist.Add(new Modelclass() { status = val.status, bugname = val.bugname, projid = proj, bugtype = val.bugtype, bugid = val.bugid,job=job });
                        }
                    }
                    else if (job == "PM")
                    {

                        var projnm = role.Select(i => i.projectid);
                        
                        
                        foreach (var val in projnm)
                        {
                            var devs = role.Where(i => i.work == "DEV" && i.projectid == val).Select(i => i.userid);
                            foreach (var n in devs)
                            {
                                var p = data.logins.Where(i => i.loginId == n).FirstOrDefault();
                                model.dev.Add(new SelectListItem() { Text = p.username, Value = p.loginId.ToString() });
                            }
                            var info = data.bugpools.Where(i=> i.projectid==val).Select(i => i);
                            foreach(var bg in info)
                            {
                                var proj = data.projects.Where(i => i.projectid == bg.projectid).Select(i => i.projectname).FirstOrDefault();
                                modellist.Add(new Modelclass() { status = bg.status, bugname = bg.bugname, projid =proj , bugtype = bg.bugtype, bugid = bg.bugid, job=job });
                            }
                            
                        }
                           
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
        public ActionResult open(int bid)
        {
            Modelclass model = new Modelclass();
            try
            {
                datamodel data = new datamodel();
                int j = (int)Session["id"];
                var role = data.roles.Where(i => i.userid == j);
                string job = role.Select(i => i.work).FirstOrDefault();
                if (job == "TESTER")
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
            return View(model);
        }
    
        [HttpPost]
        public ActionResult open(int bid,Modelclass model)
        {
            datamodel data = new datamodel();
            bugpool bugs = data.bugpools.Where(i => i.bugid == bid).Select(i => i).FirstOrDefault();
            history hist = new history();
            bugs.status = "Open";

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
        public ActionResult close(int bid)
        {
            Modelclass model = new Modelclass();
            try
            {
                datamodel data = new datamodel();
                int j = (int)Session["id"];
                var role = data.roles.Where(i => i.userid == j);
                string job = role.Select(i => i.work).FirstOrDefault();
                if (job == "TESTER")
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
            return View(model);
        }

        [HttpPost]
        public ActionResult close(int bid, Modelclass model)
        {
            datamodel data = new datamodel();
            bugpool bugs = data.bugpools.Where(i => i.bugid == bid).Select(i => i).FirstOrDefault();
            history hist = new history();
            bugs.status = "Closed";

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