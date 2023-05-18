using EasyCashIdentityProject.EntityLayer.Concrete;
using EasyCashIdentityProject.PresentationLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EasyCashIdentityProject.PresentationLayer.Controllers
{
	public class ConfirmMailController : Controller
	{
		private readonly UserManager<AppUser> _userManager;

        public ConfirmMailController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
		public IActionResult Index()
		{
			var valueMail = TempData["Mail"];
			ViewBag.v=valueMail;
			//confirmMailViewModel.Mail = valueMail.ToString();
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Index(ConfirmMailViewModel confirmMailViewModel)
		{
			var user = await _userManager.FindByEmailAsync(confirmMailViewModel.Mail);
			if(user.ConfirmCode==confirmMailViewModel.ConfirmCode)
			{
				return RedirectToAction("Index","MyProfile");
			}
			return View();
		}
	}
}
//1 kullanıcı adı şifre eşleşmeli
//2 email confirmed olmalı
