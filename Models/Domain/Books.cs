using System.ComponentModel.DataAnnotations;

namespace LabAPI.Models.Domain
{
    public class Books
    {
        [Key]
        public int BooksId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool? IsRead { get; set; }
        public DateTime? DateRead { get; set; }
        public int Rate { get; set; }
        public string Genre { get; set; }
        public string? CoverUrl { get; set; }
        public DateTime DateAdded { get; set; }
        //Khoa ngoai cua publishers
        public Publishers? Publishers { get; set; }
        public int PublishersId { get; set; }
        //Khoa chinh tro vao bang Book_Author
        public List<Book_Author>? Book_Authors { get; set; }
    }
}
