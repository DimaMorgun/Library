using Library.DataAccessLayer.Connection;
using Library.DataAccessLayer.Context;
using Library.DataAccessLayer.Interfaces;
using Library.EntityLayer.Models;

using Dapper.Contrib.Extensions;

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Library.ViewModelLayer.ViewModels;

namespace Library.DataAccessLayer.Repositories
{
    public class BookAuthorRepository : IRepository<BookAuthor>
    {
        private SqlConnection _connection;
        private LibraryDataAccessContext _context;

        public BookAuthorRepository()
        {
            _connection = new SqlConnection(CurrentConnection.ConnectionString);
            _context = new LibraryDataAccessContext();
        }

        public void Insert(BookAuthor item)
        {
            _connection.Insert(item);
        }

        public void Insert(List<BookAuthor> items)
        {
            _connection.Insert(items);
        }

        public void Delete(BookAuthor item)
        {
            _connection.Delete(item);
        }

        public BookAuthor Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<BookAuthor> GetAllByBookId(int id)
        {
            string query = $"SELECT * FROM BookAuthors LEFT JOIN Books on Books.BookId = BookAuthors.BookId LEFT JOIN Authors on Authors.AuthorId = BookAuthors.AuthorId WHERE Books.BookId = {id};";
            using (_connection)
            {
                var result = _connection.Query<BookAuthor, Book, Author, BookAuthor>(query, (ba, book, author) =>
                {
                    ba.Book = book;
                    ba.Author = author;
                    return ba;
                }, splitOn: "AuthorId");
                return result.ToList();
            }
        }

        public List<BookAuthor> GetAll()
        {
            var items = _connection.GetAll<BookAuthor>();
            return items.ToList();
        }

        public void Update(BookAuthor item)
        {
            _connection.Update(item);
        }

        public void Update(List<BookAuthor> items)
        {
            _connection.Update(items);
        }
    }
}
