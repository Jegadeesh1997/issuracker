using isuuetracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace isuuetracker.Controllers
{
    public class loginController : Controller
    {
        // GET: login
        public ActionResult login()
        {
          
            return View();
        }
        [HttpPost]
        public ActionResult login(Modelclass model)
        {
            datamodel data = new datamodel();
            try
            {
                if(ModelState.IsValid)
                {
                    int id = data.logins.Where(i => i.username == model.username.ToLower() && i.password == model.password).Select(i => i.loginId).FirstOrDefault();
                    if (id != 0)
                    {
                        Session["id"] = id;
                        string work = data.roles.Where(i => i.userid == id).Select(i => i.work).FirstOrDefault();
                        if (work == "PM")
                        {
                            return Redirect("/pm/assign");
                        }
                        if (work == "TESTER")
                        {
                            return Redirect("/testing/bug");
                        }
                        if (work == "DEV")
                        {
                            return Redirect("/dev/resolve");
                        }
                    }
                    else
                    {
                        return RedirectToAction("login","login");
                    }
                }
                

            }
            catch
            {
                return RedirectToAction("login", "login");
            }

            return View(model);
        }
        public ActionResult logout()
        {
            Session.Abandon();
            return Redirect("login/login");
        }
    }
}