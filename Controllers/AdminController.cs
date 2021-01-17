using System;
using BlogSite.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BlogSite.Controllers
{
    public class AdminController : Controller
    {
        private DBHandler db;
        private PostView pv;
        private UserView Uv;

        public AdminController()
        {
            db = new DBHandler();
            pv = new PostView();
            Uv = new UserView();
        }


        public ViewResult index()
        {

            return View("../User/index");
        }

        public ViewResult Home()
        {
            pv.posts = db.ViewAllPosts();
            return View("Home", pv.posts);
        }

        public ViewResult AllUser()
        {
            if (checkSession())
            {
                Uv.users = db.ViewAllUsers();
                return View("AllUsers", Uv.users);
            }
            return View("../User/index");
        }
        [HttpGet]
        public ViewResult Update(int id)
        {

            if (checkSession())
            {
                User u = db.findUser(id);
                return View("Update", u);

            }
            return View("../User/index");
        }
        [HttpPost]
        public ViewResult Update(User u)
        {
            if (checkSession())
            {

                if (db.UpdateUser(u))
                {
                    Uv.users = db.ViewAllUsers();
                    return View("AllUsers", Uv.users);
                }
                else
                {
                    return View("../User/index");
                }
            }
            return View("../User/index");

        }
        public ViewResult Delete(int id)
        {

            if (checkSession())
            {
                if (db.DeleteUser(id))
                {
                    Uv.users = db.ViewAllUsers();
                    return View("AllUsers", Uv.users);
                }
            }
            return View("../User/index");
        }

        [HttpGet]
        public ViewResult AddUser()
        {
            if (checkSession())
            {

                return View("AddUser");

            }
            return View("../User/index");
        }

        [HttpPost]
        public ViewResult AddUser(User u)
        {
            if (checkSession())
            {
                if (ModelState.IsValid)
                {
                    db.AddUser(u);
                    Uv.users = db.ViewAllUsers();
                    return View("AllUsers", Uv.users);
                }
            }
            return View("../User/index");
        }


        public ViewResult detailForPostByAdmin(int id)
        {

            if (checkSession())
            {
                Post p = db.findPost(id);
                return View("detail", p);
            }
            else
            { return View("../User/Index"); }

        }

        public ViewResult Logout()
        {
            HttpContext.Response.Cookies.Delete("username");
            //HttpContext.Session.Remove("username");
            return View("../User/index");
        }
        public bool checkSession()
        {
            if (HttpContext.Request.Cookies.ContainsKey("username"))
            {
                return true;
            }
            else
            {
                return false;
            }
            //if (HttpContext.Session.Keys.Contains("username"))
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
        }
    }
}