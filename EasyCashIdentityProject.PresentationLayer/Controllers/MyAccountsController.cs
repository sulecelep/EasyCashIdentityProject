using EasyCashIdentityProject.DtoLayer.Dtos.AppUserDtos;
using EasyCashIdentityProject.EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EasyCashIdentityProject.PresentationLayer.Controllers
{
    public class MyAccountsController : Controller
    {
        private readonly UserManager<AppUser>? _userManager;

        public MyAccountsController(UserManager<AppUser>? userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            AppUserEditDto appUserEditDto = new AppUserEditDto();
            appUserEditDto.Name = values.Name;
            appUserEditDto.Surname = values.Surname;
            appUserEditDto.PhoneNumber = values.PhoneNumber;
            appUserEditDto.Email = values.Email;
            appUserEditDto.City = values.City;  
            appUserEditDto.District = values.District;  
            appUserEditDto.ImageUrl = values.ImageUrl;
           
            return View(appUserEditDto);
        }
        [HttpPost]
        public async Task<IActionResult> Index(AppUserEditDto appUserEditDto)
        {
           
            if(appUserEditDto.Password== appUserEditDto.ConfirmPassword)
            {
                
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                user.Name = appUserEditDto.Name;
                user.Surname = appUserEditDto.Surname;
                user.City = appUserEditDto.City;
                user.District = appUserEditDto.District;
                user.Email = appUserEditDto.Email;
                user.PhoneNumber = appUserEditDto.PhoneNumber;
                var passwordOld = await _userManager.CheckPasswordAsync(user, appUserEditDto.Password);
                if (!passwordOld)
                {
                    user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, appUserEditDto.Password);
                }
                var result = await _userManager.UpdateAsync(user);
                if(result.Succeeded)
                {
                    if(!passwordOld )
                    {
                        return RedirectToAction("Index", "Login");
                    }
                    return RedirectToAction("Index", "MyAccounts");
                }
               
            }
            
            //else
            //{
            //    //Tempdata ile alert gönderebilirsin
            //}
            return RedirectToAction("Index", "MyAccounts");



        }
    }
}
