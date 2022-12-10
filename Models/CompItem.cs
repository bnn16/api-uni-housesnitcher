using System.ComponentModel.DataAnnotations;

namespace CompApi.Models
{
    public class CompItem
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
