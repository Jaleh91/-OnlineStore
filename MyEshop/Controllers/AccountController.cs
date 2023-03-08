using DataLayer;
using DataLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MyEshop.Controllers
{
    public class AccountController : Controller
    {
        MyEshop_DBEntities db=new MyEshop_DBEntities();
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        [Route("Register")]

        public ActionResult Register()
        {
            return View();
        }


        [HttpPost]

        [Route("Register")]

        [ValidateAntiForgeryToken]

        public ActionResult Register(RegisterViewModels register)
        {
            if (ModelState.IsValid)
            {
                if(!db.Users.Any(u=>u.Email==register.Email.Trim().ToLower()))
                {
                    Users user = new Users()
                    {
                        UserName=register.UserName,
                        Email = register.Email,
                        RegisterDate = DateTime.Now,
                        Password=FormsAuthentication.HashPasswordForStoringInConfigFile(register.Password,"MD5"),
                        RoleID=1,
                        ActiveCode=Guid.NewGuid().ToString(),
                        IsActive=false,
                    };

                    db.Users.Add(user);
                    db.SaveChanges();
                    //Sending Email Code

                    string body = PartialToStringClass.RenderPartialView("ManageEmails", "SendActivationEmail", user);
                    SendEmail.Send(user.Email, "ایمیل فعالسازی", body);
                    //End Sending Email Code
                    return View("SuccesRegister", user);
                }
                else
                {
                    ModelState.AddModelError("Email", "ایمیل وارد شده تکراری است");
                }
            }

            return View(register);
        }


        public ActionResult ActiveUser(string id)
        {
            var user = db.Users.SingleOrDefault(u => u.ActiveCode == id);
            if (user==null)
            {
                return HttpNotFound();
            }

            user.ActiveCode = Guid.NewGuid().ToString();
            user.IsActive = true;
            db.SaveChanges();
            return View();
        }


        [Route("Login")]
        public ActionResult Login()
        {
            return View();
        }

        [Route("Login")]
        [HttpPost]
        public ActionResult Login(LoginViewModels login,string ReturnUrl="/")
        {
            if (ModelState.IsValid)
            {
                string hashepassword = FormsAuthentication.HashPasswordForStoringInConfigFile(login.Password, "MD5");
                var user=db.Users.SingleOrDefault(u=>u.Email==login.Email&&u.Password==hashepassword);
                if (user != null)
                {
                    if (user.IsActive)
                    {
                        FormsAuthentication.SetAuthCookie(user.UserName, login.RememberMe);
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError("Email", "حساب کاربری شما فعال نشده است");

                    }
                }
                else
                {
                    ModelState.AddModelError("Email", "کاربری با اطلاعات وارد شده یافت نشد.");
                }


            }
            return View(login);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return Redirect("/");
        }



        [Route("ForgotPassword")]

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [Route("ForgotPassword")]

        public ActionResult ForgotPassword(ForgotPasswordViewModels forgot)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.SingleOrDefault(u => u.Email == forgot.Email);
                if (user != null)
                {
                    if (user.IsActive)
                    {
                       string bodyEmail= PartialToStringClass.RenderPartialView("ManageEmails", "SendRecoveryPasswordEmail", user);
                        SendEmail.Send(user.Email, "بازیابی کلمه عبور", bodyEmail);
                        return View("SuccesForgotPassword",user);
                    }
                    else
                    {
                        ModelState.AddModelError("Email", " حساب کاربری شما فعال نشده است.");

                    }
                }
                else
                {
                    ModelState.AddModelError("Email", " کاربری یافت نشد.");
                }
            }

            return View();
        }



        public ActionResult RecoveryPassword(string id)
        {
            return View();
        }

        [HttpPost]

        public ActionResult RecoveryPassword(string id,RecoveryPasswordViewModel recovery)
        {
            if (ModelState.IsValid)
            {
                var user=db.Users.SingleOrDefault(u=>u.ActiveCode==id);
                if (user == null)
                {
                    return HttpNotFound();
                }

                user.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(recovery.Password, "MD5");   
                user.ActiveCode = Guid.NewGuid().ToString();
                db.SaveChanges();
                return Redirect("/Login?recovery=true");

            }
            return View();
        }

    }
}
