﻿using Computer_Store_Models;

namespace Computer_Store_Client.Service.IService
{
    public interface IProductService
    {
        public Task<IEnumerable<ProductDTO>> getAll();
        public Task<ProductDTO> get(int id);
    }
}
