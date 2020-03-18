using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FilaCepac.Models
{
    public class EstoqueSetor
    {

        public EstoqueSetor() { }

        public int Setor { get; set; }

        public decimal? AreaAdicionalR { get; set; }

        public decimal? AreaAdicionalNR { get; set; }

        public int? CepacACA { get; set; }

        public int? CepacUsoParam { get; set; }

        public int IdStatus { get; set; }

        public DateTime? Atualizacao { get; set; }

    }
}