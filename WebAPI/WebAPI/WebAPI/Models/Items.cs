using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class Items
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Brand { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
