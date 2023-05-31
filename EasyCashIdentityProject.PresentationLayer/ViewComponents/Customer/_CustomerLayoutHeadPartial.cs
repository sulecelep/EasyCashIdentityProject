using Microsoft.AspNetCore.Mvc;

namespace EasyCashIdentityProject.PresentationLayer.ViewComponents.Customer
{
    public class _CustomerLayoutHeadPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();  
        }
    }
}
