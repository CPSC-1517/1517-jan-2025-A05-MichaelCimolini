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
                FeedbackMsg = $"Success:{EmploymentTitle} {EmploymentYears} {EmploymentStart} {EmploymentLevel}";
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
    }
}
