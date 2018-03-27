using Library.DataAccessLayer.Connection;
using Library.DataAccessLayer.Interfaces;
using Library.EntityLayer.Models;

using Dapper.Contrib.Extensions;
using Dapper;

using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Library.DataAccessLayer.Repositories
{
    public class BookAuthorRepository : IRepository<BookAuthor>
    {
        public BookAuthorRepository()
        {
        }

        public void Insert(BookAuthor item)
        {
            using (SqlConnection connection = new SqlConnection(CurrentConnection.ConnectionString))
            {
                connection.Insert(item);
            }
        }

        public void Insert(List<BookAuthor> items)
        {
            using (SqlConnection connection = new SqlConnection(CurrentConnection.ConnectionString))
            {
                connection.Insert(items);
            }
        }

        public void Delete(BookAuthor item)
        {
            using (SqlConnection connection = new SqlConnection(CurrentConnection.ConnectionString))
            {
                connection.Delete(item);
            }
        }

        public void Delete(List<BookAuthor> items)
        {
            using (SqlConnection connection = new SqlConnection(CurrentConnection.ConnectionString))
            {
                connection.Delete(items);
            }
        }

        public BookAuthor Get(int id)
        {
            BookAuthor bookAuthor;
            using (SqlConnection connection = new SqlConnection(CurrentConnection.ConnectionString))
            {
                bookAuthor = connection.Get<BookAuthor>(id);
            }
            return bookAuthor;
        }

        public List<BookAuthor> GetAllByBookId(int id)
        {
            string query = $"SELECT BookAuthors.*, Books.*, Authors.* FROM Books LEFT JOIN BookAuthors on BookAuthors.BookId = Books.BookId LEFT JOIN Authors on Authors.AuthorId = BookAuthors.AuthorId WHERE Books.BookId = {id};";
            List<BookAuthor> result;
            using (SqlConnection connection = new SqlConnection(CurrentConnection.ConnectionString))
            {
                result = connection.Query<BookAuthor, Book, Author, BookAuthor>(query, (ba, book, author) =>
                {
                    ba.Book = book;
                    ba.BookId = book.BookId;
                    ba.Author = author;
                    ba.AuthorId = author != null ? author.AuthorId : 0;
                    return ba;
                }, splitOn: "AuthorId").ToList();

            }
            return result.ToList();
        }

        public List<BookAuthor> GetAll()
        {
            List<BookAuthor> booksAuthors;
            using (SqlConnection connection = new SqlConnection(CurrentConnection.ConnectionString))
            {
                booksAuthors = connection.GetAll<BookAuthor>().ToList();
            }
            return booksAuthors.ToList();
        }

        public void Update(BookAuthor item)
        {
            using (SqlConnection connection = new SqlConnection(CurrentConnection.ConnectionString))
            {
                connection.Update(item);
            }
        }

        public void Update(List<BookAuthor> items)
        {
            using (SqlConnection connection = new SqlConnection(CurrentConnection.ConnectionString))
            {
                connection.Update(items);
            }
        }
    }
}
