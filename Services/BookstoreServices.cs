using LabAPI.Data;
using LabAPI.Models.Domain;
using LabAPI.Services;

using Microsoft.EntityFrameworkCore;

namespace LabAPI.Services
{
    public class BookstoreServices : IBookstoreServices
    {
        private readonly AppDbContext _db;
        public BookstoreServices(AppDbContext db)
        {
            _db = db;
        }
        #region Publishers

        public async Task<List<Publishers>> GetAllPublishers()
        {
            try
            {
                return await _db.Publishers.ToListAsync();
            }
            catch (Exception e)
            {
                return null;
            }
        }


        public async Task<Publishers> GetIdPublishers(int id, bool includePublishers = false)
        {
            try
            {
                if (includePublishers)
                {
                    return await _db.Publishers.Include(c => c.Books).FirstOrDefaultAsync(i => i.PublishersId == id);
                }

                return await _db.Publishers.FindAsync(id);
            }
            catch (Exception e)
            {
                return null;
            }
        }


        public async Task<Publishers> AddPublishers(Publishers publishers)
        {
            try
            {
                await _db.Publishers.AddAsync(publishers);
                await _db.SaveChangesAsync();
                return await _db.Publishers.FindAsync(publishers.PublishersId);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Publishers> UpdatePublishers(Publishers publishers)

        {
            try
            {
                _db.Entry(publishers).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return publishers;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeletePublishers(Publishers publishers)
        {
            try
            {
                var dbpublishers = await _db.Publishers.FindAsync(publishers.PublishersId);

                if (dbpublishers == null)
                {
                    return (false, "Publishers could not be found.");
                }

                _db.Publishers.Remove(publishers);
                await _db.SaveChangesAsync();

                return (true, "Publishers got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }
        #endregion
        #region Books

        public async Task<List<Books>> GetAllBooks()
        {
            try
            {
                return await _db.Books.ToListAsync();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<Books> GetIdBooks(int id, bool includeBooks)
        {
            try
            {
                if (includeBooks)
                {
                    return await _db.Books.Include(c => c.Book_Authors).FirstOrDefaultAsync(i => i.BooksId == id);
                }

                return await _db.Books.FindAsync(id);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<Books> AddBooks(Books books)
        {
            try
            {
                await _db.Books.AddAsync(books);
                await _db.SaveChangesAsync();
                return await _db.Books.FindAsync(books.BooksId);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<Books> UpdateBooks(Books books)
        {
            try
            {
                _db.Entry(books).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return books;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteBooks(Books books)
        {
            try
            {
                var dbCourses = await _db.Books.FindAsync(books.BooksId);
                if (dbCourses == null)
                {
                    return (false, "Not Found");
                }

                _db.Books.Remove(books);
                await _db.SaveChangesAsync();
                return (true, "Success");
            }
            catch (Exception e)
            {
                return (false, "Failed");
            }
        }
        #endregion
        #region Authors

        public async Task<List<Authors>> getAllAuthors()
        {
            try
            {
                return await _db.Authors.ToListAsync();
            }
            catch (Exception e)
            {
                return null;
            }
        }


        public async Task<Authors> GetIDAuthors(int id, bool includeAuthors = false)
        {
            try
            {
                if (includeAuthors)
                {
                    return await _db.Authors.Include(c => c.Book_Authors).FirstOrDefaultAsync(i => i.AuthorsId == id);
                }

                return await _db.Authors.FindAsync(id);
            }
            catch (Exception e)
            {
                return null;
            }
        }


        public async Task<Authors> AddAuthors(Authors authors)
        {
            try
            {
                await _db.Authors.AddAsync(authors);
                await _db.SaveChangesAsync();
                return await _db.Authors.FindAsync(authors.AuthorsId);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Authors> UpdateAuthors(Authors authors)

        {
            try
            {
                _db.Entry(authors).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return authors;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteAuthors(Authors authors)
        {
            try
            {
                var dbAuthors = await _db.Authors.FindAsync(authors.AuthorsId);

                if (dbAuthors == null)
                {
                    return (false, "Authors could not be found.");
                }

                _db.Authors.Remove(authors);
                await _db.SaveChangesAsync();

                return (true, "Authors got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }
        #endregion
        #region Book_Author
        public async Task<List<Book_Author>> GetAllB_A()
        {
            try
            {
                return await _db.Books_Author.ToListAsync();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<Book_Author> GetIdB_A(int id)
        {
            try
            {
                return await _db.Books_Author.FindAsync(id);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<Book_Author> AddB_A(Book_Author B_A)
        {
            try
            {
                await _db.Books_Author.AddAsync(B_A);
                await _db.SaveChangesAsync();

                return await _db.Books_Author.FindAsync(B_A.BooksId);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<Book_Author> UpdateB_A(Book_Author B_A)
        {
            try
            {
                _db.Entry(B_A).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return B_A;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteB_As(Book_Author B_A)
        {
            try
            {
                var dbSC = await _db.Books_Author.FindAsync(B_A.BooksId);
                if (dbSC == null)
                {
                    return (false, "Books_Author could not be found");
                }
                _db.Books_Author.Remove(B_A);
                await _db.SaveChangesAsync();
                return (true, "Amazing good job you");
            }
            catch (Exception e)
            {
                return (false, e.Message);
            }
        }
        #endregion
    }
}
