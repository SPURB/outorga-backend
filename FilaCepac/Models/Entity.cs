using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilaCepac.Models
{
    public class Entity
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
    }
}
