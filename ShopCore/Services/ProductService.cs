using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ShopCore.WebAPI.Database;
using ShopCore.WebAPI.Exceptions;
using ShopCore.WebAPI.Requests;

namespace ShopCore.WebAPI.Services
{
    public class ProductService : IProductService
    {

        public List<Product> Get()
        {
            var list = VirtualDB.Products;

            return list;
        }

        public Product GetById(int id)
        {
            var p = VirtualDB.Products.Where(x => x.ProductID == id).FirstOrDefault();

            return p;
        }

        public Product Insert(ProductInsertRequest request)
        {
            var checkTaken = VirtualDB.Products.Where(x => x.Name == request.Name).FirstOrDefault();

            if (checkTaken != null)
                throw new UserException("Name taken!");

            Product product = new Product()
            {
                ProductID = VirtualDB.GenerateID(VirtualDB.Products.Last().ProductID),
                Name = request.Name,
                Developer = request.Developer,
                Publisher = request.Publisher,
                Price = request.Price,
                Description = request.Description,
                ReleaseDate = request.ReleaseDate,
                Image = request.Image,
                Deleted = false
            };


            VirtualDB.Products.Add(product);
            return product;
        }

        public Product Update(int pid, ProductInsertRequest request)
        {
            var checkTaken = VirtualDB.Products.Where(x => x.Name == request.Name).FirstOrDefault();

            if (checkTaken != null && checkTaken.ProductID != pid)
                throw new UserException("Name taken!");

            Product product  = VirtualDB.Products.Where(x => x.ProductID == pid).FirstOrDefault();

            product.Name = request.Name;
            product.Developer = request.Developer;
            product.Publisher = request.Publisher;
            product.Price = request.Price;
            product.Description = request.Description;
            if(request.ReleaseDate != null)
                product.ReleaseDate = request.ReleaseDate;
            if (request.Image != null)
                product.Image = request.Image;
            product.DateModified = System.DateTime.Now;

            return product;
        }

        public void Delete(int id)
        {
            var p = VirtualDB.Products.Where(x => x.ProductID == id).FirstOrDefault();
            if (p != null)
                VirtualDB.Products.Remove(p);
        }

    }
}
