using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ShopCore.WebAPI.Database;
using ShopCore.WebAPI.Requests;

namespace ShopCore.WebAPI.Services
{
    public interface IProductService
    {
        List<Product> Get();
        Product GetById(int id);
        Product Insert(ProductInsertRequest p);
        Product Update(int id, ProductInsertRequest p);
        void Delete(int id);
    }
}
