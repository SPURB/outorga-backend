using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FilaCepac.Models
{
    [Table("ouc.tb_fila_sql_cepac")]
    public class Sql : Entity
    {
        [Column("nm_sql")]
        public string NumeroSql { get; set; }

        [Column("id_fila_cepac")]
        public int IdFilaCepac { get; set; }

    }
}
