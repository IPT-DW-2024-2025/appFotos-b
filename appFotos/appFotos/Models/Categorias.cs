using System.ComponentModel.DataAnnotations;

namespace appFotos.Models;

/// <summary>
/// categorias a que as fotografias podem ser associadas
/// </summary>
public class Categorias
{
    /// <summary>
    /// Identificador da categoria
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Nome da categoria que será associada às fotografias
    /// </summary>
    [Display(Name = "Descrição")]
    [Required(ErrorMessage = "A {0} é de preenchimento obrigatório.")] 
    [StringLength(20, ErrorMessage = "A {0} deve ter um máximo de {1} caracteres.")]
    public string Categoria { get; set; }
    
    /* *************************
     * Definção dos relacionamentos
     * **************************
     */

    /// <summary>
    /// Lista das fotografias associadas a uma categoria
    /// </summary>
    public ICollection<Fotografias> ListaFotografias { get; set; } = new List<Fotografias>();
}