using Computer_Store_Business.Repository.IRepository;
using Computer_Store_Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Computer_Store_DataAccess;
using Computer_Store_DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace Computer_Store_Business.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<ProductDTO> create(ProductDTO objDTO)
        {
            var product = _mapper.Map<ProductDTO, Product>(objDTO);
            var addedproduct = _db.Products.Add(product);
            await _db.SaveChangesAsync();
            return _mapper.Map<Product, ProductDTO>(addedproduct.Entity);
        }

        public async Task<int> delete(int id)
        {
            var product = await _db.Products.FirstOrDefaultAsync(u => u.Id == id);
            if (product != null)
            {
                _db.Products.Remove(product);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<ProductDTO> get(int id)
        {
            var product = await _db.Products.Include(u=> u.Category).Include(u => u.ProductPrices).FirstOrDefaultAsync(u => u.Id == id);
            if (product != null)
            {
                return _mapper.Map<Product, ProductDTO>(product);
            }
            return new ProductDTO();
        }

        public async Task<IEnumerable<ProductDTO>> getAll()
        {
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(_db.Products.Include(u => u.Category).Include(u=>u.ProductPrices));
        }

        public async Task<ProductDTO> update(ProductDTO objDTO)
        {
            var product = await _db.Products.FirstOrDefaultAsync(u => u.Id == objDTO.Id);
            if(product != null)
            {
                product.Name = objDTO.Name;
                product.Description = objDTO.Description;
                product.CostomerFavorite = objDTO.CostomerFavorite;
                product.MainFeature = objDTO.MainFeature;
                product.ShopFavorite = objDTO.ShopFavorite;
                product.ImageUrl = objDTO.ImageUrl;
                product.CategoryId = objDTO.CategoryId;

                _db.Products.Update(product);
                await _db.SaveChangesAsync();
                return _mapper.Map<Product, ProductDTO>(product);
            }
            return objDTO;
        }
    }
}
