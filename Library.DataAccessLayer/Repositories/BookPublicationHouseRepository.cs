using Library.DataAccessLayer.Connection;
using Library.EntityLayer.Models;

using Dapper;

using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Library.DataAccessLayer.Repositories
{
    public class BookPublicationHouseRepository : GenericRepository<BookPublicationHouse>
    {
        public override List<BookPublicationHouse> GetAllById<TModel>(int id)
        {
            string query = $"SELECT BookPublicationHouses.*, Books.*, PublicationHouses.* FROM Books LEFT JOIN BookPublicationHouses on BookPublicationHouses.BookId = Books.BookId LEFT JOIN PublicationHouses on PublicationHouses.PublicationHouseId = BookPublicationHouses.PublicationHouseId WHERE Books.BookId = {id};";
            List<BookPublicationHouse> booksPublicationHouses;
            using (SqlConnection connection = new SqlConnection(CurrentConnection.ConnectionString))
            {
                booksPublicationHouses = connection.Query<BookPublicationHouse, Book, PublicationHouse, BookPublicationHouse>(query, (bph, book, publicationHouse) =>
                {
                    bph.Book = book;
                    bph.BookId = book.BookId;
                    bph.PublicationHouse = publicationHouse;
                    bph.PublicationHouseId = publicationHouse != null ? publicationHouse.PublicationHouseId : 0;
                    return bph;
                }, splitOn: "PublicationHouseId").ToList();

            }
            return booksPublicationHouses.ToList();
        }
    }
}
