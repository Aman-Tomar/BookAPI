using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Books.Domain.Entities;
using Books.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Books.Data
{
    public class BookRepository : IBookRepository
    {
        BookDbContext db = new BookDbContext();
        public async Task AddBookAsync(Book book)
        {
            await db.Books.AddAsync(book);
            await db.SaveChangesAsync();
        }

        public async Task DeleteBookAsync(int bookId)
        {
            var book = db.Books.Find(bookId);
            db.Books.Remove(book);
            await db.SaveChangesAsync();
        }

        public async Task<List<Book>> GetAllBooksAsync()
        {
            return await db.Books.ToListAsync();
        }

        public async Task<Book> GetBookByIdAsync(int bookId)
        {
            return await db.Books.FindAsync(bookId);
        }

        public async Task UpdateBookAsync(Book book)
        {
            db.Books.Update(book);
            await db.SaveChangesAsync();
        }
    }
}
