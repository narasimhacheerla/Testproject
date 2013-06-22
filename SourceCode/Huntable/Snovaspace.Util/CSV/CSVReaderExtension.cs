using System.Collections.Generic;
using System.Data;

namespace Snovaspace.Util.CSV
{
    public static class CSVReaderExtension
    {
        /// <summary>
        /// Convert a CSV-formatted string into a list of objects
        /// </summary>
        /// <returns>List of objects that contains the CSV data</returns>
        public static List<object> ReadCSVLine(this string input)
        {
            using (CsvReader reader = new CsvReader(input))
                return reader.ReadRow();
        }

        /// <summary>
        /// Convert a CSV-formatted string into a DataTable
        /// </summary>
        /// <param name="headerRow">True if the first row contains headers</param>
        /// <returns>System.Data.DataTable that contains the CSV data</returns>
        public static DataTable ReadCSVTable(this string input, bool headerRow)
        {
            using (CsvReader reader = new CsvReader(input))
                return reader.CreateDataTable(headerRow);
        }
    }
}