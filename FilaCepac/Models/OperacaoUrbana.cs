using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FilaCepac.Models
{
    [Table("ouc.tb_operacao_urbana")]
    public class OperacaoUrbana
    {
        [Key]
        [Column("id_operacao_urbana")]
        public int IdOperacaoUrbana { get; set; }

        [Column("nm_nome")]
        public string Nome { get; set; }

    }
}