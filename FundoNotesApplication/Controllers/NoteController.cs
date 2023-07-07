using Microsoft.AspNetCore.Mvc;

namespace FundoNotesApplication.Controllers
{
    public class NoteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
