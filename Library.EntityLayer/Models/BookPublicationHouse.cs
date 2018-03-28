using Dapper.Contrib.Extensions;

namespace Library.EntityLayer.Models
{
    public class BookPublicationHouse
    {
        [Key]
        public int BookPublicationHouseId { get; set; }
        public int BookId { get; set; }
        [Write(false)]
        public virtual Book Book { get; set; }
        public int PublicationHouseId { get; set; }
        [Write(false)]
        public virtual PublicationHouse PublicationHouse { get; set; }
    }
}
