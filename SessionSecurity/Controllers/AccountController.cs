using System;
using System.Web.Mvc;
using SessionSecurity.Models;
using SessionSecurity.Security;
using SessionSecurity.ViewModels;

namespace SessionSecurity.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(AccountViewModel model)
        {
            var accountModel = new AccountModel();
            if (string.IsNullOrEmpty(model.Account.Username) || string.IsNullOrEmpty(model.Account.Password) ||
                accountModel.Login(model.Account.Username, model.Account.Password) == null)
            {
                ViewBag.Error = "Account's Invalid";
                return View("Index");
            }

            SessionPersister.Username = model.Account.Username;
            return View("Success");
        }

        public ActionResult Logout()
        {
            SessionPersister.Username = string.Empty;
            return RedirectToAction("Index");
        }
    }
}