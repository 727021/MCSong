using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Text.RegularExpressions;
using System.IO;
using System.Collections;

namespace MCSong
{
    static class SQLiteHelper
    {
        public class SQLRow
        {
            public SQLRow()
            {
                data = new Dictionary<string, string>();
            }

            private Dictionary<string, string> data;

            public string this[string key]
            {
                get { return data[key]; }
                set
                {
                    if (data.ContainsKey(key))
                        data[key] = value;
                    else
                        data.Add(key, value);
                }
            }
        }

        public class SQLResult : IEnumerable<SQLRow>
        {
            private List<SQLRow> rows;
            public int rowsAffected { get; private set; }
            public SQLRow this[int i]
            {
                get { return rows[i]; }
                private set { }
            }

            public SQLResult() { }
            public SQLResult(int rows) => rowsAffected = rows;
            public SQLResult(SQLiteDataReader reader)
            {
                rows = new List<SQLRow>();
                List<string> columns = new List<string>();
                for (int i = 0; i < reader.FieldCount; i++)
                    columns.Add(reader.GetName(i));
                while (reader.Read())
                {
                    SQLRow row = new SQLRow();
                    foreach (string col in columns)
                        row[col] = reader[col].ToString();
                    rows.Add(row);
                }
                rowsAffected = rows.Count;
            }

            public IEnumerator<SQLRow> GetEnumerator()
            {
                foreach (SQLRow row in rows)
                    yield return row;
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

            public int IndexOf(SQLRow row) => rows.IndexOf(row);
        }   

        private static SQLiteConnection conn = null;
        public static SQLiteConnection GetConnection()
        {
            if (conn != null)
                return conn;
            string cs = @"URI=file:" + Directory.GetCurrentDirectory() + @"\MCSong.db";
            conn = new SQLiteConnection(cs).OpenAndReturn();
            return conn;
        }

        public static SQLResult ExecuteQuery(string queryString)
        {
            switch (queryString.Split(" ".ToCharArray())[0].ToUpper())
            {
                case "CREATE":
                    new SQLiteCommand(queryString, GetConnection()).ExecuteNonQuery();
                    goto default;
                case "INSERT":
                case "UPDATE":
                case "DELETE":
                    return new SQLResult(new SQLiteCommand(queryString, GetConnection()).ExecuteNonQuery());
                case "SELECT":
                    return new SQLResult(new SQLiteCommand(queryString, GetConnection()).ExecuteReader());
                default:
                    return new SQLResult();
            }
        }

        public static SQLResult ExecuteQuery(SQLiteCommand preparedQuery)
        {
            switch (preparedQuery.CommandText.Split(" ".ToCharArray())[0].ToUpper())
            {
                case "CREATE":
                    preparedQuery.ExecuteNonQuery();
                    goto default;
                case "INSERT":
                case "UPDATE":
                case "DELETE":
                    return new SQLResult(preparedQuery.ExecuteNonQuery());
                case "SELECT":
                    return new SQLResult(preparedQuery.ExecuteReader());
                default:
                    return new SQLResult();
            }
        }

        public static object ExecuteScalar(string queryString) => new SQLiteCommand(queryString, GetConnection()).ExecuteScalar();

        public static string EscapeQuotes(string param) => param.Replace("'", "''");
    }
}
