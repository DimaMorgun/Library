using Library.EntityLayer.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TestProject
{
    class Program
    {
        static void Main(string[] args)
        {
            var book = new Book() { BookId = 1, Name = "MippleSoft", YearOfPublishing = 2019 };
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

            Console.WriteLine(sqlExpression + sqlValuesExpression);

            Console.ReadKey();
        }
    }
}
