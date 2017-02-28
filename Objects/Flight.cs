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

    public void UpdateStatus(string newStatus)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE flights SET flight_status = @NewStatus OUTPUT INSERTED.flight_status WHERE id = @FlightId;", conn);
      cmd.Parameters.Add(new SqlParameter("@NewStatus", newStatus));
      cmd.Parameters.Add(new SqlParameter("@FlightId", this.GetId()));

      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._status = rdr.GetString(0);
      }

      if (rdr != null)
      {
        rdr.Close();
      }

      if (conn != null)
      {
        conn.Close();
      }
    }

    public void AddDepartureCity(City newCity)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO flights_cities (departure_id, flight_id) VALUES (@DepartureId, @FlightId);", conn);
      cmd.Parameters.Add(new SqlParameter("@DepartureId", newCity.GetId()));
      cmd.Parameters.Add(new SqlParameter("@FlightId", this.GetId()));

      cmd.ExecuteNonQuery();

      if(conn != null)
      {
        conn.Close();
      }
    }

    public List<City> GetDepartureCity()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT departure_id FROM flights_cities WHERE flight_id = @FlightId;", conn);

      cmd.Parameters.Add(new SqlParameter("@FlightId", this.GetId()));

      SqlDataReader rdr = cmd.ExecuteReader();

      List<int> departureIds = new List<int> {};

      while (rdr.Read())
      {
        int departureId = rdr.GetInt32(0);
        departureIds.Add(departureId);
      }

      if(rdr != null)
      {
        rdr.Close();
      }

      List<City> cities = new List<City>{};

      foreach (int departureId in departureIds)
      {
        SqlCommand newCmd = new SqlCommand("SELECT * FROM cities WHERE id = @CityId;", conn);
        newCmd.Parameters.Add(new SqlParameter("@CityId", departureId));
        SqlDataReader newRdr = newCmd.ExecuteReader();

        while (newRdr.Read())
        {
          int newCityId = newRdr.GetInt32(0);
          string newCityName = newRdr.GetString(1);
          City newCity = new City(newCityName,newCityId);
          cities.Add(newCity);
        }

        if (newRdr != null)
        {
          newRdr.Close();
        }
      }

      if(conn != null)
      {
        conn.Close();
      }

      return cities;
    }

    public void DeleteThisFlight()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM flights WHERE id=@FlightId;", conn);
      cmd.Parameters.Add(new SqlParameter("@FlightId", this.GetId()));
      cmd.ExecuteNonQuery();

      conn.Close();
    }
  }
}
