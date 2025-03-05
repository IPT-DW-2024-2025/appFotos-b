namespace appFotos.Data.DbModels;

public class Fotografias
{
    public int Id { get; set; }
    
    public string Titulo { get; set; }
    
    public string Descricao { get; set; }
    
    public string Ficheiro { get; set; }
    
    public DateTime DataFotografia { get; set; }
    
    public decimal Preco { get; set; }
    
}