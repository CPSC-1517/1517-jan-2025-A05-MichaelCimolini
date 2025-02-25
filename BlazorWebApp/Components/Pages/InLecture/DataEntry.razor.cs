using Microsoft.AspNetCore.Components;
using OOPsReview;

namespace BlazorWebApp.Components.Pages.InLecture
{
    public partial class DataEntry
    {
        private string FeedbackMsg = String.Empty;

        private Dictionary<string, string> ErrorMessages = new(); //shorthand for new Dictionary<string, string>();

        //Employment Fields
        private string EmploymentTitle = String.Empty;
        private double EmploymentYears = 0.0;
        private SupervisoryLevel EmploymentLevel = SupervisoryLevel.Entry;
        private DateTime EmploymentStart = DateTime.Today;

        private Employment employmentData = null;

        //injected services
        //These are treated as auto-implemeted properties.

        //WebHostEnvironment lets us get information like filepath from our application.
        [Inject]
        public IWebHostEnvironment webHostEnvironment { get; set; }


        protected override void OnInitialized()
        {
            //FeedbackMsg = "Successful Feedback!";

            //ErrorMessages.Add("1", "First Error.");
            //ErrorMessages.Add("2", "Second Error.");

            base.OnInitialized();
        }

        private void Submit()
        {
            //Clear our any old messages
            FeedbackMsg = String.Empty;
            ErrorMessages.Clear();

            if (String.IsNullOrEmpty(EmploymentTitle))
            {
                ErrorMessages.Add("Title", "Title is Required");
            }

            if (EmploymentYears < 0)
            {
                ErrorMessages.Add("Years", "Years cannot be negative.");
            }

            if (EmploymentStart >= DateTime.Today.AddDays(1))
            {
                ErrorMessages.Add("Date", "Starte Date cannot be in the future.");
            }

            if (ErrorMessages.Count == 0)
            {
                //FeedbackMsg = $"Success:{EmploymentTitle} {EmploymentYears} {EmploymentStart} {EmploymentLevel}";

                //Want to do File IO with our collected information
                //File IO tends to fail, so we use try catch.

                try
                {
                    employmentData = new Employment(EmploymentTitle, EmploymentLevel, EmploymentStart, EmploymentYears);

                    //Where do we want to write this to?

                    //Lets find where our application is. This finds our wwwroot folder.
                    string ApplicationPathName = webHostEnvironment.ContentRootPath;

                    //The @ symbol treats characters as they appear. No need to /terminate characters.
                    string CSVFilePath = $@"{ApplicationPathName}/Data/Employment.csv";

                    //Better to use a variable when writing files as we'll often be looping or changing it.
                    string line = $"{employmentData.ToString()}\n";

                    //Wrties our line out to the CSVFilePath file.
                    System.IO.File.AppendAllText(CSVFilePath, line);

                    FeedbackMsg = "Success!";
                }
                //Catches any errors from null or missing data passed to Employment.
                catch (ArgumentNullException ex)
                {
                    ErrorMessages.Add($"Missing Data", GetInnerException(ex).Message);
                }
                //Catches any errors from bad data passed to Employment.
                catch (ArgumentException ex)
                {
                    ErrorMessages.Add($"Bad Data", GetInnerException(ex).Message);
                }
                //Catches anything else (File IO errors, etc).
                catch (Exception ex)
                {
                    ErrorMessages.Add($"System Error", GetInnerException(ex).Message);
                }
            }
        }

        /// <summary>
        /// Clears all of our form inputs and message boxes.
        /// </summary>
        //TODO: figure out why this is broken.
        private void Clear()
        {
            FeedbackMsg = String.Empty;
            ErrorMessages.Clear();

            EmploymentTitle = String.Empty;
            EmploymentYears = 0.0;
            EmploymentStart = DateTime.Today;
            EmploymentLevel = SupervisoryLevel.Entry;
        }

        //Loops through our exception until it finds the root cause.
        private Exception GetInnerException(Exception ex)
        {
            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
            }

            return ex;
        }
    }
}
