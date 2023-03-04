using Computer_Store_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computer_Store_Business.Repository.IRepository
{
    public interface IProductPriceRepository
    {
        public Task<ProductPriceDTO> Create(ProductPriceDTO objDTO);
        public Task<ProductPriceDTO> Update(ProductPriceDTO objDTO);
        public Task<int> Delete(int Id);
        public Task<ProductPriceDTO> Get(int Id);
        public Task<IEnumerable<ProductPriceDTO>> GetAll(int? id=null);
    }
}
