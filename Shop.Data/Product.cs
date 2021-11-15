using System;

namespace Shop.Data
{
    public class Product
    {
        public int Id_product { get; }
        public int Id_сategory { get; }
        public int Id_manufacturer { get; }
        public string Name_product { get; }
        public string Description { get; }
        public byte[] Image { get; } //?
        public decimal Price { get; }
        public int Available { get; }
        public string Specifications { get; }
        public int Rating { get; }


        public Product(int id_product, string name_product)
        {
            Id_product = id_product;
            Name_product = name_product;
        }
    }
}
