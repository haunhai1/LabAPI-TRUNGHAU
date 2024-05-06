using LabAPI.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace LabAPI.Models.Domain
{
    public class Publishers
    {
        [Key]
        public int PublishersId { get; set; }
        public string? Name { get; set; }
        public List<Books>? Books { get; set; }
    }
}