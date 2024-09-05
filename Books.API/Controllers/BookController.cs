using Books.Domain.Entities;
using Books.Domain.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Books.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository repo;

        public BookController(IBookRepository repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllBookAsync()
        {
            var books = await repo.GetAllBooksAsync();
            return Ok(books);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBookById(int id)
        {
            var book = await repo.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddBook(Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await repo.AddBookAsync(book);
            return Ok(book);
        }

        [HttpPut("{id}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateBook(int id, Book book)
        {
            var existingBook = await repo.GetBookByIdAsync(id);
            if (existingBook == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            existingBook.BookName = book.BookName;
            existingBook.Author = book.Author;
            existingBook.Description = book.Description;
            existingBook.Price = book.Price;

            await repo.UpdateBookAsync(existingBook);
            return Ok(book);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var existingBook = await repo.GetBookByIdAsync(id);
            if (existingBook == null)
            {
                return NotFound();
            }

            await repo.DeleteBookAsync(id);
            return Ok();
        }
    }
}
