using Microsoft.AspNetCore.Mvc;

namespace SignalRNotificationApp.Controllers
{
    public class NotfyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
