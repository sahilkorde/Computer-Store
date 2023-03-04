using AutoMapper;
using Computer_Store_Business.Repository.IRepository;
using Computer_Store_DataAccess;
using Computer_Store_DataAccess.Data;
using Computer_Store_Models;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Threading.Tasks;

namespace Computer_Store_Business.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public CategoryRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<CategoryDTO> Create(CategoryDTO objDTO)
        {
            /*Category category = new Category()
            {
               Name = objDTO.Name,
               Id = objDTO.Id,
               CreatedDate = DateTime.Now
           };*/
            var category = _mapper.Map<CategoryDTO, Category>(objDTO);
            category.CreatedDate = DateTime.Now;
            var addedCategory = _db.Categories.Add(category);
            await _db.SaveChangesAsync();
            return _mapper.Map<Category, CategoryDTO>(addedCategory.Entity);
            /*return new CategoryDTO()
            {
                Id = category.Id,
                Name = category.Name
            };*/
        }

        public async Task<int >Delete(int id)
        {
            var category = await _db.Categories.FirstOrDefaultAsync(u => u.Id == id);
            if (category != null)
            {
                _db.Categories.Remove(category);
                await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<CategoryDTO> Get(int id)
        {
            var category = await _db.Categories.FirstOrDefaultAsync(u => u.Id == id);
            if (category != null)
            {
                return _mapper.Map<Category, CategoryDTO>(category);
            }
            return new CategoryDTO();
        }

        public async Task<IEnumerable<CategoryDTO>> GetAll()
        {
            return _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDTO>>(_db.Categories);
        }

        public async Task<CategoryDTO> Update(CategoryDTO objDTO)
        {
            var category = await _db.Categories.FirstOrDefaultAsync(u => u.Id == objDTO.Id);
            if (category != null)
            {
                category.Name = objDTO.Name;
                _db.Categories.Update(category);
                await _db.SaveChangesAsync();
                return _mapper.Map<Category, CategoryDTO>(category);
            }
            return objDTO;
        }
    }
}
