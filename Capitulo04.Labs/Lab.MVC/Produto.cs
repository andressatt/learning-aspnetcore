//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Lab.MVC
{
    using System;
    using System.Collections.Generic;
    
    public partial class Produto
    {
        public int ProdutoId { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public int CategoriaCategoriaId { get; set; }
    
        public virtual Categoria Categoria { get; set; }
    }
}
