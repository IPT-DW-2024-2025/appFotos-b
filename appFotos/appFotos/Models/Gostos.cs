using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace appFotos.Models;


/// <summary>
/// regista os 'gostos' que um utilizador da app tem
/// pelas fotografias existentes
/// </summary>

[PrimaryKey(nameof(UtilizadorFk), nameof(FotografiaFk))]
public class Gostos
{
    /// <summary>
    /// data em que o utilizador marcou que 
    /// 'gosta' de uma fotografia
    /// </summary>
    public DateTime Data { get; set; }
    
    
    /* *************************
     * Definção dos relacionamentos
     * **************************
     */

    // Relacionamentos 1-N

    /// <summary>
    /// FK para referenciar o utilizador que gosta da fotografia
    /// </summary>
    [ForeignKey(nameof(Utilizador))]
    public int UtilizadorFk { get; set; }
    
    /// <summary>
    ///  FK para referenciar o utilizador que gosta da fotografia
    /// </summary>
    public Utilizadores Utilizador { get; set; }
    
    /// <summary>
    /// FK para a fotografia que o utilizador gostou
    /// </summary>
    [ForeignKey(nameof(Fotografia))]
    public int FotografiaFk { get; set; }
    
    /// <summary>
    /// FK para a fotografia que o utilizador gostou
    /// </summary>
    public Fotografias Fotografia { get; set; }
}