using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace appFotos.Models;



/// <summary>
/// Fotografias disponíveis na aplicação
/// </summary>
public class Fotografias
{
    /// <summary>
    /// identificador da fotografia
    ///
    /// NOTA: [Key] -> é necessário para atributos com nome != 'Id' que é automaticamente reconhecido pela Entity
    /// </summary>
    [Key]
    public int Id { get; set; }
    
    
    /// <summary>
    /// Título da fotografia
    /// </summary>
    [Display(Name = "Título")]
    [Required(ErrorMessage = "O {0} é de preenchimento obrigatório.")]
    public string Titulo { get; set; }
    
    /// <summary>
    /// Descrição da fotografia
    /// </summary>
    [Display(Name = "Descrição")]
    [Required(ErrorMessage = "A {0} é de preenchimento obrigatório.")]
    public string Descricao { get; set; }
    
    // <summary>
    /// Nome do ficheiro da fotografia no disco rígido
    /// do servidor
    /// </summary>
    public string Ficheiro { get; set; }
    
    /// <summary>
    /// Data em que a fotografia foi tirada
    /// </summary>
    [Display(Name = "Data da Fotografia")]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)] 
    [DataType(DataType.Date)] 
    public DateTime DataFotografia { get; set; }
    
    /// <summary>
    /// Preço de venda da fotografia
    /// </summary>
    public decimal Preco { get; set; }
    
    /// <summary>
    /// Variável auxiliar usada para validar o preço inserido pelo utilizador
    /// </summary>
    [Required(ErrorMessage = "A {0} é de preenchimento obrigatório.")] 
    [Display(Name = "Preço")] 
    [StringLength(9)] 
    [RegularExpression("[0-9]{1,6}([,.][0-9]{1,2})?", ErrorMessage = "Escreva um número com, no máximo 2 casas decimais, separadas por . ou ,")] 
    [NotMapped]
    public string PrecoAux { get; set; }
    
    
    /* *************************
     * Definção dos relacionamentos
     * **************************
     */
    
    /// <summary>
    /// FK para referenciar o Dono da fotografia
    /// </summary>
    [Display(Name = "Dono")]
    [ForeignKey(nameof(Dono))]
    public int DonoFk { get; set; }
    
    /// <summary>
    /// FK para referenciar o Dono da fotografia
    /// </summary>
    public Utilizadores Dono { get; set; }
    
    /// <summary>
    /// FK para a tabela das Categorias
    /// </summary>
    [Display(Name = "Categoria")]
    [ForeignKey(nameof(Categoria))]
    public int CategoriaFk { get; set; }
    
    /// <summary>
    /// FK para as Categorias
    /// </summary>
    public Categorias Categoria { get; set; }
    
    
    // M-N


    /// <summary>
    // /// Lista de 'gostos' de uma fotografia
    // /// </summary>
    public ICollection<Gostos> ListaGostos { get; set; } = [];

    /// <summary>
    /// Lista de 'compras' que compraram a fotografia
    /// </summary>
    public ICollection<Compras> ListaCompras { get; set; } = [];

}

