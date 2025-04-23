using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace appFotos.Models;


/// <summary>
/// utilizadores não anónimos da aplicação
/// </summary>
public class Utilizadores
{
    /// <summary>
    /// identificador único do utilizador
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// nome do utilizador
    /// </summary>
    [Required(ErrorMessage = "O {0} é de preenchimento obrigatório.")] 
    public string Nome { get; set; }
    
    /// <summary>
    /// número de identificação fiscal do utilizador 
    /// </summary>
    [Required(ErrorMessage = "O {0} é de preenchimento obrigatório.")] 
    [RegularExpression("([1-9])[0-9]{8}", ErrorMessage = "O {0} tem de seguir o formato de Portugal.")] 
    public string NIF { get; set; }
    
    /// <summary>
    /// número de telemóvel do utilizador
    /// </summary>
    [Display(Name = "Telemóvel")]
    [Required(ErrorMessage = "O {0} é de preenchimento obrigatório.")] 
    [RegularExpression("([+]|00)?[0-9]{6,17}", ErrorMessage = "O {0} só pode conter digitos. No mínimo 6.")] 
    public string Telemovel { get; set; }
    
    /// <summary>
    /// Morada do utilizador
    /// </summary>
    //[Required(ErrorMessage = "A {0} é de preenchimento obrigatório.")] 
    public string? Morada { get; set; }
    
    /// <summary>
    /// Código Postal da  morada do utilizador
    /// </summary>
    [Display(Name = "Código Postal")]
    //[Required(ErrorMessage = "O {0} é de preenchimento obrigatório.")] 
    [RegularExpression("[1-9][0-9]{3}[-|\\s][0-9]{3}", ErrorMessage = "O {0} tem de seguir o formato xxxx-xxx")] 
    public string? CodPostal { get; set; }
    
    /// <summary>
    /// País da morada do utilizador
    /// </summary>
    [Display(Name = "País")]
    public string? Pais { get; set; }
    
    [StringLength(50)] 
    public string? IdentityUserName { get; set; }
    
    /* *************************
     * Definção dos relacionamentos
     * **************************
     */

    /// <summary>
    /// Lista das fotografias que são propriedade do 
    /// utilizador
    /// </summary>
    public ICollection<Fotografias> ListaFotos { get; set; } = [];

    /// <summary>
    /// Lista dos 'gostos' de fotografias do utilizador
    /// </summary>
    public ICollection<Gostos> ListaGostos { get; set; } = [];

    /// <summary>
    /// Lista das fotografias compradas pelo utilizador
    /// </summary>
    public ICollection<Compras> ListaCompras { get; set; } = [];
}