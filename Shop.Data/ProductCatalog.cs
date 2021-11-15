using System;
using System.Linq;

namespace Shop.Data
{
    public class ProductCatalog : IProductCatalog
    {
        private int Id_category { get; }
        private readonly Product[] Products;
        public Product[] GetArrByTitle(string titlePart)
        {
            return Products.Where(product => product.Title.Contains(titlePart)).ToArray();
        }
    }
}
