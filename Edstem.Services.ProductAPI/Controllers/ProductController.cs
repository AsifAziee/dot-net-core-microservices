using AutoMapper;
using Edstem.Services.ProductAPI.Models;
using Edstem.Services.ProductAPI.Models.Dto;
using Edstem.Services.ProductAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Edstem.Services.ProductAPI.Controllers;

[ApiController]
[Route("api/products")]
public class ProductController : Controller
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<ProductController> _logger;

    public ProductController(IProductRepository productRepository, IMapper mapper, ILogger<ProductController> logger)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var products = await _productRepository.GetProductsAsync();
            var allProducts = _mapper.Map<List<ProductDto>>(products);
            _logger.LogInformation("All products are fetched");
            return Ok(allProducts);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error while getting product", ex);
            return BadRequest(ex.Message);
        }
    }
    

    [HttpGet("{productId}")]
    public async Task<IActionResult> Get([FromRoute] int productId)
    {
        try 
        {
            var product = await _productRepository.GetProductAsync(productId);

            if (product == null)
            {
                return NotFound("No record found");
            }

            var productResponse = _mapper.Map<ProductDto>(product);
            _logger.LogInformation($"The details of the product with id  {productId} is fetched ");
            return Ok(productResponse);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error while getting product {productId}", ex);
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProductDto productDto)
    {
        try
        {
            var product = _mapper.Map<Product>(productDto);
            var success = await _productRepository.CreateProductAsync(product);
            if (!success)
            {
                return Problem("Error while creating product");
            }

            var productResponse = _mapper.Map<ProductDto>(product);
            var url = "api/products/" + productResponse.ProductId;
            _logger.LogInformation($"Product with id {productResponse.ProductId} is created");
            return Created(url, productResponse);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error while adding a product", ex);
            return BadRequest(ex.Message);
        }
    }



    [HttpDelete("{productId}")]
    public async Task<IActionResult> Delete([FromRoute] int productId)
    {
        try
        {
            var success = await _productRepository.DeleteAsync(productId);

            if (!success)
            {
                return NotFound("No product found");
            }
            _logger.LogInformation($"Product with id {productId} is deleted");
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError("Error while deleting the product", ex);
            return BadRequest(ex.Message);
        }
    }


    [HttpPatch("{productId}")]
    public async Task<IActionResult> Update([FromRoute] int productId,
        [FromBody] ProductDto productDto)
    {
        try
        {
            var product = _mapper.Map<Product>(productDto);
            var productExist = await _productRepository.UpdateProductAsync(productId, product);
            if (!productExist)
            {
                return NotFound("No record found to update");
            }
            _logger.LogInformation($"Product with id {productId} is updated");
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError("Error while updating the product", ex);
            return BadRequest(ex.Message);
        }
    }
}