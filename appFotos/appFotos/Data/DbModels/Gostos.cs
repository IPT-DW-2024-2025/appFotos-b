using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace appFotos.Data.DbModels;

[PrimaryKey(nameof(UtilizadorFk), nameof(FotografiaFk))]
public class Gostos
{
    
    public DateTime Data { get; set; }
    
    [ForeignKey(nameof(Utilizador))]
    public int UtilizadorFk { get; set; }
    public Utilizadores Utilizador { get; set; }
    
    [ForeignKey(nameof(Fotografia))]
    public int FotografiaFk { get; set; }
    public Fotografias Fotografia { get; set; }

}