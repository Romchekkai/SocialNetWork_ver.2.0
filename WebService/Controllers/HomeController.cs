using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SocialNetWork.BL.ViewModels.Account;
using SocialNetWork.DAL.Models.Users;
using SocialNetWork.Models;
using System.Diagnostics;

namespace SocialNetWork.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SignInManager<User> _signInManager;

        public HomeController(ILogger<HomeController> logger, SignInManager<User> signInManager)
        {
            _logger = logger;
            _signInManager = signInManager;
        }

        [Route("")]
        [Route("[controller]/[action]")]
        public IActionResult Index()
        {
            if (_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("MyPage", "AccountManager");
            }
            else
            {
                return View(new MainViewModel());
            }
        }

        [Route("[action]")]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
