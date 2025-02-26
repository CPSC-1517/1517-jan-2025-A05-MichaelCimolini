using OOPsReview;

namespace BlazorWebApp.Components.Pages.InLecture
{
    public partial class EmploymentReport
    {
        string FeedbackMessage = String.Empty;
        List<string> ErrorMessages = new List<string>();

        List<Employment> Employments = null;

        protected override void OnInitialized()
        {
            ReadCSVandParse();
            base.OnInitialized();
        }

        /// <summary>
        /// This will clear all error or feedback messages.
        /// Will parse the Employment.csv file and populate our List.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void ReadCSVandParse()
        {
            FeedbackMessage = String.Empty;
            ErrorMessages.Clear();

            string folderPath = @"./Data/";

            List<string> fileNames = new List<string>()
            {"Employment.csv", "BadEmployments.csv", "EmptyEmployments.csv"};

            string filePath = folderPath + fileNames[0];
            string badFilePath = folderPath + fileNames[1];
            string emptyFilePath = folderPath + fileNames[2];

            List<string> lines = new List<string>();

            try
            {
                if (System.IO.File.Exists(filePath))
                {
                    lines = System.IO.File.ReadAllLines(filePath).ToList();

                    Employments = new List<Employment>();

                    foreach (string line in lines)
                    {
                        Employments.Add(Employment.Parse(line));
                    }
                }
                else
                {
                    throw new Exception($"File: {filePath} does not exist");
                }
            }
            catch(Exception ex)
            {
                ErrorMessages.Add($"System error: {GetInnerException(ex).Message}");
            }
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
