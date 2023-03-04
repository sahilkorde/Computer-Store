using AutoMapper;
using Computer_Store_Business.Repository.IRepository;
using Computer_Store_DataAccess;
using Computer_Store_DataAccess.Data;
using Computer_Store_Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computer_Store_Business.Repository
{
    public class ProductPriceRepository : IProductPriceRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public ProductPriceRepository(ApplicationDbContext db, IMapper mapper)
        { 
            _db= db;
            _mapper= mapper;
        }
        public async Task<ProductPriceDTO> Create(ProductPriceDTO objDTO)
        {
            var ProductPrice = _mapper.Map<ProductPriceDTO, ProductPrice>(objDTO);
            var addedProductPrice = _db.ProductPrices.Add(ProductPrice);
            await _db.SaveChangesAsync();
            return _mapper.Map<ProductPrice, ProductPriceDTO>(addedProductPrice.Entity);
        }

        public async Task<int> Delete(int Id)
        {
            var ProductPrice = await _db.ProductPrices.FirstOrDefaultAsync(u => u.Id== Id);
            if (ProductPrice!=null)
            {
                _db.ProductPrices.Remove(ProductPrice);
                await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<ProductPriceDTO> Get(int Id)
        {
            var ProductPrice = await _db.ProductPrices.FirstOrDefaultAsync(u => u.Id == Id);
            if (ProductPrice != null)
            {
                return _mapper.Map<ProductPrice, ProductPriceDTO>(ProductPrice);
            }
            return new ProductPriceDTO();
        }

        public async Task<IEnumerable<ProductPriceDTO>> GetAll(int? id=null)
        {
            if (id != null && id >0)
            {
                return _mapper.Map<IEnumerable<ProductPrice>, IEnumerable<ProductPriceDTO>>(_db.ProductPrices.Where(u => u.ProductId == id));
            }
            else
            {
                return _mapper.Map<IEnumerable<ProductPrice>, IEnumerable<ProductPriceDTO>>(_db.ProductPrices);
            }
        }

        public async Task<ProductPriceDTO> Update(ProductPriceDTO objDTO)
        {
            var ProductPrice = await _db.ProductPrices.FirstOrDefaultAsync(u => u.Id == objDTO.Id);
            if (ProductPrice != null)
            {
                ProductPrice.Price = objDTO.Price;
                ProductPrice.Varient = objDTO.Varient;
                ProductPrice.ProductId = objDTO.ProductId;
                _db.ProductPrices.Update(ProductPrice);
                await _db.SaveChangesAsync();
                return _mapper.Map<ProductPrice, ProductPriceDTO>(ProductPrice);
            }
            return objDTO;
        }
    }
}
