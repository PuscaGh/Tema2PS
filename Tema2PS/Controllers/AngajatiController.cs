using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Tema2PS.Models;

namespace Tema2PS.Controllers
{
    public class AngajatiController : Controller
    {
        private UsersContext db = new UsersContext();
        Boolean adminLogged = false;
        //
        // GET: /Angajati/Index

        public ActionResult Index()
        {
            return View(db.UserProfiles.ToList());
        }

        //
        // GET: /Angajati/Login

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }


        //
        // POST: /Angajati/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                foreach (var user in db.UserProfiles.ToArray())
                {
                    if (user.UserName.Equals(model.UserName) && user.UserRole == 0)
                    {
                        if(user.Password.Equals(getMd5Hash(model.Password)))
                        {
                            adminLogged = true;
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(model);
        }

        //
        // GET: /Angajati/Details/5

        public ActionResult Details(int id = 0)
        {
            Users users = db.UserProfiles.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        //
        // GET: /Angajati/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Angajati/Create

        [HttpPost]
        public ActionResult Create(Users users)
        {
            if (ModelState.IsValid && adminLogged)
            {
                users.Password = getMd5Hash(users.Password);
                db.UserProfiles.Add(users);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(users);
        }

        //
        // GET: /Angajati/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Users users = db.UserProfiles.Find(id);
            if (users == null || !adminLogged)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        //
        // POST: /Angajati/Edit/5

        [HttpPost]
        public ActionResult Edit(Users users)
        {
            if (ModelState.IsValid && adminLogged)
            {
                db.Entry(users).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(users);
        }

        //
        // GET: /Angajati/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Users users = db.UserProfiles.Find(id);
            if (users == null || !adminLogged)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        //
        // POST: /Angajati/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            if (adminLogged)
            {
                Users users = db.UserProfiles.Find(id);
                db.UserProfiles.Remove(users);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        static string getMd5Hash(string input)
        {
             MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
            sBuilder.Append(data[i].ToString("x2"));
            }

        return sBuilder.ToString();

}
    }
}