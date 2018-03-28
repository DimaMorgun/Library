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
        public override List<BookAuthor> GetAllById<TModel>(int id)
        {
            var genericType = typeof(TModel);
            var genericTypeName = genericType.Name;
            string query = string.Empty;
            List<BookAuthor> bookAuthors = new List<BookAuthor>();
            if (typeof(TModel) == typeof(Book))
            {
                query = $"SELECT BookAuthors.*, Books.*, Authors.* FROM Books LEFT JOIN BookAuthors on BookAuthors.BookId = Books.BookId LEFT JOIN Authors on Authors.AuthorId = BookAuthors.AuthorId WHERE Books.BookId = {id};";
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
            }
            if (typeof(TModel) == typeof(Author))
            {
                query = $"SELECT BookAuthors.*, Authors.*, Books.* FROM Authors LEFT JOIN BookAuthors on BookAuthors.AuthorId = Authors.AuthorId LEFT JOIN Books on Books.BookId = BookAuthors.BookId WHERE Authors.AuthorId = {id};";
                using (SqlConnection connection = new SqlConnection(CurrentConnection.ConnectionString))
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
            }

            return bookAuthors.ToList();
        }

        //public override List<BookAuthor> GetAllById<TModel>(int id)
        //{
        //    var genericType = typeof(TModel);
        //    var genericTypeName = genericType.Name;
        //    string query = $"SELECT BookAuthors.*, Books.*, Authors.* FROM Books LEFT JOIN BookAuthors on BookAuthors.BookId = Books.BookId LEFT JOIN Authors on Authors.AuthorId = BookAuthors.AuthorId WHERE {genericTypeName}s.{genericTypeName}Id = {id};";
        //    List<BookAuthor> bookAuthors;
        //    using (SqlConnection connection = new SqlConnection(CurrentConnection.ConnectionString))
        //    {
        //        bookAuthors = connection.Query<BookAuthor, Book, Author, BookAuthor>(query, (ba, book, author) =>
        //        {
        //            ba.Book = book;
        //            ba.BookId = book != null ? book.BookId : 0;
        //            ba.Author = author;
        //            ba.AuthorId = author != null ? author.AuthorId : 0;
        //            return ba;
        //        }, splitOn: "AuthorId").ToList();
        //    }
        //    return bookAuthors.ToList();
        //}
    }
}
