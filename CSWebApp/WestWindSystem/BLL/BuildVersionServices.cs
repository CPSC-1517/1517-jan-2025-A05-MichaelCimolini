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
    public class BuildVersionServices
    {
        //this makes a copy of our context usable only by this class.
        //readonly means it can only be set in our constructor.
        private readonly WestWindContext _context;

        internal BuildVersionServices(WestWindContext context)
        {
            _context = context;
        }

        #region Services
        /// <summary>
        /// This will return the 1st record from the BuildVersion table or null
        /// </summary>
        /// <returns>1st or Default</returns>
        public BuildVersion? GetBuildVersion() //BuildVersionGet -or- BuildVersion_Get
        {
            //Using IEnumerable (base base class of List)
            //Allows us to iterate through our records, but nothing else
            //This prevents our DB from runnin an unnecessary query
            IEnumerable<BuildVersion> records = _context.BuildVersions;

            return records.FirstOrDefault();
        }
        #endregion
    }
}
