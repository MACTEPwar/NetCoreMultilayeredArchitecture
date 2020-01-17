using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LSG.GenericCrud.Models;

namespace DAL.Models.Entitys
{
    [Table(name: "t_contact", Schema = "public")]
    public class Contact
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(250)]
        [Required]
        public string Address { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(18)]
        public string Phone { get; set; }

        [Required]
        public int AccountId { get; set; }
        public virtual Account Account { get; set; }
    }
}
