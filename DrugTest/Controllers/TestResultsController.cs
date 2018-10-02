using DrugTest.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace DrugTest.Controllers
{
    public class TestResultsController : Controller
    {
        private DriverContext db = new DriverContext();

        public ActionResult Index(int? id)
        {
            ViewBag.Title = "All Results";

            var testResults = db.TestResults.Include(r => r.Driver).Include(r => r.TestBatch);

            if (!testResults.Any()) return View();

            if (id == null)
            {
                var currentBatch = db.TestBatches.OrderByDescending(b => b.Id).FirstOrDefault().Id;
                ViewBag.Id = currentBatch;
                return View(testResults);
            }

            ViewBag.Id = id;
            return View(testResults);
        }

        // GET: BatchResults
        // Retrieve the batch by id or the most current batch
        public ActionResult BatchResults(int? id)
        {
            ViewBag.Title = "Current Results";

            var testBatches = db.TestBatches;

            if (!testBatches.Any()) return View("Index");

            if (id != null)
            {
                var batchResult = db.TestResults.Where(r => r.TestBatchId == id);

                return View("Index", batchResult);
            }

            var currentBatch = db.TestBatches.OrderByDescending(b => b.Id).FirstOrDefault();
            var currentResult = db.TestResults.Where(r => r.TestBatchId == currentBatch.Id);
            return View("Index", currentResult);

        }

        // GET: TestResults/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestResult testResult = db.TestResults.Find(id);
            if (testResult == null)
            {
                return HttpNotFound();
            }
            ViewBag.DriverId = new SelectList(db.Drivers, "Id", "UserName", testResult.DriverId);
            ViewBag.TestBatchId = new SelectList(db.TestBatches, "Id", "Id", testResult.TestBatchId);
            return View(testResult);
        }

        // POST: TestResults/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id, Status, Comments")] TestResult testResult)
        {
            if (ModelState.IsValid)
            {
                var result = db.TestResults.SingleOrDefault(r => r.Id == testResult.Id);
                if (result != null)
                {
                result.Status = testResult.Status;
                result.Comments = testResult.Comments;
                result.LastModified = DateTime.Now;

                db.SaveChanges();
                return RedirectToAction("Index");
                }
            }
            ViewBag.DriverId = new SelectList(db.Drivers, "Id", "UserName", testResult.DriverId);
            ViewBag.TestBatchId = new SelectList(db.TestBatches, "Id", "Id", testResult.TestBatchId);
            return View(testResult);
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
