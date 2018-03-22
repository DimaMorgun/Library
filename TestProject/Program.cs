using Library.EntityLayer.Models;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace TestProject
{
    class Program
    {
        static void Main(string[] args)
        {
            int id = 1;
            var book = new Book() { BookId = 1, Name = "MippleSoft", YearOfPublishing = 2019 };
            //---
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

            //return sqlExpression;
            //---

            Console.WriteLine(sqlExpression);

            Console.ReadKey();
        }
    }
}
