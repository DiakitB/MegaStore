using System.ComponentModel.DataAnnotations;

namespace CandySoap.Models
{
    public class Covertypes
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
