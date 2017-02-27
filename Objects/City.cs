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

    public int GetId()
    {
      return _id;
    }

    public string GetName()
    {
      return _name;
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM cities;", conn);
      cmd.ExecuteNonQuery();

      conn.Close();
    }

    public static List<City> GetAll()
    {
      List<City> allCities = new List<City>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM cities ORDER BY name;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int cityId = rdr.GetInt32(0);
        string cityName = rdr.GetString(1);

        City newCity = new City(cityName, cityId);
        allCities.Add(newCity);
      }

      if(rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allCities;
    }

    public override bool Equals(System.Object otherCity)
    {
      if (!(otherCity is City))
      {
        return false;
      }
      else
      {
        City newCity = (City) otherCity;
        bool nameEquality = (this.GetName() == newCity.GetName());
        bool idEquality = (this.GetId() == newCity.GetId());
        return (nameEquality && idEquality);
      }
    }
  }
}
