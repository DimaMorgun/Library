using Library.EntityLayer.Models;

using Dapper;

using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Library.DataAccessLayer.Repositories
{
    public class BookAuthorRepository : GenericRepository<BookAuthor>
    {
        private string _connection;

        public BookAuthorRepository(string connection) : base(connection)
        {
            _connection = connection;
        }

        public List<BookAuthor> GetAllByBookId(int id)
        {
            var query = $"SELECT BookAuthors.*, Books.*, Authors.* FROM Books LEFT JOIN BookAuthors on BookAuthors.BookId = Books.BookId LEFT JOIN Authors on Authors.AuthorId = BookAuthors.AuthorId WHERE Books.BookId = {id};";
            List<BookAuthor> bookAuthors = new List<BookAuthor>();
            using (SqlConnection connection = new SqlConnection(_connection))
            {
                bookAuthors = connection.Query<BookAuthor, Book, Author, BookAuthor>(query, (ba, book, author) =>
                {
                    ba.Book = book;
                    ba.BookId = book.BookId;
                    ba.Author = author;
                    ba.AuthorId = author != null ? author.AuthorId : 0;
                    return ba;
                }, splitOn: "AuthorId").ToList();
            }
            return bookAuthors.ToList();
        }

        public List<BookAuthor> GetAllByAuthorId(int id)
        {
            var query = $"SELECT BookAuthors.*, Authors.*, Books.* FROM Authors LEFT JOIN BookAuthors on BookAuthors.AuthorId = Authors.AuthorId LEFT JOIN Books on Books.BookId = BookAuthors.BookId WHERE Authors.AuthorId = {id};";
            List<BookAuthor> bookAuthors = new List<BookAuthor>();
            using (SqlConnection connection = new SqlConnection(_connection))
            {
                bookAuthors = connection.Query<BookAuthor, Author, Book, BookAuthor>(query, (ba, author, book) =>
                {
                    ba.Book = book;
                    ba.BookId = book != null ? book.BookId : 0;
                    ba.Author = author;
                    ba.AuthorId = author.AuthorId;
                    return ba;
                }, splitOn: "BookId").ToList();
            }
            return bookAuthors.ToList();
        }
    }
}
