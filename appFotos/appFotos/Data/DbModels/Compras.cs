namespace appFotos.Data.DbModels;

public class Compras
{
    public int Id { get; set; }
    
    public DateTime Data { get; set; }
    
    public EstadosCompras Estado  { get; set; }
    
}

public enum EstadosCompras
{
    iniciada,
    paga,
    enviada,
    concluida
}