using Library.DataAccessLayer.Connection;
using Library.DataAccessLayer.Interfaces;
using Library.DataAccessLayer.SqlExpressions;
using Library.EntityLayer.Models;

using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace Library.DataAccessLayer.Repositories
{
    public class BookRepository : IRepository<Book>
    {
        private SqlConnection _connection;

        public BookRepository()
        {
            _connection = new SqlConnection(CurrentConnection.ConnectionString);
        }

        public List<Book> GetList()
        {
            _connection.Open();

            var sqlExpression = BookSqlExpressions.GetAllSqlExpression();

            SqlCommand command = new SqlCommand(sqlExpression, _connection);
            SqlDataReader reader = command.ExecuteReader();

            List<Book> allBooks = new List<Book>();
            if (reader.HasRows)
            {
                Author author;

                while (reader.Read())
                {
                    var bookId = reader.GetInt32(0);
                    var bookName = !reader.IsDBNull(1) ? reader.GetString(1) : string.Empty;
                    var yearOfPublishing = !reader.IsDBNull(2) ? reader.GetInt32(2) : 0;
                    var authorId = !reader.IsDBNull(3) ? reader.GetInt32(3) : 0;
                    var authorName = !reader.IsDBNull(4) ? reader.GetString(4) : string.Empty;
                    var birthday = !reader.IsDBNull(5) ? reader.GetInt32(5) : 0;
                    var deathday = !reader.IsDBNull(6) ? reader.GetInt32(6) : 0;

                    var book = allBooks.FirstOrDefault(x => x.BookId == bookId);
                    if (book != null)
                    {
                        author = new Author() { AuthorId = authorId, Name = authorName, Birthday = birthday, Deathday = deathday };

                        if (!book.Authors.Contains(author))
                        {
                            book.Authors.Add(author);
                        }
                        continue;
                    }
                    book = new Book();
                    book.BookId = bookId;
                    book.Name = bookName;
                    book.YearOfPublishing = yearOfPublishing;

                    author = new Author() { AuthorId = authorId, Name = authorName, Birthday = birthday, Deathday = deathday };

                    book.Authors = new List<Author>();
                    book.Authors.Add(author);

                    allBooks.Add(book);
                }
            }

            _connection.Close();

            return allBooks;
        }

        public Book GetByid(int id)
        {
            _connection.Open();

            var sqlExpression = BookSqlExpressions.GetByIdSqlExpression(id);
            SqlCommand command = new SqlCommand(sqlExpression, _connection);
            SqlDataReader reader = command.ExecuteReader();

            Book book = new Book();
            if (reader.HasRows)
            {
                Author author;
                Book currentBook = null;
                while (reader.Read())
                {
                    var bookId = reader.GetInt32(0);
                    var bookName = !reader.IsDBNull(1) ? reader.GetString(1) : string.Empty;
                    var yearOfPublishing = !reader.IsDBNull(2) ? reader.GetInt32(2) : 0;
                    var authorId = !reader.IsDBNull(3) ? reader.GetInt32(3) : 0;
                    var authorName = !reader.IsDBNull(4) ? reader.GetString(4) : string.Empty;
                    var birthday = !reader.IsDBNull(5) ? reader.GetInt32(5) : 0;
                    var deathday = !reader.IsDBNull(6) ? reader.GetInt32(6) : 0;

                    if (currentBook != null)
                    {
                        author = new Author() { AuthorId = authorId, Name = authorName, Birthday = birthday, Deathday = deathday };

                        if (!currentBook.Authors.Contains(author))
                        {
                            currentBook.Authors.Add(author);
                        }
                        continue;
                    }
                    currentBook = new Book();
                    currentBook.BookId = bookId;
                    currentBook.Name = bookName;
                    currentBook.YearOfPublishing = yearOfPublishing;

                    author = new Author() { AuthorId = authorId, Name = authorName, Birthday = birthday, Deathday = deathday };

                    currentBook.Authors = new List<Author>();
                    currentBook.Authors.Add(author);
                }

                book = currentBook;
            }

            _connection.Close();

            return book;
        }

        public void Create(Book book)
        {
            _connection.Open();

            var sqlExpression = BookSqlExpressions.InsertSqlExpression(book);
            SqlCommand command = new SqlCommand(sqlExpression, _connection);
            var bookId = command.ExecuteScalar();

            foreach (var author in book.Authors)
            {
                sqlExpression = $"INSERT INTO BookAuthor (AuthorId, BookId) VALUES({author.AuthorId}, {bookId});";
                command.CommandText = sqlExpression;
                command.ExecuteNonQuery();
            }

            _connection.Close();
        }

        public void Update(Book book)
        {
            _connection.Open();

            var sqlExpression = BookSqlExpressions.UpdateSqlExpression(book);
            SqlCommand command = new SqlCommand(sqlExpression, _connection);
            command.ExecuteNonQuery();

            sqlExpression = $"DELETE FROM BookAuthor WHERE BookId = {book.BookId};";
            command.CommandText = sqlExpression;
            command.ExecuteNonQuery();

            foreach (var author in book.Authors)
            {
                sqlExpression = $"INSERT INTO BookAuthor (AuthorId, BookId) VALUES({author.AuthorId}, {book.BookId});";
                command.CommandText = sqlExpression;
                command.ExecuteNonQuery();
            }

            _connection.Close();
        }

        public void Delete(int id)
        {
            _connection.Open();

            var sqlExpression = $"DELETE FROM Books WHERE BookId IN ({id});";
            SqlCommand command = new SqlCommand(sqlExpression, _connection);
            command.ExecuteNonQuery();

            _connection.Close();
        }
    }
}
