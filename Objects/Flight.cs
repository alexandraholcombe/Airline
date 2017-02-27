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

    public override bool Equals(System.Object otherFlight)
    {
      if (!(otherFlight is Flight))
      {
        return false;
      }
      else
      {
        Flight newFlight = (Flight) otherFlight;
        bool idEquality = (this.GetId() == newFlight.GetId());
        bool flightNumberEquality = (this.GetFlightNumber() == newFlight.GetFlightNumber());
        bool departureTimeEquality = (this.GetDepartureTime() == newFlight.GetDepartureTime());
        bool flightStatusEquality = (this.GetStatus() == newFlight.GetStatus());

        return (idEquality && flightNumberEquality && departureTimeEquality && flightStatusEquality);
      }
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO flights (number, departure_time, flight_status) OUTPUT INSERTED.id VALUES(@FlightNumber,@DepartureTime,@FlightStatus);", conn);
      cmd.Parameters.Add(new SqlParameter("@FlightNumber", this.GetFlightNumber()));
      cmd.Parameters.Add(new SqlParameter("@DepartureTime", this.GetDepartureTime()));
      cmd.Parameters.Add(new SqlParameter("@FlightStatus", this.GetStatus()));

      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }

      if (rdr !=  null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }

    public static Flight Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM flights WHERE id = @FlightId", conn);
      cmd.Parameters.Add(new SqlParameter("@FlightId", id.ToString()));
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundId = 0;
      string foundNumber = null;
      DateTime foundDateTime = new DateTime();
      string foundStatus = null;

      while (rdr.Read())
      {
        foundId = rdr.GetInt32(0);
        foundNumber = rdr.GetString(1);
        foundDateTime = rdr.GetDateTime(2);
        foundStatus = rdr.GetString(3);
      }

      Flight foundFlight = new Flight(foundNumber, foundDateTime, foundStatus, foundId);

      if (rdr !=  null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return foundFlight;
    }
  }
}
