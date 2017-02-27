using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace AirlineApp
{
  public class FlightTest: IDisposable
  {
    public FlightTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=airline_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
      //Arrange,Act
      int result = Flight.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Equal_ReturnsTrueIfNamesAreTheSame()
    {
      //Arrange, Act
      DateTime departureTime = new DateTime(2017, 3, 14, 9, 30, 0);
      string flightStatus = "On Time";
      Flight firstFlight = new Flight("AX5390", departureTime, flightStatus);
      Flight secondFlight = new Flight("AX5390", departureTime, flightStatus);

      //Assert
      Assert.Equal(firstFlight, secondFlight);
    }

    public void Dispose()
    {
      Flight.DeleteAll();
    }
  }
}
