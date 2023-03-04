    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Computer_Store_Models;

namespace Computer_Store_Business.Repository.IRepository
{
    public interface ICategoryRepository
    {
        public Task<CategoryDTO> Create(CategoryDTO objDTO);
        public Task<CategoryDTO> Update(CategoryDTO objDTO);
        public Task<int> Delete(int id);

        public Task<CategoryDTO> Get(int id);
        public Task<IEnumerable<CategoryDTO>> GetAll();

    }
}
