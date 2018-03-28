using Library.DataAccessLayer.Connection;
using Library.EntityLayer.Models;

using Dapper;

using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Library.DataAccessLayer.Repositories
{
    public class BookAuthorRepository : GenericRepository<BookAuthor>
    {
        // TODO: Exception here!
        public override List<BookAuthor> GetAllById<TModel>(int id)
        {
            var genericType = typeof(TModel);
            var genericTypeName = genericType.Name;
            string query = $"SELECT BookAuthors.*, Books.*, Authors.* FROM Books LEFT JOIN BookAuthors on BookAuthors.BookId = Books.BookId LEFT JOIN Authors on Authors.AuthorId = BookAuthors.AuthorId WHERE {genericTypeName}s.{genericTypeName}Id = {id};";
            List<BookAuthor> bookAuthors;
            using (SqlConnection connection = new SqlConnection(CurrentConnection.ConnectionString))
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
    }
}
