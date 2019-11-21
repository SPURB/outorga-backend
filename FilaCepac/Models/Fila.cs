using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilaCepac.Models
{

    [Table("ouc.tb_fila_cepac")]
    public class Fila : Entity
    {

        [Column("dt_cadastro")]
        public DateTime Data { get; set; }
        
        [Column("nm_sub_setor")]
        public string SubSetor { get; set; }

        [Column("nr_certidao")]
        public string Certidao { get; set; }

        [Column("nm_interessado")]
        public string Interessado { get; set; }

        [Column("nm_processo_licenciamento")]
        public string Licenciamento { get; set; }

        [Column("nm_processo_sei")]
        public string Sei { get; set; }

        [Column("nr_area_ad_residencial")]
        public decimal? AreaAdResidencial { get; set; }

        [Column("nr_area_ad_n_residencial")]
        public decimal? AreaAdNaoResidencial { get; set; }

        [Column("nr_cepac_area_adicional")]
        public int? CepacAreaAdicional { get; set; }

        [Column("nr_cepac_mod_uso")]
        public int? CepacModUso { get; set; }

        [Column("nm_email")]
        public string Email { get; set; }

        [Column("nm_telefone")]
        public string Telefone { get; set; }

        [Column("nm_procurador")]
        public string Procurador { get; set; }

        [Column("nr_cepac_objeto")]
        public int? CepacObjeto { get; set; }

        [Column("nm_endereco")]
        public string Endereco { get; set; }

        [Column("nr_area_terreno")]
        public string AreaTerreno { get; set; }

        [Column("nm_zona")]
        public string Zona { get; set; }

        [Column("nm_uso_pretendido")]
        public string Uso { get; set; }

        [Column("nr_ca_projeto")]
        public string CAProjeto { get; set; }

        [Column("nm_obs")]
        public string Obs { get; set; }

        [Column("nm_codigo")]
        public string CodigoProposta { get; set; }

        [Column("nm_usuario_alteracao")]
        public string UsuarioAlteracao { get; set; }

        [Column("dt_alteracao_registro")]
        public DateTime? DataAlteracao { get; set; }

        [Column("id_status")]
        [ForeignKey("Status")]
        public int? IdStatus { get; set; }
        public Status Status { get; set; }

        [Column("id_setor")]
        [ForeignKey("SetorObj")]
        public int? IdSetor { get; set; }
        public Setor SetorObj { get; set; }


        //public ICollection<Sql> Sqls { get; set; }

    }
}
