using Computer_Store_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computer_Store_Business.Repository.IRepository
{
    public interface IProductRepository
    {
        public Task<ProductDTO> create(ProductDTO product);
        public Task<ProductDTO> update(ProductDTO product);
        public Task<int> delete(int id);
        public Task<ProductDTO> get(int id);
        public Task<IEnumerable<ProductDTO>> getAll(string? search = null, List<int>? catid =null, int? minprice = null , int? maxprice = null);

    }
}
