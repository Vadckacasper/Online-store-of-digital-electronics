using System;

namespace Shop.Logic
{
    public class Product
    {
        public int Id { get; }
        public string Title { get; }

        public Product(int id, string title)
        {
            Id = id;
            Title = title;
        }
    }
}
