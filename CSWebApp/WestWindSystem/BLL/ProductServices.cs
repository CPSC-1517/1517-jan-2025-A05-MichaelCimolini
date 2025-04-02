using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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

        #region CRUD Queries

        /// <summary>
        /// Assume there is a business rule that items can not have the same Supplier, Product Name, and Quantity per Unit
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public int AddProduct(Product product)
        {
            //Validate we have data

            if (product is null)
            { 
                throw new ArgumentNullException("Product Information Required!");
            }

            //Our PKey is an Identity key, so no need to check if it exists

            bool exists = false;

            //Test our business logic
            exists = _context.Products.Any(
                        prod => prod.SupplierID == product.SupplierID &&
                        prod.ProductName.Equals(product.ProductName) &&
                        prod.QuantityPerUnit.Equals(product.QuantityPerUnit)
                        );

            if (exists)
            {
                //Generally you want to include more info than this
                throw new ArgumentException($"Product already exists!");
            }

            //We assume our data is safe at this point

            //Step 1: Data Staging
            //This data will only be in local memory
            //We can make any final changes at this point ie: Foreign keys, stock values, data not in our form, etc.

            _context.Products.Add(product);

            //Step 2: Commit
            //This pushes our data to our database
            //Any Entity Annotation Validation will happen before the data is sent

            _context.SaveChanges();

            //This will set the the PKey ID of our product

            return product.ProductID;
        }

        public int UpdateProduct(Product product)
        {
            if (product is null)
            {
                throw new ArgumentNullException("Product Information Required!");
            }

            //Check if the ID already exists. Can't update a product that doesn't exist.

            if(!_context.Products.Any(prod => prod.ProductID == product.ProductID))
            {
                throw new ArgumentException("Product with this ID doesn't exists.");
            }

            //Check our business logic, could be different logic than create
            bool exists = false;

            //Test our business logic
            exists = _context.Products.Any(
                        prod => prod.SupplierID == product.SupplierID &&
                        prod.ProductName.Equals(product.ProductName) &&
                        prod.QuantityPerUnit.Equals(product.QuantityPerUnit) &&
                        prod.ProductID != product.ProductID //Make sure the product is unique
                        );

            if (exists)
            {
                //Generally you want to include more info than this
                throw new ArgumentException($"Product already exists!");
            }

            //This handles checking all of our fields and only modifiying the ones that changed.
            EntityEntry<Product> updating = _context.Entry(product);

            updating.State = EntityState.Modified;

            //returns the number of records updated
            return _context.SaveChanges();
        }

        #endregion

        #endregion


    }
}
