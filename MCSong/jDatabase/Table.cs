﻿/*
 *  Copyright 2013 Jonny Li (jonnyli1125)
 *  Modified by Andrew Schimelpfening (727021) for use with MCSong
 *
 *  Permission is hereby granted, free of charge, to any person obtaining a copy
 *  of this software and associated documentation files (the "Software"), to deal
 *  in the Software without restriction, including without limitation the rights
 *  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 *  copies of the Software, and to permit persons to whom the Software is
 *  furnished to do so, subject to the following conditions:
 *
 *  The above copyright notice and this permission notice shall be included in
 *  all copies or substantial portions of the Software.
 *
 *  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 *  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 *  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 *  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 *  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 *  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 *  THE SOFTWARE.
 *
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace jDatabase
{
    /// <summary>
    /// The table object, within the database.
    /// </summary>
    public class Table
    {
        private string _name;
        /// <summary>
        /// The name of the table.
        /// </summary>
        public string Name
        {
            get { return !String.IsNullOrEmpty(_name) ? Util.LastDirectory(Util.RemoveFileExtension(_name)) : ""; }
            set
            {
                value = Util.LastDirectory(Util.RemoveFileExtension(value));
                if (String.IsNullOrEmpty(value)) throw new Exception("Table name cannot be empty.");
                if (value.Contains(Path.DirectorySeparatorChar) || value.Contains(Path.AltDirectorySeparatorChar)) throw new Exception("Table database name.");
                if (Name != value && !String.IsNullOrEmpty(Name) && Exists(Name, DatabaseName))
                {
                    if (Exists(value, DatabaseName)) throw new Exception("Table with new name already exists.");
                    File.Move(Location, new Database(DatabaseName).Location + value + TableFileExtension);
                }
                _name = value;
            }
        }

        /// <summary>
        /// Default table file extension.
        /// </summary>
        private static string TableFileExtension { get { return ".txt"; } }

        private string _databaseName;
        /// <summary>
        /// The name of the database the table is in.
        /// </summary>
        public string DatabaseName
        {
            get { return _databaseName; }
            set
            {
                if (!Database.Exists(value)) throw new Exception("Database does not exist.");
                _databaseName = value;
            }
        }

        /// <summary>
        /// File location of table.
        /// </summary>
        public string Location { get { return new Database(DatabaseName).Location + Name + TableFileExtension; } }

        /// <summary>
        /// Lines of the table file.
        /// </summary>
        public List<string> Lines { get { return Util.RemoveEmptyStrings(new List<string>(File.ReadAllLines(Location))); } }

        /// <summary>
        /// Rows of the table. Each row is list of values.
        /// Each row in a table must be unique, if 2 rows are the same, one will be removed.
        /// </summary>
        public List<List<string>> Rows
        {
            get
            {
                var returnValue = new List<List<string>>();
                foreach (string line in Lines) returnValue.Add(new List<string>(line.Split(ColumnSeperator)));
                return returnValue;
            }
        }

        /// <summary>
        /// Columns of the table.
        /// </summary>
        public List<string> Columns { get { return new List<string>(File.ReadAllLines(Location)[0].Split(ColumnSeperator)); } }

        /// <summary>
        /// The character to seperate columns from eachother.
        /// </summary>
        public const char ColumnSeperator = '§';

        /// <summary>
        /// Reads an existing table.
        /// Use <code>new Table(name, dbName, columns)</code> to create a new one.
        /// </summary>
        /// <param name="name">The name of the table.</param>
        /// <param name="dbName">The name of the database the table is in.</param>
        public Table(string name, string dbName)
        {
            if (String.IsNullOrEmpty(name) || String.IsNullOrEmpty(dbName)) throw new Exception("Name cannot be empty.");
            if (!Database.Exists(dbName)) throw new Exception("Database does not exist.");
            if (!Exists(name, dbName)) throw new Exception("Table does not exist.");
            Name = name;
            DatabaseName = dbName;
        }
        /// <summary>
        /// Creates a new table.
        /// Use <code>new Table(name, dbName)</code> to read an existing one.
        /// </summary>
        /// <param name="name">The name of the table.</param>
        /// <param name="dbName">The name of the database the table is in.</param>
        /// <param name="columns">The columns of the table.</param>
        public Table(string name, string dbName, IEnumerable<string> columns)
        {
            if (String.IsNullOrEmpty(name) || String.IsNullOrEmpty(dbName)) throw new Exception("Name cannot be empty.");
            if (!Database.Exists(dbName)) throw new Exception("Database does not exist.");
            if (Exists(name, dbName)) throw new Exception("Table already exists.");
            if (new List<string>(columns).Count <= 0) throw new Exception("Must have at least 1 column.");
            Name = name;
            DatabaseName = dbName;
            File.WriteAllText(Location, String.Join(ColumnSeperator.ToString(), columns));
            MCSong.Server.s.Log("Created jDatabase table \"" + name + "\"");
            MCSong.Server.s.Debug("Saved to " + Location);
        }

        /// <summary>
        /// Renames a table.
        /// </summary>
        /// <param name="newName">The name to rename the table to.</param>
        public void Rename(string newName) { Name = newName; }

        /// <summary>
        /// Deletes the table and all data within it.
        /// </summary>
        public void Delete() { File.Delete(Location); }

        /// <summary>
        /// Checks to see if a table exists or not.
        /// </summary>
        /// <param name="name">The name of the table.</param>
        /// <param name="dbName">The name of the database the table is in.</param>
        /// <returns>Whether the table exists or not.</returns>
        public static bool Exists(string name, string dbName) { return File.Exists(new Database(dbName).Location + name + TableFileExtension); }

        /// <summary>
        /// Add a column to the table.
        /// </summary>
        /// <param name="columnName">The name of the column to add.</param>
        public void AddColumn(string columnName) { AddColumn(columnName, ""); }
        /// <param name="columnName">The name of the column to add.</param>
        /// <param name="defaultValue">The default value of the column.</param>
        public void AddColumn(string columnName, string defaultValue) { AddColumn(columnName, defaultValue, ""); }
        /// <param name="columnName">The name of the column to add.</param>
        /// <param name="defaultValue">The default value of the column.</param>
        /// <param name="afterColumn">The name of the column to insert the new column after. If this is empty or does not exist, the new column will just be added on the end.</param>
        public void AddColumn(string columnName, string defaultValue, string afterColumn) { AddColumn(columnName, defaultValue, (!String.IsNullOrEmpty(afterColumn) && ColumnExists(afterColumn) ? Columns.IndexOf(CapitalizeColumn(afterColumn)) + 1 : Columns.Count)); }
        /// <param name="columnName">The name of the column to add.</param>
        /// <param name="defaultValue">The default value of the column.</param>
        /// <param name="afterColumnIndex">The zero-based index from the list of columns to insert the new column at.</param>
        public void AddColumn(string columnName, string defaultValue, int afterColumnIndex)
        {
            if (String.IsNullOrEmpty(columnName)) throw new Exception("Column name cannot be empty.");
            if (ColumnExists(columnName)) throw new Exception("Column already exists.");
            var columns = Columns;
            if (afterColumnIndex >= columns.Count) columns.Add(columnName);
            else columns.Insert(afterColumnIndex, columnName);
            var lines = Lines;
            lines[0] = String.Join(ColumnSeperator.ToString(), columns);
            for (int i = 1; i < lines.Count; i++)
            {
                var values = new List<string>(lines[i].Split(ColumnSeperator));
                if (afterColumnIndex >= values.Count) values.Add(defaultValue);
                else values.Insert(afterColumnIndex, defaultValue);
                lines[i] = String.Join(ColumnSeperator.ToString(), values);
            }
            SetLines(lines);
        }

        /// <summary>
        /// Deletes a column from the table.
        /// </summary>
        /// <param name="columnName">The name of the column to delete.</param>
        public void DeleteColumn(string columnName)
        {
            if (String.IsNullOrEmpty(columnName)) throw new Exception("Column name cannot be empty.");
            if (!ColumnExists(columnName)) throw new Exception("Column does not exist.");
            var columns = Columns;
            int removalIndex = columns.IndexOf(CapitalizeColumn(columnName));
            columns.RemoveAt(removalIndex);
            var lines = Lines;
            lines[0] = String.Join(ColumnSeperator.ToString(), columns);
            for (int i = 1; i < lines.Count; i++)
            {
                var values = new List<string>(lines[i].Split(ColumnSeperator));
                values.RemoveAt(removalIndex);
                lines[i] = String.Join(ColumnSeperator.ToString(), values);
            }
            SetLines(lines);
        }

        /// <summary>
        /// Renames a column in the table.
        /// </summary>
        /// <param name="columnName">The name of the existing column.</param>
        /// <param name="newColumnName">The name to rename the column to.</param>
        public void RenameColumn(string columnName, string newColumnName)
        {
            if (String.IsNullOrEmpty(columnName) || String.IsNullOrEmpty(newColumnName)) throw new Exception("Column name cannot be empty.");
            if (!ColumnExists(columnName)) throw new Exception("Column does not exist.");
            if (ColumnExists(newColumnName)) throw new Exception("Column with that name already exists.");
            var columns = Columns;
            int columnIndex = columns.IndexOf(CapitalizeColumn(columnName));
            columns[columnIndex] = newColumnName;
            var lines = Lines;
            lines[0] = String.Join(ColumnSeperator.ToString(), columns);
            SetLines(lines);
        }

        /// <summary>
        /// Write the contents of the table file.
        /// </summary>
        /// <param name="contents">The contents to write.</param>
        public void SetLines(IEnumerable<string> contents) { if (Util.RemoveEmptyStrings(contents) != Lines) File.WriteAllLines(Location, Util.RemoveEmptyStrings(contents)); }

        /// <summary>
        /// Write the contents of the table file.
        /// </summary>
        /// <param name="contents">The contents to write.</param>
        public void SetRows(IEnumerable<IEnumerable<string>> contents)
        {
            if (contents != Rows)
            {
                var lines = new List<string>();
                foreach (var row in contents)
                {
                    string line = String.Join(ColumnSeperator.ToString(), row).Trim();
                    if (!String.IsNullOrEmpty(line)) lines.Add(line);
                }
                SetLines(lines);
            }
        }

        /// <summary>
        /// Checks if a column exists.
        /// </summary>
        /// <param name="columnName">The name of the column to check.</param>
        /// <returns>Whether the column exists or not.</returns>
        public bool ColumnExists(string columnName) { return Columns.Contains(CapitalizeColumn(columnName)); }

        /// <summary>
        /// Gets the column name with the correct capitalization.
        /// If the column does not exist, it will just return the inputted string with that capitalization.
        /// </summary>
        /// <param name="columnName">The column name to correctly capitalize.</param>
        /// <returns>The column with the correct capitalization.</returns>
        public string CapitalizeColumn(string columnName)
        {
            foreach (string s in Columns)
                if (columnName.ToLower() == s.ToLower())
                    return s;
            return columnName;
        }
        
        /// <summary>
        /// Adds a row to the end of the table.
        /// </summary>
        /// <param name="values">The values (ordered in the same way as the columns) to add to the table.</param>
        public void AddRow(IEnumerable<string> values) { AddRow(values, Lines.Count); }
        /// <summary>
        /// Inserts a row to the table at rowNumber.
        /// </summary>
        /// <param name="values">The values (ordered in the same way as the columns) to insert into the table.</param>
        /// <param name="rowNumber">The row number to insert the new row at.</param>
        public void AddRow(IEnumerable<string> values, int rowNumber)
        {
            if (rowNumber <= 0) throw new Exception("Row number must be greater or equal to 1.");
            if (new List<string>(values).Count != Columns.Count) throw new Exception("The amount of column values to add and columns must be equal.");
            var lines = Lines;
            string line = String.Join(ColumnSeperator.ToString(), values);
            if (lines.Contains(line)) throw new Exception("Cannot re-add an existing row. Each row must be unique.");
            if (rowNumber >= Lines.Count) lines.Add(line);
            else lines.Insert(rowNumber, line);
            SetLines(lines);
        }

        /// <summary>
        /// Deletes the row at the given row number.
        /// </summary>
        /// <param name="rowNumber">The row number to delete the row at.</param>
        public void DeleteRow(int rowNumber)
        {
            if (rowNumber <= 0) throw new Exception("Row number must be greater or equal to 1.");
            if (rowNumber > Rows.Count) throw new Exception("Row number is out of bounds.");
            var lines = Lines;
            lines.RemoveAt(rowNumber);
            SetLines(lines);
        }

        /// <summary>
        /// Deletes the rows where columnName has the value of value.
        /// </summary>
        /// <param name="columnName">The column name of the value.</param>
        /// <param name="value">The value to look for.</param>
        public void DeleteRows(string columnName, string value)
        {
            if (value.Contains(ColumnSeperator)) throw new Exception("Value cannot contain column seperator ('" + ColumnSeperator + "').");
            var lines = Lines;
            foreach (int i in GetRowNumbers(columnName, value)) lines.RemoveAt(i);
            SetLines(lines);
        }

        /// <summary>
        /// Get a row by its row number.
        /// </summary>
        /// <param name="rowNumber">The number the row is at.</param>
        /// <returns>The row.</returns>
        public List<string> GetRow(int rowNumber)
        {
            if (rowNumber <= 0) throw new Exception("Row number must be greater or equal to 1.");
            if (rowNumber > Rows.Count) throw new Exception("Row number is out of bounds.");
            return Rows[rowNumber];
        }

        /// <summary>
        /// Gets the rows where columnName has the value of value.
        /// If none of the rows matched the conditions, return value will be empty.
        /// </summary>
        /// <param name="columnName">The column name of the value.</param>
        /// <param name="value">The value to look for.</param>
        /// <returns>The rows where the conditions are met.</returns>
        public List<List<string>> GetRows(string columnName, string value)
        {
            if (value.Contains(ColumnSeperator)) throw new Exception("Value cannot contain column seperator ('" + ColumnSeperator + "').");
            var returnValue = new List<List<string>>();
            if (!ColumnExists(columnName)) throw new Exception("Column does not exist.");
            foreach (int i in GetRowNumbers(columnName, value)) returnValue.Add(new List<string>(Rows[i]));
            return returnValue;
        }

        /// <summary>
        /// Gets the row numbers where columnName has the value of value.
        /// If none of the rows matched the conditions, return value will be empty.
        /// </summary>
        /// <param name="columnName">The column name of the value.</param>
        /// <param name="value">The value to look for.</param>
        /// <returns>The row numbers where the conditions are met.</returns>
        public List<int> GetRowNumbers(string columnName, string value)
        {
            if (value.Contains(ColumnSeperator)) throw new Exception("Value cannot contain column seperator ('" + ColumnSeperator + "').");
            var returnValue = new List<int>();
            int columnIndex = Columns.IndexOf(CapitalizeColumn(columnName));
            if (columnIndex == -1) throw new Exception("Column does not exist.");
            for (int i = 1; i < Rows.Count; i++)
                if (Rows[i][columnIndex] == value)
                    returnValue.Add(i);
            return returnValue;
        }

        /// <summary>
        /// Gets the value at columnName on rowNumber.
        /// </summary>
        /// <param name="rowNumber">The row number.</param>
        /// <param name="columnName">The column name.</param>
        /// <returns>The value at columnName on rowNumber.</returns>
        public string GetValue(int rowNumber, string columnName)
        {
            if (rowNumber <= 0) throw new Exception("Row number must be greater or equal to 1.");
            if (rowNumber > Rows.Count) throw new Exception("Row number is out of bounds.");
            int columnIndex = Columns.IndexOf(CapitalizeColumn(columnName));
            if (columnIndex == -1) throw new Exception("Column does not exist.");
            return Rows[rowNumber][columnIndex];
        }

        // Added by 727021 for use with MCSong
        /// <summary>
        /// Gets a row with a specific set of column values
        /// </summary>
        /// <param name="columns">The names of the columns the given values belong in</param>
        /// <param name="values">The values the found row needs to have</param>
        /// <returns>The row with all the given values, or an empty string list</returns>
        public List<string> GetRow(string[] columns, string[] values)
        {
            if (columns.Length != values.Length) throw new Exception("Must search using same number of columns and values");
            if (columns.Length > Columns.Count) throw new Exception("Too many columns.");
            if (values.Length > Columns.Count) throw new Exception("Too many values.");
            foreach (List<string> row in Rows)
            {
                for (int i = 0; i < columns.Length; i++)
                {
                    int x = Rows.IndexOf(row);
                    int c = Columns.IndexOf(CapitalizeColumn(columns[i]));
                    if (c == -1) throw new Exception("Column does not exist.");
                    if (Rows[x][c] != values[i])
                        goto Next;
                }
                return row;
                Next:;
            }
            return new List<string>();
        }

        // Added by 727021 for use with MCSong
        /// <summary>
        /// Gets all rows that match a specific set of column values
        /// </summary>
        /// <param name="columns">The names of the columns the given values belong in</param>
        /// <param name="values">The values the found rows need to have</param>
        /// <returns>The rows with all the given values, or an empty string list list</returns>
        public List<List<string>> GetRows(string[] columns, string[] values)
        {
            List<List<string>> r = new List<List<string>>();
            if (columns.Length != values.Length) throw new Exception("Must search using same number of columns and values");
            if (columns.Length > Columns.Count) throw new Exception("Too many columns.");
            if (values.Length > Columns.Count) throw new Exception("Too many values.");
            foreach (List<string> row in Rows)
            {
                for (int i = 0; i < columns.Length; i++)
                {
                    int x = Rows.IndexOf(row);
                    int c = Columns.IndexOf(CapitalizeColumn(columns[i]));
                    if (c == -1) throw new Exception("Column does not exist.");
                    if (Rows[x][c] != values[i])
                        goto Next;
                }
                r.Add(row);
                Next:;
            }
            return r;
        }

        /// <summary>
        /// Gets the values where columnName has the value of value.
        /// </summary>
        /// <param name="rowNumber">The row number.</param>
        /// <param name="columnName">The column name.</param>
        /// <returns>The values where columnName has the value of value.</returns>
        public List<string> GetValues(string columnName, string value)
        {
            if (value.Contains(ColumnSeperator)) throw new Exception("Value cannot contain column seperator ('" + ColumnSeperator + "').");
            var returnValue = new List<string>();
            int columnIndex = Columns.IndexOf(CapitalizeColumn(columnName));
            if (columnIndex == -1) throw new Exception("Column does not exist.");
            foreach (int i in GetRowNumbers(columnName, value)) returnValue.Add(Rows[i][columnIndex]);
            return returnValue;
        }

        /// <summary>
        /// Sets the value of columnName on rowNumber.
        /// </summary>
        /// <param name="rowNumber">The row number.</param>
        /// <param name="columnName">The column name.</param>
        public void SetValue(int rowNumber, string columnName, string value)
        {
            if (value.Contains(ColumnSeperator)) throw new Exception("Value cannot contain column seperator ('" + ColumnSeperator + "').");
            if (rowNumber <= 0) throw new Exception("Row number must be greater or equal to 1.");
            if (rowNumber > Rows.Count) throw new Exception("Row number is out of bounds.");
            int columnIndex = Columns.IndexOf(CapitalizeColumn(columnName));
            if (columnIndex == -1) throw new Exception("Column does not exist.");
            var rows = Rows;
            rows[rowNumber][columnIndex] = value;
            SetRows(rows);
        }

        /// <summary>
        /// Removes all rows in the table.
        /// </summary>
        public void Truncate() { File.WriteAllText(Location, String.Join(ColumnSeperator.ToString(), Columns)); }
    }
}
