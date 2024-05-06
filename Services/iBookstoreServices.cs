using LabAPI.Models.Domain;
using LabAPI.Data;


namespace LabAPI.Services
{
    public interface IBookstoreServices
    {
        // Sach
        Task<List<Books>> GetAllBooks();
        Task<Books> GetIdBooks(int id, bool includeBooks = false);
        Task<Books> AddBooks(Books books);
        Task<Books> UpdateBooks(Books books);
        Task<(bool, string)> DeleteBooks(Books books);

        // Tac Gia

        Task<List<Authors>> getAllAuthors();
        Task<Authors> GetIDAuthors(int id, bool includeAuthors = false);
        Task<Authors> AddAuthors(Authors authors);
        Task<Authors> UpdateAuthors(Authors authors);
        Task<(bool, string)> DeleteAuthors(Authors authors);

        //Nhà Xuất Bản
        Task<List<Publishers>> GetAllPublishers();
        Task<Publishers> GetIdPublishers(int id, bool includePublishers = false);
        Task<Publishers> AddPublishers(Publishers publishers);
        Task<Publishers> UpdatePublishers(Publishers publishers);
        Task<(bool, string)> DeletePublishers(Publishers publishers);

        //Sach Và Xuất bản
        Task<List<Book_Author>> GetAllB_A();
        Task<Book_Author> GetIdB_A(int id);
        Task<Book_Author> AddB_A(Book_Author B_A);
        Task<Book_Author> UpdateB_A(Book_Author B_A);
        Task<(bool, string)> DeleteB_As(Book_Author B_A);
    }
}
