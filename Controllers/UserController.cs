using System;
using BlogSite.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BlogSite.Controllers
{
    public class UserController : Controller
    {
        private DBHandler db;
        private PostView pv;
        public string sessionUser;
        public UserController()
        {
            db = new DBHandler();
            pv = new PostView();
        }

        public ViewResult Index()
        {
            return View();
        }
        [HttpGet]
        public ViewResult login()
        {
            return View();
        }
        [HttpPost]
        public ViewResult login(User user)
        {

            if (user.Username != null && user.Password != null)
            {
                if (user.Username == "Admin" && user.Password == "admin")
                {
                    HttpContext.Response.Cookies.Append("username", user.Username);
                    //HttpContext.Session.SetString("username", user.Username);
                    if (checkSession())
                    {
                        pv.posts = db.ViewAllPosts();
                        ViewBag.Message = "Login Successfully";
                        return View("../Admin/Home", pv.posts);
                    }

                }
                else
                {

                    User u = db.CheckUserValidation(user.Username, user.Password);
                    if (u.Username != null)
                    {
                        HttpContext.Response.Cookies.Append("username", u.Username);
                        //HttpContext.Session.SetString("username", u.Username);
                        if (checkSession())
                        {
                            pv.posts = db.ViewAllPosts();
                            ViewBag.Message = "Login Successfully";
                            return View("Home", pv.posts);
                        }
                    }
                    else
                    {
                        return View();
                    }
                }
            }
            return View();
        }
        [HttpGet]
        public ViewResult signup()
        {
            return View();
        }
        [HttpPost]
        public ViewResult signup(User u)
        {
            if (ModelState.IsValid)
            {
                HttpContext.Response.Cookies.Append("username", u.Username);
                //HttpContext.Session.SetString("username", u.Username);
                db.AddUser(u);
                pv.posts = db.ViewAllPosts();
                return View("Home", pv.posts);
            }
            return View();
        }
        [HttpPost]
        public ViewResult update(User u)
        {
            if (checkSession())
            {

                if (db.UpdateUser(u))
                {
                    return View("Profile", u);
                }
                else
                {
                    return View("index");
                }
            }
            return View("index");

        }
        [HttpGet]
        public ViewResult update_post(int id)
        {
            if (checkSession())
            {
                Post p = db.findPost(id);
                return View("update_post", p);
            }
            return View("index");

        }
        [HttpPost]
        public ViewResult update_post(Post p)
        {
            if (checkSession())
            {
                p.Username = HttpContext.Request.Cookies["username"];
                //p.Username = HttpContext.Session.GetString("username");
                p.Date = DateTime.Now;

                if (db.UpdatePost(p))
                {
                    pv.posts = db.ViewAllPosts();
                    return View("Home", pv.posts);
                }
            }
            return View("index");
        }
        public ViewResult Logout()
        {
            HttpContext.Response.Cookies.Delete("username");
            //HttpContext.Session.Remove("username");
            return View("index");
        }

        public ViewResult edit()
        {
            if (checkSession())
            {
                string username = HttpContext.Request.Cookies["username"];
                //string username = HttpContext.Session.GetString("username");
                User u = db.findUser(username);
                return View("Profile", u);
            }
            else
            {
                return View("index");
            }
        }

        public ViewResult delete(int id)
        {
            if (checkSession())
            {
                if (db.DeletePost(id))
                {
                    pv.posts = db.ViewAllPosts();
                    return View("Home", pv.posts);
                }
            }
            return View("index");
        }
        public ViewResult detailForPost(int id)
        {

            if (checkSession())
            {
                Post p = db.findPost(id);
                string user = HttpContext.Request.Cookies["username"];
                //string user = HttpContext.Session.GetString("username");
                user = user.Split(" ")[0];
                string username = p.Username;
                username = username.Split(" ")[0];

                if ((String.Equals(username, user)) == true)
                {

                    return View("detail", p);
                }
                else
                {

                    return View("outside_detail", p);
                }
            }
            else
            { return View("index"); }

        }

        public ViewResult outside_detail()
        {
            return View();
        }
        public ViewResult Home()
        {
            if (checkSession())
            {
                pv.posts = db.ViewAllPosts();
                return View(pv.posts);
            }
            else
            {
                return View("index");
            }
        }
        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }
        [HttpPost]
        public ViewResult Create(Post p)
        {
            if (checkSession())
            {
                p.Username = HttpContext.Request.Cookies["username"];
                //p.Username = HttpContext.Session.GetString("username");
                p.Date = DateTime.Now;
                if (db.CreatePost(p))
                {
                    pv.posts = db.ViewAllPosts();
                    return View("Home", pv.posts);
                }
                return View();
            }
            else
            {
                return View("index");
            }

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
