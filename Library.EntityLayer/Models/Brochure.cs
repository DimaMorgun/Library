using System.ComponentModel.DataAnnotations;

namespace Library.EntityLayer.Models
{
    public class Brochure
    {
        [Key]
        public int BrochureId { get; set; }
        public string Name { get; set; }
        public string TypeOfCover { get; set; }
        public int? NumberOfPages { get; set; }
    }
}
