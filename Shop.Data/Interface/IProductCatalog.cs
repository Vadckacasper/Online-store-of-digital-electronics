using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Data
{
    public interface IProductCatalog
    {
        Product[] GetArrByTitle(string titlePart);
    }
}
