using System.ComponentModel.DataAnnotations.Schema;

namespace appFotos.Data.DbModels;

public class Compras
{
    public int Id { get; set; }
    
    public DateTime Data { get; set; }
    
    public EstadosCompras Estado  { get; set; }
    
    public ICollection<Fotografias> ListaFotografias { get; set; }
    
    [ForeignKey(nameof(Comprador))]
    public int CompradorId { get; set; }
    public Utilizadores Comprador { get; set; }
}

public enum EstadosCompras
{
    iniciada,
    paga,
    enviada,
    concluida
}