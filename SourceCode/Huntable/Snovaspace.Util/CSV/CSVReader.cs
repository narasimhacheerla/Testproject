using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace Snovaspace.Util.CSV
{
    public class CsvReader : IDisposable
    {
        public string NEWLINE = "\r";

        /// <summary>
        /// This reader will read all of the CSV data
        /// </summary>
        private readonly BinaryReader _reader;

        /// <summary>
        /// The number of rows to scan for types when building a DataTable (0 to scan the whole file)
        /// </summary>
        public int ScanRows;

        public CsvReader(FileInfo csvFileInfo)
        {
            if (csvFileInfo == null)
                throw new ArgumentNullException("csvFileInfo");

            _reader = new BinaryReader(File.OpenRead(csvFileInfo.FullName));
        }

        public CsvReader(string csvData)
        {
            if (csvData == null)
                throw new ArgumentNullException("csvData");

            _reader = new BinaryReader(new MemoryStream(Encoding.UTF8.GetBytes(csvData)));
        }

        public CsvReader(TextReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");

            _reader = new BinaryReader(new MemoryStream(Encoding.UTF8.GetBytes(reader.ReadToEnd())));
        }

        string _currentLine = "";

        public CsvReader(Stream csvFileInfo)
        {
            _reader = new BinaryReader(csvFileInfo);
        }

        public List<object> ReadRow()
        {
            // ReadLine() will return null if there's no next line
            if (_reader.BaseStream.Position >= _reader.BaseStream.Length)
                return null;

            var builder = new StringBuilder();

            // Read the next line
            while ((_reader.BaseStream.Position < _reader.BaseStream.Length) && (!builder.ToString().EndsWith(NEWLINE)))
            {
                char c = _reader.ReadChar();

                if (c == '\n') continue;

                builder.Append(c);
            }

            _currentLine = builder.ToString();
            if (_currentLine.EndsWith(NEWLINE))
                _currentLine = _currentLine.Remove(_currentLine.IndexOf(NEWLINE, StringComparison.Ordinal), NEWLINE.Length);

            // Build the list of objects in the line
            var objects = new List<object>();
            while (_currentLine != "")
                objects.Add(ReadNextObject());
            return objects;
        }

        /// <summary>
        /// Read the next object from the currentLine string
        /// </summary>
        /// <returns>The next object in the currentLine string</returns>
        private object ReadNextObject()
        {
            if (_currentLine == null)
                return null;

            // Check to see if the next value is quoted
            bool quoted = _currentLine.StartsWith("\"");

            // Find the end of the next value
            int i = 0;
            int len = _currentLine.Length;
            bool foundEnd = false;
            while (!foundEnd && i <= len)
            {
                // Check if we've hit the end of the string
                if ((!quoted && i == len) // non-quoted strings end with a comma or end of line
                    || (!quoted && _currentLine.Substring(i, 1) == ",")
                    // quoted strings end with a quote followed by a comma or end of line
                    || (quoted && i == len - 1 && _currentLine.EndsWith("\""))
                    || (quoted && _currentLine.Substring(i, 2) == "\","))
                    foundEnd = true;
                else
                    i++;
            }
            if (quoted)
            {
                if (i > len || !_currentLine.Substring(i, 1).StartsWith("\""))
                    throw new FormatException("Invalid CSV format: " + _currentLine.Substring(0, i));
                i++;
            }
            string nextObjectString = _currentLine.Substring(0, i).Replace("\"\"", "\"");

            _currentLine = i < len ? _currentLine.Substring(i + 1) : "";

            if (quoted)
            {
                if (nextObjectString.StartsWith("\""))
                    nextObjectString = nextObjectString.Substring(1);
                if (nextObjectString.EndsWith("\""))
                    nextObjectString = nextObjectString.Substring(0, nextObjectString.Length - 1);
                return nextObjectString;
            }
            object convertedValue;
            Utility.ConvertString(nextObjectString, out convertedValue);
            return convertedValue;
        }

        /// <summary>
        /// Read the row data read using repeated ReadRow() calls and build a DataColumnCollection with types and column names
        /// </summary>
        /// <param name="headerRow">True if the first row contains headers</param>
        /// <returns>System.Data.DataTable object populated with the row data</returns>
        public DataTable CreateDataTable(bool headerRow)
        {
            // Read the CSV data into rows
            var rows = new List<List<object>>();
            List<object> readRow;
            while ((readRow = ReadRow()) != null)
                rows.Add(readRow);

            // The types and names (if headerRow is true) will be stored in these lists
            var columnTypes = new List<Type>();
            var columnNames = new List<string>();

            // Read the column names from the header row (if there is one)
            if (headerRow)
                columnNames.AddRange(rows[0].Select(name => name.ToString()));

            // Read the column types from each row in the list of rows
            bool headerRead = false;
            foreach (List<object> row in rows)
                if (headerRead || !headerRow)
                    for (int i = 0; i < row.Count; i++)
                        // If we're adding a new column to the columnTypes list, use its type.
                        // Otherwise, find the common type between the one that's there and the new row.
                        if (columnTypes.Count < i + 1)
                            columnTypes.Add(row[i].GetType());
                        else
                            columnTypes[i] = Utility.FindCommonType(columnTypes[i], row[i].GetType());
                else
                    headerRead = true;

            // Create the table and add the columns
            var table = new DataTable();
            for (int i = 0; i < columnTypes.Count; i++)
            {
                table.Columns.Add();
                table.Columns[i].DataType = columnTypes[i];
                if (i < columnNames.Count)
                    table.Columns[i].ColumnName = columnNames[i];
            }

            // Add the data from the rows
            headerRead = false;
            foreach (List<object> row in rows)
                if (headerRead || !headerRow)
                {
                    DataRow dataRow = table.NewRow();
                    for (int i = 0; i < row.Count; i++)
                        dataRow[i] = row[i];
                    table.Rows.Add(dataRow);
                }
                else
                    headerRead = true;

            return table;
        }

        /// <summary>
        /// Read a CSV file into a table
        /// </summary>
        /// <param name="filename">Filename of CSV file</param>
        /// <param name="headerRow">True if the first row contains column names</param>
        /// <param name="scanRows">The number of rows to scan for types when building a DataTable (0 to scan the whole file)</param>
        /// <returns>System.Data.DataTable object that contains the CSV data</returns>
        public static DataTable ReadCSVFile(string filename, bool headerRow, int scanRows)
        {
            using (var reader = new CsvReader(new FileInfo(filename)))
            {
                reader.ScanRows = scanRows;
                return reader.CreateDataTable(headerRow);
            }
        }

        /// <summary>
        /// Read a CSV file into a table
        /// </summary>
        /// <param name="filename">Filename of CSV file</param>
        /// <param name="headerRow">True if the first row contains column names</param>
        /// <returns>System.Data.DataTable object that contains the CSV data</returns>
        public static DataTable ReadCSVFile(string filename, bool headerRow)
        {
            using (var reader = new CsvReader(new FileInfo(filename)))
                return reader.CreateDataTable(headerRow);
        }



        #region IDisposable Members

        public void Dispose()
        {
            if (_reader != null)
            {
                try
                {
                    // Can't call BinaryReader.Dispose due to its protection level
                    _reader.Close();
                }
                catch
                { }
            }
        }

        #endregion
    }
}