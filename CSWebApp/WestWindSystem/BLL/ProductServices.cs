using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using WestWindSystem.DAL;
using WestWindSystem.Entities;
#endregion

namespace WestWindSystem.BLL
{
    public class ProductServices
    {
        //this makes a copy of our context usable only by this class.
        //readonly means it can only be set in our constructor.
        private readonly WestWindContext _context;

        internal ProductServices(WestWindContext context)
        {
            _context = context;
        }

        #region Services

        public List<Product> GetProductsByCategoryID(int categoryID)
        {
            IEnumerable<Product> records = _context.Products
                                            .Include(product => product.Supplier)
                                            .Where(product => product.CategoryID == categoryID)
                                            .OrderBy(product => product.ProductName);

            return records.ToList();
        }

        public Product? GetProductByID(int ID)
        {
            Product? record = _context.Products
                                            .Include(product => product.Supplier)
                                            .Where(product => product.ProductID == ID)
                                            .FirstOrDefault();

            return record;
        }

        public int AddProduct(Product product)
        {
            throw new NotImplementedException();
        }

        #endregion


    }
}
