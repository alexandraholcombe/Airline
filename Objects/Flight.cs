using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AirlineApp
{
  public class Flight
  {
    private int _id;
    private string _flightNumber;
    private DateTime _departureTime;
    private string _status;

    public Flight(string flightNumber, DateTime departureTime, string status, int id = 0)
    {
      _id = id;
      _flightNumber = flightNumber;
      _departureTime = departureTime;
      _status = status;
    }

    public int GetId()
    {
      return _id;
    }

    public string GetFlightNumber()
    {
      return _flightNumber;
    }

    public DateTime GetDepartureTime()
    {
      return _departureTime;
    }

    public string GetStatus()
    {
      return _status;
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM flights;", conn);
      cmd.ExecuteNonQuery();

      conn.Close();
    }

    public static List<Flight> GetAll()
    {
      List<Flight> allFlights = new List<Flight>{};
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM flights ORDER BY departure_time;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while (rdr.Read())
      {
        int flightId = rdr.GetInt32(0);
        string flightNo = rdr.GetString(1);
        DateTime departureTime = rdr.GetDateTime(2);
        string flightStatus = rdr.GetString(3);

        Flight newFlight = new Flight(flightNo, departureTime, flightStatus, flightId);
        allFlights.Add(newFlight);
      }

      if (rdr !=  null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return allFlights;
    }
  }
}
