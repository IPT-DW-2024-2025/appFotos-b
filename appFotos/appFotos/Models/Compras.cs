using System.ComponentModel.DataAnnotations.Schema;

namespace appFotos.Models;


/// <summary>
/// compras efetuadas por um utilizador
/// </summary>
public class Compras
{
    /// <summary>
    /// Identificador da compra
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Data da compra
    /// </summary>
    public DateTime Data { get; set; }
    
    /// <summary>
    /// Estado da compra.
    /// Representa um conjunto de valores pre-determinados
    /// que representam a evolução da 'compra'
    /// </summary>
    public EstadosCompras Estado  { get; set; }
    
    /* *************************
     * Definção dos relacionamentos
     * **************************
     */
    
    /// <summary>
    /// Lista das fotografias compradas pelo utilizador
    /// </summary> 
    public ICollection<Fotografias> ListaFotografias { get; set; }
    
    // Relacionamentos 1-N

    /// <summary>
    /// FK para referenciar o comprador da fotografia
    /// </summary>
    [ForeignKey(nameof(Comprador))]
    public int CompradorId { get; set; }
    
    /// <summary>
    /// FK para referenciar o Comprador da fotografia
    /// </summary>
    public Utilizadores Comprador { get; set; }
}

// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/enum
/// <summary>
/// Estados associados a uma 'compra'
/// </summary>
public enum EstadosCompras
{
    iniciada,
    paga,
    enviada,
    concluida
}