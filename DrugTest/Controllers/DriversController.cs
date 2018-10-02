using DrugTest.Models;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace DrugTest.Controllers
{
    public class DriversController : Controller
    {
        private DriverContext db = new DriverContext();

        //GET: Drivers
        public ActionResult Index()
        {
            return View(db.Drivers.ToList());
        }

        //GET: Driver results
        public ActionResult DriverResults(int? id)
        {
            var testResults = db.TestResults.Include(t => t.Driver).Include(t => t.TestBatch);

            if (testResults.Any())
            {
                testResults = testResults.Where(r => r.DriverId == id);
                return View(testResults);
            }

            return View(testResults);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
