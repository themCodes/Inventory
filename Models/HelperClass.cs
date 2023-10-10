using Inventory.Data;

namespace Inventory.Models
{
    public class HelperClass
    {
        // Return the date of the last inventory count as well as how long ago that was.
        public static string FormatLastInventoryCount(DateTime lastInventoryCount)
        {
            string swedenTimeZoneId = "Central European Standard Time";
            TimeZoneInfo swedenTimeZone = TimeZoneInfo.FindSystemTimeZoneById(swedenTimeZoneId);
            DateTime currentTimeInSweden = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, swedenTimeZone);

            TimeSpan timePassed = currentTimeInSweden - lastInventoryCount;
            string timePassedString = "";

            int timePassedDays = (int)timePassed.TotalDays;
            if (timePassedDays > 0)
            {
                timePassedString += timePassedDays + "d ";
                timePassed = timePassed.Subtract(TimeSpan.FromDays(timePassedDays));
            }

            int timePassedHours = (int)timePassed.TotalHours;
            if (timePassedHours > 0)
            {
                timePassedString += timePassedHours + "h ";
                timePassed = timePassed.Subtract(TimeSpan.FromHours(timePassedHours));
            }
            int timePassedMinutes = (int)timePassed.TotalMinutes;
            if (timePassedMinutes > 0)
            {
                timePassedString += timePassedMinutes + "m ";
                timePassed = timePassed.Subtract(TimeSpan.FromMinutes(timePassedMinutes));
            }

            int timePassedSeconds = (int)timePassed.TotalSeconds;
            if (timePassedSeconds == 0)
            {
                timePassedString += "1s ";
            }
            else if (timePassedSeconds > 0)
            {
                timePassedString += timePassedSeconds + "s ";
            }

            string lastInventoryCountString = timePassedString + "sedan (" + lastInventoryCount.ToString("yyyy-MM-dd HH:mm:ss") + ")";

            return lastInventoryCountString;
        }

        // Get the date and time in a specific time zone.
        public static DateTime GetDateTimeInTimeZone(string timeZoneId)
        {
            TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
            DateTime dateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneInfo);

            return dateTime;
        }

        // Return a datetime formatted in yyyy-MM-dd HH:mm:ss
        public static string FormatDateTime(DateTime dateInput)
        {
            return dateInput.ToString("yyyy-MM-dd HH:mm:ss");
        }

        // Convert a string from an input field to an int and return it. Used when saving a new item to the database or saving when done editing an existing item.
        public static int GetIntFromString(string stringValue)
        {
            int intValue;
            int.TryParse(stringValue, out intValue);

            return intValue;
        }

        // Returns the name of the css style to use depending on whether or not the ratio is above 1.
        public static string GetRatioStyleName(int numberOfItems, int numberOfItemsTrigger)
        {
            if (numberOfItemsTrigger == 0)      // If numberOfItemsTrigger is 0 it's assumed the trigger value is of no interest and there is no need to highlight the ratio.
                return "listItemsTdRatioGood";

            double ratio = (double)numberOfItems / (double)numberOfItemsTrigger;
            string ratioStyleName;

            if (ratio >= 1)
                ratioStyleName = "listItemsTdRatioGood";
            else
                ratioStyleName = "listItemsTdRatioBad";

            return ratioStyleName;
        }

        // Returns the ratio. Used to prevent division by 0. If so, return "-".
        public static string GetRatioNumber(int numberOfItems, int numberOfItemsTrigger)
        {
            if (numberOfItemsTrigger == 0)
                return "-";

            double ratio = (double)numberOfItems / (double)numberOfItemsTrigger;
            return ratio.ToString("0.0");
        }

        // Add an entry into the log table.
        public static void AddLogEntry(ApplicationDbContext applicationDbContext, string userName, string actionCategory, string action)
        {
            LogEntry logEntry = new LogEntry();

            logEntry.Date = HelperClass.GetDateTimeInTimeZone("Central European Standard Time");
            logEntry.Username = userName;
            logEntry.ActionCategory = actionCategory;
            logEntry.Action = action;

            applicationDbContext.Log.Add(logEntry);
            applicationDbContext.SaveChanges();
        }
    }
}
