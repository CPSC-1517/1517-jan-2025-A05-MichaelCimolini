namespace OOPsReview
{
    /// <summary>
    /// This is a class that manages the employment
    /// of our companies employees.
    /// </summary>
    public class Employment
    {
        #region Attributes

        //Employment job title.
        private String _Title;

        //Years in this Employment role.
        private double _Years;

        private SupervisoryLevel _Level;

        #endregion

        #region Properties

        public String Title
        {
            //Simple get
            get { return _Title; }
            
            //
            set {
                //This will cover cases of strings with whitespace ex."   "
                //Better than .IsNullorEmpty()
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Title is Required.");
                }

                _Title = value.Trim(); }
        }

        public double Years
        {
            get { return _Years; }
            set 
            { 
                //Change to Utilities.IsPositiveOrZero
                if (!Utilities.IsPositive(value))
                {
                    throw new ArgumentException($"Year value {value} is invalid. Must be non-negative.");
                }

                _Years = value;
            }
        }

        public SupervisoryLevel Level
        {
            get { return _Level; }
            private set
            {
                if (!Enum.IsDefined(typeof(SupervisoryLevel), value))
                {
                    throw new ArgumentException($"Supervisory level {value} is invalid.");
                }
                _Level = value;
            }
        }

        public DateTime StartDate { get; set; }
        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Employment()
        {
            //Title can not be null or whitespace.
            Title = "unknown";

            //Years defaults to 0, therefore we don't need to define it here.
            Level = SupervisoryLevel.TeamMember;
            StartDate = DateTime.Today;
        }

        public Employment(String title, SupervisoryLevel level, DateTime startDate, double years = 0.0)
        {
            Title = title;
            Level = level;

            if (CheckDate(startDate))
            {
                StartDate = startDate;
            }

            if (years != 0.0 || startDate == DateTime.Today)
            {
                Years = years;
            }
            else
            {
                CalculateAndSetYears(startDate);
            }
        }
        #endregion

        #region Methods
        //Method Signiatures: access returnDateType methodName(parameters - optional)

        /// <summary>
        /// Sets our employment responsibility level for this instance.
        /// </summary>
        /// <param name="level">The SupervisoryLevel enum to set our Employment level to.</param>
        public void SetEmploymentResponsibilityLevel(SupervisoryLevel level)
        {
            Level = level;
        }

        /// <summary>
        /// Returns a string representing this instance as a CSV (comma separated value).
        /// Format: Title, Level, MMM,dd,yyy, Years
        /// </summary>
        /// <returns>A string in CSV format.</returns>
        public override string ToString()
        {
            return $"{Title},{Level},{StartDate.ToString("MMM. dd yyyy")},{Years}";
        }

        /// <summary>
        /// This method validates our StartDate as no validation is present in the set method.
        /// </summary>
        /// <param name="startDate">The DateTime to set StartDate to and validate.</param>
        public void CorrectStartDate(DateTime startDate)
        {
            if (CheckDate(startDate))
            {
                StartDate = startDate;
            }

            CalculateAndSetYears(startDate);
        }

        /// <summary>
        /// Checks to make sure our date is not in the future.
        /// </summary>
        /// <param name="value">The DateTime to check.</param>
        /// <returns>true if not in the future.</returns>
        /// <exception cref="ArgumentException">Throws an Argument Exception if the provided date is in the future.</exception>
        private static bool CheckDate(DateTime value)
        {
            if (value >= DateTime.Today.AddDays(1))
            {
                throw new ArgumentException($"The start date {value} is in the future.");
            }

            return true;
        }

        /// <summary>
        /// Calculates years in position give the provided DateTime.
        /// </summary>
        /// <param name="startDate">The DateTime representing the start date for this position.</param>
        private void CalculateAndSetYears(DateTime startDate)
        {
            TimeSpan span = DateTime.Now - startDate;
            Years = Math.Round((span.Days / 365.25), 1); // 365.25 is the number of days in a year accounting for leap years.
        }
        #endregion
    }
}
