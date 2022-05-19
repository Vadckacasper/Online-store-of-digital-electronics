using Online_store_of_digital_electronics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Online_store_of_digital_electronics.ViewModel
{
    public class DetailsProductViewModel
    {
        public Products products { get; set; }
        public string[] Title;
        public string[] Content;
        public DetailsProductViewModel(Products _products)
        {
            products = _products;

        }

        public void GetSpecifications(Products _products)
        {
            string Specifications = _products.Specifications;
        }
    }
}
