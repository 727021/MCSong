/*
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
    /// The database object.
    /// </summary>
    public class Database
    {
        private string _name;
        /// <summary>
        /// The name of the database.
        /// </summary>
        public string Name
        {
            get { return Util.LastDirectory(_name); }
            set
            {
                value = Util.LastDirectory(value);
                if (String.IsNullOrEmpty(value)) throw new Exception("Database name cannot be empty.");
                if (value.Contains(Path.DirectorySeparatorChar) || value.Contains(Path.AltDirectorySeparatorChar)) throw new Exception("Invalid database name.");
                if (Name != value && !String.IsNullOrEmpty(Name) && Exists(Name))
                {
                    if (Exists(value)) throw new Exception("Database with new name already exists.");
                    Directory.Move(Location, RootLocation + "/" + value);
                }
                _name = value;
            }
        }
        
        /// <summary>
        /// Location of root directory where all the databases are.
        /// </summary>
        public static string RootLocation
        {
            get
            {
                if (!Directory.Exists("jDatabase")) Directory.CreateDirectory("jDatabase");
                return "jDatabase";
            }
        }
        
        /// <summary>
        /// Directory location of database.
        /// </summary>
        public string Location { get { return RootLocation + "/" + Name + "/"; } }

        /// <summary>
        /// Reads a database. If database does not exist, it will automatically create one.
        /// </summary>
        /// <param name="name">The name of the database.</param>
        public Database(string name)
        {
            if (!Directory.Exists(RootLocation)) Directory.CreateDirectory(RootLocation);
            Name = name;
            if (!Directory.Exists(Location)) Directory.CreateDirectory(Location);
        }

        /// <summary>
        /// Renames the database.
        /// </summary>
        /// <param name="newName">The name to rename the database to.</param>
        public void Rename(string newName) { Name = newName; }

        /// <summary>
        /// Deletes the database and all data within it.
        /// </summary>
        public void Delete() { Directory.Delete(Location, true); }

        /// <summary>
        /// Checks if a database exists or not.
        /// </summary>
        /// <param name="name">The name of the database.</param>
        /// <returns>Whether the database exists or not.</returns>
        public static bool Exists(string name) { return Directory.Exists(RootLocation + "/" + name); }

        /// <summary>
        /// Gets a list of all tables in the database.
        /// </summary>
        /// <returns>A list of all tables in the database.</returns>
        public List<Table> GetTables()
        {
            var returnValue = new List<Table>();
            foreach (string s in Directory.GetFiles(Location))
                returnValue.Add(new Table(Util.LastDirectory(Util.RemoveFileExtension(s)), Name));
            return returnValue;
        }

        /// <summary>
        /// Reads an existing table.
        /// </summary>
        /// <param name="tableName">The name of the table.</param>
        /// <returns>The table.</returns>
        public Table GetTable(string tableName)
        {
            if (!Table.Exists(tableName, Name)) throw new Exception("Table does not exist.");
            return new Table(tableName, Name);
        }

        /// <summary>
        /// Creates a new table.
        /// </summary>
        /// <param name="tableName">The name of the table.</param>
        /// <param name="columns">The columns of the table.</param>
        /// <returns>The table.</returns>
        public Table CreateTable(string tableName, List<string> columns)
        {
            if (Table.Exists(tableName, Name)) throw new Exception("Table already exists.");
            return new Table(tableName, Name, columns);
        }

        /// <summary>
        /// Deletes a table.
        /// </summary>
        /// <param name="tableName">The name of the table.</param>
        public void DeleteTable(string tableName)
        {
            if (!Table.Exists(tableName, Name)) throw new Exception("Table does not exist.");
            new Table(tableName, Name).Delete();
        }

        /// <summary>
        /// Gets a list of all the database names.
        /// </summary>
        /// <returns>A list of all the database names.</returns>
        public static List<string> GetDatabases()
        {
            var returnValue = new List<string>();
            foreach (string s in Directory.GetDirectories(RootLocation)) returnValue.Add(Util.LastDirectory(s));
            return returnValue;
        }
    }
}
