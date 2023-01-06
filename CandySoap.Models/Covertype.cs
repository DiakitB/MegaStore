using System.ComponentModel.DataAnnotations;

namespace CandySoap.Models
{
    public class Covertype
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Cover Type")]
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}

