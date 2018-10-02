using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace DrugTest.Models
{
    public class TestBatch
    {
        public TestBatch()
        {
            TestResults = new HashSet<TestResult>();
            DateCreated = DateTime.Now;
        }

        [DisplayName("Batch")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DisplayName("Date Created")]
        public DateTime DateCreated { get; set; }

        public string Criteria { get; set; }

        public virtual ICollection<TestResult> TestResults { get; set; }
    }
}