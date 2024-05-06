using System.ComponentModel.DataAnnotations;

namespace LabAPI.Models.Domain
{
    public class Authors
    {
        [Key]
        public int AuthorsId { get; set; }
        public string? FullName { get; set; }
        public List<Book_Author>? Book_Authors { get; set; }
    }
}