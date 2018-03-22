using Library.EntityLayer.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Library.DataAccessLayer.SqlExpressions
{
    public static class BookSqlExpressions
    {
        public static string GetAllSqlExpression()
        {
            string sqlLeftJoinBookAuthor = string.Empty;
            string sqlLeftJoinAuthors = string.Empty;

            var bookType = typeof(Book);
            var authorType = typeof(Author);

            PropertyInfo[] bookProperties = bookType.GetProperties();
            PropertyInfo[] authorProperties = authorType.GetProperties();

            bookProperties = bookProperties.Where(x => x.PropertyType != typeof(ICollection<Author>)).ToArray();
            authorProperties = authorProperties.Where(x => x.PropertyType != typeof(ICollection<Book>)).ToArray();

            var sqlExpression = "SELECT ";
            for (int i = 0; i < bookProperties.Length; i++)
            {
                sqlExpression += $"{bookProperties[i].DeclaringType.Name}s.{bookProperties[i].Name}, ";
                if (i == 0)
                {
                    sqlLeftJoinBookAuthor = $"LEFT JOIN {bookType.Name}{authorType.Name} ON {bookProperties[i].DeclaringType.Name}s.{bookProperties[i].Name} = {bookType.Name}{authorType.Name}.{bookProperties[i].Name}";
                }
            }
            for (int i = 0; i < authorProperties.Length; i++)
            {
                sqlExpression += $"{authorProperties[i].DeclaringType.Name}s.{authorProperties[i].Name}" + (i != authorProperties.Length - 1 ? string.Format(", ") : string.Format(" "));
                if (i == 0)
                {
                    sqlLeftJoinAuthors = $"LEFT JOIN {authorType.Name}s ON {authorProperties[i].DeclaringType.Name}s.{authorProperties[i].Name} = {bookType.Name}{authorType.Name}.{authorProperties[i].Name}";
                }
            }
            sqlExpression += $"FROM {bookType.Name}s {sqlLeftJoinBookAuthor} {sqlLeftJoinAuthors};";

            return sqlExpression;
        }

        public static string GetByIdSqlExpression(int id)
        {
            string sqlLeftJoinBookAuthor = string.Empty;
            string sqlLeftJoinAuthors = string.Empty;
            string sqlWhereExpression = string.Empty;

            var bookType = typeof(Book);
            var authorType = typeof(Author);

            PropertyInfo[] bookProperties = bookType.GetProperties();
            PropertyInfo[] authorProperties = authorType.GetProperties();

            bookProperties = bookProperties.Where(x => x.PropertyType != typeof(ICollection<Author>)).ToArray();
            authorProperties = authorProperties.Where(x => x.PropertyType != typeof(ICollection<Book>)).ToArray();

            var sqlExpression = "SELECT ";
            for (int i = 0; i < bookProperties.Length; i++)
            {
                sqlExpression += $"{bookProperties[i].DeclaringType.Name}s.{bookProperties[i].Name}, ";
                if (i == 0)
                {
                    sqlLeftJoinBookAuthor = $"LEFT JOIN {bookType.Name}{authorType.Name} ON {bookProperties[i].DeclaringType.Name}s.{bookProperties[i].Name} = {bookType.Name}{authorType.Name}.{bookProperties[i].Name}";
                    sqlWhereExpression = $"WHERE {bookProperties[i].DeclaringType.Name}s.{bookProperties[i].Name} = {id}";
                }
            }
            for (int i = 0; i < authorProperties.Length; i++)
            {
                sqlExpression += $"{authorProperties[i].DeclaringType.Name}s.{authorProperties[i].Name}" + (i != authorProperties.Length - 1 ? string.Format(", ") : string.Format(" "));
                if (i == 0)
                {
                    sqlLeftJoinAuthors = $"LEFT JOIN {authorType.Name}s ON {authorProperties[i].DeclaringType.Name}s.{authorProperties[i].Name} = {bookType.Name}{authorType.Name}.{authorProperties[i].Name}";
                }
            }
            sqlExpression += $"FROM {bookType.Name}s {sqlLeftJoinBookAuthor} {sqlLeftJoinAuthors} {sqlWhereExpression};";

            return sqlExpression;
        }

        public static string InsertSqlExpression(Book book)
        {
            var bookType = typeof(Book);

            PropertyInfo[] bookProperties = bookType.GetProperties();

            bookProperties = bookProperties.Where(x => x.PropertyType != typeof(ICollection<Author>) && x.Name != bookProperties[0].Name).ToArray();

            var sqlExpression = $"INSERT INTO {bookType.Name}s(";
            var sqlValuesExpression = $"VALUES(";
            for (int i = 0; i < bookProperties.Length; i++)
            {
                sqlExpression += $"{bookProperties[i].Name}" + (i != bookProperties.Length - 1 ? string.Format(", ") : string.Format(") "));

                sqlValuesExpression += bookProperties[i].PropertyType == typeof(String) ?
                    $"'{book.GetType().GetProperty(bookProperties[i].Name).GetValue(book, null)}'" :
                    $"{book.GetType().GetProperty(bookProperties[i].Name).GetValue(book, null)}";
                sqlValuesExpression += i != bookProperties.Length - 1 ? string.Format(", ") : string.Format(");");
            }
            sqlValuesExpression += " SELECT SCOPE_IDENTITY();";

            return sqlExpression + sqlValuesExpression;
        }

        public static string UpdateSqlExpression(Book book)
        {
            var bookType = typeof(Book);

            PropertyInfo[] bookProperties = bookType.GetProperties();

            bookProperties = bookProperties.Where(x => x.PropertyType != typeof(ICollection<Author>)).ToArray();

            var sqlExpression = $"UPDATE {bookType.Name}s ";
            var sqlSetExpression = $"SET ";
            var sqlWhereExpression = $"WHERE ";
            for (int i = 0; i < bookProperties.Length; i++)
            {
                if (i == 0)
                {
                    sqlWhereExpression += $"{bookType.Name}s.{bookProperties[i].Name} = {book.BookId};";
                    continue;
                }
                sqlSetExpression += $"{bookProperties[i].Name} = ";
                sqlSetExpression += bookProperties[i].PropertyType == typeof(String) ?
                    $"'{book.GetType().GetProperty(bookProperties[i].Name).GetValue(book, null)}'" :
                    $"{book.GetType().GetProperty(bookProperties[i].Name).GetValue(book, null)}";
                sqlSetExpression += i != bookProperties.Length - 1 ? string.Format(", ") : string.Format(" ");
            }

            return sqlExpression + sqlSetExpression + sqlWhereExpression;
        }

        public static string DeleteSqlexpression(int id)
        {
            var bookType = typeof(Book);

            PropertyInfo[] bookProperties = bookType.GetProperties();

            bookProperties = bookProperties.Where(x => x.PropertyType != typeof(ICollection<Author>)).ToArray();

            var sqlExpression = $"DELETE FROM {bookType.Name}s WHERE ";
            for (int i = 0; i < bookProperties.Length; i++)
            {
                if (i == 0)
                {
                    sqlExpression += $"{bookProperties[i].Name} = {id};";
                    continue;
                }
            }

            return sqlExpression;
        }
    }
}
