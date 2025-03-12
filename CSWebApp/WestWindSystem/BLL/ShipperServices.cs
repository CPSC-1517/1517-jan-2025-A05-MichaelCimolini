using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WestWindSystem.DAL;
using WestWindSystem.Entities;

namespace WestWindSystem.BLL
{
    //Anything in BLL will generally be public as this is how other applications/classes/etc should access
    //our database.
    public class ShipperServices
    {
        //this makes a copy of our context usable only by this class.
        //readonly means it can only be set in our constructor.
        private readonly WestWindContext _context;

        internal ShipperServices(WestWindContext context)
        {
            _context = context;
        }

        #region Services
        /// <summary>
        /// This will return all Shipper records.
        /// 
        /// This could return a huge number of records. Must be careful to only use on small
        /// datasets.
        /// 
        /// </summary>
        /// <returns>A list of records (may be empty)</returns>
        public List<Shipper> GetAllShippers() //ShipperGet -or- Shipper_Get
        {
            //Using IEnumerable (base base class of List)
            //Allows us to iterate through our records, but nothing else
            //This prevents our DB from runnin an unnecessary query
            IEnumerable<Shipper> records = _context.Shippers;


            //This will sort alphabetically A->Z
            return records.ToList();
        }
        #endregion
    }
}
