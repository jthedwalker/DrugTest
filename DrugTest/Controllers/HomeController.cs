using System.Web.Mvc;

namespace DrugTest.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "For assistance, submit a help ticket with Letica IT.";

            return View();
        }
    }
}