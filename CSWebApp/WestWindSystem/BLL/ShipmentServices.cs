using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WestWindSystem.DAL;
using WestWindSystem.Entities;
using Microsoft.EntityFrameworkCore;

namespace WestWindSystem.BLL
{
    //Anything in BLL will generally be public as this is how other applications/classes/etc should access
    //our database.
    public class ShipmentServices
    {
        //this makes a copy of our context usable only by this class.
        //readonly means it can only be set in our constructor.
        private readonly WestWindContext _context;

        internal ShipmentServices(WestWindContext context)
        {
            _context = context;
        }

        #region Services
        /// <summary>
        /// Get any shippments in the given year and month.
        /// </summary>
        /// <param name="year">4 digit year yyyy (1950-present)</param>
        /// <param name="month">1 or 2 digit month mm (1-12)</param>
        /// <returns></returns>
        public List<Shipment> GetShipmentsByYearAndMonth(int year, int month) //ShipmentGet -or- Shipment_Get
        {
            //Using IEnumerable (base base class of List)
            //Allows us to iterate through our records, but nothing else
            
            //This prevents our DB from runnin an unnecessary query

            //Version 1 - see V2 below
            //IEnumerable<Shipment> records = _context.Shipments
            //                        .Where(shipment => shipment.ShippedDate.Year == year
            //                                        && shipment.ShippedDate.Month == month)
            //                        .OrderBy(shipment => shipment.ShippedDate);

            //This is the same result as above, but requires 2 DB queries instead of 1.
            //IEnumerable<Shipment> records = _context.Shipments.Where(shipment => shipment.ShippedDate.Year == year && shipment.ShippedDate.Month == month);

            /*
             * If we want to include data from another table, we have 2 options:
             * 1) Bring in the second dataset and handle all of the reference updates with our lists.
             * This is complex and a lot of code.
             * 
             * 2) We can use .Include() and have SQL do it for us.
            */

            //V2
            IEnumerable<Shipment> records = _context.Shipments
                        .Include(shipment => shipment.ShipViaNavigation)
                        .Where(shipment => shipment.ShippedDate.Year == year
                                        && shipment.ShippedDate.Month == month)
                        .OrderBy(shipment => shipment.ShippedDate);

            return records.ToList();
        }
        #endregion
    }
}
