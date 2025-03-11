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
    public class RegionServices
    {
        //this makes a copy of our context usable only by this class.
        //readonly means it can only be set in our constructor.
        private readonly WestWindContext _context;

        internal RegionServices(WestWindContext context)
        {
            _context = context;
        }

        #region Services
        /// <summary>
        /// This will return all region records.
        /// 
        /// This could return a huge number of records. Must be careful to only use on small
        /// datasets.
        /// 
        /// </summary>
        /// <returns>A list of records (may be empty)</returns>
        public List<Region> GetAllRegions() //RegionGet -or- Region_Get
        {
            //Using IEnumerable (base base class of List)
            //Allows us to iterate through our records, but nothing else
            //This prevents our DB from runnin an unnecessary query
            IEnumerable<Region> records = _context.Regions;


            //This will sort alphabetically A->Z
            return records.OrderBy(record => record.RegionDescription).ToList();
        }


        /// <summary>
        /// Returns the region with matching ID. Null if no region found.
        /// </summary>
        /// <param name="id">The target ID.</param>
        /// <returns>The matching region or null.</returns>
        public Region? GetRegionByID(int id)
        {
            Region? region = null;

            //FirstOrDefault can take a predicate to further refine our search.
            region = _context.Regions.FirstOrDefault(x => x.RegionID == id);

            return region;
        }
        #endregion
    }
}
