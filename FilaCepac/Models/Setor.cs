using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FilaCepac.Models
{
    [Table("ouc.tb_setor")]
    public class Setor
    {
        [Key]
        [Column("id_setor")]
        public int Id { get; set; }

        [Column("id_operacao_urbana")]
        [ForeignKey("OperacaoUrbana")]
        public int? IdOperacaoUrbana { get; set; }
        public OperacaoUrbana OperacaoUrbana { get; set; }

        [Column("nm_nome")]
        public string Nome { get; set; }
    }
}