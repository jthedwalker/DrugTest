using DrugTest.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace DrugTest.Controllers
{
    public class TestBatchesController : Controller
    {
        private DriverContext db = new DriverContext();

        // GET: TestBatches
        public ActionResult Index()
        {
            var testBatches = db.TestBatches.Include(b => b.TestResults).ToList();

            return !testBatches.Any() ? View() : View(testBatches);
        }

        // GET: TestBatches/Details/5
        public ActionResult Details(int? id)
        {
            if (db.TestResults.ToList().Any())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                TestBatch testBatch = db.TestBatches.Find(id);
                if (testBatch == null)
                {
                    return HttpNotFound();
                }

                return View(testBatch);
            }

            return View();
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
