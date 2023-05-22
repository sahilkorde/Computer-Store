using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Computer_Store_Business.Repository;
using Computer_Store_Business.Repository.IRepository;
using Computer_Store_Models;

namespace Computer_Store_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        [HttpGet]
        public async Task<IActionResult> getAll(string? search=null, string? catid=null, int? minprice=null, int? maxprice=null)
        {
            List<int> catIds = new List<int>();
            if(catid != null) { 
                catIds = catid.Split(',').Select(int.Parse).ToList();
            }
            return Ok(await _productRepository.getAll(search, catIds, minprice, maxprice));
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> get(int? productId)
        {
            if(productId == null)
            {
                return BadRequest(new ErrorModelDTO()
                {
                    ErrorMessage = "Invalid Id",
                    StatusCode= StatusCodes.Status400BadRequest
                });
            }
            var product = await _productRepository.get(productId.Value);
            if(product == null)
            {
                return BadRequest(new ErrorModelDTO()
                {
                    ErrorMessage = "Invalid Id",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }
            return Ok(product);
        }
    }
}
