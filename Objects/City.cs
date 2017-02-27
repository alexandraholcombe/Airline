using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AirlineApp
{
  public class City
  {
    private string _name;
    private int _id;

    public City(string Name, int Id = 0)
    {
      _name = Name;
      _id = Id;
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM cities;", conn);
      cmd.ExecuteNonQuery();

      conn.Close();
    }
  }
}
