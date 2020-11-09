using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FilaCepac.Data
{
    public class FilaCepacContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public FilaCepacContext() : base("name=FilaCepacContext")
        {
        }

        public System.Data.Entity.DbSet<FilaCepac.Models.Status> Status { get; set; }

        public System.Data.Entity.DbSet<FilaCepac.Models.Setor> Setores { get; set; }

        public System.Data.Entity.DbSet<FilaCepac.Models.Fila> Filas { get; set; }

        public System.Data.Entity.DbSet<FilaCepac.Models.Sql> Sqls { get; set; }

        public System.Data.Entity.DbSet<FilaCepac.Models.Autorizacao> Autorizacoes { get; set; }

        public System.Data.Entity.DbSet<FilaCepac.Models.ArquivoFila> ArquivosFila { get; set; }

    }
}
