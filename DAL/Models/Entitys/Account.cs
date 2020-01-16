using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LSG.GenericCrud.Models;

namespace DAL.Models.Entitys
{
    [Table(name: "t_account", Schema = "public")]
    public class Account : IEntity
    {
        public Account()
        {
            Id = Guid.NewGuid();
        }
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
