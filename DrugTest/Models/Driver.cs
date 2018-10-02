using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DrugTest.Models
{
    public class Driver
    {
        public Driver()
        {
            TestResults = new HashSet<TestResult>();
        }     

        [Index(IsUnique = true)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DisplayName("User Name")]
        [Index(IsUnique = true)]
        [StringLength(50)]
        public string UserName { get; set; }

        [DisplayName("First Name")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        [StringLength(50)]
        public string LastName { get; set; }

        public bool Active { get; set; }

        [DisplayName("Active")]
        [NotMapped]
        public string IsActive => Active ? "IsActive" : "NotActive";

        [JsonIgnore]
        public virtual ICollection<TestResult> TestResults { get; set; }
    }
}