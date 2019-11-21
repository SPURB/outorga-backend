using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FilaCepac.Models
{
    [Table("ouc.tb_fila_status")]
    public class Status : Entity
    {
        [Column("nm_status")]
        public string Nome { get; set; }


    }
}
