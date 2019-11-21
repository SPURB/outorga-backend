using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FilaCepac.Models
{
    [Table("ouc.tb_fila_arquivos")]
    public class ArquivoFila : Entity
    {
        [Column("nm_arquivo")]
        public string Nome { get; set; }

        [Column("nm_url")]
        public string Url { get; set; }

        [Column("id_fila_cepac")]
        [ForeignKey("FilaCepac")]
        public int IdFilaCepac { get; set; }
        public Fila FilaCepac { get; set; }

    }
}
