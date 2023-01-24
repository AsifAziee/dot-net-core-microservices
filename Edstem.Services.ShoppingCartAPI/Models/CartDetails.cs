using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Edstem.Services.ShoppingCartAPI.Models;

public class CartDetails
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CartDetailsId { get; set; }
    public int CartHeaderId { get; set; }
    
    [ForeignKey("CartHeaderId")]
    public virtual CartHeader CartHeader { get; set; }
    
    public int ProductId { get; set; }
    public int Count { get; set; }
}