using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DrugTest.Models
{
    public class TestResult
    {
        public TestResult()
        {
            LastModified = DateTime.Now;
        }

        public int Id { get; set; }

        [DisplayName("Driver Id")]
        [Index(IsUnique = true)]
        public virtual Driver Driver { get; set; }
        public int? DriverId { get; set; }

        public string UserName { get; set; }

        [DisplayName("Batch")]
        [Index]
        public virtual TestBatch TestBatch { get; set; }
        public int? TestBatchId { get; set; }

        [Required]
        public Status Status { get; set; }

        public Alcohol Alcohol { get; set; }

        [DisplayName("Last Modified")]
        public DateTime LastModified { get; set; }

        public string Comments { get; set; }
    }
}