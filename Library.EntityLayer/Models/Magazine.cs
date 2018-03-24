using Dapper.Contrib.Extensions;

namespace Library.EntityLayer.Models
{
    public class Magazine
    {
        [Key]
        public int MagazineId { get; set; }
        public string Name { get; set; }
        public int? Number { get; set; }
        public int? YearOfPublishing { get; set; }
    }
}
