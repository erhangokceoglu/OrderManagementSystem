using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.API.Models;
using OrderManagementSystem.BLL.AbstractServices;
using OrderManagementSystem.BLL.DTOs.ProductDtos;
namespace OrderManagementSystem.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var products = await _productService.GetAllAsync();

            if (products == null || products.Count == 0)
            {
                return NotFound(ApiResponse<string>.NotFound("Ürünler bulunamadı."));
            }

            return Ok(ApiResponse<List<ProductDto>>.Ok(products, "Ürünler getirildi."));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse<string>.InternalError("Ürünler gelmedi." + ex.Message));
        }
    }
}

