using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FilaCepac.Models
{
    [Table("ouc.tb_fila_autorizacao")]
    public class Autorizacao : Entity
    {

        [Column("nm_usuario")]
        public string Usuario { get; set; }

        [Column("dt_cadastro")]
        public DateTime Cadastro { get; set; }

        [Column("dt_desativacao")]
        public DateTime Desativacao { get; set; }

        [Column("bl_ativo")]
        public bool Ativo { get; set; }

    }
}