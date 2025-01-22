﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPsReview
{
    public class Person
    {
        #region Attributes
        private String _FirstName;
        private String _LastName;
        #endregion

        #region Properties
        public String FirstName {
            get { return _FirstName; }
            set { _FirstName = value; }
        }

        public String LastName {
            get { return _LastName; }
            set { _LastName = value; }
        }

        public ResidentAddress Address { get; set; }

        public List<Employment> EmploymentPositions { get; set; }
        #endregion

        #region Contructors
        public Person()
        {
            FirstName = "Unknown";
            LastName = "Unknown";

            EmploymentPositions = new List<Employment>();
        }
        #endregion

        #region Methods
        #endregion
    }
}
