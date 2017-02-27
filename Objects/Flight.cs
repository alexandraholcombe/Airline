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
  }
}
