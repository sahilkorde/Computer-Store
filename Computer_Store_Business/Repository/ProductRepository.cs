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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

        public async Task<IEnumerable<ProductDTO>> getAll(string? search, List<int>? catid, int? minprice, int? maxprice)
        {
            IQueryable<Product> product = _db.Products.Include(p => p.Category).Include(p => p.ProductPrices);
            
            if (!string.IsNullOrEmpty(search))
            {
                //List<string> searchlist = search.Split(' ').ToList();
                product = product.Where(p => p.Name.Contains(search) || p.Description.Contains(search) || p.MainFeature.Contains(search) || p.Category.Name.Contains(search));
            }
            if (minprice.HasValue && minprice>0)
            {
                product = product.Where(p => p.ProductPrices.Any(pp => pp.Price >= minprice.Value));
            }

            if (maxprice.HasValue & maxprice >0)
            {
                product = product.Where(p => p.ProductPrices.Any(pp => pp.Price <= maxprice.Value));
            }
            if (catid != null && catid.Count > 0)
            {
                product = product.Where(p => catid.Contains(p.CategoryId));
            }
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(product);
            /*if (search == null && catid == null) {
                return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(_db.Products.Include(u => u.Category).Include(u=>u.ProductPrices));
            }
            else
            {
                if(search == null) {
                    return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(_db.Products.Where(u => u.CategoryId == catid).Include(u => u.Category).Include(u => u.ProductPrices));
                }
                if(catid == null)
                {
                    return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(_db.Products.Where(u => u.Name.Contains(search) || u.MainFeature.Contains(search)).Include(u => u.Category).Include(u => u.ProductPrices));
                }
                return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(_db.Products.Where(u => (u.Name.Contains(search) || u.MainFeature.Contains(search)) & (u.CategoryId == catid)).Include(u => u.Category).Include(u => u.ProductPrices));
            }*/
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
