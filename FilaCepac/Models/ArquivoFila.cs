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
        //API Externa arquivos
        [Column("id_arquivo")]
        public string IdArquivo { get; set; }

        [Column("id_fila_cepac")]
        public int IdFilaCepac { get; set; }

    }
}
