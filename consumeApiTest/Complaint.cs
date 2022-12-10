using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace consumeApiTest
{
    public class Complaint
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Body { get; set; }
        public Status Status { get; set; }
        [Required]
        public DateTime Created { get; set; }
        public DateTime? Completed { get; set; }
    }
    public enum Status
    {
        Unresolved, Resolved
    }
}
