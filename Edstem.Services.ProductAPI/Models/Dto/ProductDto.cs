using System.ComponentModel.DataAnnotations;

namespace Edstem.Services.ProductAPI.Models.Dto;

public class ProductDto
{
    public int ProductId { get; set; }
    [Required] public string ProductName { get; set; }
    [Range(1.0, 1000.0)] public double Price { get; set; }
    public string Description { get; set; }
    public string CategoryName { get; set; }
    public string ImageUrl { get; set; }
}