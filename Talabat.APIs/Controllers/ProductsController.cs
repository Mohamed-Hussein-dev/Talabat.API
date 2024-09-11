using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.APIs.Helper;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Core.Specifications;


namespace Talabat.APIs.Controllers
{

    public class ProductsController : APIBaseController
    {
        private readonly IGenarciRepository<Product> _productRepo;
        private readonly IGenarciRepository<ProductType> _typeRepo;
        private readonly IGenarciRepository<ProductBrand> _brandRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenarciRepository<Product> ProductRepo ,
                                  IGenarciRepository<ProductType> TypeRepo,
                                  IGenarciRepository<ProductBrand> BrandRepo, IMapper mapper)
        {
            _productRepo = ProductRepo;
            _typeRepo = TypeRepo;
            _brandRepo = BrandRepo;
            _mapper = mapper;
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturn>>> GetAllProductsAsync([FromQuery]ProdcutSpecParams Params)
        {
            var productSpac = new ProductSpacifications(Params);

            productSpac.IncludeBrand().IncludeType();

            var ret = await _productRepo.GetAllWithSpecAsync(productSpac);

            var products = _mapper.Map<IReadOnlyList<ProductToReturn>>(ret);

            var ProductCountSpec = new ProductFilterationCountAsync(Params);
            var CountProduct = await _productRepo.GetCountWithSpecAsync(ProductCountSpec);

            return Ok(new Pagination<ProductToReturn>(Params.PageSize , Params.PageIndex , products , CountProduct));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductToReturn), 200)]
        [ProducesResponseType(typeof(ApiResponse) , StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturn>> GetByIdAsync(int id)
        {
            var productSpac = new ProductSpacifications(P => P.Id == id);

            productSpac.IncludeBrand().IncludeType();

            var ret = await _productRepo.GetByIdWithSpecAsync(productSpac);
            if (ret is null) return NotFound(new ApiResponse(404));
            var mappedProduct = _mapper.Map<ProductToReturn>(ret);
            return Ok(mappedProduct);
        }


        [HttpGet("Types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> getTypes()
        {
            var Types = await _typeRepo.GetAllAsync();
            return Ok(Types);
        }

        [HttpGet("Brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> getBrands()
        {
            var Brands = await _brandRepo.GetAllAsync();
            return Ok(Brands);
        }
       
    }
}
