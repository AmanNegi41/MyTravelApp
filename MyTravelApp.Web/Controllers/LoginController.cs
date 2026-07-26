using Microsoft.AspNetCore.Mvc;
using MyTravelApp.Data.Models;
using MyTravelApp.Services;

namespace MyTravelApp.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly LoginRepository _lgrepo;
        private readonly IConfiguration _configuration;
        public LoginController(LoginRepository lgrepo, IConfiguration configuration)
        {
            _lgrepo = lgrepo;
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login(string mail, string pass)
        {
            LoginUser lguser = new LoginUser { UserId = mail, PasswordHash = pass};
            var result = _lgrepo.LoginRepo(lguser);
            if(result)
            {
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                TempData["errorMsg"] = "Invalid mail or password please try again!";
                return View("Index");
            } 
        }
    }
}
