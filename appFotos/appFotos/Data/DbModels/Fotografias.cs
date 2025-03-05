using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace appFotos.Data.DbModels;

public class Fotografias
{
    [Key]
    public int Id { get; set; }
    
    public string Titulo { get; set; }
    
    public string Descricao { get; set; }
    
    public string Ficheiro { get; set; }
    
    public DateTime DataFotografia { get; set; }
    
    public decimal Preco { get; set; }
    
    // FK para Utilizador -> one to many
    [ForeignKey(nameof(Dono))]
    public int DonoFk { get; set; }
    public Utilizadores Dono { get; set; }
    
    // FK para Categoria -> one to many
    [ForeignKey(nameof(Categoria))]
    public int CategoriaFk { get; set; }
    public Categorias Categoria { get; set; }
    
    // many to many -> gostos
    public ICollection<Gostos> ListaGostos { get; set; }
    // many to many -> compras
    public ICollection<Compras> ListaCompras { get; set; }
    
}

