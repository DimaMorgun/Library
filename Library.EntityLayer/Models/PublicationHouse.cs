using Dapper.Contrib.Extensions;

using System.Collections.Generic;

namespace Library.EntityLayer.Models
{
    public class PublicationHouse
    {
        [Key]
        public int PublicationHouseId { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
    }
}
