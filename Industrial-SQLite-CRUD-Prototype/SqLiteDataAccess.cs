using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace Industrial_SQLite_CRUD_Prototype {
  public static class SQLiteDataAccess {
    // Database connection string pointing to the local file
    private static readonly string ConnectionString = "Data Source=./IndustryData.db;Version=3;";

    /// <summary>
    /// Ensures the database file and necessary tables are created on startup.
    /// </summary>
    public static void InitializeDatabase() {
      using(var connection = new SQLiteConnection(ConnectionString)) {
        connection.Open();

        // SQL to create the DeviceLog table for DAQ purposes
        string sql = @"
                    CREATE TABLE IF NOT EXISTS DeviceLog (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        TagName TEXT NOT NULL,
                        Value REAL NOT NULL,
                        Quality INTEGER DEFAULT 1,
                        Timestamp DATETIME DEFAULT CURRENT_TIMESTAMP,
                        Remark TEXT
                    );";

        using(var cmd = new SQLiteCommand(sql, connection)) {
          cmd.ExecuteNonQuery();
        }
      }
    }

    /// <summary>
    /// Fetches all historical records from the database ordered by latest first.
    /// </summary>
    public static List<DeviceDataModel> LoadData() {
      var dataList = new List<DeviceDataModel>();
      string sql = "SELECT * FROM DeviceLog ORDER BY Timestamp DESC";

      using(var connection = new SQLiteConnection(ConnectionString)) {
        connection.Open();
        using(var cmd = new SQLiteCommand(sql, connection)) {
          using(var dr = cmd.ExecuteReader()) {
            while(dr.Read()) {
              dataList.Add(new DeviceDataModel {
                Id = Convert.ToInt32(dr["Id"]),
                TagName = dr["TagName"].ToString(),
                Value = Convert.ToDouble(dr["Value"]),
                Quality = Convert.ToInt32(dr["Quality"]),
                Timestamp = Convert.ToDateTime(dr["Timestamp"]),
                Remark = dr["Remark"]?.ToString()
              });
            }
          }
        }
      }
      return dataList;
    }

    /// <summary>
    /// Saves a single sensor reading into the SQLite database.
    /// </summary>
    public static int SaveData(DeviceDataModel data) {
      using(var connection = new SQLiteConnection(ConnectionString)) {
        connection.Open();
        string sql = "INSERT INTO DeviceLog (TagName, Value, Quality, Timestamp, Remark) " +
                     "VALUES (@TagName, @Value, @Quality, @Timestamp, @Remark)";

        using(var cmd = new SQLiteCommand(sql, connection)) {
          cmd.Parameters.AddWithValue("@TagName", data.TagName);
          cmd.Parameters.AddWithValue("@Value", data.Value);
          cmd.Parameters.AddWithValue("@Quality", data.Quality);
          cmd.Parameters.AddWithValue("@Timestamp", data.Timestamp);
          cmd.Parameters.AddWithValue("@Remark", data.Remark);
          return cmd.ExecuteNonQuery();
        }
      }
    }
  }
}