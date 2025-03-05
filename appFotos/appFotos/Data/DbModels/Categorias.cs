namespace appFotos.Data.DbModels;

public class Categorias
{
    public int Id { get; set; }
    
    public string Categoria { get; set; }
    
    public ICollection<Fotografias> ListaFotografias { get; set; }
}