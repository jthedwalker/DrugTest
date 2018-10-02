using DrugTest.Models;
using System;
using System.Linq;

namespace DrugTest.Business_Logic
{
    public class GenerateBatch
    {
        public void Generate()
        {
            var db = new DriverContext();

            try
            {
                var testBatch = new TestBatch();

                // Create the batch to use below.
                db.TestBatches.Add(testBatch);

                int batchValue;
                int alcoholValue;
                string batchCriteria;
                
                // Get the settings.
                var alcoholSetting = AppSettings.GetAppSettings("AlcoholPercentage");
                var drugSetting = AppSettings.GetAppSettings("DrugPercentage");

                // Convert to a percentage.
                var alcoholPercentage = (int.Parse(alcoholSetting) / 100.0);
                var drugPercentage = (int.Parse(drugSetting) / 100.0);

                // Get the quantity of alcohol and drug drivers to pull.
                int alcoholToPull = Convert.ToInt16(Math.Ceiling(db.Drivers.Count(d => d.Active) * alcoholPercentage));
                int drugToPull = Convert.ToInt16(Math.Ceiling(db.Drivers.Count(d => d.Active) * drugPercentage));

                // Check to see which one is greater to base the initial pull quantity from.
                if (drugPercentage > alcoholPercentage)
                {
                    batchValue = drugToPull;
                    alcoholValue = alcoholToPull;
                }
                else
                {
                    batchValue = alcoholToPull;
                    alcoholValue = alcoholToPull;
                }

                // Get the drivers based off the condition.
                var drivers = db.Drivers.OrderBy(r => Guid.NewGuid()).Take(batchValue);
                var batchId = db.TestBatches.Local.OrderByDescending(b => b.Id).Select(b => b.Id).First();

                // Insert a test result and batchId for each driver.
                foreach (var driver in drivers)
                {
                    TestResult testResult = new TestResult
                    {
                        DriverId = driver.Id,
                        UserName = driver.UserName,
                        TestBatchId = batchId
                    };

                    db.TestResults.Add(testResult);
                }

                // Get the quantity of drivers for alcohol testing from the existing context.
                // If the alcohol setting is greater than the drug setting all flags will be flipped.
                var alcoholCanidates = db.TestResults.Local.AsQueryable().Take(alcoholValue);

                // Flip the flag for alcohol test applicants.
                foreach (var canidate in alcoholCanidates)
                {
                    canidate.Alcohol = Alcohol.IsAlcohol;
                }

                // Insert batch criteria.
                var updateBatch = db.TestBatches.Local.OrderByDescending(b => b.Id).First();
                batchCriteria = $"Drivers Pulled: {drivers.Count()}, Alcohol: {alcoholCanidates.Count()}, Drug: {drugSetting}%, Alcohol: {alcoholSetting}%";
                updateBatch.Criteria = batchCriteria;

                // Save changes to the context.
                db.SaveChanges();
            }
            catch (Exception e)
            {
                var error = new Error
                {
                    ErrorDesc = e.ToString()
                };

                db.Errors.Add(error);
                db.SaveChanges();
            }

        }
    }
}