namespace appFotos.Data.DbModels;

public class Utilizadores
{
    public int Id { get; set; }
    
    public string Nome { get; set; }
    
    public string NIF { get; set; }
    
    public string Telemovel { get; set; }
    
    public string Morada { get; set; }
    
    public string CodPostal { get; set; }
    
    public string Pais { get; set; }
    
    public ICollection<Fotografias> ListaFotos { get; set; }
    
    public ICollection<Gostos> ListaGostos { get; set; }
}